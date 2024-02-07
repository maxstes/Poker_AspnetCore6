using Microsoft.EntityFrameworkCore;
using Poker.Data;
using Poker.Data.Entities;
using Poker.Models.Game;

namespace Poker.Services
{
    public class BankService
    {
        readonly ApplicationDbContext context;

        public BankService(ApplicationDbContext _context) 
        {
            context = _context;
        }
        public async Task SetMoney(Bank bank)
        {
            await context.Banks.AddAsync(bank);
            context.SaveChanges();
        }
        public async Task<int> GetPrize(int IdWinner,int idRoom)
        {
            int Sum = await GetSumPrize(idRoom);
            await SendBankWinner(Sum, IdWinner);
            return Sum;
        }
        private async Task SendBankWinner(int Sum,int idWinner)
        {
            var old = await context.Player 
                .Where(x => x.Id ==  idWinner)
                .FirstOrDefaultAsync();

            old.Balance += Sum;

            context.Player.Update(old);
        }
        private async Task<int> GetSumPrize(int idRoom)
        {
            var result = await context.Banks
                .Where(bank => bank.IdRoom == idRoom)
                .SumAsync(x => x.Money);
            return result;
        }
    }
}
