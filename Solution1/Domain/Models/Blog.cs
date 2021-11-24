using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string LogoImagePath { get; set; }

        [ForeignKey("Category")] //this specifies that the CategoryId is the foreign key representing the reference with name "Category
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } //navigational property because through this property you can access fields in another "table"

    }
}
