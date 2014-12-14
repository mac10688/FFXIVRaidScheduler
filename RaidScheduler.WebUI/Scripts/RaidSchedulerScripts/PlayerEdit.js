var PlayerEdit = function (data) {
    var self = this;

    self.FirstName = ko.observable(data.FirstName).extend({
        required: true
    });

    self.LastName = ko.observable(data.LastName).extend({
        required: true
    });

    self.SelectedServer = ko.observable(data.SelectedServer).extend({
        required: true
    });

    self.AvailableServers = ko.observableArray(data.AvailableServers);

    self.PlayerPotentialJobs = ko.observableArray(ko.utils.arrayMap(data.PlayerPotentialJobs, function (playerPotentialJob) {
        return new PlayerPotentialJobModel(playerPotentialJob);
    }));

    self.PotentialJobsToChoose = ko.observableArray(ko.utils.arrayMap(data.PotentialJobsToChoose, function (potentialJobToChoose) {
        return new JobModel(potentialJobToChoose);
    }));

    self.DaysAndTimesAvailable = ko.observableArray(ko.utils.arrayMap(data.DaysAndTimesAvailable, function (dayAndTimeAvailable) {
        return new DayAndTimeAvailableModel(dayAndTimeAvailable);
    }));
    
    self.RaidsAvailable = ko.observableArray(data.RaidsAvailable);

    self.RaidsRequested = ko.observableArray(data.RaidsRequested);

    self.DaysToChoose = ko.observableArray(data.DaysToChoose);

    self.AddNewPotentialJob = function () {
        var potentialJob = new PlayerPotentialJobModel({ PotentialJobId: 0, ILvl: '', ComfortLevel: 0 });
        self.PlayerPotentialJobs.push(potentialJob);
    }

    self.RemovePotentialJob = function (potentialJob) {
        self.PlayerPotentialJobs.remove(potentialJob);
    }

    self.AddDayAndTimeAvailable = function () {
        var date = new Date();
        date.setTime(0);
        date.setHours(18);
        date.setMinutes(0);
        date.setSeconds(0);
        var data = { "Day": "", "TimeAvailableStart": date, "TimeAvailableEnd": date}
        self.DaysAndTimesAvailable.push(new DayAndTimeAvailableModel(data));
    };

    self.RemoveDayAndTimeAvailable = function (dayAndTimeAvailable) {
        self.DaysAndTimesAvailable.remove(dayAndTimeAvailable);
    }

    self.errors = ko.validation.group(self);

    self.SavePlayer = function () {
        if (self.errors().length == 0) {
            var jsObject = ko.toJSON(self);
            console.log(jsObject);

            //http://stackoverflow.com/questions/14287963/knockout-with-mapping-plugin-posting-using-postjson-with-mvc-model-collections
            $.ajax({
                type: "post",
                contentType: "application/json",
                data: jsObject,
                url: "/Profile/SavePlayer",
            })
            .done(function (msg) {
                if (msg.Message == 'success') {
                    $('#saveSuccess').addClass('alert-success').text('Save Successful').fadeIn();
                    $('#saveSuccess').delay(5000).removeClass('alert-success').val('').fadeOut();
                } else {
                    $('#saveSuccess').addClass('alert-danger').text('Save Failed').fadeIn();
                    $('#saveSuccess').delay(5000).removeClass('alert-danger').val('').fadeOut();
                }
            });
        } else {
            self.errors.showAllMessages();
            $('#saveSuccess').addClass('alert-danger').text('Please fill out required fields.').fadeIn();
            $('#saveSuccess').delay(5000).removeClass('alert-danger').val('').fadeOut();
        }
        
    }
};

var PlayerPotentialJobModel = function (data) {
    var self = this;
    self.PotentialJobID = ko.observable(data.PotentialJobID);
    self.ILvl = ko.observable(data.ILvl).extend({ required: true, number: true });
};

var JobModel = function (data) {
    var self = this;
    self.JobID = ko.observable(data.JobID);
    self.JobName = ko.observable(data.JobName);
};

var DayAndTimeAvailableModel = function (data) {
    var self = this;

    self.Day = ko.observable(data.Day);

    if (data.TimeAvailableStart instanceof Date) {
        self.TimeAvailableStart = ko.observable(data.TimeAvailableStart);
    } else {

        var date = new Date(data.TimeAvailableStart);
        var offset = date.getTimezoneOffset() * 60 * 1000;
        self.TimeAvailableStart = ko.observable(new Date(date.valueOf() + offset));
    }

    if (data.TimeAvailableEnd instanceof Date) {
        self.TimeAvailableEnd = ko.observable(data.TimeAvailableEnd);
    } else {
        var date = new Date(data.TimeAvailableEnd);
        var offset = date.getTimezoneOffset() * 60 * 1000;
        self.TimeAvailableEnd = ko.observable(new Date(date.valueOf() + offset));
    }

    DayAndTimeAvailableModel.prototype.toJSON = function () {
        var copy = ko.toJS(this);
        var startDateTime = new Date(copy.TimeAvailableStart);
        //Calculate the minutes
        copy.TimeAvailableStart = startDateTime.getTime() - (startDateTime.getTimezoneOffset() * 60 * 1000);

        var endDateTime = new Date(copy.TimeAvailableEnd);
        copy.TimeAvailableEnd = endDateTime.getTime() - (startDateTime.getTimezoneOffset() * 60 * 1000);

        return copy;
    }
};

var RaidModel = function (data) {
    var self = this;
    self.RaidID = ko.observable(data.RaidID);
    self.RaidName = ko.observable(data.RaidName);
};
