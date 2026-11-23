using FinanceTrackerApi.Data;
using FinanceTrackerApi.DTOs;
using FinanceTrackerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTrackerApi.Services
{
    public class TransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsAsync(int userId)
        {
            return await _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id, int userId)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> UpdateTransactionAsync(int id, int userId, Transaction updated)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (transaction == null) return false;

            transaction.Amount = updated.Amount;
            transaction.Description = updated.Description;
            transaction.Date = updated.Date;
            transaction.CategoryId = updated.CategoryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTransactionAsync(int id, int userId)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<SummaryDto> GetSummaryAsync(int userId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .ToListAsync();

            double income = transactions
                .Where(t => t.Category.Type == "Income")
                .Sum(t => t.Amount);

            double expense = transactions
                .Where(t => t.Category.Type == "Expense")
                .Sum(t => t.Amount);

            double balance = income - expense;

            SummaryDto summary = new()
            {
                TotalExpense = expense,
                TotalIncome = income,
                Balance = balance
            };

            return summary;

        }

    }
}
