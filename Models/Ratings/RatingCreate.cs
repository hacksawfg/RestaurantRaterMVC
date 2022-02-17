using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantRaterMVC.Models.Ratings
{
    public class RatingCreate
    {
        [Required]
        [Display(Name = "Restaurant")]
        public int RestaurantId { get; set; }
        [Required]
        [Range(1,10)]
        public double Score { get; set; }
        public IEnumerable<SelectListItem> RestaurantOptions { get; set; } = new List<SelectListItem>();
    }
}