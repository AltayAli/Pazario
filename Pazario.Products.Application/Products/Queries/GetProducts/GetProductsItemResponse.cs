namespace Pazario.Products.Application.Products.Queries.GetProducts
{
    public record GetProductsItemResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? ModelName { get; set; }
        public int VariantsCount { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
