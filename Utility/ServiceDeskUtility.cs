using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using PZLServiceDesk.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace PZLServiceDesk.Utility
{
    public class ServiceDeskUtility
    {


        public List<IssueCategory> getIssueCategory()
        {
            try
            {
                NonDBServiceDeskContext dc = new NonDBServiceDeskContext();

                var res = dc.IssueCategory.FromSqlRaw($"GetIssueCategory").ToList();
                return res;

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

        public postresponse postIssue(CreateIssue Data)
        {
            postresponse res = new postresponse();

            try
            {
                DBServiceDeskContext dc = new DBServiceDeskContext();

                IssueTbl Issue = new IssueTbl();

                var getAssignedto = dc.IssueCategoryTbls.Where(m => m.Id == Data.Category).FirstOrDefault();
                //var users = dc.Users.ToList();
                var User = dc.Users.Where(m => m.Username == Data.CreatedBy).FirstOrDefault();


                Issue.Subject = Data.Subject;
                Issue.Category = Data.Category;
                Issue.Priority = Data.Priority;
                Issue.AssinedTo = getAssignedto.CategoryAssignee;
                Issue.Description = Data.Description;
                Issue.Status = Data.Status;
                Issue.CreatedBy = Data.CreatedBy;
                Issue.DateCreated = DateTime.Now;

                dc.IssueTbls.Add(Issue);
                dc.SaveChanges();

                SendNewIssueMail("IT Service Desk Notification", Issue.Subject, Issue.Description, Issue.Status, Issue.CreatedBy, Issue.DateCreated, User.Email,
                                                   @"
                                            <body left margin='10' marginwidth='0' topmargin='10' marginheight='0' offset='0'>
                                            <center>
                                            <table style='border-style: solid;  border-color: maroon;' cellpadding='0' cellspacing='0' height='100%' width='100%' id='backgroundTable'>
                                            <tr><td align='center' valign='top' style='padding-top:20px;'>

                                            <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateBody'>
                                            <tr><td valign='top'>
                                              Dear {m.Username}
                                            <br>
                                            <p> We would like to inform you that you have successfully logged your request</p>
                                             <p>Subject: {IssueSubject} </p>
                                             <p> Description: {IssueDescription}</p>
                                             <p> Status: {IssueStatus} </p>
                                             <p> Date:{DateCreated} </p>

                                            <p> We are working on your request  </p>                 
                                            <p>Warm Regards.</P>


                           
                                            <br>
                                
                                           
                       
                                            </td></tr>
                                            </table>
                                            </td></tr>

                                            </table>

                                            </center>
                                            </body></html>

                                            ");

                SendNewIssueMail("IT Service Desk Notification", Issue.Subject, Issue.Description, Issue.Status, getAssignedto.CategoryAssignee, Issue.DateCreated, getAssignedto.AsigneeEmail,
                                                 @"
                                            <body left margin='10' marginwidth='0' topmargin='10' marginheight='0' offset='0'>
                                            <center>
                                            <table style='border-style: solid;  border-color: maroon;' cellpadding='0' cellspacing='0' height='100%' width='100%' id='backgroundTable'>
                                            <tr><td align='center' valign='top' style='padding-top:20px;'>

                                            <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateBody'>
                                            <tr><td valign='top'>
                                              Dear {m.Username}
                                            <br>
                                            <p> We would like to inform you that a request has been assigned to you</p>
                                             <p>Subject: {IssueSubject} </p>
                                             <p> Description: {IssueDescription}</p>
                                             <p> Status: {IssueStatus} </p>
                                             <p> Date:{DateCreated} </p>

                                            <p> Kindly Resolve  </p>                 
                                            <p>Warm Regards.</P>


                           
                                            <br>
                                
                                           
                       
                                            </td></tr>
                                            </table>
                                            </td></tr>

                                            </table>

                                            </center>
                                            </body></html>

                                            ");



                res.responsecode = "00";
                res.response = "Success";

                return res;






            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                res.responsecode = "01";
                res.response = "Failure";

                return res;
            }

        }

        public GetSingleIssue GetIssueByID(int ID)
        {
            try
            {
                NonDBServiceDeskContext dc = new NonDBServiceDeskContext();
                DBServiceDeskContext db = new DBServiceDeskContext();
                GetSingleIssue CompleteIssueRes = new GetSingleIssue();

                var issue = dc.GetAllIssues.FromSqlRaw($"GetIssuesByIdForDisplay @Id={ID}").ToList();

                if (issue.Count != 0)
                {
                    var CompleteIssue = issue.FirstOrDefault();

                    var ResolutionDesc = db.ResolutionTbls.FromSqlRaw($"GetResolutionsByIssueId @IssueID={ID}").ToList();

                    var singleResolution = ResolutionDesc.FirstOrDefault();

                    CompleteIssueRes.AssinedTo = CompleteIssue.AssinedTo;
                    CompleteIssueRes.CreatedBy = CompleteIssue.CreatedBy;
                    CompleteIssueRes.DateCreated = CompleteIssue.DateCreated;
                    CompleteIssueRes.DateResolved = CompleteIssue.DateResolved;
                    CompleteIssueRes.Id = CompleteIssue.Id;
         
                    CompleteIssueRes.Subject = CompleteIssue.Subject;
                    CompleteIssueRes.Description = CompleteIssue.Description;
                    CompleteIssueRes.Category = CompleteIssue.Category;
                    CompleteIssueRes.Priority = CompleteIssue.Priority;
                    CompleteIssueRes.ResolvedBy = CompleteIssue.ResolvedBy;
                    CompleteIssueRes.ResolvedDesc = (singleResolution == null) ? null : singleResolution.ResolutionDesc ;
                    CompleteIssueRes.Status = CompleteIssue.Status;


                }


                return CompleteIssueRes;

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

        public List<CountObj> CountOfNewIssues()
        {
            try
            {
                NonDBServiceDeskContext  sp = new NonDBServiceDeskContext();


                var claims = sp.CountObj.FromSqlInterpolated($"CountOfNewIssues").ToList();

                return claims;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CountObj> CountOfTotalIssues()
        {
            try
            {
                NonDBServiceDeskContext sp = new NonDBServiceDeskContext();


                var claims = sp.CountObj.FromSqlInterpolated($"CountOfTotalIssues").ToList();

                return claims;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<CountObj> CountOfResolvedIssues()
        {
            try
            {
                NonDBServiceDeskContext sp = new NonDBServiceDeskContext();


                var claims = sp.CountObj.FromSqlInterpolated($"CountOfResolvedIssues").ToList();

                return claims;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CountObj> CountOfUsers()
        {
            try
            {
                NonDBServiceDeskContext sp = new NonDBServiceDeskContext();


                var claims = sp.CountObj.FromSqlInterpolated($"CountOfUsers").ToList();

                return claims;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<Dashboard> getDashboard()
        {
            Dashboard Dash1 = new Dashboard();
            Dashboard Dash2 = new Dashboard();
            Dashboard Dash3 = new Dashboard();
            Dashboard Dash4 = new Dashboard();

            List<Dashboard> Dashlist = new List<Dashboard>();

 
            Dash1.Value = CountOfTotalIssues().FirstOrDefault().ID.ToString();
            Dash1.Title = "Total Issues";
            Dash1.IDNo = 1;
  
            Dashlist.Add(Dash1);


        
            Dash2.Value = CountOfNewIssues().FirstOrDefault().ID.ToString();
            Dash2.Title = "New Issues";
            Dash2.IDNo = 2;
       
            Dashlist.Add(Dash2);

        
            Dash3.Value = CountOfResolvedIssues().FirstOrDefault().ID.ToString();
            Dash3.Title = "Resolved Issues";
            Dash3.IDNo = 3;

            Dashlist.Add(Dash3);

       
            Dash4.Value = CountOfUsers().FirstOrDefault().ID.ToString();
            Dash4.Title = "Users";
            Dash4.IDNo = 4;
  
            Dashlist.Add(Dash4);




            return Dashlist;


        }

        public List<GetAllIssues> GetAllIssues()
        {
            try
            {
                NonDBServiceDeskContext dc = new NonDBServiceDeskContext();

                var res = dc.GetAllIssues.FromSqlRaw($"GetIssues").ToList();
                return res;

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

        public List<GetAllTechnicians> GetAllTechnicians()
        {
            try
            {
                NonDBServiceDeskContext dc = new NonDBServiceDeskContext();

                var res = dc.GetAllTechnicians.FromSqlRaw($"getTechnician").ToList();
                return res;

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
        public List<GetAllIssues> GetAllIssuesByRequester(string Requester)
        {
            try
            {
                NonDBServiceDeskContext dc = new NonDBServiceDeskContext();

                var res = dc.GetAllIssues.FromSqlInterpolated($"GetIssuesByRequester @Requester = {Requester}").ToList();
                //var res = dc.GetAllIssues.FromSqlRaw($"GetIssuesByStatus @status ={status}").ToList();
               // var res = dc.GetAllIssues.FromSqlRaw($"GetIssuesByStatus @status ={Requester}").ToList();
                return res;

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

        public List<GetAllIssues> GetAllIssuesByStatus(string status)
        {
            try
            {
                NonDBServiceDeskContext dc = new NonDBServiceDeskContext();

                var res = dc.GetAllIssues.FromSqlInterpolated($"GetIssuesByStatus @status ={status}").ToList();
                return res;

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
        public List<GetAllIssues> GetAllIssuesByRequesterandStatus(string Requester ,string Status)
        {
            try
            {
                NonDBServiceDeskContext dc = new NonDBServiceDeskContext();

                var res = dc.GetAllIssues.FromSqlInterpolated($"GetIssuesByRequesterandStatus @Requester ={Requester},@Status ={Status}").ToList();
                return res;

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

        public List<GetAllIssues> GetAllIssuesByTechnicianandStatus(string Technician, string Status)
        {
            try
            {
                NonDBServiceDeskContext dc = new NonDBServiceDeskContext();

                var res = dc.GetAllIssues.FromSqlInterpolated($"GetIssuesByStatusAndTechnician @Technician ={Technician},@Status ={Status}").ToList();
                return res;

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
        public List<Priority> getPriority()
        {
            try
            {
                NonDBServiceDeskContext dc = new NonDBServiceDeskContext();

                var res = dc.Priority.FromSqlRaw($"GetPriority").ToList();
                return res;

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

        public List<User> getUsers()
        {
            try
            {
                DBServiceDeskContext db = new DBServiceDeskContext();

                var res = db.Users.ToList();

                return res;
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

        public List<ApplicationTable> getApplication()
        {
            try
            {
                DBServiceDeskContext dc = new DBServiceDeskContext();
               var res =  dc.ApplicationTables.ToList();
                return res;
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

        public postresponse PostResolution(CreateResolutions Data)
        {
            postresponse res = new postresponse();

            try
            {
                DBServiceDeskContext dc = new DBServiceDeskContext();

                //IssueTbl Issue = new IssueTbl();

                ResolutionTbl PzlResolution = new ResolutionTbl();

                var UpdateIssue = dc.IssueTbls.FromSqlRaw($"GetIssuesByID @id={Data.issueID}").ToList();
                var ResolverEmail = dc.Users.Where(m => m.Username == Data.Resolvedby).FirstOrDefault();
                PzlResolution.DateResolved = DateTime.Now;
                PzlResolution.Resolvedby = Data.Resolvedby;
                PzlResolution.Status = Data.status;
                PzlResolution.IssueId = Data.issueID;
                PzlResolution.ResolutionDesc = Data.ResolutionDesc;
                dc.ResolutionTbls.Add(PzlResolution);

                if (UpdateIssue.Count != 0)
                {
                    var OriginalUpdateIssue = UpdateIssue.FirstOrDefault();

                    var User = dc.Users.Where(m => m.Username == OriginalUpdateIssue.CreatedBy).FirstOrDefault();

                    OriginalUpdateIssue.Status = Data.status;
                    OriginalUpdateIssue.ResolvedBy = Data.Resolvedby;
                    OriginalUpdateIssue.DateResolved = Data.DateResolved;
                    dc.IssueTbls.Update(OriginalUpdateIssue);

                    SendTreatedIssueMail("IT Service Desk Notification", OriginalUpdateIssue.Subject, OriginalUpdateIssue.Description, OriginalUpdateIssue.Status, OriginalUpdateIssue.CreatedBy, Data.ResolutionDesc, Data.Resolvedby, Data.DateResolved, OriginalUpdateIssue.DateCreated, User.Email,
                   @"
                                            <body left margin='10' marginwidth='0' topmargin='10' marginheight='0' offset='0'>
                                            <center>
                                            <table style='border-style: solid;  border-color: maroon;' cellpadding='0' cellspacing='0' height='100%' width='100%' id='backgroundTable'>
                                            <tr><td align='center' valign='top' style='padding-top:20px;'>

                                            <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateBody'>
                                            <tr><td valign='top'>
                                              Dear {m.Username}
                                            <br>
                                            <p> We would like to inform you that you request has been treated</p>
                                             <p>Subject: {IssueSubject} </p>
                                             <p> Description: {IssueDescription}</p>
                                             <p> Status: {IssueStatus} </p>
                                             <p> Date:{DateCreated} </p>
                                             <p> Comments: {ResolutionComment} </p>
                                             <p> Date Resolved:{DateTreated} </p>

                                            <p> Kindly Confirm </p>                 
                                            <p>Warm Regards.</P>


                           
                                            <br>
                                
                                           
                       
                                            </td></tr>
                                            </table>
                                            </td></tr>

                                            </table>

                                            </center>
                                            </body></html>

                                            ");

                    SendTreatedIssueMail("IT Service Desk Notification", OriginalUpdateIssue.Subject, OriginalUpdateIssue.Description, OriginalUpdateIssue.Status, Data.Resolvedby, Data.ResolutionDesc, Data.Resolvedby, Data.DateResolved, OriginalUpdateIssue.DateCreated, ResolverEmail.Email,
                                                     @"
                                            <body left margin='10' marginwidth='0' topmargin='10' marginheight='0' offset='0'>
                                            <center>
                                            <table style='border-style: solid;  border-color: maroon;' cellpadding='0' cellspacing='0' height='100%' width='100%' id='backgroundTable'>
                                            <tr><td align='center' valign='top' style='padding-top:20px;'>

                                            <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateBody'>
                                            <tr><td valign='top'>
                                              Dear {m.Username}
                                            <br>
                                            <p> We would like to inform you that you resolved this request</p>
                                             <p>Subject: {IssueSubject} </p>
                                             <p> Description: {IssueDescription}</p>
                                             <p> Status: {IssueStatus} </p>
                                             <p> Date:{DateCreated} </p>

                                            <p> Kindly Resolve  </p>                 
                                            <p>Warm Regards.</P>


                           
                                            <br>
                                
                                           
                       
                                            </td></tr>
                                            </table>
                                            </td></tr>

                                            </table>

                                            </center>
                                            </body></html>

                                            ");




                }




                dc.SaveChanges();




                res.responsecode = "00";
                res.response = "Success";

                return res;






            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                res.responsecode = "01";
                res.response = "Failure";

                return res;
            }

        }

        public postresponse CreateUser(CreateNewUser UserDetail)
        {
            postresponse res = new postresponse();
            User NewUser = new User();

            DBServiceDeskContext dc = new DBServiceDeskContext();

            try
            {
                NewUser.Username = UserDetail.Username;
                NewUser.Status = UserDetail.Status;
                NewUser.IsDefault = true;
                NewUser.RequisionRoleId = UserDetail.RequisionRoleId;
                NewUser.Password = AuthUtility.SHA.GenerateSHA512String(UserDetail.Password);
                NewUser.IssueRoleId = UserDetail.IssueRoleId;
                NewUser.Email = UserDetail.Email;
                NewUser.DeptD = UserDetail.Department;
                NewUser.DateCreated = DateTime.Now;

                dc.Users.Add(NewUser);
                dc.SaveChanges();


                res.responsecode = "00";
                res.response = "Success";

                return res;


            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                res.responsecode = "01";
                res.response = "Failure";

                return res;
            }


        }

        public List<GetUserforModification> getuserbyUsername(string username)
        {
            NonDBServiceDeskContext dc = new NonDBServiceDeskContext();
            GetUserforModification userinfo = new GetUserforModification();

           var res = dc.GetUserforModifications.FromSqlRaw("GetUserByUsername").ToList();


            return res;





        }

        //public postresponse ResetClaimsPasswordByUserName(string Username)
        //{
        //    NonDBServiceDeskContext dc = new NonDBServiceDeskContext();


        //}

        public bool SendNewIssueMail(String Subject, String IssueSubject, String IssueDescription, String IssueStatus, String Username,DateTime? Date, String DestinationEmail, String MailBody)
        {
            //String SMTPHOST = "172.29.90.118";
            //String SMTPPORT = "25";
            //String SMTPHOST = Configuration.GetSection("SMTPHOST").Value;
            //String SMTPPORT = Configuration.GetSection("SMTPHOST").Value;

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            String SMTPHOST = configuration["SMTPHOST"];
            String SMTPPORT = configuration["SMTPPORT"];
            //String SMTPHOST1 = Configuration["SMTPHOST"];

            try
            {
                MailAddressCollection co = new MailAddressCollection();

                String ToEmail = DestinationEmail;
                String ToFrom = "Prudential Zenith NoReply@Prudentialzenith.com";
                //String ToFrom = SenderEmail;
                MailMessage mail = new MailMessage(ToFrom, ToEmail);
                mail.Subject = Subject;
                mail.CC.Add(new MailAddress("PZLITunit@prudentialzenith.com"));

                co.Add("PZLITUnit@prudentialzenith.com");
            

                mail.Body = MailBody.Replace("{m.Username}", Username).Replace("{IssueSubject}", IssueSubject).Replace("{IssueDescription}", IssueDescription).Replace("{IssueStatus}", IssueStatus).Replace("{DateCreated}", Convert.ToString(Date));

                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                // mail.Attachments.Add(new Attachment("Attachm"));

                SmtpClient smtp = new SmtpClient(SMTPHOST, Convert.ToInt16(SMTPPORT));

                //SmtpClient smtp = new SmtpClient(SMTPIP, SMTPPORT);

                Console.Write(mail);
                smtp.Send(mail);
                return true;

            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return false;
            }
        }

        public bool SendTreatedIssueMail(String Subject, String IssueSubject, String IssueDescription, String IssueStatus, String Username, String ResolutionComment, String TreatedBy, DateTime? DateTreated, DateTime? DateCreated, String DestinationEmail, String MailBody)
        {
            //String SMTPHOST = "172.29.90.118";
            //String SMTPPORT = "25";
            //String SMTPHOST = Configuration.GetSection("SMTPHOST").Value;
            //String SMTPPORT = Configuration.GetSection("SMTPHOST").Value;

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            String SMTPHOST = configuration["SMTPHOST"];
            String SMTPPORT = configuration["SMTPPORT"];
            //String SMTPHOST1 = Configuration["SMTPHOST"];

            try
            {

                String ToEmail = DestinationEmail;
                String ToFrom = "Prudential Zenith NoReply@Prudentialzenith.com";
                //String ToFrom = SenderEmail;
                MailMessage mail = new MailMessage(ToFrom, ToEmail);
                mail.Subject = Subject;
                mail.CC.Add(new MailAddress("PZLITunit@prudentialzenith.com"));
                mail.Body = MailBody.Replace("{m.Username}", Username).Replace("{IssueSubject}", IssueSubject).Replace("{ResolutionComment}", ResolutionComment).Replace("{TreatedBy}", TreatedBy).Replace("{DateTreated}", Convert.ToString(DateTreated)).Replace("{IssueDescription}", IssueDescription).Replace("{IssueStatus}", IssueStatus).Replace("{DateCreated}", Convert.ToString(DateCreated));

                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                // mail.Attachments.Add(new Attachment("Attachm"));

                SmtpClient smtp = new SmtpClient(SMTPHOST, Convert.ToInt16(SMTPPORT));

                //SmtpClient smtp = new SmtpClient(SMTPIP, SMTPPORT);

                Console.Write(mail);
                smtp.Send(mail);
                return true;

            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return false;
            }
        }

        public bool SendMail(String Subject, String Username, String DestinationEmail, String MailBody)
        {
            //String SMTPHOST = "172.29.90.118";
            //String SMTPPORT = "25";
            //String SMTPHOST = Configuration.GetSection("SMTPHOST").Value;
            //String SMTPPORT = Configuration.GetSection("SMTPHOST").Value;

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            String SMTPHOST = configuration["SMTPHOST"];
            String SMTPPORT = configuration["SMTPPORT"];
            //String SMTPHOST1 = Configuration["SMTPHOST"];

            try
            {

                String ToEmail = DestinationEmail;
                String ToFrom = "Prudential Zenith NoReply@Prudentialzenith.com";
                //String ToFrom = SenderEmail;
                MailMessage mail = new MailMessage(ToFrom, ToEmail);
                mail.Subject = Subject;

                mail.Body = MailBody.Replace("{m.Username}", Username);

                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                // mail.Attachments.Add(new Attachment("Attachm"));

                SmtpClient smtp = new SmtpClient(SMTPHOST, Convert.ToInt16(SMTPPORT));

                //SmtpClient smtp = new SmtpClient(SMTPIP, SMTPPORT);
                smtp.Send(mail);
                return true;

            }
            catch (Exception ex)
            {

                String fileName = "ErrorLog_" + String.Format("{0:yyyy-MM-dd}", DateTime.Now) + ".txt";
                String path = AppDomain.CurrentDomain.BaseDirectory + "\\wwwroot\\" + "\\Logs\\" + fileName;
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(ex + " " + DateTime.Now);

                }
                return false;
            }
        }
    }
}
