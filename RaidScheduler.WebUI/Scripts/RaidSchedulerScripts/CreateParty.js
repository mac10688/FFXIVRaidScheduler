var CreateParty = function (data) {
    var self = this;
    self.Players = ko.observableArray();

    self.setupEngine = function () {
        var engine = new Bloodhound({
            datumTokenizer: function (d) {
                return Bloodhound.tokenizers.obj.whitespace(d.Name);
            },
            queryTokenizer: Bloodhound.tokenizers.whitespace,
            remote:{ 
                url: '/Party/SearchForPlayer?name=%QUERY',
                replace: function (query) {
                    var q = '/Party/SearchForPlayer?name=' + $('#autoComplete').val() + '&server=' + $('#serverSelection').val();
                    return q;
                }
        }
        });

        engine.initialize(true);
        return engine;
    };

    self.setupTypeAhead = function (engine) {
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
    };

    self.configureAutoComplete = function () {
        var engine = self.setupEngine();
        self.setupTypeAhead(engine);
    };

    $(document).ready(function () {        
        self.configureAutoComplete();
        
        //$('#serverSelection').change(function () {
        //    $('#autoComplete').typeahead('destroy');
        //    self.configureAutoComplete();
        //});

    });

}