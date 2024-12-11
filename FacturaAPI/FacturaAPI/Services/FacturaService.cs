using FacturaApi.Models;
using Newtonsoft.Json;

namespace FacturaApi.Services
{
    public class FacturaService : IFacturaService
    {
        private List<Factura> facturas;

         public FacturaService()
        {
            facturas = new List<Factura>();
            CargarFacturasDesdeArchivo();
        }
        
        public void CargarFacturasDesdeArchivo()
        {
            var json = System.IO.File.ReadAllText("Data/JsonEjemplo.json");
            facturas = JsonConvert.DeserializeObject<List<Factura>>(json);

        } 

        public IEnumerable<Factura> ObtenerFacturasConTotales()
        {
             foreach (var factura in facturas)
             {
                if (factura.DetalleFactura != null)
                {
                    factura.TotalFactura = factura.DetalleFactura.Sum(d => d.TotalProducto);
                }
                else
                {
                    factura.TotalFactura = 0; 
                }
            }
            return facturas; 
        }

        public IEnumerable<Factura> ObtenerFacturasPorRut(double rut)
        {
            return facturas.Where(f => f.RUTComprador == rut).ToList();
        }
 
        public Factura ObtenerCompradorConMasCompras()
        {
            return facturas
                        .GroupBy(f => f.RUTComprador)
                        .OrderByDescending(g => g.Count())
                        .FirstOrDefault()?
                        .FirstOrDefault() ?? new Factura(); 
        }

        public IEnumerable<CompradorTotal> ObtenerCompradoresConTotal()
        {
            return facturas
                .GroupBy(f => f.RUTComprador)
                .Select(g => new CompradorTotal
                {
                    RUTComprador = g.Key,
                    MontoTotal = g.Sum(f => f.TotalFactura)
                })
                .ToList();
        }

        public IEnumerable<Factura> ObtenerFacturasPorComuna(double comuna)
        {
            return facturas.Where(f => f.ComunaComprador == comuna).ToList();
        }
    }
}
