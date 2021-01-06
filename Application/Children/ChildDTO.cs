using System;
using System.Collections.Generic;
using Domain;

namespace Application.Children
{
    public class ChildDTO
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
    }
}