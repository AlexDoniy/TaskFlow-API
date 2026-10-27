namespace Application.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string entity, Guid id)
        : base($"{entity} of id {id} not found") { }
}