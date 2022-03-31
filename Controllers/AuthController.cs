using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PZLServiceDesk.Models;
using PZLServiceDesk.Utility;

namespace PZLServiceDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {


        //[Produces("application/json")]
        //[HttpGet("DoorKnob")]
        //public async Task<IActionResult> Auth(String Username, String AuthKey)
        //{
        //    if (Username != null || AuthKey != null)
        //    {
        //        try
        //        {
        //            AuthUtility Prudential = new AuthUtility();
        //            var res = Prudential.auth(Username, AuthKey);
        //            return Ok(res);
        //        }
        //        catch (Exception)
        //        {

        //            return BadRequest();
        //        }


        //    }
        //    else
        //    {
        //        return BadRequest();

        //    }
        //}


        //[Produces("application/json")]
        //[HttpGet("ReDoorKnob")]
        //public async Task<IActionResult> ReAuth(String Username, String AuthKey)
        //{
        //    if (Username != null || AuthKey != null)
        //    {
        //        try
        //        {
        //            AuthUtility Prudential = new AuthUtility();
        //            var res = Prudential.Resetauth(Username, AuthKey);
        //            return Ok(res);
        //        }
        //        catch (Exception)
        //        {

        //            return BadRequest();
        //        }


        //    }
        //    else
        //    {
        //        return BadRequest();

        //    }
        //}

    }
}
