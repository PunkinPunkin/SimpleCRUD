using DTO.DB.MTC;
using DTO.Enum;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL
{
    public class TphMtcFileContext : System.Data.Entity.DbContext
    {
        public TphMtcFileContext() : base("TPH_MTC_F") { }
        public TphMtcFileContext(DbName db) : base(db.ToString()) { }
        public DbSet<Document> DocumentRepository { get; set; } 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TphMtcFileContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
