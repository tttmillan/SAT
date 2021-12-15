using System.ComponentModel.DataAnnotations;//added

namespace SAT.DATA.EF//.Metadata
{
    public class CourseMetadata
    {
        //public int CourseId { get; set; }

        [Display(Name ="Course")]
        [Required(ErrorMessage ="* Required")]
        [StringLength(50, ErrorMessage ="* Must be 50 characters or less")]
        public string CourseName { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "* Required")]
        [UIHint("MultilineText")]
        public string CourseDescription { get; set; }

        [Display(Name = "Credit Hours")]
        [Required(ErrorMessage = "* Required")]
        [Range(0,15, ErrorMessage ="* Must be between 0 and 15")]
        public byte CreditHours { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        [StringLength(250, ErrorMessage = "* Must be 250 characters or less")]
        public string Curriculum { get; set; }

        [DisplayFormat(NullDisplayText = "No additional information.")]
        [StringLength(500, ErrorMessage = "* Must be 500 characters or less")]
        [UIHint("MultilineText")]
        public string Notes { get; set; }

        [Display(Name = "Active?")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(CourseMetadata))]
    public partial class Course { }

}
