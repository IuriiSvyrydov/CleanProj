
namespace CleanProj.Domain.Common;

    public interface IModifiedByEntity
    {
          string  ModifiedByUserId { get; set; }
         DateTimeOffset? ModifiedAt { get; set; }
    }