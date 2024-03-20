using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ada.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public string? Instructor { get; set; }
        [Display(Name = "Price per person for 1 to 3 attendees")]
        public double? Price { get; set; }
        [Display(Name = "Price per person for 4 or more attendees")]
        public double? Price4 { get; set; }
        [Display(Name = "Price for a private class with a maximum of 10 attendees")]
        public double? PricePrivate { get; set; }
        public bool? Online { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

    }
}
