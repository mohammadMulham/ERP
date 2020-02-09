using ERPAPI.ViewModels.Auth;
using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.SwaggerExamples.Auth
{
    public class CreateTokenViewModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new GenerateJwtViewModel
            {
                client_id = "ERPLite",
                client_secret = "",
                grant_type = "password",
                password = "123456",
                username = "root"
            };
        }
    }
}
