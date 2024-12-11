using FacturaApi.Models;
using FacturaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FacturaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet("facturas")]
        public IActionResult GetFacturasConTotales()
        {
            var facturas = _facturaService.ObtenerFacturasConTotales();
            return Ok(facturas);
        }

        [HttpGet("facturas/{rut}")]
        public IActionResult GetFacturasPorRut(double rut)
        {
            var facturas = _facturaService.ObtenerFacturasPorRut(rut);
            return Ok(facturas);
        }

        [HttpGet("comprador/mayor-compra")]
        public IActionResult GetCompradorConMasCompras()
        {
            var comprador = _facturaService.ObtenerCompradorConMasCompras();
            return Ok(comprador);
        }

        [HttpGet("compradores")]
        public IActionResult GetCompradoresConTotal()
        {
            var compradores = _facturaService.ObtenerCompradoresConTotal();
            return Ok(compradores);
        }

        [HttpGet("facturas/comuna/{comuna}")]
        public IActionResult GetFacturasPorComuna(double comuna)
        {
            var facturas = _facturaService.ObtenerFacturasPorComuna(comuna);
            return Ok(facturas);
        }
    }
}

