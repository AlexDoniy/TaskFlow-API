using Application.Abstractions.Interfaces;
using Application.Abstractions.Interfaces.Repositories;
using Application.Abstractions.Interfaces.Services;
using Application.Dtos.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public TaskItemService(ITaskItemRepository taskItemRepository, IProjectRepository projectRepository, IUnitOfWork unitOfWork, TimeProvider timeProvider)
    {
        _taskItemRepository = taskItemRepository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _timeProvider = timeProvider;
    }

    public async Task<TaskItemResponse?> CreateAsync(Guid projectId, CreateTaskItemRequest request, CancellationToken ct)
    {
        if (!await _projectRepository.ExistsAsync(projectId, ct)) return null;

        var time = _timeProvider.GetUtcNow();
        
        TaskItemEntity task = new TaskItemEntity
        {
            Title = request.Title,
            Description = request.Description,
            Status = TaskItemStatus.Todo,
            Priority = request.Priority,
            DueDate = request.DueDate,
            CreatedAt = time,
            UpdatedAt = time,
            ProjectId = projectId
        };
             
        _taskItemRepository.Add(task);
        await _unitOfWork.SaveChangesAsync(ct);
        return ToResponse(task);
    }

    public async Task<TaskItemResponse?> GetByIdAsync(Guid projectId, Guid id, CancellationToken ct)
    {
        var result = await _taskItemRepository.GetByIdInProjectAsync(projectId, id, ct);

        return result is null ? null : ToResponse(result);
    }
    
    public async Task<IReadOnlyList<TaskItemResponse>?> GetAllAsync(Guid projectId, CancellationToken ct)
    {
        if (!await _projectRepository.ExistsAsync(projectId, ct)) return null;
        
        var tasks = await _taskItemRepository.GetAllByProjectAsync(projectId, ct);
        var responseCollection = tasks
            .Select(ToResponse)
            .ToList();

        return responseCollection;
    }
    
    public async Task<TaskItemResponse?> UpdateAsync(Guid projectId, Guid id, UpdateTaskItemRequest request, CancellationToken ct)
    {

        var task = await _taskItemRepository.GetByIdInProjectAsync(projectId, id, ct);

        if (task is null) return null;
        
        task.Title = request.Title;
        task.Description = request.Description;
        task.Status = request.Status;
        task.Priority = request.Priority;
        task.DueDate = request.DueDate;
        task.UpdatedAt = _timeProvider.GetUtcNow();
        
        await _unitOfWork.SaveChangesAsync(ct);
        
        return ToResponse(task);
    }

    public async Task<bool> DeleteAsync(Guid projectId, Guid id, CancellationToken ct)
    {
        
        var task = await _taskItemRepository.GetByIdInProjectAsync(projectId, id, ct);
        if (task is null) return false;
        
        _taskItemRepository.Remove(task);
        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }
    
    private static TaskItemResponse ToResponse(TaskItemEntity task) =>
        new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Priority = task.Priority,
            Status = task.Status,
            ProjectId = task.ProjectId,
            DueDate = task.DueDate,
            CreatedAt = task.CreatedAt,
            UpdatedAt = task.UpdatedAt
        };
}