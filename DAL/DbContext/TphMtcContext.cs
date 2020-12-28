using DTO.DB.MTC;
using DTO.Enum;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DAL
{
    public class TphMtcContext : System.Data.Entity.DbContext
    {
        public TphMtcContext() : base("TPH_MTC") { }
        public TphMtcContext(DbName db) : base(db.ToString()) { }
        public DbSet<CodeInfo> CodeInfos { get; set; }
        public DbSet<MentalillnessToHospitalRecord> ToHospitalRecords { get; set; }
        public DbSet<EscortUnit> ToHospitalUnits { get; set; }
        public DbSet<PoliceStation> PoliceStations { get; set; }
        public DbSet<SexCode> SexCodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TphMtcContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}