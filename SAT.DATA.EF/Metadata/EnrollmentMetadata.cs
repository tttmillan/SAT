using System.ComponentModel.DataAnnotations;//added

namespace SAT.DATA.EF//.Metadata
{
    class EnrollmentMetadata
    {

        //public int EnrollmentId { get; set; }
               
        //public int StudentId { get; set; }
      
        //public int ScheduledClassId { get; set; }

        [Display(Name = "Enrollment Date")]
        [Required(ErrorMessage = "* Required")]   
        [DataType(DataType.DateTime, ErrorMessage ="* month day year Required")]
        public System.DateTime EnrollmentDate { get; set; }
    }

    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }

}
