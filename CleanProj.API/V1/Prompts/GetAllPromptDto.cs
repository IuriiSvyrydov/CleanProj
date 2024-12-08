namespace CleanProj.API.V1.Prompts;

public record GetAllPromptDto(
    Guid Id,
    string Title,
    string Description,
    string? ImageUrl,
    bool IsActive);