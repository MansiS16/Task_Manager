using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagerCrud.CommonLayer.Model
{
    public class UpdateTaskRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string AssignedTo { get; set; }
        public string AssignedBy { get; set; }

    }

    public class UpdateTaskResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
