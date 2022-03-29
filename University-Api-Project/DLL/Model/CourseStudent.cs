using DLL.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using University_Api.Model;

namespace DLL.Model
{
    public class CourseStudent : ISoftDeletable
    {
        public int CourseId { get; set; }
        public Course Couse { get; set; }
        public int StudentId { get ; set; } 
        public Student Student { get; set;}

        public DateTime DateTime { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; }
    }
}
