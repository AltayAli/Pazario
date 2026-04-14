namespace Pazario.Products.Application.Markas.Queries.GetMarkas
{
    public class GetMarkasResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int ModelsCount { get; set; }
    }
}
