using System.ComponentModel.DataAnnotations;

namespace CleanProj.API.V1.Prompts;

public sealed record CreatePromptDTO
{
    public CreatePromptDTO(string title, string description, string content, IFormFile? image, bool isActive, List<Guid> categoryIds, List<string> placeHoldersName)
    {
        Title = title;
        Description = description;
        Content = content;
        Image = image;
        IsActive = isActive;
        CategoryIds = categoryIds;
        PlaceHoldersName = placeHoldersName;
    }

    [Required(ErrorMessage = "Title is required field")]
    
    public string Title{get;set;}
    public string Description {get;set;}
    public string Content{get;set;}
        public IFormFile? Image {get;set;}
    public bool IsActive  {get;set;}
        public List<Guid> CategoryIds{get;set;}
    public List<string> PlaceHoldersName {get;set;}
}
   
