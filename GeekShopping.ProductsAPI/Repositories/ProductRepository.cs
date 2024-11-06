using AutoMapper;
using GeekShopping.ProductsAPI.Data.ValueObjects;
using GeekShopping.ProductsAPI.Models;
using GeekShopping.ProductsAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductsAPI.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MySQLContext _context;
    private readonly IMapper _mapper;

    public ProductRepository(MySQLContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductVO>> FindAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        return _mapper.Map<List<ProductVO>>(products);
    }

    public async Task<ProductVO> FindByIdAsync(long id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> CreateAsync(ProductVO productVo)
    {
        var product = _mapper.Map<Product>(productVo);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<ProductVO> UpdateAsync(ProductVO productVo)
    {
        var product = _mapper.Map<Product>(productVo);
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductVO>(product);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}