using CleanArchMvc.Domain.Entities;
using FluentAssertions;


namespace CleanArchMvc.Domain.Tests;

public class ProductUnitTest1
{
    [Fact]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product name","Product description", 19.99M , 99, "Product image");
        action.Should()
            .NotThrow<CleanArchMvc.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "Product name", "Product description", 19.99M, 99, "Product image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value.");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "Pr", "Product description", 19.99M, 99, "Product image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minimum 3 charecters.");
    }

    [Fact]
    public void CreateProduct_MissingDescriptionValue_DomainExceptionEmptyDescription()
    {
        Action action = () => new Product(1, "Product name", null, 19.99M, 99, "Product image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid description. Description is required.");
    }

    [Fact]
    public void CreateProduct_ShortDescriptionValue_DomainExceptionShortDescription()
    {
        Action action = () => new Product(1, "Product name", "Prod", 19.99M, 99, "Product image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid description, too short, minimum 5 charecters.");
    }

    [Fact]
    public void CreateProduct_NegativePriceValue_DomainExceptionInvalidPrice()
    {
        Action action = () => new Product(1, "Product name", "Product description", -1, 99, "Product image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid price value.");
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_NegativeStockValue_DomainExceptionInvalidStock(int value)
    {
        Action action = () => new Product(1, "Product name", "Product description", 19.99M, value, "Product image");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid stock value.");
    }

    [Fact]
    public void CreateProduct_NullImageName_ResultObjectValidState()
    {
        Action action = () => new Product(1, "Product name", "Product description", 19.99M, 99, null);
        action.Should()
            .NotThrow<NullReferenceException>();
    }

    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImageName()
    {
        Action action = () => new Product(1, "Product name", "Product description", 19.99M, 99, "Product imageeeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee eeeeeeeeee ");
        action.Should()
            .Throw<CleanArchMvc.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid image name, too long, maximum 250 charecters.");
    }
}
