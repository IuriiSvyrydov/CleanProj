namespace CleanProj.Persistence.EntityFramework.Services;

public interface ICurrentUserService
{
    long? GetUserId();
}