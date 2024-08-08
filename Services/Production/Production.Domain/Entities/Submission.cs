using Articles.Entitities;

namespace Production.Domain.Entities
{
    public record Submission : ValueObject
    {
        //talk - complex property with one to many relations is not possible
        // CP is only to store data is not possible to reference another table but only another complex property
        public required string Title { get; set; }
        public required string Type { get; set; }
        public required string Doi { get; set; }
        public DateTime SubmissionDate { get; set; }
        public required virtual User SubmissionUser { get; set; }
        public DateTime AcceptedOn { get; set; }

        //public virtual ICollection<Author> Authors { get; } = new List<Author>();

        //protected override IEnumerable<object> GetAtomicValues()
        //{
        //    yield return Title;
        //    yield return Doi;
        //}
    }
}
