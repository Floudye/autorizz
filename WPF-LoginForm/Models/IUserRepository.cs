using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LoginForm.Models
{
    public interface IUserRepository
    {
        void EditUser(string newusername, string newpassword, string newname, string newaccesslvl);
        void DeleteUsername(int Id);
        bool AuthenticateUser(NetworkCredential credential);
        void Add(string Username, string Password, string Name, string accesslvl);
        void Edit(UserModel userModel);
        void Remove(int id);
        void CreateUser(string login, string password, string acceslvl);
        bool logcheck(string Username);
        UserModel GetById(int id);

        UserModel GetByUsername(string username);
        IEnumerable<UserModel> GetByAll();
        //...
    }
}
