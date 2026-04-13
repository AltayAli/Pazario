namespace Pazario.Products.Presentation.Products.DTOs
{
    public record ProductListResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ModelName { get; set; }
        public int VariantsCount { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
