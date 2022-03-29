using DLL.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace University_Api.Model
{
    public class Department : ISoftDeletable
    {
        [Key]
        public int DepartmentId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(30)")]
        public string Code { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get;  set; }

        public ICollection<Student> Students { get; set; }
    }
}
