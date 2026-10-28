using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using empresaStar.Models;
using System.Data;

namespace empresaStar.Repositorio
{
    public class UsuorioRepositorio : IUsuarioRepositorio
    {
        private readonly string _connctionString;

        private readonly PasswordHasher<UsuarioModel> passwordHasher;

        public UsuarioRepositorio(IConfiguration configuration)
        {
            _connctionString = configuration.GetConnectionString("Connection") ?? string.Empty;
            _passwordHasher = new PasswordHasher<UsuarioModel>();
        }

        public async Task<bool> Cadastrar(UsuarioModel usuario, string senhaPura)
        {
            try
            {
                usuario.SenhaHash = _passwordHasher.HashPassword(usuario, senhaPura);

                using var conexao = new MySqlConnection(_connctionString);

                await conexao.OpenAsync();

                var query = "INSERT INTO usuarios (Nome, Email, SenhaHash) VALUES (@Nome, @Email, @SenhaHash)";

                var linhasAfetadas = await comando.ExecuteNonQueryAsync();

                return linhasAfetadas > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro detalhado no MySQL: {ex.Message}");
            }
        }

      public async Task<UsuarioModel?> BuscarEmail(string email)
        {
            using var conexao = new MySqlConnection(_connctionString);
            await conexao.OpenAsync();

            var query = "SELECT Id, Nome, Emai, SenhaHash FROM usuarios WHERE Email = @Email";

            using var comando = new MySqlCommand(query, conexao);

            comando.Parameters.AddWithValue("@Email", email);

            using var reader = await comando.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new UsuarioModel
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Nome = reader.GetString("Nome"),
                    Email = reader.GetString("Email"),
                    SenhaHash = reader.GetString("SenhaHash")
                };
            }
            return null;
        }  
    }
}
