using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ShoeStore.Data;
using ShoeStore.Models;
using System.Text.Json;

public class CartController : Controller
{
    private readonly ShoeStoreContext _context;

    public CartController(ShoeStoreContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var cartItemsJson = HttpContext.Session.GetString("Cart");
        var cartItems = string.IsNullOrEmpty(cartItemsJson) ? new List<Shoe>() : JsonSerializer.Deserialize<List<Shoe>>(cartItemsJson);

        Console.WriteLine($"Cart has {cartItems.Count} items.");

        return View(cartItems);
    }


    public IActionResult AddToCart(int id)
    {
        var shoe = _context.Shoes.Find(id);

        if (shoe != null)
        {
            Console.WriteLine($"Product with id {id} found: {shoe.Name}");

            var cartItemsJson = HttpContext.Session.GetString("Cart");
            var cartItems = string.IsNullOrEmpty(cartItemsJson) ? new List<Shoe>() : JsonSerializer.Deserialize<List<Shoe>>(cartItemsJson);
            cartItems.Add(shoe);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));

            Console.WriteLine($"Product with id {id} added to cart. Cart now has {cartItems.Count} items.");
        }
        else
        {
            Console.WriteLine($"Product with id {id} not found.");
        }

        return RedirectToAction("Index", "Cart");
    }

    public IActionResult RemoveFromCart(int id)
    {
        var cartItemsJson = HttpContext.Session.GetString("Cart");
        if (!string.IsNullOrEmpty(cartItemsJson))
        {
            var cartItems = JsonSerializer.Deserialize<List<Shoe>>(cartItemsJson);
            var itemToRemove = cartItems.FirstOrDefault(s => s.Id == id);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);
                HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cartItems));
            }
        }

        return RedirectToAction("Index", "Cart");
    }



}
