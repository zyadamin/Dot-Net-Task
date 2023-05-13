using System.Threading.Tasks;
using task.Models;

namespace task.Data
{
    public interface IAuthRepository
    {
         Task<Person> register(Person person);
         Task<bool> resetUserPassword(Person person);
         Task<Person> retrieveUserData(Person person);
         
    }

}