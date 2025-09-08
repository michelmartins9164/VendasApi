using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.DTO;
using API.Model;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;

    public UsersController(UserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> Get()
    {
        var users = await _service.GetUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(int id)
    {
        var user = await _service.GetByUserIdAsync(id);
        if (user == null)
            return NotFound(new { Status = 404, Message = "User not found" });

        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<User>> Post(CreateUserDTO dto)
    {
        var newUser = await _service.CreateUserAsync(dto);
        if (newUser == null)
            return BadRequest(new { Status = 400, Message = "Campos obrigatórios não preenchidos" });

        return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
    }

    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(LoginDTO dto)
    {
        var user = await _service.AuthenticateAsync(dto);
        if (user == null)
            return Unauthorized(new { Status = 401, Message = "Login ou senha inválidos ou usuário inativo" });
        
        return Ok(user); // aqui você pode gerar token JWT se quiser
    }

    [HttpPatch("{id}/status/{status}")]
    public async Task<ActionResult<User>> Inactivate(int id, bool status)
    {
        var user = await _service.InactivateUserAsync(id, status);
        if (user == null)
            return NotFound(new { Status = 404, Message = "Usuário não encontrado" });

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<User>> UpdateUserAsync(int id, UserUpdateDTO user)
    {
        var FindedUser = await _service.UpdateUserAsync(id, user);
        if (FindedUser == null)
            return NotFound(new { Status = 404, Message = "Usuário não encontrado" });

        return Ok(user);
    }
}
