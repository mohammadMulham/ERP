using ERPAPI.ViewModels.Entries;
using Swashbuckle.AspNetCore.Examples;

namespace ERPAPI.SwaggerExamples.Entries
{
    public class NewEntryViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new NewEntryViewModel()
            {

            };
        }
    }
}
