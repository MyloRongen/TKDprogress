using System.ComponentModel.DataAnnotations;
using TKDprogress_BLL.Models;

namespace TKDprogress.Models.UpdateModels
{
    public class UpdateTulViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }

        public List<Movement>? Movements { get; set; }

        public List<Movement>? MovementsChoices { get; set; }
    }
}
