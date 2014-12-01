


$(document).ready(function(){

    var engine = new Bloodhound({
        datumTokenizer: function(d){
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
        alert('guid: ' + datum.Id + '\nName: ' + datum.Name );
    }).bind('typeahead:autocompleted', function (obj, datum) {
        alert('autocompleted');
    });

});

var CreateParty = function (data) {
    var self = this;

    self.PlayersAvailable = ko.observableArray();
    self.SelectedPlayer = ko.observable();
    self.PlayerSearch = ko.observable();

}