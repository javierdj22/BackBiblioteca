using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientes = await _repository.GetAllAsync();
            return clientes.Select(MapToDto);
        }

        public async Task<ClienteDto?> GetByIdAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            if (cliente == null)
                return null;

            return MapToDto(cliente);
        }

        public async Task<int> CreateAsync(ClienteDto dto)
        {
            var entity = MapToEntity(dto);
            entity.Id = 0;
            entity.EnListaNegra = false;

            return await _repository.CreateAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ClienteDto dto)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
                return false;

            existingEntity.Nombres = dto.Nombres;
            existingEntity.Apellidos = dto.Apellidos;
            existingEntity.NumeroDocumento = dto.NumeroDocumento;
            existingEntity.Telefono = dto.Telefono;
            existingEntity.Email = dto.Email;
            existingEntity.Direccion = dto.Direccion;
            existingEntity.Ubigeo = dto.Ubigeo;
            existingEntity.EnListaNegra = dto.EnListaNegra;

            return await _repository.UpdateAsync(id, existingEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private ClienteDto MapToDto(Cliente cliente)
        {
            return new ClienteDto
            {
                Id = cliente.Id,
                Nombres = cliente.Nombres,
                Apellidos = cliente.Apellidos,
                NumeroDocumento = cliente.NumeroDocumento,
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                Direccion = cliente.Direccion,
                Ubigeo = cliente.Ubigeo,
                EnListaNegra = cliente.EnListaNegra
            };
        }

        private Cliente MapToEntity(ClienteDto dto)
        {
            return new Cliente
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                NumeroDocumento = dto.NumeroDocumento,
                Telefono = dto.Telefono,
                Email = dto.Email,
                Direccion = dto.Direccion,
                Ubigeo = dto.Ubigeo,
                EnListaNegra = dto.EnListaNegra
            };
        }
    }
}
