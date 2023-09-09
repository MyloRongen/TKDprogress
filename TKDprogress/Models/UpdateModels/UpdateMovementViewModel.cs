using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TKDprogress.Models.UpdateModels
{
    public class UpdateMovementViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        public string? ImageUrl { get; set; }

        [NotMapped] 
        public IFormFile? Image { get; set; }
    }
}
