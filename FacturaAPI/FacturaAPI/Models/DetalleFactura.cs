namespace FacturaAPI.Models
{
    public class DetalleFactura
    {
        public double CantidadProducto { get; set; }
        public Producto Producto { get; set; } = new Producto();
        public double TotalProducto { get; set; }
    }
}
