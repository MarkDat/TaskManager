using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Todos
{
    public class GetTodoResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? IsCheck { get; set; }
        public virtual IEnumerable<GetTodoResponse> ChildTodos { get; set; }
    }
}
