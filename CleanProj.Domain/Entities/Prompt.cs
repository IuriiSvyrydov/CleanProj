using CleanProj.Domain.Common;

namespace CleanProj.Domain.Entities;

    public sealed class Prompt: BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public bool  IsActive { get; set; } = true;
        public string ImageUrl { get; set; } = string.Empty;
      
    
        public ICollection<PromptCategory> PromptCategories { get; set; } = [];
        public ICollection<UserFavoritePrompt> UserFavoritePrompts { get; set; } = [];
        public ICollection<UserLikePrompt> UserLikePrompts { get; set; } = [];
        public ICollection<PlaceHolder> PlaceHolders { get; set; } = [];

    }
