using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CGDriveApplication.Models;
using CGDriveApplication.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CGDriveApplication.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly CG_DocsContext _cg_docsContext;
        public UserController(CG_DocsContext cg_docsContext)
        {
            _cg_docsContext = cg_docsContext;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult Get()
        {
            var getUser = _cg_docsContext.UserTable.ToList();
            return Ok(getUser);
        }

        // GET: api/User/5
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var getUsId = _cg_docsContext.UserTable.Where(o => o.UserId == id);
            return Ok(getUsId);
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] UserRequestModel value)
        {
            UserTable us = new UserTable()
            {
                UserName = value.UserName,
                UserPassword = value.UserPassword,
                CreatedAt = DateTime.Now
            };
            _cg_docsContext.UserTable.Add(us);
            _cg_docsContext.SaveChanges();
        }

        //// PUT: api/User/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
