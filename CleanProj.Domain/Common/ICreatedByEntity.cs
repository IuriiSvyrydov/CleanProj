
namespace CleanProj.Domain.Common;

    public interface ICreatedByEntity
    {
       string  CreatedByUserId { get; set; }
         DateTimeOffset CreateAt { get; set; }

    }