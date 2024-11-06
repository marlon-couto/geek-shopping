using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.ProductsAPI.Controllers;

public class ProductController : ControllerBase
{
    // GET
    public IActionResult Index()
    {
        return Ok();
    }
}