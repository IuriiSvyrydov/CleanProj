using CleanProj.Domain.Common;
using CleanProj.Domain.Identity;

namespace CleanProj.Domain.Entities;

    public sealed class Prompt: BaseEntity
    {
        public string Title { get;  set; } = string.Empty;
        public string Description { get;  set; } = string.Empty;
        public string Content { get;  set; } = string.Empty;
        public bool  IsActive { get;  set; } = true;
        public string ImageUrl { get;  set; } = string.Empty;
        public int LikeCount { get;  set; }
//public Guid CreatorId { get;private set; }
       // public ApplicationUser Creator { get; private set; }
    
        public ICollection<PromptCategory> PromptCategories { get; set; } = [];
        public ICollection<UserFavoritePrompt> UserFavoritePrompts { get; set; } = [];
        public ICollection<UserLikePrompt> UserLikePrompts { get; set; } = [];
        public ICollection<PlaceHolder> PlaceHolders { get; set; } = [];
        public ICollection<UserPromptComment> UserPromptComments { get; set; } = [];

        
                
        

        public void SetImageUrl(string imageUrl)
        {
            ImageUrl = imageUrl;
        }

        public void Update( string title, string description, string content, bool isActive )
        {
            Title = title;
            Description = description;
            Content = content;
            IsActive = isActive;
        }


        public static Prompt Create(string title, string description, string content, bool isActive)
            => new Prompt
            {
                Id = Guid.CreateVersion7(),
                Title = title,
                Description = description,
                Content = content,
                IsActive = isActive,
            };
        
    }
