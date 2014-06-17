using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaidScheduler.Data;
using RaidScheduler.Entities;

namespace RaidScheduler.Business
{
    public class PersonRepository
    {
        private readonly RaidSchedulerContext context;
        public PersonRepository(RaidSchedulerContext context)
        {
            this.context = context;
        }

        public void Get(int id)
        {
            using(var db = new RaidSchedulerContext())
            {

            }
            
        }

    }
}
