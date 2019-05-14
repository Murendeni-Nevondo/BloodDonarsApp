using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BloodTraceApi.Models;

namespace BloodTraceApi.Controllers
{
    public class BloodUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //POST: api/BloodUsers
        public IHttpActionResult Post([FromBody] BloodUser _blooduser)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.BloodUsers.Add(_blooduser);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.Created);
        }
        //GET: api/BloodUsers

        public IEnumerable<BloodUser> Get(string _bloodGroup, string _country)
        {
            var bloodusers = db.BloodUsers.Where(user => user.BloodGroup.StartsWith(_bloodGroup) && user.Country.StartsWith(_country));
            return bloodusers;
        }

        //GET: api/BloodUsers

        public IEnumerable<BloodUser> Get()
        {
            var latestbloodusers = db.BloodUsers.OrderByDescending(user => user.Date);
            return latestbloodusers;
        }
    }
}