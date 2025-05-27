using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldVision.Domain.Entities.User
{


    public class ULoginData
    {
        public string Credetial { get; set; }
        public string Password { get; set; }
        public string LoginIp { get; set; }
        public DateTime LoginDataTime { get; set; }

    }
}
