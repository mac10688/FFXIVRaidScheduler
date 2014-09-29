using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.DomainModels.JobDomain;

namespace RaidScheduler.Domain.DomainModels.JobDomain
{
    public class JobFactory : IJobFactory
    {

        public Job CreateJob(JobTypes job)
        {
            switch(job)
            {
                case JobTypes.Paladin:
                    {
                        return CreatePaladin();
                    }
                case JobTypes.Warrior:
                    {
                        return CreateWarrior();
                    }
                case JobTypes.WhiteMage:
                    {
                        return CreateWhiteMage();
                    }
                case JobTypes.Scholar:
                    {
                        return CreateScholar();
                    }
                case JobTypes.Summoner:
                    {
                        return CreateSummoner();
                    }
                case JobTypes.Dragoon:
                    {
                        return CreateDragoon();
                    }
                case JobTypes.Monk:
                    {
                        return Createmonk();
                    }
                case JobTypes.BlackMage:
                    {
                        return CreateBlackMage();
                    }
                case JobTypes.Bard:
                    {
                        return CreateBard();
                    }
                default:
                    return null;
            }                
        }

        public IEnumerable<Job> GetAllJobs()
        {
            foreach(var enumValue in Enum.GetValues(typeof(JobTypes)))
            {
                yield return CreateJob((JobTypes)enumValue);
            }
        }

        private Job CreatePaladin()
        {
            return new Job(JobTypes.Paladin, "Paladin", JobAttributes.Tank, JobAttributes.Silencer, JobAttributes.Stunner);
        }

        private Job CreateWarrior()
        {
            return new Job(JobTypes.Warrior, "Warrior", JobAttributes.Tank, JobAttributes.Stunner);
        }

        private Job CreateWhiteMage()
        {
            return new Job(JobTypes.WhiteMage, "White Mage", JobAttributes.Healer);
        }

        private Job CreateScholar()
        {
            return new Job(JobTypes.Scholar, "Scholar", JobAttributes.Healer);
        }

        private Job CreateSummoner()
        {
            return new Job(JobTypes.Summoner, "Summoner", JobAttributes.Dps, JobAttributes.RangedDps, JobAttributes.MagicalDps);
        }

        private Job CreateDragoon()
        {
            return new Job(JobTypes.Dragoon, "Dragoon",JobAttributes.Dps, JobAttributes.MeleeDps, JobAttributes.PhysicalDps);
        }

        private Job Createmonk()
        {
            return new Job(JobTypes.Monk, "Monk", JobAttributes.Dps, JobAttributes.MeleeDps, JobAttributes.PhysicalDps, JobAttributes.Silencer);
        }

        private Job CreateBlackMage()
        {
            return new Job(JobTypes.BlackMage, "Black Mage", JobAttributes.Dps, JobAttributes.RangedDps, JobAttributes.MagicalDps);
        }

        private Job CreateBard()
        {
            return new Job(JobTypes.Bard, "Bard", JobAttributes.Dps, JobAttributes.RangedDps, JobAttributes.PhysicalDps, JobAttributes.Silencer);
        }

    }

    
}
