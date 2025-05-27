using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldVision.Domain.Entities.User
{
    public class ULoginResp
    {
        public bool Status { get; set; }
        public string StatusMsg { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }
    }

    public class UserLoginAction
    {
        public ULoginResp PerformLogin()
        {
            var result = new
            {
                Level = "Admin", // Example value
                Id = 123 // Example value
            };

            return new ULoginResp
            {
                Status = true,
                Role = result.Level.ToString(),
                UserId = result.Id
            };
        }
    }
}
