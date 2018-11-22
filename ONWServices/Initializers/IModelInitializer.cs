using ONWServices.Models;

namespace ONWServices.Initializers
{
    public interface IModelInitializer<T>
    {
        T Setup(T game);
    }
}