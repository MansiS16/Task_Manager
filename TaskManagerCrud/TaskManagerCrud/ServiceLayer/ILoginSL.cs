using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerCrud.CommonLayer.Model;

namespace TaskManagerCrud.ServiceLayer
{
    public interface ILoginSL
    {
        public Task<LoginModelResponse> LoginModel(LoginModelRequest request);
        //public Task<ReadTaskResponse> ReadTask();
    }
}
