using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos.Tasks;

public record UpdateTaskItemStatusRequest
{
    public TaskItemStatus Status{ get; init; }
}