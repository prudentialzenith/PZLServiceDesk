using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PZLServiceDesk.Models;
using Microsoft.EntityFrameworkCore;

namespace PZLServiceDesk.Utility
{
    public class AuthUtility
    {


        public GuardResponse auth(String username, String AuthKey)
        {

            try
            {
                using (DBServiceDeskContext dbcontext = new DBServiceDeskContext())
                {
                    ServiceDeskUtility Prudential = new ServiceDeskUtility();

                    var user = dbcontext.Users.Where(n => n.Username == username).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.Status != "Active")
                        {
                            return new GuardResponse { rescode = "91" }; //inactive user
                        }
                        else if (user.IsDefault == true)
                        {

                            if (GuardCheck(username, AuthKey).Equals("00"))
                            {
                                return new GuardResponse { rescode = "77" }; //DefaultPassword
                            }
                            else
                            {
                                return new GuardResponse { rescode = "99" };//failed
                            }

                        }
                        else if (GuardCheck(username, AuthKey).Equals("00"))
                        {
                            NonDBServiceDeskContext sp = new NonDBServiceDeskContext();


                            var userdetails = sp.Set<UserQuery>().FromSqlInterpolated($"getUser {username}").ToList();




                            GuardResponse sendback = new GuardResponse
                            {
                                username = userdetails.FirstOrDefault().Username,
                                Role = userdetails.FirstOrDefault().Role,
                                Dept = userdetails.FirstOrDefault().Dept,
                                Token = (SHA.GenerateSHA512String(username.ToLower() + ">|<" + userdetails.FirstOrDefault().Role)).ToLower(),
                                RoleId = userdetails.FirstOrDefault().IssueRoleID,
                                DeptID = userdetails.FirstOrDefault().DeptD,

                                rescode = "00"
                            };
                            user.Username = sendback.username;
                            user.SessionId = sendback.Token;
                            user.LastLogin = DateTime.Now;

                            dbcontext.SaveChanges();


                            Prudential.SendMail("IT Service Desk Login Notification", user.Username, user.Email,
                                                   @"
                                            <body left margin='10' marginwidth='0' topmargin='10' marginheight='0' offset='0'>
                                            <center>
                                            <table style='border-style: solid;  border-color: maroon;' cellpadding='0' cellspacing='0' height='100%' width='100%' id='backgroundTable'>
                                            <tr><td align='center' valign='top' style='padding-top:20px;'>

                                            <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateBody'>
                                            <tr><td valign='top'>
                                              Dear {m.Username}
                                            <br>
                                            <p>
                                             Login Was Successful</p>
                                            <br/>
                                            <p> If this wasn't initiated by you, Please do a reset on your password and contact IT.
                                            
                                        

                                            

                          
                                            <p>Warm Regards.</P>


                           
                                            <br>
                                
                                           
                       
                                            </td></tr>
                                            </table>
                                            </td></tr>

                                            </table>

                                            </center>
                                            </body></html>

                                            ");

                            return sendback; //sucessful
                        }
                        else
                            return new GuardResponse { rescode = "99" };//failed
                    }
                    else return new GuardResponse { rescode = "97" }; // invalid user
                }
            }
            catch (Exception ex)
            {
                return new GuardResponse { rescode = "96" + ex.Message };
            }
        }

        public GuardResponse Resetauth(String username, String AuthKey)
        {

            try
            {
                using (DBServiceDeskContext dbcontext = new DBServiceDeskContext())
                {
                    ServiceDeskUtility Prudential = new ServiceDeskUtility();
                    var user = dbcontext.Users.Where(n => n.Username == username).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.Status != "Active")
                        {
                            return new GuardResponse { rescode = "91" }; //inactive user
                        }
                        else
                        {
                            var newp = SHA.GenerateSHA512String(AuthKey);
                            user.Password = newp;
                            user.IsDefault = false;

                            dbcontext.SaveChanges();

                            NonDBServiceDeskContext sp = new NonDBServiceDeskContext();


                            var userdetails = sp.Set<UserQuery>().FromSqlInterpolated($"getUser {username}").ToList();




                            GuardResponse sendback = new GuardResponse
                            {
                                username = userdetails.FirstOrDefault().Username,
                                Role = userdetails.FirstOrDefault().Role,
                                Dept = userdetails.FirstOrDefault().Dept,
                                Token = (SHA.GenerateSHA512String(username.ToLower() + ">|<" + userdetails.FirstOrDefault().Role)).ToLower(),
                                RoleId = userdetails.FirstOrDefault().IssueRoleID,
                                DeptID = userdetails.FirstOrDefault().DeptD,

                                rescode = "00"
                            };
                            user.Username = sendback.username;
                            user.SessionId = sendback.Token;
                            user.LastLogin = DateTime.Now;

                            dbcontext.SaveChanges();

                            Prudential.SendMail("IT Service Desk Reset Notification", user.Username, user.Email,
                                                   @"
                                            <body left margin='10' marginwidth='0' topmargin='10' marginheight='0' offset='0'>
                                            <center>
                                            <table style='border-style: solid;  border-color: maroon;' cellpadding='0' cellspacing='0' height='100%' width='100%' id='backgroundTable'>
                                            <tr><td align='center' valign='top' style='padding-top:20px;'>

                                            <table border='0' cellpadding='0' cellspacing='0' width='600' id='templateBody'>
                                            <tr><td valign='top'>
                                              Dear {m.Username}
                                            <br>
                                            <p>
                                             Password Reset was successful</p>
                                            
                                           <p> Click on the link below,</p>

                                            https://webservicestest.prudentialzenith.com/ITServiceDesk/

                          
                                            <p>Warm Regards.</P>


                           
                                            <br>
                                
                                           
                       
                                            </td></tr>
                                            </table>
                                            </td></tr>

                                            </table>

                                            </center>
                                            </body></html>

                                            ");

                            return sendback; //sucessful
                        }

                    }
                    else return new GuardResponse { rescode = "97" }; // invalid user
                }
            }
            catch (Exception ex)
            {

                return new GuardResponse { rescode = "96" + ex.Message };
            }
        }

        //private String authOTP(String username, String DoorKey)
        //{

        //    try
        //    {

        //        VT_SDKSoapClient call = new VT_SDKSoapClient();
        //        var s = call.AuthenticateUser("africa.int.zenithbank.com", username, DoorKey);

        //        return s.Split('-')[0];

        //    }
        //    catch (Exception ex)
        //    {
        //        return "96";
        //    }
        //}

        private string GuardCheck(string username, string password)
        {
            using (DBServiceDeskContext dc = new DBServiceDeskContext())
            {

                try
                {
                    var KnockKnock = dc.Users.Where(c => c.Username == username).FirstOrDefault();
                    var CorrectResponse = KnockKnock.Password;
                    var newp = SHA.GenerateSHA512String(password);


                    if (newp == CorrectResponse)
                    {
                        KnockKnock.LastLogin = DateTime.Now;
                        return "00";

                    }
                    else
                    {
                        return "99";
                    }
                }
                catch (Exception ex)
                {

                    return "98";
                }

            }


        }

        public static class SHA
        {

            public static string GenerateSHA512String(string inputString)
            {
                SHA512 sha512 = SHA512Managed.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hash = sha512.ComputeHash(bytes);
                return GetStringFromHash(hash);
            }

            public static string GetStringFromHash(byte[] hash)
            {
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("X2"));
                }
                return result.ToString();
            }

        }
    }
}
