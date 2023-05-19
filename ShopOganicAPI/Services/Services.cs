using Microsoft.EntityFrameworkCore;
using ShopOganicAPI.Context;
using ShopOganicAPI.IServices;
using ShopOganicAPI.Models;
namespace ShopOganicAPI.Services
{
    public class Services<T> : IServices<T> where T : class
    {
        private readonly OganicDBContext dbContext;
        private readonly DbSet<T> dbSet;

        public Services()
        {
            dbContext = new OganicDBContext();
            dbSet = dbContext.Set<T>();
        }

        public bool Create(T entity)
        {
            try
            {
                dbSet.AddAsync(entity);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Delete(Guid id)
        {
            try
            {
                var entity = GetById(id);
                dbSet.Remove(entity);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        public bool Update(T entity)
        {
            try
            {
                dbSet.Update(entity);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
