namespace MVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdditionalFieldsToModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Department", c => c.String());
            AddColumn("dbo.Courses", "Description", c => c.String());
            AddColumn("dbo.Courses", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Courses", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Enrollments", "EnrollmentDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Enrollments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Students", "Email", c => c.String());
            AddColumn("dbo.Students", "PhoneNumber", c => c.String());
            AddColumn("dbo.Students", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Address");
            DropColumn("dbo.Students", "PhoneNumber");
            DropColumn("dbo.Students", "Email");
            DropColumn("dbo.Enrollments", "IsActive");
            DropColumn("dbo.Enrollments", "EnrollmentDate");
            DropColumn("dbo.Courses", "EndDate");
            DropColumn("dbo.Courses", "StartDate");
            DropColumn("dbo.Courses", "Description");
            DropColumn("dbo.Courses", "Department");
        }
    }
}
