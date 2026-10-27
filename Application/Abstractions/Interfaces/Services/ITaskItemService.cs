using Application.Dtos.Projects;
using Application.Dtos.Tasks;

namespace Application.Abstractions.Interfaces.Services;

public interface ITaskItemService
{
    Task<TaskItemResponse> CreateAsync(Guid projectId, CreateTaskItemRequest request, CancellationToken ct);
    Task<TaskItemResponse> GetByIdAsync(Guid projectId, Guid id, CancellationToken ct);
    Task<IReadOnlyList<TaskItemResponse>> GetAllAsync(Guid projectId, CancellationToken ct);
    Task<TaskItemResponse> UpdateAsync(Guid projectId, Guid id, UpdateTaskItemRequest request, CancellationToken ct);
    Task<TaskItemResponse> UpdateStatusAsync(Guid projectId, Guid id, UpdateTaskItemStatusRequest request, CancellationToken ct);

    Task DeleteAsync(Guid projectId, Guid id, CancellationToken ct);
}