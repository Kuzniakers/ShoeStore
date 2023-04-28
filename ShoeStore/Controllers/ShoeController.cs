using Microsoft.AspNetCore.Mvc;
using ShoeStore.Data;
using ShoeStore.Models;
using System.Collections.Generic;


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
            var shoes = new List<Shoe>
            {
                new Shoe { Id = 1, Name = "But sportowy", Description = "Wygodny but sportowy", Price = 200, Size = 42 },
                new Shoe { Id = 2, Name = "Elegancki but", Description = "Elegancki but na specjalne okazje", Price = 300, Size = 45 }
            };

            return View(shoes);
        }
        // Metoda GET do wyświetlania formularza tworzenia nowego produktu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Metoda POST do przesłania danych z formularza tworzenia nowego produktu
        [HttpPost]
        public IActionResult Create(Shoe shoe)
        {
            if (ModelState.IsValid)
            {
                _context.Shoes.Add(shoe);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoe);
        }

        // Metoda POST do usuwania istniejącego produktu
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var shoe = _context.Shoes.Find(id);

            if (shoe != null)
            {
                _context.Shoes.Remove(shoe);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
