using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Equinox.Models.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected EquinoxContext context { get; }
        private readonly DbSet<T> dbset;

        public Repository(EquinoxContext ctx)
        {
            context = ctx;
            dbset = context.Set<T>();
        }

        public int Count => dbset.Count();

        public IEnumerable<T> List(QueryOptions<T> options) => BuildQuery(options).ToList();
        public T? Get(int id) => dbset.Find(id);
        public T? Get(string id) => dbset.Find(id);
        public T? Get(QueryOptions<T> options) => BuildQuery(options).FirstOrDefault();

        public void Insert(T entity) => dbset.Add(entity);
        public void Update(T entity) => dbset.Update(entity);
        public void Delete(T entity) => dbset.Remove(entity);
        public void Save() => context.SaveChanges();

        private IQueryable<T> BuildQuery(QueryOptions<T> options)
        {
            IQueryable<T> query = dbset;

            if (options.GetIncludes() != null)
            {
                foreach (var include in options.GetIncludes()!)
                {
                    query = query.Include(include);
                }
            }
            
            if (options.HasWhere) query = query.Where(options.Where!);
            
            if (options.HasOrderBy)
            {
                query = options.OrderByDirection == "asc"
                    ? query.OrderBy(options.OrderBy!)
                    : query.OrderByDescending(options.OrderBy!);
            }
            
            if (options.HasPaging) query = query.PageBy(options.PageNumber, options.PageSize);

            return query;
        }
    }
}
