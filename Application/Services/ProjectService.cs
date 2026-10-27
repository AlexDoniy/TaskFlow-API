using Application.Abstractions.Interfaces;
using Application.Abstractions.Interfaces.Repositories;
using Application.Abstractions.Interfaces.Services;
using Application.Dtos.Projects;
using Application.Exceptions;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class ProjectService : IProjectService
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectRepository _projectRepository;
    private readonly TimeProvider _timeProvider;
    private readonly IValidator<CreateProjectRequest> _createProjectRequestValidator;
    private readonly IValidator<UpdateProjectRequest> _updateProjectRequestValidator;

    public ProjectService(IProjectRepository projectRepository,
        IUnitOfWork unitOfWork,
        TimeProvider timeProvider,
        IValidator<CreateProjectRequest> createProjectRequestValidator,
        IValidator<UpdateProjectRequest> updateProjectRequestValidator
        )
    {
        _unitOfWork = unitOfWork;
        _projectRepository = projectRepository;
        _timeProvider = timeProvider;
        _createProjectRequestValidator = createProjectRequestValidator;
        _updateProjectRequestValidator = updateProjectRequestValidator;
    }

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct)
    {
        await _createProjectRequestValidator.ValidateAndThrowAsync(request, ct);
        
        var project = new ProjectEntity
        {
            Name = request.Name,
            Description = request.Description,
            CreatedAt = _timeProvider.GetUtcNow()
        };

        _projectRepository.Add(project);
        await _unitOfWork.SaveChangesAsync(ct);
        return ToResponse(project);
    }

    public async Task<ProjectResponse> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var project = await _projectRepository.GetByIdAsync(id, ct);

        if (project is null) throw new NotFoundException("Project", id);
        
        
        return ToResponse(project);
    }

    public async Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken ct)
    {
        var projects = await _projectRepository.GetAllAsync(ct);
        var responseCollection = projects
            .Select(ToResponse)
            .ToList();

        return responseCollection;
    }

    public async Task<ProjectResponse> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken ct)
    {
        
        await _updateProjectRequestValidator.ValidateAndThrowAsync(request, ct);
        var project = await _projectRepository.GetByIdAsync(id, ct);
        
        if (project is null) throw new NotFoundException("Project", id);
        
        project.Name = request.Name;
        project.Description = request.Description;
        await _unitOfWork.SaveChangesAsync(ct);
        
        return ToResponse(project);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var project = await _projectRepository.GetByIdAsync(id, ct);
        
        if (project is null) throw new NotFoundException("Project", id);
        
        _projectRepository.Remove(project);
        await _unitOfWork.SaveChangesAsync(ct);

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