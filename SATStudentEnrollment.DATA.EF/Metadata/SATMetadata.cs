using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SATStudentEnrollment.DATA.EF//.Metadata
{

    #region Course Metadata
    [MetadataType(typeof(CourseMetadata))]
    public partial class Course { }

    public class CourseMetadata
    {
        //public int CourseId { get; set; }

        [Required(ErrorMessage = "* Name is required")]
        [StringLength(50, ErrorMessage = "* Must not exceed 50 characters")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "* Description is required")]
        [StringLength(500, ErrorMessage = "* Must not exceed 500 characters")]
        [Display(Name = "Description")]
        public string CourseDescription { get; set; }

        [Required(ErrorMessage = "* Credit Hours are required")]
        [Display(Name = "Credit Hours")]
        public byte CreditHours { get; set; }

        [StringLength(250, ErrorMessage = "* Must not exceed 250 characters")]
        public string Curriculum { get; set; }

        [StringLength(500, ErrorMessage = "* Must not exceed 500 characters")]
        public string Notes { get; set; }

        [Display(Name = "Active course?")]
        public bool IsActive { get; set; }
    }
    #endregion

    #region Enrollment
    [MetadataType(typeof(EnrollmentMetadata))]
    public partial class Enrollment { }

    public class EnrollmentMetadata
    {
        //public int EnrollmentId { get; set; }

        [Display(Name = "Student")]
        [Required(ErrorMessage = "* Student is required")]
        public int StudentId { get; set; }

        [Display(Name = "Scheduled Class")]
        [Required(ErrorMessage = "* Scheduled class is required")]
        public int ScheduledClassId { get; set; }

        [Display(Name = "Enrollment Date")]
        [Required(ErrorMessage = "* Enrollment date is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime EnrollmentDate { get; set; }
    }
    #endregion

    #region Scheduled Class
    [MetadataType(typeof(ScheduledClassMetadata))]
    public partial class ScheduledClass
    {
        public string ClassInfo { get { return $"Start Date: {Startdate:d}\nCourse Name: {CourseId}\nLocation: {Location}"; } }
    }

    public class ScheduledClassMetadata
    {
        //public int ScheduledClassId { get; set; }

        [Display(Name = "Course")]
        [Required(ErrorMessage = "* Course is required")]
        public int CourseId { get; set; }

        [Display(Name = "Start Date")]
        [Required(ErrorMessage = "* Start date is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime Startdate { get; set; }

        [Display(Name = "End Date")]
        [Required(ErrorMessage = "* End date is required")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime EndDate { get; set; }

        [Display(Name = "Instructor")]
        [MaxLength(40, ErrorMessage = "* Must not exceed 40 characters")]
        [Required(ErrorMessage = "* Instructor is required")]
        public string InstructorName { get; set; }

        [MaxLength(20, ErrorMessage = "* Must not exceed 20 characters")]
        [Required(ErrorMessage = "* Location is required")]
        public string Location { get; set; }

        [Display(Name = "Class Status")]
        public int SCSID { get; set; }
    }
    #endregion

    #region Scheduled class status
    [MetadataType(typeof(ScheduledClassStatusMetadata))]
    public partial class ScheduledClassStatus { }

    public class ScheduledClassStatusMetadata
    {
        //public int SCSID { get; set; }

        [Display(Name = "Status Name")]
        [Required(ErrorMessage = "* Status Name is required")]
        [MaxLength(50, ErrorMessage = "* Must not exceed 50 characters")]
        public string SCSName { get; set; }
    }
    #endregion

    #region Student Metadata
    [MetadataType(typeof(StudentMetadata))]
    public partial class Student
    {
        public string FullName { get { return FirstName + " " + LastName; } }
    }

    public class StudentMetadata
    {
        //public int StudentId { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(20, ErrorMessage = "* Must not exceed 20 characters")]
        [Required(ErrorMessage = "* First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(20, ErrorMessage = "* Must not exceed 20 characters")]
        [Required(ErrorMessage = "* Last Name is required")]
        public string LastName { get; set; }

        [MaxLength(15, ErrorMessage = "* Must not exceed 15 characters")]
        public string Major { get; set; }

        [MaxLength(50, ErrorMessage = "* Must not exceed 50 characters")]
        public string Address { get; set; }

        [MaxLength(25, ErrorMessage = "* Must not exceed 25 characters")]
        public string City { get; set; }

        [MaxLength(2, ErrorMessage = "* Must not exceed 2 characters")]
        public string State { get; set; }

        [Display(Name = "Zip")]
        [MaxLength(10, ErrorMessage = "* Must not exceed 10 characters")]
        public string ZipCode { get; set; }

        [MaxLength(13, ErrorMessage = "* Must not exceed 13 characters")]
        public string Phone { get; set; }

        [MaxLength(60, ErrorMessage = "* Must not exceed 60 characters")]
        [Required(ErrorMessage = "* Email is required")]
        public string Email { get; set; }

        [Display(Name = "Photo")]
        [MaxLength(100, ErrorMessage = "* Must not exceed 100 characters")]
        public string PhotoUrl { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "* Status is required")]
        public int SSID { get; set; }

    }
    #endregion

    #region Student Statuses
    [MetadataType(typeof(StudentStatusMetadata))]
    public partial class StudentStatus { }

    public class StudentStatusMetadata
    {
        //public int SSID { get; set; }

        [Required(ErrorMessage ="* Name is required")]
        [MaxLength(30, ErrorMessage ="* Must not exceed 30 characters")]
        [Display(Name ="Student Status")]
        public string SSName { get; set; }

        [MaxLength(250, ErrorMessage ="* Must not exceed 250 characters")]
        [Display(Name ="Status Description")]
        public string SSDescription { get; set; }
    }

    #endregion
}
