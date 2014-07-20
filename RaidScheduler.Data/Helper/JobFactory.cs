using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.DTO;

namespace RaidScheduler.Data.Helper
{
    public class JobFactory
    {

        public Job CreateJob(JobType job)
        {
            switch(job)
            {
                case JobType.Paladin:
                    {
                        return CreatePaladin();
                    }
                case JobType.Warrior:
                    {
                        return CreateWarrior();
                    }
                case JobType.WhiteMage:
                    {
                        return CreateWhiteMage();
                    }
                case JobType.Scholar:
                    {
                        return CreateScholar();
                    }
                case JobType.Summoner:
                    {
                        return CreateSummoner();
                    }
                case JobType.Dragoon:
                    {
                        return CreateDragoon();
                    }
                case JobType.Monk:
                    {
                        return Createmonk();
                    }
                case JobType.BlackMage:
                    {
                        return CreateBlackMage();
                    }
                default:
                    return null;
            }                
        }

        private Job CreatePaladin()
        {
            return new Job
                    {
                        JobID = 1,
                        JobName = "Paladin",
                        IsDps = false,
                        IsMeleeDps = false,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = false,
                        IsTank = true,
                        IsHealer = false,
                        CanSilence = true,
                        CanStun = true

                    };
        }

        private Job CreateWarrior()
        {
            return new Job
                    {
                        JobID = 2,
                        JobName = "Warrior",
                        IsDps = false,
                        IsMeleeDps = false,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = false,
                        IsTank = true,
                        IsHealer = false,
                        CanSilence = false,
                        CanStun = true
                    };
        }

        private Job CreateWhiteMage()
        {
            return new Job
                    {
                        JobID = 3,
                        JobName = "White Mage",
                        IsDps = false,
                        IsMeleeDps = false,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = false,
                        IsTank = false,
                        IsHealer = true,
                        CanSilence = false,
                        CanStun = false
                    };
        }

        private Job CreateScholar()
        {
            return new Job
                    {
                        JobID = 4,
                        JobName = "Scholar",
                        IsDps = false,
                        IsMeleeDps = false,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = false,
                        IsTank = false,
                        IsHealer = true,
                        CanSilence = false,
                        CanStun = false
                    };
        }

        private Job CreateSummoner()
        {
            return new Job
                    {
                        JobID = 5,
                        JobName = "Summoner",
                        IsDps = true,
                        IsMeleeDps = false,
                        IsRangedDps = true,
                        IsMagicalDps = true,
                        IsPhysicalDps = false,
                        IsTank = false,
                        IsHealer = false,
                        CanSilence = false,
                        CanStun = false
                    };
        }

        private Job CreateDragoon()
        {
            return new Job
                    {
                        JobID = 6,
                        JobName = "Dragoon",
                        IsDps = true,
                        IsMeleeDps = true,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = true,
                        IsTank = false,
                        IsHealer = false,
                        CanSilence = false,
                        CanStun = false
                    };
        }

        private Job Createmonk()
        {
            return new Job
                    {
                        JobID = 7,
                        JobName = "Monk",
                        IsDps = true,
                        IsMeleeDps = true,
                        IsRangedDps = false,
                        IsMagicalDps = false,
                        IsPhysicalDps = true,
                        IsTank = false,
                        IsHealer = false,
                        CanSilence = true,
                        CanStun = false
                    };
        }

        private Job CreateBlackMage()
        {
            return new Job
            {
                JobID = 7,
                JobName = "Black Mage",
                IsDps = true,
                IsMeleeDps = false,
                IsRangedDps = true,
                IsMagicalDps = true,
                IsPhysicalDps = false,
                IsTank = false,
                IsHealer = false,
                CanSilence = false,
                CanStun = false
            };
        }

    }

    public enum JobType
    {
        Paladin,
        Warrior,
        WhiteMage,
        Scholar,
        BlackMage,
        Summoner,
        Dragoon,
        Monk
    }
}
