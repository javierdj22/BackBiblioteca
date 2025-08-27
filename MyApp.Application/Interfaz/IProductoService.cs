
using MyApp.Domain.Entities;

public interface IProductoService
{
    Task<IEnumerable<CopiaLibro>> ObtenerProductosAsync();
    Task<CopiaLibro> ObtenerPorIdAsync(int id);
    Task<CopiaLibro> CrearProductoAsync(CopiaLibro producto);
    Task<CopiaLibro> ActualizarProductoAsync(CopiaLibro producto);
    Task<CopiaLibro> EliminarProductoAsync(int id);
}
