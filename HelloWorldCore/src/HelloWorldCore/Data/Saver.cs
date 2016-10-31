using HelloWorldCore.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldCore.Data
{
    public interface ISaver<TEntity>
    {
        Task GoAsync(TEntity entity);
    }
    public abstract class Saver<TEntity> : ISaver<TEntity> where TEntity : Entity
    {
        private readonly ApplicationDbContext _db;

        public Saver(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task GoAsync(TEntity entity)
        {
            var isPersisted = entity.Id != 0;

            if (isPersisted)
            {
                _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            else
            {
                _db.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            }
            await _db.SaveChangesAsync();
        }
    }
}
