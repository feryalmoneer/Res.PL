using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Re.PL.Areas.Dashboard.ViewModels
{
    public class ItemVMForm
    {
        public int Id { get; set; }

        public IFormFile Image { get; set; }

        public string? NameOfImg { get; set; }

        [Required(ErrorMessage = "Please select a portfolio.")]
        public int? PortfoliosId { get; set; }

        public SelectList? Portfolios { get; set; }  // Will be populated in the controller
    }
}
