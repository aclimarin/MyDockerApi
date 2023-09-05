using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using MyDockerApi.Infra;

namespace MyDockerApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{

    private readonly IRepository<Models.Task> _repository;


    public TaskController(IRepository<Models.Task> repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Models.Task task)
    {
        var savedTask = await _repository.Save(task);
        return Ok(savedTask);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] Models.Task task)
    {
        var savedTask = await _repository.Save(task);
        return Ok(savedTask);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var tasks = await _repository.All();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(long id)
    {
        var task = await _repository.FindById(id);
        return Ok(task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var task = await _repository.FindById(id);
        await _repository.Delete(task);
        return Ok($"Id {id} was deleted!");
    }
}
