using System.Threading.Tasks;
using task.Models;

namespace task.Data
{
    public interface IAuthRepository
    {
         Task<Response> register(Person person);
         Task<Response> resetUserPassword(passwordReset person );
         Task<Response> retrieveUserData(userLogin Person);
         
    }

}