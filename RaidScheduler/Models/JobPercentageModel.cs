using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RaidScheduler.Models
{
    public class JobPercentageModel
    {
        //Because of google chars api, need to declare a collection of objects
        private ICollection<object[]> jobAndCountModel;
        public ICollection<object[]> JobAndCountModel
        { 
            get
            {
                return jobAndCountModel ?? (jobAndCountModel = new List<object[]>());
            }
            set
            {
                jobAndCountModel = value;
            }
        }
        
    }
}