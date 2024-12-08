
using System.ComponentModel;
using CleanProj.Domain.Common;
using TSID.Creator.NET;

namespace CleanProj.Domain.Entities;

    public sealed class Category: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public List<Prompt> Prompts { get; set; } = [];
        public List<PromptCategory> PromptCategories { get; set; } = [];

        #region Static factories

        public static Category Create(string name , string description)
        {
            return new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description
            };
        }    
        #endregion
        

    }
