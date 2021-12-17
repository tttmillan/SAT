using System.ComponentModel.DataAnnotations;//added

namespace SAT.DATA.EF//.Metadata
{
    class StudentMetadata
    {

        //public int StudentId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(20, ErrorMessage = "* Must be 20 characters or less")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "* Required")]
        [StringLength(20, ErrorMessage = "* Must be 20 characters or less")]
        public string LastName { get; set; }
        
        
        [StringLength(15, ErrorMessage = "* Must be 15 characters or less")]
        [DisplayFormat(NullDisplayText = "Undecided")]
        public string Major { get; set; }
        
        
        [StringLength(50, ErrorMessage = "* Must be 50 characters or less")]
        [DisplayFormat(NullDisplayText = "Not Provided")]
        public string Address { get; set; }

        [StringLength(25, ErrorMessage = "* Must be 25 characters or less")]
        [DisplayFormat(NullDisplayText = "Not Provided")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "* Must be 2 characters or less")]
        [DisplayFormat(NullDisplayText = "Not Provided")]
        public string State { get; set; }

        [StringLength(10, ErrorMessage = "* Must be 10 characters or less")]
        [DisplayFormat(NullDisplayText = "Not Provided")]
        public string ZipCode { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(13, ErrorMessage = "* Must be 13 characters or less")]
        [DisplayFormat(NullDisplayText = "Not Provided")]
        public string Phone { get; set; }

        
        [Required(ErrorMessage = "* Required")]
        [DataType(DataType.EmailAddress, ErrorMessage ="* Enter valid Format")]
        [StringLength(60, ErrorMessage = "* Must be 60 characters or less")]
        public string Email { get; set; }

        [Display(Name = "Photo")]        
        [StringLength(60, ErrorMessage = "* Must be 60 characters or less")]
        [DisplayFormat(NullDisplayText = "Not Provided")]
        public string PhotoUrl { get; set; }


        //public int SSID { get; set; }
    }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student { }

}
