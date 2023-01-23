using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCrud.CommonLayer.Model;

namespace TaskManagerCrud.RepositoryLayer
{
    public interface ILoginRL
    {
        public Task<LoginModelResponse> LoginModel(LoginModelRequest request);
    }
}
