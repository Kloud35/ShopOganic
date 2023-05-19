
namespace ShopOganicAPI.IServices
{
    public interface IServices<T> where T : class
    {
        List<T> GetAll();
        T GetById(Guid id);
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(Guid id);
    }
}
