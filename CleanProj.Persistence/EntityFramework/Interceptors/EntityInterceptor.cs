﻿using CleanProj.Domain.Common;
using CleanProj.Persistence.EntityFramework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CleanProj.Persistence.EntityFramework.Interceptors;

public class EntityInterceptor: SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public EntityInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private  void UpdateEntities(DbContext? context)
    {
        if (context is null)
            return;
        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State is EntityState.Added or EntityState.Deleted or EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                var utcNow = DateTimeOffset.UtcNow;
                if(entry.State==EntityState.Added)
                {
                    entry.Entity.CreatedByUserId =_currentUserService.GetUserId()?.ToString();
                    entry.Entity.ModifiedAt = utcNow;
                }
                if(entry.State==EntityState.Modified)
                {
                    entry.Entity.ModifiedByUserId = _currentUserService.GetUserId().ToString();
                    entry.Entity.ModifiedAt = utcNow;
                }
            }
        }
    }
   
}
public static class Extensions
{
    public static bool HasChangedOwnedEntities( this EntityEntry entity)
        =>entity.References.Any(r=>
            r.TargetEntry !=null &&
            r.TargetEntry.Metadata.IsOwned()
            && 
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
}