using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCrud.CommonLayer.Model;
using TaskManagerCrud.RepositoryLayer;

namespace TaskManagerCrud.ServiceLayer
{
    public class LoginSL: ILoginSL
    {

        public readonly ILoginRL _loginRL;
        public LoginSL(ILoginRL loginRL)
        {
            _loginRL = loginRL;
        }
        public async Task<LoginModelResponse> LoginModel(LoginModelRequest request)
        {
            return await _loginRL.LoginModel(request);
        }
    }
}
