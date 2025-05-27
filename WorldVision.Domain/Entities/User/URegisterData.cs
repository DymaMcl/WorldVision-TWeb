using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Domain.Enums;

namespace WorldVision.Domain.Entities.User
{
    public class URegisterData

    {
        public string Email { get; set; }
        public string Credential { get; set; }
        public string Password { get; set; }
        public string LoginIp { get; set; }
        public DateTime LoginDataTime { get; set; }

        public URole Level { get; set; }
    }
}
