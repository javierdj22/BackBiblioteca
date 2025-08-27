using MyApp.Domain.Entities;

namespace MyApp.Domain.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<CopiaLibro>> GetProductosAsync();
        Task<CopiaLibro> GetProductoByIdAsync(int id);
        Task<CopiaLibro> AddProductoAsync(CopiaLibro producto);
        Task<CopiaLibro> UpdateProductoAsync(CopiaLibro producto);
        Task<CopiaLibro> DeleteProductoAsync(int id);
    }
}
