using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeakingInBitsWeb.Models;

/// <summary>
/// Represents a course a student can enroll in.
/// </summary>
public class Course
{
    /// <summary>
    /// Unique identifier for the course.
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// The student facing title of the course.
    /// </summary>
    /// <remarks>The title is limited to a maximum of 100 characters. Assigning a value longer than this limit
    /// will result in a validation error.</remarks>
    [StringLength(100)]
    public required string Title { get; set; }

    /// <summary>
    /// Short unique code for the course, e.g. "CPW230". Course codes
    /// start with at least one letter (can be up to 5) ending with 3 digits.
    /// </summary>
    [RegularExpression(@"^[A-Za-z]+[0-9]{3}$", ErrorMessage = "Course code must start with letters and end with 3 digits.")]
    [StringLength(8)]
    public string? CourseCode { get; set; }

    /// <summary>
    /// User facing description of the course.
    /// </summary>
    [StringLength(300)]
    public string? Description { get; set; }
}
