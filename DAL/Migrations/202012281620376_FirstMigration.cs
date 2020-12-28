namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodeInfo",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Type = c.String(nullable: false, maxLength: 20),
                    Code = c.String(nullable: false, maxLength: 10),
                    Desc = c.String(maxLength: 20),
                    Content = c.String(maxLength: 200),
                    Order = c.Decimal(precision: 18, scale: 2),
                    IsDelete = c.Boolean(nullable: false),
                    DeleteTime = c.DateTime(),
                })
                .PrimaryKey(t => new { t.Type, t.Code });

            CreateTable(
                "dbo.PoliceStation",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    Zip = c.String(maxLength: 10),
                    Address = c.String(maxLength: 200),
                    Tel = c.String(maxLength: 20),
                    IsDeleted = c.Boolean(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                    UpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.SexCode",
                c => new
                {
                    Code = c.Int(nullable: false, identity: true),
                    DisplayName = c.String(maxLength: 10),
                    Memo = c.String(maxLength: 255),
                    IsDelete = c.Boolean(nullable: false),
                    DeleteTime = c.DateTime(),
                })
                .PrimaryKey(t => t.Code);

            CreateTable(
                "dbo.ToHospitalRecords",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    WorkTime = c.DateTime(nullable: false),
                    Sources = c.String(nullable: false, maxLength: 100),
                    SourceName = c.String(maxLength: 50),
                    SourcePhoneNumber = c.String(maxLength: 15),
                    CaseName = c.String(nullable: false, maxLength: 50),
                    CaseIdentityNumber = c.String(nullable: false, maxLength: 20),
                    CaseSex = c.String(nullable: false, maxLength: 1),
                    CaseBirthday = c.DateTime(),
                    CasePhoneNumber = c.String(nullable: false, maxLength: 15),
                    CaseAddress = c.String(nullable: false, maxLength: 200),
                    LocationArea = c.String(nullable: false, maxLength: 50),
                    Location = c.String(nullable: false, maxLength: 200),
                    AttenderName = c.String(nullable: false, maxLength: 50),
                    AttenderPhoneNumber = c.String(nullable: false, maxLength: 15),
                    Relationship = c.String(nullable: false, maxLength: 10),
                    EscortMain = c.String(nullable: false, maxLength: 100),
                    EscortSup = c.String(maxLength: 50),
                    SupportContent = c.String(maxLength: 20),
                    InPerson = c.Boolean(nullable: false),
                    IsPhoneContact = c.Boolean(),
                    IsTrauma = c.Boolean(),
                    EmergencyAgencyId = c.String(maxLength: 20),
                    PsyHistory = c.Int(nullable: false),
                    Diagnose = c.String(maxLength: 30),
                    DiagnoseOther = c.String(maxLength: 30),
                    Symptoms = c.String(maxLength: 100),
                    SymptomOther = c.String(maxLength: 50),
                    Hospital = c.String(nullable: false, maxLength: 50),
                    MedicalPersonImage = c.Guid(nullable: false),
                    Comment = c.String(maxLength: 500),
                    FormImage = c.Guid(),
                    IntfDate = c.DateTime(nullable: false),
                    CreateIp = c.String(maxLength: 50),
                    UpdateIp = c.String(maxLength: 50),
                    CreatorId = c.Int(nullable: false),
                    CreateDate = c.DateTime(nullable: false),
                    UpdaterId = c.Int(nullable: false),
                    UpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ToHospitalUnits",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    HeaderId = c.Int(nullable: false),
                    Type = c.Int(nullable: false),
                    Code = c.String(maxLength: 20),
                    Name = c.String(nullable: false, maxLength: 20),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ToHospitalRecords", t => t.HeaderId, cascadeDelete: true)
                .Index(t => t.HeaderId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.ToHospitalUnits", "HeaderId", "dbo.ToHospitalRecords");
            DropIndex("dbo.ToHospitalUnits", new[] { "HeaderId" });
            DropTable("dbo.ToHospitalUnits");
            DropTable("dbo.ToHospitalRecords");
            DropTable("dbo.SexCode");
            DropTable("dbo.PoliceStation");
            DropTable("dbo.CodeInfo");
        }
    }
}
