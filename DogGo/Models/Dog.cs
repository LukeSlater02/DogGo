using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [DisplayName("Owner")]
        public int OwnerId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Breed { get; set; }
        public string Notes { get; set; }
        
        [Url]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }
    }
}
