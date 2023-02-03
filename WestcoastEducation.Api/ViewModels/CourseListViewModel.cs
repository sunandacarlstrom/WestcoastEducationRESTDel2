using WestcoastEducation.Api.Models;

namespace WestcoastEducation.Api.ViewModels;

public class CourseListViewModel
{
    public int Id { get; set; }
    // byter ut int TeacherId
    public string Teacher { get; set; } = "";
    public string? Number { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; }
}
