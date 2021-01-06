namespace Domain
{
    public class FamilyRelationship
    {
        public string ParentId { get; set; }
        public virtual Parent Parent { get; set; }
        public string ChildId { get; set; }
        public virtual Child Child { get; set; }
        public int GenerationDifference { get; set; }
    }
}