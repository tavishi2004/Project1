using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGDriveApplication.Models;
using CGDriveApplication.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JWTEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public static int userid;
        public static string username;
        private  IConfiguration _config;
        private readonly CG_DocsContext _cg_docsContext;
        public LoginController(IConfiguration config, CG_DocsContext cg_docsContext)
        {
            _config = config;
           _cg_docsContext=cg_docsContext;
    }
        // POST: api/Login
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginRequestModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString ,id=userid,name=username});
            }

            return response;
        }
        private string BuildToken(UserRequestModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private UserRequestModel Authenticate(LoginRequestModel login)
        {
            UserRequestModel user = null;
            var res = _cg_docsContext.UserTable.FirstOrDefault( o => o.UserName == login.UserName);
            if (res != null)
            {
                if (res.UserName != null && res.UserPassword == login.UserPassword)
                {
                    user = new UserRequestModel { UserName = res.UserName, UserPassword = res.UserPassword };
                    userid = res.UserId;
                    username = res.UserName;
                    return user;
                }
            }
            return null;
        }
        //// PUT: api/Login/5
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
