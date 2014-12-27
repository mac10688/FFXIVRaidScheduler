var PlayerEdit = function (data) {
    var self = this;

    self.PlayerId = ko.observable(data.PlayerId);

    self.TimeZoneList = data.TimeZoneList;
    self.UserTimezone = data.UserTimezone;

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

    self.PotentialJobsToChoose = ko.observableArray(ko.utils.arrayMap(data.PotentialJobsToChoose, function (potentialJobToChoose) {
        return new JobModel(potentialJobToChoose);
    }));

    self.PlayerPotentialJobs = ko.observableArray();

    ko.utils.arrayForEach(data.PlayerPotentialJobs, function (playerPotentialJob) {
        var newJob = new PlayerPotentialJobModel(playerPotentialJob, self);
        self.PlayerPotentialJobs.push(newJob);
    });

    self.DaysAndTimesAvailable = ko.observableArray(ko.utils.arrayMap(data.DaysAndTimesAvailable, function (dayAndTimeAvailable) {
        return new DayAndTimeAvailableModel(dayAndTimeAvailable);
    }));
    
    self.RaidsAvailable = ko.observableArray(data.RaidsAvailable);

    self.RaidsRequested = ko.observableArray(data.RaidsRequested);

    self.DaysToChoose = ko.observableArray(data.DaysToChoose);

    self.CanAddJob = ko.computed(function () {
        return self.PlayerPotentialJobs().length < self.PotentialJobsToChoose().length;
    });

    self.AddNewPotentialJob = function () {
        var potentialJob = new PlayerPotentialJobModel({ PotentialJobId: 0, ILvl: '', ComfortLevel: 0 }, self);
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
        var data = { "Day": "", "TimeAvailableStart": date, "TimeAvailableEnd": date, "Timezone": self.UserTimezone }
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

var PlayerPotentialJobModel = function (data, root) {
    var self = this;
    self.PotentialJobID = ko.observable(data.PotentialJobID);
    self.ILvl = ko.observable(data.ILvl).extend({ required: true, number: true });

    self.ChooseableJobs = ko.computed(function () {
        var result = ko.utils.arrayFilter(root.PotentialJobsToChoose(), function (job) {
            var result = true;
            ko.utils.arrayForEach(root.PlayerPotentialJobs(), function (chosenjob) {
                if (chosenjob.PotentialJobID() == job.JobID() && chosenjob.PotentialJobID() != self.PotentialJobID()) {
                    result = false;
                }
            });
            return result;
        });
        return result;        
    });
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

    self.Timezone = ko.observable(data.Timezone);

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