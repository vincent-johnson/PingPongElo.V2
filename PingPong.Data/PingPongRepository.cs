using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PingPong.Data
{
    public class PingPongRepository<T>:
        IPingPongRepository<T> where T : class
    {
        internal PingPongContext _context;
        internal DbSet<T> _dbSet;

        public PingPongRepository()
        {
            _context = new PingPongContext();
            _dbSet = _context.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            return query;
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual void Delete(object id)
        {
            T entity = _dbSet.Find(id);
            Delete(entity);
            _context.SaveChanges();
        }
    }
}
