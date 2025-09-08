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

        public async Task<Client?> CreateClientAsync(CreateClientDTO dto)
        {
            var newClient = new Client
            {
                Empresa = dto.Empresa,
                NomeCompleto = dto.NomeCompleto,
                Ativo = dto.Ativo,
                EnderecoEntrega = dto.EnderecoEntrega,
                Cidade = dto.Cidade,
                Telefone = dto.Telefone,
                Observacao = dto.Observacao
            };

            return await _repository.CreateClientAsync(newClient);
        }

        public async Task<Client?> InactivateClientAsync(int id, bool status)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) return null;

            client.Ativo = status;
            return await _repository.UpdateClientAsync(client);
        }

        public async Task<Client?> UpdateClientAsync(int id, CreateClientDTO client)
        {
            var FindedClient = await _repository.GetByIdAsync(id);
            if (FindedClient == null) return null;
            var ClientUpdate = new Client
            {
                Id = id,
                Empresa = client.Empresa,
                NomeCompleto = client.NomeCompleto,
                Ativo = client.Ativo,
                EnderecoEntrega = client.EnderecoEntrega,
                Cidade = client.Cidade,
                Telefone = client.Telefone,
                Observacao = client.Observacao,
                DataAlteracao = DateTime.Now
            }; 
            return await _repository.UpdateClientAsync(ClientUpdate);
        }

        public async Task<Client?> DeleteClientAsync(int id)
        {
            var FindedClient = await _repository.GetByIdAsync(id);
            if (FindedClient == null) return null;

            return await _repository.DeleteClientAsync(FindedClient);
        }
    }
}