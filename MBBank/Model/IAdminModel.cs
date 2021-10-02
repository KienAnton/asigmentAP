using System.Collections.Generic;
using MBBank.Entity;

namespace MBBank.Model
{
    public interface IAdminModel
    {
        Admin Save(Admin admin);
        bool Update(string id, Admin updateAdmin);
        bool Delete(string id);
        Admin FindByUsername(string username);
        List<Admin> FindAll(int page, int limit);
    }
}