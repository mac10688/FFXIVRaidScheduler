using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaidScheduler.Domain.DomainModels;

using NodaTime;
using FluentAssertions;
using System.Collections.Generic;
using RaidScheduler.Domain.Services;
using RaidScheduler.Domain.DomainModels.SharedValueObject;
using RaidScheduler.Domain.DomainModels.PlayerDomain;

namespace RaidScheduler.Domain.Tests.Services
{
    [TestClass]
    public class SchedulingDomainTests
    {
        private const string CentralStandardTime = "Central Standard Time";
        private const string EasternStandardTime = "Eastern Standard Time";


        [TestMethod]
        public void TwoPlayers_With_SameTimeAndTimezone()
        {
            var player1 = new Player(Guid.NewGuid().ToString(), "Player1", "Player1", CentralStandardTime);
            var player1StartTime = new LocalDateTime(2014,9,16,11,0,0);
            var player1EndTime = new LocalDateTime(2014,9,16,13,0,0);
            var player1DayAndTime = new DayAndTime(IsoDayOfWeek.Monday, player1StartTime.TickOfDay, player1EndTime.TickOfDay);
            var player1DayAndTimeAvailable = new PlayerDayAndTimeAvailable(player1DayAndTime);
            player1.DaysAndTimesAvailable.Add(player1DayAndTimeAvailable);


            var player2 = new Player(Guid.NewGuid().ToString(), "Player2", "Player2", CentralStandardTime);
            var player2StartTime = new LocalDateTime(2014, 9, 16, 11, 0, 0);
            var player2EndTime = new LocalDateTime(2014, 9, 16, 13, 0, 0);
            var player2DayAndTime = new DayAndTime(IsoDayOfWeek.Monday, player2StartTime.TickOfDay, player2EndTime.TickOfDay);
            var player2DayAndTimeAvailable = new PlayerDayAndTimeAvailable(player2DayAndTime);
            player2.DaysAndTimesAvailable.Add(player2DayAndTimeAvailable);

            var players = new List<Player>() { player1, player2 };
            var schedulingDomain = new SchedulingDomain();
            var result = schedulingDomain.CommonScheduleAmongAllPlayers(players);

            var tz = NodaTime.DateTimeZoneProviders.Bcl.GetZoneOrNull(CentralStandardTime);
            var offset = tz.GetUtcOffset(SystemClock.Instance.Now);
            var ticks = offset.Ticks;
            var utcElevenOclock = player1StartTime.PlusTicks(-ticks);
            var utcOneOclock = player1EndTime.PlusTicks(-ticks);

            var utcStartTime = new LocalDateTime(2014, 9, 16, 16, 0, 0);
            var utcEndTime = new LocalDateTime(2014, 9, 16, 18, 0, 0);

            result.Should().HaveCount(1);
            result.Should().ContainSingle(d => d.DayOfWeek == IsoDayOfWeek.Monday);
            result.Should().ContainSingle(d => d.TimeStart == utcStartTime.TickOfDay);
            result.Should().ContainSingle(d => d.TimeEnd == utcEndTime.TickOfDay);
        }

        [TestMethod]
        public void TwoPlayers_With_SameTimeButDifferentTimezones()
        {
            var player1 = new Player(Guid.NewGuid().ToString(), "Player1", "Player1", CentralStandardTime);
            var player1Start = new LocalDateTime(2014, 9, 16, 11, 0, 0);
            var player1End = new LocalDateTime(2014, 9, 16, 13, 0, 0);
            var player1DayAndTime = new DayAndTime(IsoDayOfWeek.Monday, player1Start.TickOfDay, player1End.TickOfDay);
            var player1DayAndTimeAvailable = new PlayerDayAndTimeAvailable(player1DayAndTime);
            player1.DaysAndTimesAvailable.Add(player1DayAndTimeAvailable);
            
            var player2 = new Player(Guid.NewGuid().ToString(), "Player2", "Player2", EasternStandardTime);
            var player2Start = new LocalDateTime(2014, 9, 16, 12, 0, 0);
            var player2End = new LocalDateTime(2014, 9, 16, 14, 0, 0);
            var player2DayAndTime = new DayAndTime(IsoDayOfWeek.Monday, player2Start.TickOfDay, player2End.TickOfDay);
            var player2DayAndTimeAvailable = new PlayerDayAndTimeAvailable(player2DayAndTime);
            player2.DaysAndTimesAvailable.Add(player2DayAndTimeAvailable);

            var players = new List<Player>() { player1, player2 };
            var schedulingDomain = new SchedulingDomain();
            var result = schedulingDomain.CommonScheduleAmongAllPlayers(players);

            var utcStart = new LocalDateTime(2014, 9, 16, 16, 0, 0);
            var utcEnd = new LocalDateTime(2014, 9, 16, 18, 0, 0);

            result.Should().HaveCount(1);
            result.Should().ContainSingle(d => d.DayOfWeek == IsoDayOfWeek.Monday);
            result.Should().ContainSingle(d => d.TimeStart == utcStart.TickOfDay);
            result.Should().ContainSingle(d => d.TimeEnd == utcEnd.TickOfDay);
        }

