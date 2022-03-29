using DLL.Model;
using DLL.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_Api.Model
{
    public class Student:ISoftDeletable
    {
        [Key]
        public int StudentId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
