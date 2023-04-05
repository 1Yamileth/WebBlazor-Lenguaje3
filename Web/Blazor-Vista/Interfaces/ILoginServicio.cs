
namespace Blazor_Vista.Interfaces
{
    public interface ILoginServicio
    {
        Task<bool> ValidarUsuarioAsync(Login login);
    }
}
