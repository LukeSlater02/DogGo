using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogGo.Models
{
    public class Walker
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId {  get; set; }

        [Url]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }
        public Neighborhood Neighborhood { get; set; }
    }
}
