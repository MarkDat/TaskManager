using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TM.API.DTOs.Todos
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCheck { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<TodoModel> Items { get; set; }
    }

    public class TodoUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCheck { get; set; }
        public virtual ICollection<TodoUpdateModel> Items { get; set; }
    }
}
