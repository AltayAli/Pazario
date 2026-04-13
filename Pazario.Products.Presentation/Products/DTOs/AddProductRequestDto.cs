namespace Pazario.Products.Presentation.Products.DTOs
{
    public record AddProductRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ModelId { get; set; }
        public List<Guid> CategoryIds { get; set; } = new();
        public List<AddProductVariantRequestDto> Variants { get; set; } = new();
    }
}
