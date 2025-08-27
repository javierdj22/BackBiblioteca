using MyApp.Domain.Entities;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Domain.Repositories;
using System.Data;

public class ProductoRepository : IProductoRepository
{
    private readonly IServiceProvider _serviceProvider;

    public ProductoRepository(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<IEnumerable<CopiaLibro>> GetProductosAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();

        var sql = "SELECT * FROM productos";
        return await connection.QueryAsync<CopiaLibro>(sql);
    }

    public async Task<CopiaLibro> GetProductoByIdAsync(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();

        var sql = "SELECT * FROM productos WHERE id = @Id";
        return await connection.QueryFirstOrDefaultAsync<CopiaLibro>(sql, new { Id = id });
    }

    public async Task<CopiaLibro> AddProductoAsync(CopiaLibro producto)
    {
        using var scope = _serviceProvider.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();

        var sql = @"
            INSERT INTO productos (name, price, category)
            OUTPUT INSERTED.*
            VALUES (@Name, @Price, @Category)";

        return await connection.QueryFirstOrDefaultAsync<CopiaLibro>(sql, producto);
    }

    public async Task<CopiaLibro> UpdateProductoAsync(CopiaLibro producto)
    {
        using var scope = _serviceProvider.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();

        var sql = @"
            UPDATE productos
            SET name = @Name, price = @Price, category = @Category
            OUTPUT INSERTED.*
            WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<CopiaLibro>(sql, producto);
    }

    public async Task<CopiaLibro> DeleteProductoAsync(int id)
    {
        using var scope = _serviceProvider.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<IDbConnection>();

        var sql = @"
            DELETE FROM productos
            OUTPUT DELETED.*
            WHERE id = @Id";

        return await connection.QueryFirstOrDefaultAsync<CopiaLibro>(sql, new { Id = id });
    }
}
