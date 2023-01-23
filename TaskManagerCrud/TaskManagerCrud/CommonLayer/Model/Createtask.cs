using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerCrud.CommonLayer.Model
{
    public class CreatetaskRequest
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime dueDate { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public string assignedTo { get; set; }
        public string assignedBy { get; set; }

    }

    public class CreateTaskResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
