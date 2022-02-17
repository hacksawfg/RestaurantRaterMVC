using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantRaterMVC.Data;
using RestaurantRaterMVC.Models;
using RestaurantRaterMVC.Models.Ratings;

namespace RestaurantRaterMVC.Controllers
{
    public class RatingController : Controller
    {
        private RestaurantDbContext _context;
        public RatingController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET Method
        public async Task<IActionResult> Index()
        {
            IEnumerable<RatingListItem> ratings = await _context.Rating
                .Select(r => new RatingListItem()
                {
                    RestaurantName = r.Restaurant.Name,
                    Score = r.Score
                }).ToListAsync();
            
            return View(ratings);
        }

        public async Task<IActionResult> Restaurant(int id)
        {
            IEnumerable<RatingListItem> ratings = await _context.Rating
            .Where(r => r.RestaurantId == id)
            .Select(r => new RatingListItem()
            {
                RestaurantName = r.Restaurant.Name,
                Score = r.Score
            }).ToListAsync();

            Restaurant restaurant = await _context.Restaurants.FindAsync(id);
            ViewBag.RestaurantName = restaurant.Name; // Need info on ViewBag

            return View(ratings);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<SelectListItem> restaurantOptions = await _context.Restaurants.Select(r => new SelectListItem()
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToListAsync();

            RatingCreate model = new RatingCreate();
            model.RestaurantOptions = restaurantOptions;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RatingCreate model)
        {
            if (!ModelState.IsValid)
                return View();
            
            Rating rating = new Rating()
            {
                RestaurantId = model.RestaurantId,
                Score = model.Score
            };

            _context.Rating.Add(rating);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}