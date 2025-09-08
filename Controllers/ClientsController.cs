using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.DTO;
using API.Model;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly ClientService _service;

    public ClientsController(ClientService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> Get()
    {
        var clients = await _service.GetClientsAsync();
        return Ok(clients);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetById(int id)
    {
        var client = await _service.GetByClientIdAsync(id);
        if (client == null)
            return NotFound(new { Status = 404, Message = "Client not found" });

        return Ok(client);
    }

    [HttpPost]
    public async Task<ActionResult<Client>> Post(CreateClientDTO dto)
    {
        var newClient = await _service.CreateClientAsync(dto);
        if (newClient == null)
            return BadRequest(new { Status = 400, Message = "Campos obrigatórios não preenchidos" });

        return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, newClient);
    }


    [HttpPatch("{id}/status/{status}")]
    public async Task<ActionResult<Client>> Inactivate(int id, bool status)
    {
        var client = await _service.InactivateClientAsync(id, status);
        if (client == null)
            return NotFound(new { Status = 404, Message = "Usuário não encontrado" });

        return Ok(client);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Client>> UpdateClientAsync(int id, CreateClientDTO client)
    {
        var FindedClient = await _service.UpdateClientAsync(id, client);
        if (FindedClient == null)
            return NotFound(new { Status = 404, Message = "Usuário não encontrado" });

        return Ok(client);
    }
        [HttpDelete("{id}")]
    public async Task<ActionResult<Client>> DeleteClientAsync(int id)
    {
        var FindedClient = await _service.DeleteClientAsync(id);
        if (FindedClient == null)
            return NotFound(new { Status = 404, Message = "Usuário não encontrado" });

        return Ok("Usuário deletado com sucesso");
    }
}
