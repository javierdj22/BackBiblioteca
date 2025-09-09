using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using MyApp.Infrastructure.Context;

namespace MyApp.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DapperContext _context;

        public UsuarioRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuarios>> GetAllNpgAsync()
        {
            const string query = "SELECT * FROM Usuarios";

            using (var connection = _context.CreatePostgresConnection())
            {
                return await connection.QueryAsync<Usuarios>(query);
            }
        }
    }
}
