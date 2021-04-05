using System.Threading.Tasks;
using ProjectAPI_Selise.Models;

namespace ProjectAPI_Selise.Repository
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string username);
        Task<bool> register(UserRegistrationModel userRegistrationModel);
    }
}