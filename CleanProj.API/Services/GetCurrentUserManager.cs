using CleanProj.Persistence.EntityFramework.Services;

namespace CleanProj.API.Services;

public class GetCurrentUserManager: ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetCurrentUserManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long? GetUserId()
    { 
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
        return userId is null ? null : long.Parse(userId);
    }
}