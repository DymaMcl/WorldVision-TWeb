using System.ComponentModel.DataAnnotations;

namespace WorldVision.Web.Models
{
    public class UserLogin
    {
        public string Credential { get; set; }
        public string Password { get; set; }
    }
}