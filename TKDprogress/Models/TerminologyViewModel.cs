﻿namespace TKDprogress.Models
{
    public class TerminologyViewModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Meaning { get; set; }

        public int CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<CategoryViewModel> Categories { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
