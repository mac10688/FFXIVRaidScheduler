using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Entities
{
    public class Job
    {
        public int JobID { get; set; }
        public string JobName { get; set; }
        public bool IsMeleeDps { get; set; }
        public bool IsRangedDps { get; set; }
        public bool CanStun { get; set; }
        public bool CanSilence { get; set; }
        public bool IsMagicalDps { get; set; }
        public bool IsPhysicalDps { get; set; }
        public bool IsHealer { get; set; }
        public bool IsTank { get; set; }
        public bool IsDps { get; set; }

        private ICollection<PotentialJob> _potentialJobs;
        public virtual ICollection<PotentialJob> PotentialJobs
        {
            get
            {
                return _potentialJobs ?? (_potentialJobs = new Collection<PotentialJob>());
            }
            set
            {
                _potentialJobs = value;
            }
        }
    }
}
