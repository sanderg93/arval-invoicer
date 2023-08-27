using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Arval.Invoicer.API.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class InvoiceController : ControllerBase
{
    private readonly ILogger<InvoiceController> _logger;

    public InvoiceController(ILogger<InvoiceController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Task<DateTime> Get(DateTime startDate, DateTime endDate)
    {
        return Task.FromResult(startDate);
    }
}
