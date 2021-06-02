using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace HoneyDo.Models
{
    public class HoneyDoItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string Priority { get; set; }
        public string Category { get; set; }
        public DateTime DueDate { get; set; }
        public string Recurrence { get; set; }
        public bool Completed { get; set; }
    }
}
