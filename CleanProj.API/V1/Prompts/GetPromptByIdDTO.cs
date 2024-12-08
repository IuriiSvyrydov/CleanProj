namespace CleanProj.API.V1.Prompts;

public record GetPromptByIdDTO(
    Guid Id,
    string Title,
    string Description,
    string Content,
    string? ImageUrl,
    bool IsActive,
    List<PromptCategoryDto> Categories,
    List<PlaceHolderDto> PlaceHolders);
    
    public sealed record PromptCategoryDto(Guid Id,string Name);
    public sealed record PlaceHolderDto(Guid Id, string Name);