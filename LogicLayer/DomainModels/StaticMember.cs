using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace RaidScheduler.Domain.DomainModels
{
    public class StaticMember
    {

        public StaticMember(Player player, StaticParty staticParty, PotentialJob chosenJob)
        {
            Player = player;
            if(player != null)
            {
                PlayerId = player.PlayerId;
            }            

            StaticParty = staticParty;
            if(staticParty != null)
            {
                StaticPartyId = staticParty.StaticPartyId;
            }

            ChosenPotentialJob = chosenJob;
            if(chosenJob != null)
            {
                ChosenPotentialJobId = chosenJob.PotentialJobId;
            }
            
            
        }

        protected StaticMember() { }
        
        public int StaticMemberId { get; protected set; }

        public int PlayerId { get; protected set; }
        public virtual Player Player { get; protected set; }

        public int ChosenPotentialJobId { get; protected set; }
        public virtual PotentialJob ChosenPotentialJob { get; protected set; }

        public int StaticPartyId { get; protected set; }
        public virtual StaticParty StaticParty { get; protected set; }

    }
}
