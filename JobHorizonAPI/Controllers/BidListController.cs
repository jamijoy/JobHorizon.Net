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
    [RoutePrefix("api/jobbids")]
    public class BidListController : ApiController
    {
        IRepository<BidList> repo;
        IBid BidListRepo;
        public BidListController()
        {
            this.repo        = new BidRepository();
            this.BidListRepo = new BidRepository();
        }

        [Route("")] // Get data
        public IHttpActionResult Get()
        {
            var bids = repo.GetAll().ToList();
            //LINKS
            foreach (var bid in bids)
            {
                bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids",             Method = "GET",     Rel = "Get All Bids" });
                bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "GET",     Rel = "Get a specific bid" });
                bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids",             Method = "POST",    Rel = "Create a new bid" });
                bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "PUT",     Rel = "Update specific bid" });
                bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "DELETE",  Rel = "Delete specific bid" });
            }
            return Ok(bids);
        }
        [Route("")] // Post Data
        public IHttpActionResult Post([FromBody]BidList bid)
        {
            repo.Insert(bid);
            string url = Url.Link("GetBitById", new { id = bid.BidId });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids",             Method = "GET",   Rel = "Get All Bids" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "GET",   Rel = "Get a specific bid" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids",             Method = "POST",  Rel = "Create a new bid" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "PUT",   Rel = "Update specific bid" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "DELETE",Rel = "Delete specific bid" });
            return Created(url, bid);
        }

        [Route("{id}", Name = "GetBidById")]
        public IHttpActionResult Get(int id) // Show data
        {
            var bid = repo.Get(id);
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids",             Method = "GET",    Rel = "Get All Bids" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "GET",    Rel = "Get a specific bid" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids",             Method = "POST",   Rel = "Create a new bid" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "PUT",    Rel = "Update specific bid" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "DELETE", Rel = "Delete specific bid" });
            return Ok(bid);
        }


        [Route("{id}")] //Update Data
        public IHttpActionResult Put([FromBody]BidList bid, [FromUri]int id)
        {
            bid.BidId = id;
            repo.Update(bid);
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids",             Method = "GET",    Rel = "Get All Bids" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "GET",    Rel = "Get a specific bid" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids",             Method = "POST",   Rel = "Create a new bid" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "PUT",    Rel = "Update specific bid" });
            bid.Links.Add(new Links() { HRef = "http://localhost:1569/api/jobbids" + bid.BidId, Method = "DELETE", Rel = "Delete specific bid" });
            return Ok(bid);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("Bids/{id}")]
        public IHttpActionResult GetBidList(int id) // Show data
        {
            var bid = BidListRepo.GetBidList(id);
            return Ok(bid);
        }
    }
}