        [TestMethod]
        public void TwoPlayers_With_NoTimeInCommon()
        {
            var player1 = new Player(Guid.NewGuid().ToString(), "Player1", "Player1", CentralStandardTime);

            var elevenOclock = new LocalDateTime(2014, 9, 16, 11, 0, 0);
            var oneOclock = new LocalDateTime(2014, 9, 16, 13, 0, 0);

            var tz = NodaTime.DateTimeZoneProviders.Bcl.GetZoneOrNull(CentralStandardTime);
            var offset = tz.GetUtcOffset(SystemClock.Instance.Now);
            var ticks = offset.Ticks;

            var player1DayAndTime = new DayAndTime(IsoDayOfWeek.Monday, elevenOclock.TickOfDay, oneOclock.TickOfDay);
            var player1DayAndTimeAvailable = new PlayerDayAndTimeAvailable(player1DayAndTime);
            player1.DaysAndTimesAvailable.Add(player1DayAndTimeAvailable);

            var twelveOclock = new LocalDateTime(2014, 9, 16, 14, 0, 0);
            var twoOclock = new LocalDateTime(2014, 9, 16, 16, 0, 0);

            var player2 = new Player(Guid.NewGuid().ToString(), "Player2", "Player2", EasternStandardTime);
            var player2DayAndTime = new DayAndTime(IsoDayOfWeek.Monday, twelveOclock.TickOfDay, twoOclock.TickOfDay);
            var player2DayAndTimeAvailable = new PlayerDayAndTimeAvailable(player2DayAndTime);
            player2.DaysAndTimesAvailable.Add(player2DayAndTimeAvailable);

            var players = new List<Player>() { player1, player2 };
            var schedulingDomain = new SchedulingDomain();
            var result = schedulingDomain.CommonScheduleAmongAllPlayers(players);

            var utcElevenOclock = elevenOclock.PlusTicks(-ticks);
            var utcOneOclock = oneOclock.PlusTicks(-ticks);

            result.Should().BeEmpty();
        }


        [TestMethod]
        public void TwoPlayers_With_DifferentTz_OneHourOverlap_NearMonday()
        {
            var player1 = new Player(Guid.NewGuid().ToString(), "Player1", "Player1", CentralStandardTime);                        
            var player1TimeStart = new LocalDateTime(2014, 9, 21, 19, 0, 0);
            var player1TimeEnd = new LocalDateTime(2014, 9, 21, 21, 0, 0);
            var player1DayAndTime = new DayAndTime(IsoDayOfWeek.Sunday, player1TimeStart.TickOfDay, player1TimeEnd.TickOfDay);
            var player1DayAndTimeAvailable = new PlayerDayAndTimeAvailable(player1DayAndTime);
            player1.DaysAndTimesAvailable.Add(player1DayAndTimeAvailable);

            
            var player2 = new Player(Guid.NewGuid().ToString(), "Player2", "Player2", EasternStandardTime);
            var player2TimeStart = new LocalDateTime(2014, 9, 21, 21, 0, 0);
            var player2TimeEnd = new LocalDateTime(2014, 9, 21, 0, 0, 0);
            var player2DayAndTime = new DayAndTime(IsoDayOfWeek.Sunday, player2TimeStart.TickOfDay, player2TimeEnd.TickOfDay);
            var player2DayAndTimeAvailable = new PlayerDayAndTimeAvailable(player2DayAndTime);
            player2.DaysAndTimesAvailable.Add(player2DayAndTimeAvailable);

            var players = new List<Player>() { player1, player2 };
            var schedulingDomain = new SchedulingDomain();
            var result = schedulingDomain.CommonScheduleAmongAllPlayers(players);
            
            var offset = NodaTime.DateTimeZoneProviders.Bcl.GetZoneOrNull(CentralStandardTime).GetUtcOffset(SystemClock.Instance.Now);

            var utcTimeStart = new LocalDateTime(2014, 9, 22, 1, 0, 0);
            var utcTimeEnd = new LocalDateTime(2014, 9, 22, 2, 0, 0);

            result.Should().HaveCount(1);
            result.Should().ContainSingle(d => d.DayOfWeek == IsoDayOfWeek.Monday);
            result.Should().ContainSingle(d => d.TimeStart == utcTimeStart.TickOfDay);
            result.Should().ContainSingle(d => d.TimeEnd == utcTimeEnd.TickOfDay);
        }

    }
}
