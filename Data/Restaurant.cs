using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Data
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        public double Score
        {
            get
            {
                return Rating.Count > 0 ? Rating.Select(r => r.Score).Sum() / Rating.Count : 0;
            }
        }
        public virtual List<Rating> Rating { get; set; } = new List<Rating>();
    }
}