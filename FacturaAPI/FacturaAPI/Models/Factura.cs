using FacturaAPI.Models;

namespace FacturaApi.Models
{
    public class Factura
    {
        public double NumeroDocumento { get; set; }
        public double RUTVendedor { get; set; }
        public string DvVendedor { get; set; } = string.Empty; 
        public double RUTComprador { get; set; }
        public string DvComprador { get; set; } = string.Empty;
        public string DireccionComprador { get; set; } = string.Empty;
        public double ComunaComprador { get; set; }
        public double RegionComprador { get; set; }
        public double TotalFactura { get; set; }
        public List<DetalleFactura>? DetalleFactura { get; set; }
    }
}

