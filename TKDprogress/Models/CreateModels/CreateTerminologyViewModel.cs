using System.ComponentModel.DataAnnotations;

namespace TKDprogress.Models.CreateModels
{
    public class CreateTerminologyViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Word is required.")]
        [MaxLength(255, ErrorMessage = "Word cannot exceed 255 characters.")]
        public string Word { get; set; }

        [Required(ErrorMessage = "Meaning is required.")]
        [MaxLength(255, ErrorMessage = "Meaning cannot exceed 255 characters.")]
        public string Meaning { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}
