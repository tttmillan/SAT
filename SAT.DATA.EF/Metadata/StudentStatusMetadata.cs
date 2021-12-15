using System.ComponentModel.DataAnnotations;//added

namespace SAT.DATA.EF//.Metadata
{
    class StudentStatusMetadata
    {
        //public int SSID { get; set; }

        [Display(Name = "Student Status")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(30, ErrorMessage = "* Must be 30 characters or less")]
        public string SSName { get; set; }

        [Display(Name = "Student Status Description")]        
        [StringLength(250, ErrorMessage = "* Must be 250 characters or less")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string SSDescription { get; set; }
    }

    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }

}
