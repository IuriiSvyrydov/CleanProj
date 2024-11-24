
using System.ComponentModel;
using CleanProj.Domain.Common;

namespace CleanProj.Domain.Entities;

    public sealed class Category: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public List<Prompt> Promts { get; set; } = [];
       
    }
