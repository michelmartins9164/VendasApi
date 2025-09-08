using API.DTO;
using API.Model;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public class ClientService
    {
        private readonly ClientRepository _repository;

        public ClientService(ClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Client>> GetClientsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Client?> GetByClientIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Client?> CreateClientAsync(Client dto)
        {
            return await _repository.CreateClientAsync(dto);
        }

        public async Task<Client?> InactivateClientAsync(int id, bool status)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) return null;

            client.Ativo = status;
            return await _repository.UpdateClientAsync(client);
        }

        public async Task<Client?> UpdateClientAsync(int id, Client client)
        {
            var FindedClient = await _repository.GetByIdAsync(id);
            if (FindedClient == null) return null;

            return await _repository.UpdateClientAsync(client);
        }
    }
}