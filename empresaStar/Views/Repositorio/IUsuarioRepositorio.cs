using empresaStar.Models;

namespace empresaStar.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Task<bool> Cadastrar(UsuarioModel usuario, string senhaPura);
        Task <UsuarioModel?> BuscarEmail (string  email);
    }
}
