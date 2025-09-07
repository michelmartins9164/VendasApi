using Microsoft.AspNetCore.Mvc;
using API.Services;
using API.Model;
using API.DTO;

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
        {
            return NotFound(new
            {
                Status = 404,
                Message = "User not found"
            });
        }
        return Ok(user);
    }
    [HttpPost]
    public async Task<ActionResult<User>> Post(CreateUserDTO userDto)
    {
        var newUser = await _service.CreateUserAsync(userDto);

        if (newUser == null)
        {
            return BadRequest(new
            {
                Status = 400,
                Message = "Não foi possível criar o usuário"
            });
        }

        return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
    }


}
