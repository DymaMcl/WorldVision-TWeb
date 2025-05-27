using System.Data.Entity;
using WorldVision.Domain.Entities.User;

namespace WorldVision.BussinesLogic.DBModel
{
    class UserContext : DbContext
    {
        public UserContext() :
            base("name=dimaBase") // connectionstring name define in  web.config
        {
        }

        public virtual DbSet<UDbTable> Users { get; set; }
    }
}
