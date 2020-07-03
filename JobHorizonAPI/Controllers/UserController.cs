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
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        IRepository<User> repo;
        IUserRepository UserRepo;
        public UserController()
        {
            this.repo = new UserRepository();
            this.UserRepo = new UserRepository();
        }

        [Route("")] // Get data
        public IHttpActionResult Get()
        {
            var users = repo.GetAll().ToList();
            //LINKS
            foreach (var user in users)
            {
                user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users", Method = "GET", Rel = "Get All Users" });
                user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "GET", Rel = "Get a specific user" });
                user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users", Method = "POST", Rel = "Create a new New User" });
                user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "PUT", Rel = "Update specific User" });
                user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "DELETE", Rel = "Delete specific User" });

            }


            return Ok(users);
        }
        [Route("")] // Post Data
        public IHttpActionResult Post([FromBody]User user)
        {

            repo.Insert(user);
            string url = Url.Link("GetUserById", new { id = user.UserId });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users", Method = "GET", Rel = "Get All Users" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "GET", Rel = "Get a specific user" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users", Method = "POST", Rel = "Create a new New User" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "PUT", Rel = "Update specific User" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "DELETE", Rel = "Delete specific User" });
            return Created(url, user);
        }

        [Route("{id}", Name = "GetUserById")]
        public IHttpActionResult Get(int id) // Show data
        {
            var user = repo.Get(id);
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users", Method = "GET", Rel = "Get All Users" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "GET", Rel = "Get a specific user" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users", Method = "POST", Rel = "Create a new New User" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "PUT", Rel = "Update specific User" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "DELETE", Rel = "Delete specific User" });
            return Ok(user);
        }


        [Route("{id}")] //Update Data
        public IHttpActionResult Put([FromBody]User user, [FromUri]int id)
        {
            user.UserId = id;
            repo.Update(user);
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users", Method = "GET", Rel = "Get All Users" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "GET", Rel = "Get a specific user" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users", Method = "POST", Rel = "Create a new New User" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "PUT", Rel = "Update specific User" });
            user.Links.Add(new Links() { HRef = "http://localhost:1569/api/users" + user.UserId, Method = "DELETE", Rel = "Delete specific User" });
            return Ok(user);
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {
            repo.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
