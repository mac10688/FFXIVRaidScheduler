




var CreateParty = function (data) {
    var self = this;
    self.Players = ko.observableArray();

    $(document).ready(function () {

        var engine = new Bloodhound({
            datumTokenizer: function (d) {
                return Bloodhound.tokenizers.obj.whitespace(d.Name);
            },
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            remote: '/Party/SearchForPlayer?name=%QUERY',
        });

        engine.initialize();

        $('#autoComplete').typeahead({
            hint: true,
            highlight: true,
            minLength: 2
        },
        {
            name: 'player-search',
            displayKey: 'Name',
            source: engine.ttAdapter()
        }).bind('typeahead:selected', function (obj, datum) {
            $.ajax({
                type: "post",
                contentType: "application/json",
                data: JSON.stringify({ Id: datum.Id }),
                url: "/Party/GetPlayerInfo",
            })
                .done(function (msg) {
                    self.Players.push(msg);
                });
        }).bind('typeahead:autocompleted', function (obj, datum) {
            alert('autocompleted');
        });

    });

}