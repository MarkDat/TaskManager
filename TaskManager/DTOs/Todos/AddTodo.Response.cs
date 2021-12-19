using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Todos
{
    public class AddTodoResponse
    {
        public int Id { get; set; }
        public int? CardId { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
    }
}
