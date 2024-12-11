namespace FacturaApi.Tests;


using FacturaApi.Models;
using FacturaApi.Services;
using Xunit;
using System.Collections.Generic;
using System.Linq;

public class FacturaServiceTests
{
    private readonly FacturaService _service;

    public FacturaServiceTests()
    {
        _service = new FacturaService();
        _service.CargarFacturasDesdeArchivo();
    }

    [Fact]
    public void ObtenerFacturasConTotales_DeberiaCalcularTotalesCorrectamente()
    {
        var facturas = _service.ObtenerFacturasConTotales();
        Assert.All(facturas, factura =>
        {
             var totalCalculado = factura.DetalleFactura?.Sum(d => d.TotalProducto) ?? 0;
            Assert.Equal(totalCalculado, factura.TotalFactura);
        });
    }

    [Fact]
    public void ObtenerFacturasPorRut_DeberiaDevolverFacturasCorrectas()
    {

        double rut = 11488103;

        var resultado = _service.ObtenerFacturasPorRut(rut);
        Assert.NotEmpty(resultado);
        Assert.All(resultado, factura => Assert.Equal(rut, factura.RUTComprador));
    }

    [Fact]
    public void ObtenerCompradorConMasCompras_DeberiaDevolverElCorrecto()
    {

        var comprador = _service.ObtenerCompradorConMasCompras();
        Assert.NotNull(comprador);
        Assert.True(comprador.DetalleFactura?.Any() ?? false);
    }
    [Fact]
    public void ObtenerCompradoresConTotal_DeberiaDevolverListaCorrecta()
    {
        var compradores = _service.ObtenerCompradoresConTotal();

        Assert.NotEmpty(compradores); 
        Assert.All(compradores, comprador =>
        {
            Assert.True(comprador.MontoTotal >= 0, "El monto total debe ser mayor o igual a 0");
        });
    }


    [Fact]
    public void ObtenerFacturasPorComuna_DeberiaDevolverFacturasCorrectas()
    {
 
        double comuna = 65;

        var resultado = _service.ObtenerFacturasPorComuna(comuna);
        Assert.NotEmpty(resultado);
        Assert.All(resultado, factura => Assert.Equal(comuna, factura.ComunaComprador));
    }

}


