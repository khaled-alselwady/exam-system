using System.Threading.Tasks;

namespace ExamSystem.Services
{
    public interface IValidation<TEntity, TService>
    {
        Task<bool> IsValid(TEntity entity, TService service);
    }
}
