using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RaidScheduler.Data;
using RaidScheduler.Entities;
using RaidScheduler.Models;

namespace RaidScheduler.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IRepository<PotentialJob> potentialJobRepository;

        public DashboardController(IRepository<PotentialJob> potentialJobRepository)
        {
            this.potentialJobRepository = potentialJobRepository;
        }
        
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PlayerPercentageChart()
        {
            var jobAndCountModel = new JobPercentageModel();

            jobAndCountModel.JobAndCountModel.Add(new object[2]{
                "Job", "Job Portion"
            });

            var potentialJobs = potentialJobRepository.Get().GroupBy(j => j.Job.JobID);

            foreach(var job in potentialJobs)
            {
                jobAndCountModel.JobAndCountModel.Add(new object[2]
                    {                        
                        job.First().Job.JobName,
                        job.Count()            
                    });
            }
            return PartialView("_PlayerPercentageChart", jobAndCountModel);
        }

	}
}