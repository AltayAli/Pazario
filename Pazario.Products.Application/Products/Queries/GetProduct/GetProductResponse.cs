namespace Pazario.Products.Application.Products.Queries.GetProduct
{
    public record GetProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ModelId { get; set; }
        public string? ModelName { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public List<GetProductCategoryItem> Categories { get; set; } = new();
        public List<GetProductVariantItem> Variants { get; set; } = new();
    }

    public record GetProductCategoryItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public record GetProductVariantItem
    {
        public Guid Id { get; set; }
        public string Sku { get; set; }
        public string Barcode { get; set; }
        public decimal PriceAmount { get; set; }
        public string PriceCurrency { get; set; }
        public decimal? CostAmount { get; set; }
        public string? CostCurrency { get; set; }
        public decimal? TaxRate { get; set; }
        public int StockQuantity { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public decimal DiscountAmount { get; set; }
        public string DiscountCurrency { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public List<GetProductVariantImageItem> Images { get; set; } = new();
        public List<GetProductVariantPropertyItem> Properties { get; set; } = new();
    }

    public record GetProductVariantImageItem
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsMain { get; set; }
    }

    public record GetProductVariantPropertyItem
    {
        public Guid Id { get; set; }
        public Guid CategoryPropertyId { get; set; }
        public string CategoryPropertyName { get; set; }
        public string Value { get; set; }
    }
}
