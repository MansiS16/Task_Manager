using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerCrud.CommonLayer.Model
{
    public class DeleteTaskRequest
    {
        public int id { get; set; }
    }

    public class DeleteTaskResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
