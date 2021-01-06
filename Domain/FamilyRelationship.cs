using System;

namespace Domain
{
    public class FamilyRelationship
    {
        public Guid ParentId { get; set; }
        public virtual Parent Parent { get; set; }
        public Guid ChildId { get; set; }
        public virtual Child Child { get; set; }
        public int GenerationDifference { get; set; }
    }
}