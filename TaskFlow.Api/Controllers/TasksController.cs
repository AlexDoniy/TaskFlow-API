using Application.Abstractions.Interfaces.Services;
using Application.Dtos.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TaskFlow.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskItemService _taskService;

    public TasksController(ITaskItemService taskService)
    {
        _taskService = taskService;
    }

    [HttpPost]
    [ProducesResponseType<TaskItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskItemResponse>> Create(Guid projectId, CreateTaskItemRequest request, CancellationToken ct)
    {
        var createdTask = await _taskService.CreateAsync(projectId, request, ct);
        return CreatedAtAction(nameof(GetById), new { projectId, id = createdTask.Id }, createdTask);
    }
    
    [HttpGet("{id:guid}")]
    [ProducesResponseType<TaskItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskItemResponse>> GetById(Guid projectId, Guid id, CancellationToken ct)
    {
        var task = await _taskService.GetByIdAsync(projectId, id, ct);
        return Ok(task);
    }
    
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<TaskItemResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<TaskItemResponse>>> GetAll(Guid projectId, CancellationToken ct)
    {
        var tasks = await _taskService.GetAllAsync(projectId, ct);
        return Ok(tasks);
    }
    
    [HttpPut("{id:guid}")]
    [ProducesResponseType<TaskItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskItemResponse>> Update(Guid projectId, Guid id, UpdateTaskItemRequest request, CancellationToken ct)
    {
        var task = await _taskService.UpdateAsync(projectId, id, request, ct);
        return Ok(task);
    }
 
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid projectId, Guid id, CancellationToken ct)
    {
        await _taskService.DeleteAsync(projectId, id, ct);
        return NoContent();
    }
    
    
    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType<TaskItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<TaskItemResponse>> UpdateStatus(Guid projectId, Guid id, UpdateTaskItemStatusRequest request, CancellationToken ct)
    {
        var task = await _taskService.UpdateStatusAsync(projectId, id, request, ct);
        return Ok(task);
    }
    
}