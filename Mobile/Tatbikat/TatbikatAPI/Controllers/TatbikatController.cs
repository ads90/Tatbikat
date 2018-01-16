using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TatbikatAPI.DatabaseOperations; 

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
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dbm.GetAllApps());
        }
 
    }
}
