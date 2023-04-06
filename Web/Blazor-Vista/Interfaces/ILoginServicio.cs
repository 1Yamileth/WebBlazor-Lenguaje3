
using Modelos;

namespace Blazor_Vista.Interfaces
{
    public interface ILoginServicio
    {
        Task<bool> ValidarUsuarioAsync(LoginM login);
    }
}
