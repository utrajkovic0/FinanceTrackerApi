using System.ComponentModel.DataAnnotations;

namespace FinanceTrackerApi.DTOs
{
    public class CreateCategoryDto
    {
        [Required] public string Name { get; set; }

        [Required] public string Type { get; set; }
    }
}
