using JobHorizonAPI.Interface;
using JobHorizonAPI.Models;
using JobHorizonAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobHorizonAPI.Controllers
{
    [RoutePrefix("api/joblists")]
    public class JobListController : ApiController
    {
        IRepository<JobList> repo;
        IJobList JobRepo;
        public JobListController()
        {
            this.repo = new JobListRepository();
            this.JobRepo = new JobListRepository();
        }

        [Route("")] // Get data
        public IHttpActionResult Get()
        {
            var jobs = repo.GetAll().ToList();
            //LINKS
            foreach (var job in jobs)
            {
                job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists",                Method = "GET",    Rel = "Get All Jobs" });
                job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId,    Method = "GET",    Rel = "Get a specific job" });
                job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists",                Method = "POST",   Rel = "Create a new job" });
                job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId,    Method = "PUT",    Rel = "Update specific job" });
                job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId,    Method = "DELETE", Rel = "Delete specific job" });
            }
            return Ok(jobs);
        }
        [Route("list")] // Get data
        public IHttpActionResult GetList()
        {
            return Ok(repo.GetAll().ToList());
        }
        [Route("")] // Post Data
        public IHttpActionResult Post([FromBody]JobList job)
        {

            repo.Insert(job);
            string url = Url.Link("GetJobById", new { id = job.JobId });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists",             Method = "GET",    Rel = "Get All Jobs" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId, Method = "GET",    Rel = "Get a specific job" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists",             Method = "POST",   Rel = "Create a new job" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId, Method = "PUT",    Rel = "Update specific job" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId, Method = "DELETE", Rel = "Delete specific job" });
            return Created(url, job);
        }

        [Route("{id}", Name = "GetJobById")]
        public IHttpActionResult Get(int id) // Show data
        {
            var job = repo.Get(id);
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists",             Method = "GET",    Rel = "Get All Jobs" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId, Method = "GET",    Rel = "Get a specific job" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists",             Method = "POST",   Rel = "Create a new job" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId, Method = "PUT",    Rel = "Update specific job" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId, Method = "DELETE", Rel = "Delete specific job" });
            return Ok(job);
        }


        [Route("{id}")] //Update Data
        public IHttpActionResult Put([FromBody]JobList job, [FromUri]int id)
        {
            job.JobId = id;
            repo.Update(job);
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists",             Method = "GET",    Rel = "Get All Jobs" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId, Method = "GET",    Rel = "Get a specific job" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists",             Method = "POST",   Rel = "Create a new job" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId, Method = "PUT",    Rel = "Update specific job" });
            job.Links.Add(new Links() { HRef = "http://localhost:1569/api/joblists" + job.JobId, Method = "DELETE", Rel = "Delete specific job" });
            return Ok(job);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
