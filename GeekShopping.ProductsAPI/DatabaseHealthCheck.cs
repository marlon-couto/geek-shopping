using GeekShopping.ProductsAPI.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductsAPI;

public class DatabaseHealthCheck
{
    private readonly MySQLContext _dbContext;

    public DatabaseHealthCheck(MySQLContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool IsConnectionSuccessful()
    {
        try
        {
            _dbContext.Database.OpenConnection();
            _dbContext.Database.CloseConnection();
            return true;
        }
        catch
        {
            return false;
        }
    }
}