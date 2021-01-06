using System;
using System.Collections.Generic;

namespace Domain
{
    public class Child
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Birthday { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
        public ICollection<FamilyRelationship> FamilyRelationships { get; set; }
    }
}