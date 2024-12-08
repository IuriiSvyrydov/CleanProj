namespace CleanProj.API.V1.Prompts;

public sealed record UpdatePromptDTO(
    Guid Id,
    string Title,
    string Description,
    string Content,
    string? ImageUrl,
    bool IsActive,
    List<Guid> CategoryIds,
    List<string> PlaceHoldersName);
