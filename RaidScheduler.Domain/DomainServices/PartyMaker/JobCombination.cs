using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.DomainModels.JobDomain;
using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.RaidDomain;
using RaidScheduler.Domain.DomainModels.StaticPartyDomain;
using RaidScheduler.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.Services
{
    public class JobCombination : IJobCombination
    {
        /// <summary>
        /// This function looks at the players and raid to determine if they have the job combinations to complete the raid.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="raid"></param>
        /// <returns>
        /// A collection of static members with their chosen jobs. If no possible combination can complete the raid, null is returned.
        /// </returns>
        public ICollection<StaticMember> FindPotentialJobCombination(ICollection<Player> members, Raid raid, ICollection<Job> allJobs)
        {
            var attributes = raid.FlattenAttributesNeededForRaid();
            var potentialMembers = new List<StaticMember>();

            //We can't have members without jobs goin into this function. 
            //This function is tightly coupled with FindPotentialJobCombination.
            //Need this to be performant.
            var membersWithJobs = members.Where(m => m.PotentialJobs.Any());
            var playerStack = new Stack<Player>(membersWithJobs);
            var result = FindPotentialJobCombination(playerStack, attributes, potentialMembers, allJobs, raid.RaidCriteria.NumberOfPlayersRequired);

            if (result)
            {
                return potentialMembers;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Finds a combination of PotentialJobs for a given group of players.
        /// </summary>
        /// <param name="members"></param>
        /// <param name="attributesStillNeeded"></param>
        /// <param name="potentialMembers"></param>
        /// <returns>A boolean, indicating if the attributes were satisfied</returns>
        private bool FindPotentialJobCombination(Stack<Player> members,
            ICollection<JobAttributes> attributesStillNeeded,
            ICollection<StaticMember> potentialMembers,
            ICollection<Job> allJobs,
            int limit)
        {
            bool result = false;
            //If we have gone through all the members
            if (!members.Any() || potentialMembers.Count() >= limit)
            {
                //If we have satisified all the requirements, let's just return
                return !attributesStillNeeded.Any();
            }

            //Pop each member, don't worry about putting them back
            var member = members.Pop();

            //Go through each job of the potential member
            foreach (var potentialJob in member.PotentialJobs)
            {
                
                //Make a copy of the attributes. This is so we don't mutate the original copy
                var attributesNeededCopy = new List<JobAttributes>();
                foreach(var a in attributesStillNeeded)
                {
                    attributesNeededCopy.Add(a);
                }

                var job = allJobs.Where(j => j.JobType == potentialJob.JobId).Single();

                //Remove the attributes, if any, that the job already covers
                foreach (var jobAttribute in job.Attributes)
                {
                    attributesNeededCopy.Remove(jobAttribute);
                }                

                //Add the staticmember to the list, send it in.
                var potentialMember = new StaticMember(member.PlayerId, job.JobType);
                potentialMembers.Add(potentialMember);
                result = FindPotentialJobCombination(members, attributesNeededCopy, potentialMembers, allJobs, limit);

                //we found a combination that works, let's get out of here
                if (result) break;

                //So it appears, that job didn't work out for the combination.
                //Take it out of the list and try another one.
                potentialMembers.Remove(potentialMember);
            }

            return result;
        }
    }
}
