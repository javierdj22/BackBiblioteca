using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<CopiaLibro>> ObtenerProductosAsync()
        {
            return await _productoRepository.GetProductosAsync();
        }

        public async Task<CopiaLibro> ObtenerPorIdAsync(int id)
        {
            return await _productoRepository.GetProductoByIdAsync(id);
        }

        public async Task<CopiaLibro> CrearProductoAsync(CopiaLibro producto)
        {
            return await _productoRepository.AddProductoAsync(producto);
        }

        public async Task<CopiaLibro> ActualizarProductoAsync(CopiaLibro producto)
        {
            return await _productoRepository.UpdateProductoAsync(producto);
        }

        public async Task<CopiaLibro> EliminarProductoAsync(int id)
        {
            return await _productoRepository.DeleteProductoAsync(id);
        }

    }
}
