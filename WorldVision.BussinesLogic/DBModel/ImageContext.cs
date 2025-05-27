using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVision.Domain.Entities.Images;

namespace WorldVision.BusinessLogic.DBModel
{
    public class ImageContext : DbContext
    {
        public ImageContext() : base("name = dimaBase")
        {
        }

        public virtual DbSet<IDbTable> Images { get; set; }
    }
}
