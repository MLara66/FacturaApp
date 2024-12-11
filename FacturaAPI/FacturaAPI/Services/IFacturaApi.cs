using FacturaApi.Models;

namespace FacturaApi.Services
{
    public interface IFacturaService
    {
        void CargarFacturasDesdeArchivo();
        IEnumerable<Factura> ObtenerFacturasConTotales();
        IEnumerable<Factura> ObtenerFacturasPorRut(double rut);
        Factura ObtenerCompradorConMasCompras();
        IEnumerable<CompradorTotal> ObtenerCompradoresConTotal();
        IEnumerable<Factura> ObtenerFacturasPorComuna(double comuna);
    }
}
