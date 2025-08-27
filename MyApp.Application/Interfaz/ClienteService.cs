using Microsoft.EntityFrameworkCore;
using MyApp.Domain.DTOs;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;

namespace MyApp.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly LibraryDbContext _context;

        public ClienteService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteResponseDto>> GetAllAsync()
        {
            return await _context.Clientes
                .Select(c => new ClienteResponseDto
                {
                    Id = c.Id,
                    Nombres = c.Nombres,
                    Apellidos = c.Apellidos,
                    NumeroDocumento = c.NumeroDocumento,
                    Telefono = c.Telefono,
                    Email = c.Email,
                    Direccion = c.Direccion,
                    Ubigeo = c.Ubigeo,
                    EnListaNegra = c.EnListaNegra
                }).ToListAsync();
        }

        public async Task<ClienteResponseDto> GetByIdAsync(int id)
        {
            var c = await _context.Clientes.FindAsync(id);
            if (c == null) return null;

            return new ClienteResponseDto
            {
                Id = c.Id,
                Nombres = c.Nombres,
                Apellidos = c.Apellidos,
                NumeroDocumento = c.NumeroDocumento,
                Telefono = c.Telefono,
                Email = c.Email,
                Direccion = c.Direccion,
                Ubigeo = c.Ubigeo,
                EnListaNegra = c.EnListaNegra
            };
        }

        public async Task<int> CreateAsync(ClienteRequestDto dto)
        {
            var cliente = new Cliente
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                NumeroDocumento = dto.NumeroDocumento,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Direccion = dto.Direccion,
                Ubigeo = dto.Ubigeo
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente.Id;
        }

        public async Task<bool> UpdateAsync(int id, ClienteRequestDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            cliente.Nombres = dto.Nombres;
            cliente.Apellidos = dto.Apellidos;
            cliente.NumeroDocumento = dto.NumeroDocumento;
            cliente.Telefono = dto.Telefono;
            cliente.Email = dto.Email;
            cliente.Direccion = dto.Direccion;
            cliente.Ubigeo = dto.Ubigeo;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
