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
                    Id = r.Id,
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

        public async Task<IActionResult> Delete(int id)
        {
            Rating rating = await _context.Rating
            .Include(rest => rest.Restaurant) 
            .FirstOrDefaultAsync(rest => rest.Id == id);
            
            if (rating is null)
                return RedirectToAction(nameof(Index));
            
            RatingListItem ratingDelete = new RatingListItem()
            {
                Id = rating.Id,
                RestaurantName = rating.Restaurant.Name,
                Score = rating.Score
            };

            return View(ratingDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, RatingListItem model)
        {
            Rating rating = await _context.Rating.FindAsync(id);
            if (rating is null)
                return RedirectToAction(nameof(Index));
            
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



    }
}