using System.ComponentModel.DataAnnotations;

namespace BoatApi.Models
{
    public class Boat : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}