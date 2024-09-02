using Demomvc.Controllers;
using Demomvc.Models;

namespace Demomvc.Repository
{
    public interface ILoginRepository
    {
        int UserCredentials(Login login);

        string InsertUser(Login login);


        string Updateuser(Login login);
        string Deleteuser(Login login);
        string Getuser(Login login);
        string Searchuser(Login login);
    }
    } 
