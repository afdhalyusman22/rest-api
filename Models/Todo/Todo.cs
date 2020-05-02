using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.Todo
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Complete { get; set; }
        public DateTime ExpiredDate { get; set; }
    }

    public class RequestIncomingTodo
    {
        public string startdate { get; set; }
        public string enddate { get; set; }
    }

    public class RequestAddTodo
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class RequestUpdateToDo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Complete { get; set; }
        public int Id { get; set; }
    }

    public class RequestCompleteToDo
    {
        public int Complete { get; set; }
        public int Id { get; set; }
    }

    public class RequestDoneToDo
    {
        public bool isDone { get; set; }
        public int Id { get; set; }
    }
}
