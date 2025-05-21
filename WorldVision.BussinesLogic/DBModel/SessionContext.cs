using System.Data.Entity;
using WorldVision.Domain.Entities.User;

namespace WorldVision.BussinesLogic.DBModel
{
    public class SessionContext : DbContext
    {
        public SessionContext() : base("name=base")
        {
        }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}
