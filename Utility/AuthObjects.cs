using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PZLServiceDesk.Utility
{
    public class GuardResponse
    {
        public string username { get; set; }
        public string Role { get; set; }
        public string Dept { get; set; }

        public int? DeptID { get; set; }

        public string rescode { get; set; }
        public string Token { get; set; }
        public int RoleId { get; set; }


    }


    public class UserQuery
    {

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Dept { get; set; }

        public int? DeptD { get; set; }

        public int IssueRoleID { get; set; }

        public string Email { get; set; }
        public DateTime? LastLogin { get; set; }

        public string SessionId { get; set; }
        public DateTime? SessionTime { get; set; }
        public DateTime DateCreated { get; set; }

        public string Status { get; set; }
    }

}
