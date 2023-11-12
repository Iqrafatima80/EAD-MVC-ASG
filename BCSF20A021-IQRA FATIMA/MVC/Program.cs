// See https://aka.ms/new-console-template for more information
#nullable disable
using System;
using System.Data.Entity;
using System.Linq;

Console.WriteLine("Granting rights to user IQRA\\hp");

using (var context = new MyContext())
{
    string sql = @"
        USE MyContext;
        
        GRANT SELECT, INSERT, UPDATE, DELETE ON MyContext.dbo.Students TO [IQRA\hp];
        GRANT SELECT, INSERT, UPDATE, DELETE ON MyContext.dbo.Enrollments TO [IQRA\hp];
        GRANT SELECT, INSERT, UPDATE, DELETE ON MyContext.dbo.Courses TO [IQRA\hp];
    ";

    try
    {
        context.Database.ExecuteSqlCommand(sql);
        Console.WriteLine("Rights granted successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error granting rights: {ex.Message}");
    }

    Console.WriteLine("Adding new students");

    var student = new Student
    {
        FirstMidName = "Atyia",
        LastName = "Alam",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString()),
        Email = "atyia@example.com",
        PhoneNumber = "555-555-5555"
    };

    context.Students.Add(student);

    var student1 = new Student
    {
        FirstMidName = "Ali",
        LastName = "Ahmed",
        EnrollmentDate = DateTime.Parse(DateTime.Today.ToString()),
        Email = "ali@example.com",
        PhoneNumber = "555-555-5556"
    };

    context.Students.Add(student1);
    context.SaveChanges();

    var students = (from s in context.Students
                    orderby s.FirstMidName
                    select s).ToList<Student>();

    Console.WriteLine("Retrieve all Students from the database:");

    foreach (var stdnt in students)
    {
        Console.WriteLine("ID: {0}, Name: {1}, Email: {2}, Phone: {3}, Enrollment Date: {4}",
                          stdnt.ID,
                          stdnt.FirstMidName + " " + stdnt.LastName,
                          stdnt.Email,
                          stdnt.PhoneNumber,
                          stdnt.EnrollmentDate.ToString("yyyy-MM-dd"));
    }

    Console.WriteLine("Press any key to exit...");
    Console.ReadKey();
}
public enum Grade
{
    A, B, C, D, F
}
public class Student
{
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }

    // Additional fields
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; }
}

public class Course
{
    public int CourseID { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }

    // Additional fields
    public string Department { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; }
}

public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    // Additional fields
    public DateTime EnrollmentDate { get; set; }
    public bool IsActive { get; set; }

    public virtual Course Course { get; set; }
    public virtual Student Student { get; set; }
}

public class MyContext : DbContext
{
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Enrollment> Enrollments { get; set; }
    public virtual DbSet<Student> Students { get; set; }
}


