var PlayerCombinationsModel = function(data){
    var self = this;
    self.Parties = ko.observableArray(ko.utils.arrayMap(data.Parties, function (party) { return new PartyModel(party); }));
}

var PartyModel = function(data) {
    var self = this;
    self.ScheduleTimes = ko.observableArray(data.ScheduledTimes);
    self.PartyCombination = ko.observableArray(ko.utils.arrayMap(data.PartyCombination, function (partyCombination) { return new DisplayPlayerModel(partyCombination); }));
    self.RaidName = ko.observable(data.RaidName);
}

var DisplayPlayerModel = function(data){
    var self = this;
    self.PlayerFirstName = ko.observable(data.PlayerFirstName);
    self.PlayerLastName = ko.observable(data.PlayerLastName);
    self.ChosenJob = ko.observable(data.ChosenJob)

    self.PlayerName = ko.computed(function () {
        return self.PlayerFirstName() + ' ' + self.PlayerLastName();
    });
}