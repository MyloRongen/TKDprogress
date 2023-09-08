namespace TKDprogress.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<TerminologyViewModel> Terminologies { get; set; }
    }
}
