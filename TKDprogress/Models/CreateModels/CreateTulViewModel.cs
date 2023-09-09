using System.ComponentModel.DataAnnotations;
using TKDprogress_SL.Entities;

namespace TKDprogress.Models.CreateModels
{
    public class CreateTulViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }

        public List<MovementDto>? Movements { get; set; }
    }
}
