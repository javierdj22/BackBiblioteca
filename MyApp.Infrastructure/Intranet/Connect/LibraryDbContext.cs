
using System.Data;

public class DatabaseService
{
    private readonly IDbConnection _context;

    public DatabaseService(IDbConnection context)
    {
        _context = context;
    }
}
