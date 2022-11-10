using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
//to do add to the api log for unsuccessful attempts to connect and add it to each of the methods
namespace JwtAuthenticationTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysproController : ControllerBase
    {

        Authentication _authentication;
        SqlConnectionsAndLogging _log;
        public SysproController()
        {
            Authentication authentication = new Authentication();
            SqlConnectionsAndLogging log = new SqlConnectionsAndLogging();
            _authentication = authentication;
            _log = log;
        }
        [Route("GetToken")]
        [HttpGet]
        public string Get(User UserInfo)
        {
            if (UserInfo != null)
            {
                return _authentication.GetToken(UserInfo);
            }
            else
            {
                return "User info is empty";
            }
        }
        // GET: api/<SysproController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SysproController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string token = Request.Headers["Authorization"];
            token = token.Replace("Bearer ", "");
            if (!_authentication.validateToken(token))
            {
                return "token is not valid";
            }
            _log.logApi(DateTime.Now, Request.Path, token);
            return "value";
        }

        // POST api/<SysproController>
        [HttpPost]
        public string Post()
        {
            string token =Request.Headers["Authorization"];
            token = token.Replace("Bearer ", "");
            if (!_authentication.validateToken(token))
            {
                return "token is not valid";
            }
            _log.logApi(DateTime.Now,Request.Path, token);
            try
            {
                //SYSPROWCFServicesClientLibrary40.SYSPROWCFServicesClient client = new SYSPROWCFServicesClientLibrary40.SYSPROWCFServicesClient("net.tcp://localhost:31001/SYSPROWCFService/Rest", SYSPROWCFServicesClientLibrary40.SYSPROWCFBinding.NetTcp, "ADMIN", "", "DMOM", "");
                //client.TransactionPost("PrefixDoc + Document", "Params", "Document");
                return "Success";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            
        }

        // PUT api/<SysproController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            string token = Request.Headers["Authorization"];
            token = token.Replace("Bearer ", "");
            if (_authentication.validateToken(token))
            {
            _log.logApi(DateTime.Now, Request.Path, token);
            }
        }

        // DELETE api/<SysproController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            string token = Request.Headers["Authorization"];
            token = token.Replace("Bearer ", "");
            if (_authentication.validateToken(token))
            {
                _log.logApi(DateTime.Now, Request.Path, token);
            }
        }
    }
}
