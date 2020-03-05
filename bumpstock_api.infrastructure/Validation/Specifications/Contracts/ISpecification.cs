using bumpstock_api.infrastructure.Validation.Rules;

namespace bumpstock_api.infrastructure.Validation.Specifications
{
    public interface ISpecification
    {
        Contract Contract { get; }
    }
}