using Application.Abstractions.Interfaces;
using Application.Abstractions.Interfaces.Repositories;
using Application.Abstractions.Interfaces.Services;
using Application.Dtos.Projects;
using Domain.Entities;

namespace Application.Services;

public class ProjectService : IProjectService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _repository;
    private readonly TimeProvider _timeProvider;

    public ProjectService(IProjectRepository repository, IUnitOfWork unitOfWork, TimeProvider timeProvider)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _timeProvider = timeProvider;
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct)
    {
        var project = new ProjectEntity
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = _timeProvider.GetUtcNow()
        };

        _repository.Add(project);
        await _unitOfWork.SaveChangesAsync(ct);
        return ToResponse(project);
    }

    public async Task<ProjectResponse?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var project = await _repository.GetByIdAsync(id, ct);
        return project is null ? null : ToResponse(project);
    }

    public async Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken ct)
    {
        var projects = await _repository.GetAllAsync(ct);
        var responseCollection = projects
            .Select(ToResponse)
            .ToList();

        return responseCollection;
    }

    public async Task<ProjectResponse?> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken ct)
    {
        var project = await _repository.GetByIdAsync(id, ct);
        
        if (project is null) return null;
        
        project.Name = request.Name;
        project.Description = request.Description;
        await _unitOfWork.SaveChangesAsync(ct);
        
        return ToResponse(project);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var project = await _repository.GetByIdAsync(id, ct);
        if (project is null) return false;
        
        _repository.Remove(project);
        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }
    
    private static ProjectResponse ToResponse(ProjectEntity project) =>
        new()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreatedAt = project.CreatedAt
        };
}