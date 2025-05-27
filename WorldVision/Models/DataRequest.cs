using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorldVision.Domain.Enums;

namespace WorldVision.Models.Images
{
    public class DataRequest
    {
        public string UserName { get; set; }
        public URole Level { get; set; }

    }
}