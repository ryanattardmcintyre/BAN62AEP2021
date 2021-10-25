using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Models
{
    public class Category
    {
        [Key] //primarykey + identity
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
