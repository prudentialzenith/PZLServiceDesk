using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PZLServiceDesk.Utility
{
   public class IssueCategory   
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string AsigneeEmail { get; set; }
        public string CategoryAssignee { get; set; }
        public DateTime Datecreated { get; set; }


    }

    public class Priority
    {
        public int Id { get; set; }
        public string PriorityName { get; set; }
        public int Prioritytime { get; set; }
        public string PriorityDesc { get; set; }
        public DateTime DateCreated { get; set; }

    }

    public class CreateIssue
    {
  
        public int? Category { get; set; }
    
        public string Subject { get; set; }
        public string Description { get; set; }
   
        public string Status { get; set; }
     
        public string CreatedBy { get; set; }
        public int Priority { get; set; }
        public DateTime? DateCreated { get; set; }

      
    }

    public class CreateResolutions
    {

        public int? issueID { get; set; }

        public string ResolutionDesc { get; set; }

        public string status { get; set; }

        public string Resolvedby { get; set; }
        public int ResSeq { get; set; }
        public DateTime? DateResolved { get; set; }


    }

    public class postresponse
    {
        public string responsecode { get; set; }
        public string response { get; set; }

    }

    public class GetAllIssues
    {
 
        public int Id { get; set; }
        public string Category { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }
 
        public string CreatedBy { get; set; }
        public string Priority { get; set; }
        public DateTime? DateCreated { get; set; }

        public string AssinedTo { get; set; }

        public string ResolvedBy { get; set; }
        public DateTime? DateResolved { get; set; }

    }

    public class CountObj
    {
        public int ID { get; set; }
    }

    public class Dashboard
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public int IDNo { get; set; }



    }

    public class GetAllTechnicians
    {

        public int ID { get; set; }
        public string Username { get; set; }

        public int DeptD { get; set; }
        public int IssueRoleID { get; set; }

        public string Role { get; set; }

        public string Dept { get; set; }
        public string Email { get; set; }

        public string Status { get; set; }


    }

    public class GetSingleIssue
    {

        public int Id { get; set; }
        public string Category { get; set; }

        public string Subject { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public string CreatedBy { get; set; }
        public string Priority { get; set; }
        public DateTime? DateCreated { get; set; }

        public string AssinedTo { get; set; }

        public string ResolvedBy { get; set; }

        public string ResolvedDesc { get; set; }

        public DateTime? DateResolved { get; set; }

    }

    public class ReAssignIssue
    {
        public int issueID { get; set; }
        public string OldAssignee { get; set; }

        public string NewAssignee { get; set; }


    }

    public class CreateNewUser
    {
        public string Username { get; set; }
   
        public string Password { get; set; }
        public int? Department { get; set; }
    
        public int? IssueRoleId { get; set; }
      
        public int? RequisionRoleId { get; set; }
    
        public string Email { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }
        public bool IsDefault { get; set; }

    }

    public class GetUserforModification
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public int? Department { get; set; }

        public int? IssueRoleId { get; set; }

        public int? RequisionRoleId { get; set; }

        public string Email { get; set; }

        public DateTime LastLogin { get; set; }

        public string Islocked { get; set; }
       

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }
        public bool IsDefault { get; set; }

    }

}
