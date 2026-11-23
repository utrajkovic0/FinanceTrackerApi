using FinanceTrackerApi.Models;
using System.ComponentModel.DataAnnotations;

namespace FinanceTrackerApi.DTOs
{
    public class CreateTransactionDto
    {
        [Range(0, int.MaxValue)]
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

    }
}
