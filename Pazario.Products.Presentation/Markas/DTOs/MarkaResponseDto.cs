namespace Pazario.Products.Presentation.Markas.DTOs
{
    public record MarkaResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int ModelsCount { get; set; }
    }
}
