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
    [RoutePrefix("api/jobhistory")]
    public class JobHisotryController : ApiController
    {
        IRepository<JobHistory> repo;
        IJobHistory JobHisotryRepo;
        public JobHisotryController()
        {
            this.repo           = new JobHistoryRepository();
            this.JobHisotryRepo = new JobHistoryRepository();
        }

        [Route("")] // Get data
        public IHttpActionResult Get()
        {
            var jobhistories = repo.GetAll().ToList();
            //LINKS
            foreach (var jobhistory in jobhistories)
            {
                jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory",                           Method = "GET",    Rel = "Get All jobhistories" });
                jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId,    Method = "GET",    Rel = "Get a specific jobhistory" });
                jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory",                           Method = "POST",   Rel = "Create a new jobhistory" });
                jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId,    Method = "PUT",    Rel = "Update specific jobhistory" });
                jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId,    Method = "DELETE", Rel = "Delete specific jobhistory" });
            }
            return Ok(jobhistories);
        }
        [Route("")] // Post Data
        public IHttpActionResult Post([FromBody]JobHistory jobhistory)
        {
            repo.Insert(jobhistory);
            string url = Url.Link("GetHistoryById", new { id = jobhistory.HistoryId });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory",                        Method = "GET",    Rel = "Get All jobhistories" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId, Method = "GET",    Rel = "Get a specific jobhistory" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory",                        Method = "POST",   Rel = "Create a new jobhistory" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId, Method = "PUT",    Rel = "Update specific jobhistory" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId, Method = "DELETE", Rel = "Delete specific jobhistory" });
            return Created(url, jobhistory);
        }

        [Route("{id}", Name = "GetHistoryById")]
        public IHttpActionResult Get(int id) // Show data
        {
            var jobhistory = repo.Get(id);
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory", Method = "GET", Rel = "Get All jobhistories" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId, Method = "GET", Rel = "Get a specific jobhistory" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory", Method = "POST", Rel = "Create a new jobhistory" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId, Method = "PUT", Rel = "Update specific jobhistory" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId, Method = "DELETE", Rel = "Delete specific jobhistory" });
            return Ok(jobhistory);
        }


        [Route("{id}")] //Update Data
        public IHttpActionResult Put([FromBody]JobHistory jobhistory, [FromUri]int id)
        {
            jobhistory.HistoryId = id;
            repo.Update(jobhistory);
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory",                        Method = "GET",    Rel = "Get All jobhistories" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId, Method = "GET",    Rel = "Get a specific jobhistory" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory",                        Method = "POST",   Rel = "Create a new jobhistory" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId, Method = "PUT",    Rel = "Update specific jobhistory" });
            jobhistory.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobhistory" + jobhistory.HistoryId, Method = "DELETE", Rel = "Delete specific jobhistory" });
            return Ok(jobhistory);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
