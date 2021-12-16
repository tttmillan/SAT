using System.ComponentModel.DataAnnotations;//added

namespace SAT.DATA.EF//.Metadata
{
    class ScheduledClassMetadata
    {
        //public int ScheduledClassId { get; set; }
        //public int CourseId { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "* Required")]
        [DataType(DataType.DateTime, ErrorMessage = "* month day year Required")]
        public System.DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "* Required")]
        [DataType(DataType.DateTime, ErrorMessage = "* month day year Required")]
        public System.DateTime EndDate { get; set; }

        [Display(Name = "Instructor Name")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(40, ErrorMessage = "* Must be 40 characters or less")]
        public string InstructorName { get; set; }

        [Required(ErrorMessage = "* Required")]
        [StringLength(20, ErrorMessage = "* Must be 20 characters or less")]
        public string Location { get; set; }
        //public int SCSID { get; set; }
    }

    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass
    {
        public string Summary
        {
            get { return ($"{StartDate:d} {CourseId} {Location}"); }
        }
    }
}
