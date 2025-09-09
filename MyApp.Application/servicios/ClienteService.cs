using Microsoft.Extensions.Logging;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository, ILogger<ClienteService> logger)
        {
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        //public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        //{
        //    //var clientes = await _clienteRepository.GetAllAsync();
        //    //return clientes.Select(MapToDto);

        //    var clientes = await _clienteRepository.GetAllAsync();
        //    var usuarios = await _usuarioRepository.GetAllNpgAsync();

        //    var clientesDto = clientes.Select(MapToDto);
        //    var usuariosDto = usuarios.Select(MapUsuarioToDto);
        //    return clientesDto.Concat(usuariosDto);
        //}
        public async Task<IEnumerable<ClienteDto>> GetAllAsync()
        {
            var clientesDto = new List<ClienteDto>();

            // SQL Server
            try
            {
                var clientes = await _clienteRepository.GetAllAsync();
                clientesDto.AddRange(clientes.Select(MapToDto));
            }
            catch (Exception ex)
            {
                clientesDto.Add(new ClienteDto
                {
                    Id = 0,
                    Nombres = "Error",
                    Apellidos = "SQL Server",
                    Fuente = "sql",
                    ErrorMensaje = $"Error SQL: {ex.Message}"
                });
            }

            // PostgreSQL
            try
            {
                var usuarios = await _usuarioRepository.GetAllNpgAsync();
                clientesDto.AddRange(usuarios.Select(MapUsuarioToDto));
            }
            catch (Exception ex)
            {
                clientesDto.Add(new ClienteDto
                {
                    Id = 0,
                    Nombres = "Error",
                    Apellidos = "PostgreSQL",
                    Fuente = "postgres",
                    ErrorMensaje = $"Error PostgreSQL: {ex.Message}"
                });
            }

            return clientesDto;
        }


        public async Task<ClienteDto?> GetByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente == null)
                return null;

            return MapToDto(cliente);
        }

        public async Task<int> CreateAsync(ClienteDto dto)
        {
            var entity = MapToEntity(dto);
            entity.Id = 0;
            entity.EnListaNegra = false;

            return await _clienteRepository.CreateAsync(entity);
        }

        public async Task<bool> UpdateAsync(int id, ClienteDto dto)
        {
            var existingEntity = await _clienteRepository.GetByIdAsync(id);
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

            return await _clienteRepository.UpdateAsync(id, existingEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _clienteRepository.DeleteAsync(id);
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
                EnListaNegra = cliente.EnListaNegra,
                Fuente = "sql"
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
        private ClienteDto MapUsuarioToDto(Usuarios usuario)
        {
            return new ClienteDto
            {
                Id = usuario.id,
                Nombres = usuario.username,
                Apellidos = usuario.password_hash,
                Fuente = "postgres"
            };
        }
    }
}
