using API.Data;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task<Client?> CreateClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }
        public async Task<Client?> UpdateClientAsync(Client client)
        {
            var existingUser = await _context.Clients.FindAsync(client.Id);
            if (existingUser == null) return null;
            await _context.SaveChangesAsync();
            return existingUser;
        }

    }
}