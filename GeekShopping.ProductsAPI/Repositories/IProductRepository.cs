using GeekShopping.ProductsAPI.Data.ValueObjects;

namespace GeekShopping.ProductsAPI.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductVO>> FindAllAsync();
    Task<ProductVO> FindByIdAsync(long id);
    Task<ProductVO> CreateAsync(ProductVO product);
    Task<ProductVO> UpdateAsync(ProductVO product);
    Task<bool> DeleteAsync(long id);
}