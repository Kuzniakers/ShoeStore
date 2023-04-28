using System.ComponentModel.DataAnnotations;

namespace ShoeStore.Models
{
    public class Shoe
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Rozmiar")]
        public int Size { get; set; }
    }
}
