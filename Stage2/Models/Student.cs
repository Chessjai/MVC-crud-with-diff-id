using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Stage2.Models
{
    [Table("student")]
    public class Student
    {
        [Key]
        public string studentid { get; set; }

        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Invalid name")]
        [Required(ErrorMessage = "name not be empty")]
        [MaxLength(50)]
     
        public string name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = " {0:MM/dd/yyyy }", ApplyFormatInEditMode =true)]
        [DateTimeNow]
        [Required(ErrorMessage = " Date Of Birth  Required")]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedOn { get; set; }


    }
}
