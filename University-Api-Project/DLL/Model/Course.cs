using DLL.Model.Interfaces;
using System;
using System.Collections.Generic;

namespace DLL.Model
{
    public class Course : ISoftDeletable
    {
        public int CourseId { get; set; }
        public string Name { get; set; }    
        public string Code { get; set; }
        public decimal Credit { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; }

        public ICollection<CourseStudent> CourseStudents { get; set; }
    }
}
