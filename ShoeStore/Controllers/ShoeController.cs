using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using ShoeStore.Data;

namespace ShoeStore.Controllers
{
    public class ShoeController : Controller
    {
        private readonly ShoeStoreContext _context;

        public ShoeController(ShoeStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (!_context.Shoes.Any())
            {
                var shoes = new List<Shoe>
        {
            new Shoe { Name = "But sportowy", Description = "Wygodny but sportowy", Price = 200, Size = 42 },
            new Shoe { Name = "Elegancki but", Description = "Elegancki but na specjalne okazje", Price = 300, Size = 45 }
        };

                foreach (var shoe in shoes)
                {
                    _context.Add(shoe);
                }

                _context.SaveChanges();
            }

            var shoesToDisplay = _context.Shoes.ToList();
            return View(shoesToDisplay);
        }



        [HttpGet]
        public IActionResult Cart()
        {
            return View();
        }


        [HttpPost]
        [Route("api/shoes/addtocart/{id}")]
        public JsonResult AddToCart(int id)
        {
            var shoe = _context.Shoes.Find(id);

            if (shoe != null)
            {
                var cartItemsJson = HttpContext.Session.GetString("Cart");
                var cartItems = string.IsNullOrEmpty(cartItemsJson) ? new List<Shoe>() : JsonSerializer.Deserialize<List<Shoe>>(cartItemsJson);
                cartItems.Add(shoe);
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

    }
}
