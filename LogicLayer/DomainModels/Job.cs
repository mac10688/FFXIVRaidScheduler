using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.Domain.DomainModels
{
    public class Job
    {

        public Job(string jobName)
        {
            JobName = jobName;
        }

        protected Job() { }

        public int JobId { get; protected set; }
        public string JobName { get; protected set; }
        public bool IsMeleeDps { get; set; }
        public bool IsRangedDps { get; set; }
        public bool CanStun { get; set; }
        public bool CanSilence { get; set; }
        public bool IsMagicalDps { get; set; }
        public bool IsPhysicalDps { get; set; }
        public bool IsHealer { get; set; }
        public bool IsTank { get; set; }
        public bool IsDps { get; set; }

    }
}
