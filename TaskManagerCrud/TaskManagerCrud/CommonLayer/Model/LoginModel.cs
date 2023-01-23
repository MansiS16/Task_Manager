using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerCrud.CommonLayer.Model
{
    public class LoginModelRequest

    {
        public string username { get; set; }
        public string Password { get; set; }
    }

    public class LoginModelResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
         
        public string Username { get; set; }
    }

}
