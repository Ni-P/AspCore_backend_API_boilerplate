using System;
using System.Collections.Generic;
using Domain;

namespace Application.Parents
{
    public class ParentDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<Child> Children { get; set; }
    }
}