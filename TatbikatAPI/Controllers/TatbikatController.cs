using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatbikatAPI.DatabaseOperations;
using TatbikatAPI.Models;

namespace TatbikatAPI.Controllers
{
    [Route("api/[controller]")]
    public class TatbikatController : Microsoft.AspNetCore.Mvc.Controller  
    {
        private readonly DatabaseManager _dbm;
        public TatbikatController(DatabaseManager dbm)
        { 
            _dbm = dbm; 
        }
       
        //if we want to make route    [HttpGet("getall")]
        [HttpGet("getallapps")]
        public async Task<IActionResult> GetAllApps()
        {
            return Ok(await _dbm.GetAllApps());
        }

        [HttpGet("getallcategortries")]
        public IActionResult GetAllCategortries()
        {
            return Ok(_dbm.GetAllCategortries());
        }

        [HttpPost("postnewapp")]
        public void PostNewApp([FromBody] TatbikatApp app)
        {
            _dbm.PostNewApp(app);
        }

    }
}
