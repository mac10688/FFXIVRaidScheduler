﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RaidScheduler.Domain;
using RaidScheduler.Domain.DomainModels;
using RaidScheduler.Domain.Repositories;
using RaidScheduler.WebUI.Models;

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

            var potentialJobs = potentialJobRepository.Get().GroupBy(j => j.Job.JobId);

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