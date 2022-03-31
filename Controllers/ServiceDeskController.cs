using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PZLServiceDesk.Utility;

namespace PZLServiceDesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceDeskController : ControllerBase
    {

        [Produces("application/json")]
        [HttpGet("GetIssueType")]
        public async Task<IActionResult> GetIssueType()
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.getIssueCategory().ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpGet("GetPriority")]
        public async Task<IActionResult> GetPriority()
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.getPriority().ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpGet("GetAllIssue")]
        public async Task<IActionResult> GetAllIssue()
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.GetAllIssues().ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpGet("GetAllTechnicians")]
        public async Task<IActionResult> GetAllTechnicians()
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.GetAllTechnicians().ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpGet("GetDashboard")]
        public async Task<IActionResult> GetDashboard()
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.getDashboard().ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }


        [Produces("application/json")]
        [HttpGet("GetAllIssuesByRequester")]
        public async Task<IActionResult> GetAllIssuesByRequester(string Requester)
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.GetAllIssuesByRequester(Requester).ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpGet("GetAllIssuesByStatus")]
        public async Task<IActionResult> GetAllIssuesByStatus(string status)
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.GetAllIssuesByStatus(status).ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }


        [Produces("application/json")]
        [HttpGet("GetAllIssuesByRequesterandStatus")]
        public async Task<IActionResult> GetAllIssuesByRequesterandStatus(string Requester, string status)
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.GetAllIssuesByRequesterandStatus(Requester, status).ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpGet("GetAllIssuesByTechnicianandStatus")]
        public async Task<IActionResult> GetAllIssuesByTechnicianandStatus(string Technician, string status)
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.GetAllIssuesByTechnicianandStatus(Technician, status).ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpGet("getApplication")]
        public async Task<IActionResult> getApplication()
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.getApplication().ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }


        [Produces("application/json")]
        [HttpGet("getUsers")]
        public async Task<IActionResult> GetAllgetUsersIssue()
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.getUsers().ToList();
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpGet("GetIssueByID")]
        public async Task<IActionResult> GetIssueByID(int ID)
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.GetIssueByID(ID);
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }


        [Produces("application/json")]
        [HttpPost("PostNewIssue")]
        public async Task<IActionResult> PostNewIssue(CreateIssue Data)
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.postIssue(Data);
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpPost("PostNewResolution")]
        public async Task<IActionResult> PostNewResolution(CreateResolutions Data)
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.PostResolution(Data);
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }

        [Produces("application/json")]
        [HttpPost("PostReasignIssue")]
        public async Task<IActionResult> PostReasignIssue(ReAssignIssue Data)
        {

            ServiceDeskUtility Pru = new ServiceDeskUtility();
            try
            {

                var res = Pru.PostReasignIssue(Data);
                return Ok(res);
            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return null;
            }


        }
    }
}
