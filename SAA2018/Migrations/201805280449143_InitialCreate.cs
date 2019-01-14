namespace SAA2018.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        EmailPassword = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SurName = c.String(),
                        MidleName = c.String(),
                        PhoneNumber = c.String(),
                        AccountId = c.Int(),
                        UniversityId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId)
                .ForeignKey("dbo.Universities", t => t.UniversityId)
                .Index(t => t.AccountId)
                .Index(t => t.UniversityId);
            
            CreateTable(
                "dbo.Faculties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UniversityId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Universities", t => t.UniversityId)
                .Index(t => t.UniversityId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Course = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        UniversityId = c.Int(),
                        FacultyId = c.Int(),
                        SpecialtyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId)
                .ForeignKey("dbo.Specialties", t => t.SpecialtyId)
                .ForeignKey("dbo.Universities", t => t.UniversityId)
                .Index(t => t.UniversityId)
                .Index(t => t.FacultyId)
                .Index(t => t.SpecialtyId);
            
            CreateTable(
                "dbo.Specialties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UniversityId = c.Int(),
                        FacultyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId)
                .ForeignKey("dbo.Universities", t => t.UniversityId)
                .Index(t => t.UniversityId)
                .Index(t => t.FacultyId);
            
            CreateTable(
                "dbo.Universities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TeacherGroups",
                c => new
                    {
                        Teacher_Id = c.Int(nullable: false),
                        Group_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_Id, t.Group_Id })
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Deans",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        University_Id = c.Int(),
                        FacultyId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .ForeignKey("dbo.Universities", t => t.University_Id)
                .ForeignKey("dbo.Faculties", t => t.FacultyId)
                .Index(t => t.Id)
                .Index(t => t.University_Id)
                .Index(t => t.FacultyId);
            
            CreateTable(
                "dbo.Rectors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        University_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .ForeignKey("dbo.Universities", t => t.University_Id)
                .Index(t => t.Id)
                .Index(t => t.University_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        University_Id = c.Int(),
                        Specialty_Id = c.Int(),
                        Faculty_Id = c.Int(),
                        CardNumber = c.String(),
                        GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .ForeignKey("dbo.Universities", t => t.University_Id)
                .ForeignKey("dbo.Specialties", t => t.Specialty_Id)
                .ForeignKey("dbo.Faculties", t => t.Faculty_Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.Id)
                .Index(t => t.University_Id)
                .Index(t => t.Specialty_Id)
                .Index(t => t.Faculty_Id)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        University_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .ForeignKey("dbo.Universities", t => t.University_Id)
                .Index(t => t.Id)
                .Index(t => t.University_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "University_Id", "dbo.Universities");
            DropForeignKey("dbo.Teachers", "Id", "dbo.People");
            DropForeignKey("dbo.Students", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Students", "Faculty_Id", "dbo.Faculties");
            DropForeignKey("dbo.Students", "Specialty_Id", "dbo.Specialties");
            DropForeignKey("dbo.Students", "University_Id", "dbo.Universities");
            DropForeignKey("dbo.Students", "Id", "dbo.People");
            DropForeignKey("dbo.Rectors", "University_Id", "dbo.Universities");
            DropForeignKey("dbo.Rectors", "Id", "dbo.People");
            DropForeignKey("dbo.Deans", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Deans", "University_Id", "dbo.Universities");
            DropForeignKey("dbo.Deans", "Id", "dbo.People");
            DropForeignKey("dbo.People", "UniversityId", "dbo.Universities");
            DropForeignKey("dbo.People", "AccountId", "dbo.Accounts");
            DropForeignKey("dbo.TeacherGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.TeacherGroups", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Specialties", "UniversityId", "dbo.Universities");
            DropForeignKey("dbo.Groups", "UniversityId", "dbo.Universities");
            DropForeignKey("dbo.Faculties", "UniversityId", "dbo.Universities");
            DropForeignKey("dbo.Groups", "SpecialtyId", "dbo.Specialties");
            DropForeignKey("dbo.Specialties", "FacultyId", "dbo.Faculties");
            DropForeignKey("dbo.Groups", "FacultyId", "dbo.Faculties");
            DropIndex("dbo.Teachers", new[] { "University_Id" });
            DropIndex("dbo.Teachers", new[] { "Id" });
            DropIndex("dbo.Students", new[] { "GroupId" });
            DropIndex("dbo.Students", new[] { "Faculty_Id" });
            DropIndex("dbo.Students", new[] { "Specialty_Id" });
            DropIndex("dbo.Students", new[] { "University_Id" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.Rectors", new[] { "University_Id" });
            DropIndex("dbo.Rectors", new[] { "Id" });
            DropIndex("dbo.Deans", new[] { "FacultyId" });
            DropIndex("dbo.Deans", new[] { "University_Id" });
            DropIndex("dbo.Deans", new[] { "Id" });
            DropIndex("dbo.TeacherGroups", new[] { "Group_Id" });
            DropIndex("dbo.TeacherGroups", new[] { "Teacher_Id" });
            DropIndex("dbo.Specialties", new[] { "FacultyId" });
            DropIndex("dbo.Specialties", new[] { "UniversityId" });
            DropIndex("dbo.Groups", new[] { "SpecialtyId" });
            DropIndex("dbo.Groups", new[] { "FacultyId" });
            DropIndex("dbo.Groups", new[] { "UniversityId" });
            DropIndex("dbo.Faculties", new[] { "UniversityId" });
            DropIndex("dbo.People", new[] { "UniversityId" });
            DropIndex("dbo.People", new[] { "AccountId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.Rectors");
            DropTable("dbo.Deans");
            DropTable("dbo.TeacherGroups");
            DropTable("dbo.Universities");
            DropTable("dbo.Specialties");
            DropTable("dbo.Groups");
            DropTable("dbo.Faculties");
            DropTable("dbo.People");
            DropTable("dbo.Accounts");
        }
    }
}
