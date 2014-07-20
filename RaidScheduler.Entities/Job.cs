using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidScheduler.DTO
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

    }
}
