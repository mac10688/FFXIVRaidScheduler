using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RaidScheduler.Domain;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Repositories;
using RaidScheduler.WebUI.Models;
using RaidScheduler.Domain.DomainModels.PlayerDomain;
using RaidScheduler.Domain.DomainModels.JobDomain;

namespace RaidScheduler.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IRepository<Player> _playerRepository;
        private readonly IJobFactory _jobFactory;

        public DashboardController(IRepository<Player> playerRepository, IJobFactory jobFactory)
        {
            _playerRepository = playerRepository;
            _jobFactory = jobFactory;
        }
        
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Finds the ration of jobs and people with those jobs
        /// </summary>
        /// <returns>Partial View of job percentage</returns>
        public ActionResult PlayerPercentageChart()
        {
            var jobAndCountModel = new JobPercentageModel();

            jobAndCountModel.JobAndCountModel.Add(new object[2]{
                "Job", "Job Portion"
            });

            var jobs = _jobFactory.GetAllJobs();

            var potentialJobs = _playerRepository.Get().SelectMany(p => p.PotentialJobs).GroupBy(p => p.JobId);

            foreach(var job in potentialJobs)
            {
                var jobType = job.First().JobId;
                jobAndCountModel.JobAndCountModel.Add(new object[2]
                    {                        
                        jobs.Where(j => j.JobType == jobType).Single().JobName,
                        job.Count()            
                    });
            }
            return PartialView("_PlayerPercentageChart", jobAndCountModel);
        }

	}
}