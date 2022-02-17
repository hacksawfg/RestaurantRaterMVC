using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Models.Ratings
{
    public class RatingListItem
    {
        public int Id { get; set; }
        [Display(Name = "Restaurant")]
        public string RestaurantName { get; set; }
        [Display(Name = "Rating")]
        public double Score { get; set; }
    }
}