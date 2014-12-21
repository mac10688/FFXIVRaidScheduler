var PlayerChoiceModel = function(data){
    var self = this;

    self.PlayerModels = ko.observableArray(ko.utils.arrayMap(data.PlayerModels, function (p) {
        return new PlayerModel(p);
    }));

    self.ShowNoPlayersText = ko.computed(function () {
        return !self.PlayerModels().length > 0;
    });

    self.RemovePlayer = function (player) {
        $.ajax({
            type: "post",
            contentType: "application/json",
            data: JSON.stringify({ playerId: player.PlayerId }),
            url: "/Profile/RemovePlayer",
        })
            .done(function (msg) {
                if (msg === true) {
                    self.PlayerModels.remove(player);
                }
            });
    };
}

var PlayerModel = function(data){
    var self = this;
    self.PlayerId = data.PlayerId;
    self.PlayerFirstName = ko.observable(data.PlayerFirstName);
    self.PlayerLastName = ko.observable(data.PlayerLastName);

    self.PlayerFullname = ko.computed(function () {
        return self.PlayerFirstName() + ' ' + self.PlayerLastName();
    });    

    self.GoToEditPlayerScreen = function () {
        window.location = '/Profile/PlayerEdit?playerId=' + self.PlayerId;
    };

}