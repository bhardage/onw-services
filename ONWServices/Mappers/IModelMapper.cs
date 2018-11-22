using ONWServices.Models;
using ONWServices.ViewModels;

namespace ONWServices.Mappers
{
    public interface IModelMapper<Domain, View>
    {
        Domain ToDomainModel(View vm);
        View ToViewModel(Domain domain);
    }
}