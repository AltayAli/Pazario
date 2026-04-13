namespace Pazario.Products.Presentation.Products.DTOs
{
    public record UpdateProductRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ModelId { get; set; }
        public List<Guid> CategoryIds { get; set; } = new();
    }
}
