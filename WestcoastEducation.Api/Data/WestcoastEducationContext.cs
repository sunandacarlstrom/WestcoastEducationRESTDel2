using Microsoft.EntityFrameworkCore;
using WestcoastEducation.Api.Models;

namespace WestcoastEducation.Api.Data;
public class WestcoastEducationContext : DbContext
{
    public DbSet<CourseModel> Courses => Set<CourseModel>();
    public DbSet<StudentModel> Students => Set<StudentModel>();
    public DbSet<TeacherModel> Teachers => Set<TeacherModel>();
    public DbSet<TeacherSkillsModel> TeacherSkills => Set<TeacherSkillsModel>();

    public WestcoastEducationContext(DbContextOptions options) : base(options)
    {
    }
}
