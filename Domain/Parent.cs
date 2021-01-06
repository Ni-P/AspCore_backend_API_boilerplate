using System;
using System.Collections.Generic;

namespace Domain
{
    public class Parent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public virtual ICollection<Child> Children { get; set; }
        public virtual ICollection<FamilyRelationship> FamilyRelationships { get; set; }
    }
}