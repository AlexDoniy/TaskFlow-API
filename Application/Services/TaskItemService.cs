using Application.Abstractions;
using Application.Abstractions.Interfaces;
using Application.Abstractions.Interfaces.Repositories;
using Application.Abstractions.Interfaces.Services;
using Application.Dtos;
using Application.Dtos.Tasks;
using Application.Exceptions;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;

namespace Application.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<GetTaskItemsRequest> _getTaskItemsValidator;
    private readonly IValidator<CreateTaskItemRequest> _createTaskItemValidator;
    private readonly IValidator<UpdateTaskItemRequest> _updateTaskItemValidator;
    private readonly IValidator<UpdateTaskItemStatusRequest> _updateTaskItemStatusValidator;


    public TaskItemService(ITaskItemRepository taskItemRepository,
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        TimeProvider timeProvider,
        IValidator<CreateTaskItemRequest> createTaskItemValidator,
        IValidator<UpdateTaskItemRequest> updateTaskItemValidator,
        IValidator<UpdateTaskItemStatusRequest> updateTaskItemStatusValidator,
        IValidator<GetTaskItemsRequest> getTaskItemsValidator)
    {
        _taskItemRepository = taskItemRepository;
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
        _timeProvider = timeProvider;
        _createTaskItemValidator = createTaskItemValidator;
        _updateTaskItemValidator = updateTaskItemValidator;
        _updateTaskItemStatusValidator = updateTaskItemStatusValidator;
        _getTaskItemsValidator = getTaskItemsValidator;
    }

    public async Task<TaskItemResponse> CreateAsync(Guid projectId, CreateTaskItemRequest request, CancellationToken ct)
    {
        await _createTaskItemValidator.ValidateAndThrowAsync(request, ct);
        if (!await _projectRepository.ExistsAsync(projectId, ct)) throw new NotFoundException("Project", projectId);

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

    public async Task<TaskItemResponse> GetByIdAsync(Guid projectId, Guid id, CancellationToken ct)
    {
        var task = await _taskItemRepository.GetByIdInProjectAsync(projectId, id, ct);

        if (task is null) throw new NotFoundException("Task", id);
        return ToResponse(task);
    }
    
    public async Task<PagedResponse<TaskItemResponse>> GetAllAsync(Guid projectId, GetTaskItemsRequest request, CancellationToken ct)
    {
        await _getTaskItemsValidator.ValidateAndThrowAsync(request, ct);
        if (!await _projectRepository.ExistsAsync(projectId, ct))  throw new NotFoundException("Project", projectId);
        
        var result = await _taskItemRepository.GetPagedByProjectAsync(projectId, request.Page, request.PageSize, ct);
        var items = result.Items
            .Select(ToResponse)
            .ToList();

        return new PagedResponse<TaskItemResponse>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = result.TotalCount
        };
    }
    
    public async Task<TaskItemResponse> UpdateAsync(Guid projectId, Guid id, UpdateTaskItemRequest request, CancellationToken ct)
    {
        await _updateTaskItemValidator.ValidateAndThrowAsync(request, ct);
        var task = await _taskItemRepository.GetByIdInProjectAsync(projectId, id, ct);

        if (task is null) throw new NotFoundException("Task", id);
        
        task.Title = request.Title;
        task.Description = request.Description;
        task.Priority = request.Priority;
        task.DueDate = request.DueDate;
        task.UpdatedAt = _timeProvider.GetUtcNow();
        
        await _unitOfWork.SaveChangesAsync(ct);
        
        return ToResponse(task);
    }

    public async Task DeleteAsync(Guid projectId, Guid id, CancellationToken ct)
    {
        
        var task = await _taskItemRepository.GetByIdInProjectAsync(projectId, id, ct);
        if (task is null) throw new NotFoundException("Task", id);
        
        _taskItemRepository.Remove(task);
        await _unitOfWork.SaveChangesAsync(ct);
    }


    public async Task<TaskItemResponse> UpdateStatusAsync(Guid projectId, Guid id, UpdateTaskItemStatusRequest request, CancellationToken ct)
    {
        await _updateTaskItemStatusValidator.ValidateAndThrowAsync(request, ct);
        
        var task = await _taskItemRepository.GetByIdInProjectAsync(projectId, id, ct);
        if (task is null) throw new NotFoundException("Task", id);

        if (task.Status == TaskItemStatus.Todo && request.Status == TaskItemStatus.Done)
            throw new ConflictException("Cannot change task status from Todo directly to Done. Move the task to InProgress first.");
        if (task.Status == request.Status)
            throw new ConflictException($"Task already has status {task.Status}.");
        
        task.Status = request.Status;
        task.UpdatedAt = _timeProvider.GetUtcNow();
        
        await _unitOfWork.SaveChangesAsync(ct);
        
        return ToResponse(task);
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