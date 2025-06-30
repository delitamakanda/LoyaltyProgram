using System;
using System.Text.RegularExpressions;
using LoyaltyProgram.Domain;
using LoyaltyProgram.Infrastructure;

namespace LoyaltyProgram.Application
{
    public class ClientService
    {
        private readonly LoyaltyDbContext _context;
        public ClientService(LoyaltyDbContext context)
        {
            _context = context;
        }
        public void RegisterClient(Client client)
        {
            if (string.IsNullOrEmpty(client.FirstName) || string.IsNullOrEmpty(client.LastName) ||
                string.IsNullOrEmpty(client.Address) || (string.IsNullOrEmpty(client.Email) && !IsValidEmail(client.Email!)) ||
                string.IsNullOrEmpty(client.PhoneNumber))
            {
                throw new ArgumentException("Invalid client data");
            }
            client.Register(GenerateCardNumber(), LoyaltyCardStatus.Active, client);
            client.DateCreated = DateTime.UtcNow;
            _context.Clients.Add(client);
            _context.SaveChanges();
        }
        public Client? GetClientById(int id)
        {
            return _context.Clients.Find(id);
        }

        public void UpdateClient(Client client)
        {
            _context.Clients.Update(client);
            _context.SaveChanges();
        }
        public void DeleteClient(int id)
        {
            var client = _context.Clients.Find(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }
        }
        public List<Client> GetClients()
        {
            return _context.Clients.ToList();
        }

        static bool IsValidEmail(string email)
        {
            var regex = new Regex(@"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
            return regex.IsMatch(email) ? true : false;
        }
        static string GenerateCardNumber()
        {
            var prefix = new Random().Next(1000, 9999).ToString();
            var uniqueSuffix = Guid.NewGuid().ToString("N").Substring(6, 6);;
            return $"LOYALTY-{prefix}-{uniqueSuffix}";
        }
    }
}
