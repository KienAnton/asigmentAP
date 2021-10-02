using System;
using MBBank.Entity;
using MBBank.Model;
using MBBank.Util;

namespace MBBank.Controller
{
    public class AdminController: IAdminController
    {

        private IAdminModel _adminModel;

        public AdminController()
        {
            _adminModel = new AdminModel();
        }
        
        public Admin Register()
        {
            bool check;// default = false.  
            Admin admin = null;
            do
            {
                admin = GetAdminInformation();     //1. Nhập dữ liệu.
                var errors = admin.CheckValid();    //2. Validate dữ liệu.
                check = errors.Count > 0;
                if (CheckExistUsername(admin.UserName))
                {
                    errors.Add("username", "Duplicate username, please choose another!");
                }

                check = errors.Count > 0;
                // In ra thông báo khi thông tin nhập vào lỗi
                if (errors.Count <= 0) continue;
                foreach (var error in errors)
                {
                    Console.WriteLine(error.Value);
                }

                Console.WriteLine("Please try again enter account information!");
            } while (check);
            var existingUsername = _adminModel.FindByUsername(admin.UserName);
            admin.EncryptPassword(); //3. Mã hóa.
            var result = _adminModel.Save(admin);//4. Lưu vào database 
            if (result == null) return null;
            Console.WriteLine("Register success!");
            return null;
        }

        private bool CheckExistUsername(string username)
        {
            return _adminModel.FindByUsername(username) != null;
        }
        private static Admin GetAdminInformation()
        {
            var admin = new Admin();
            Console.WriteLine("Please enter Username: ");
            admin.UserName = Console.ReadLine();
            Console.WriteLine("Please enter Password:");
            admin.Password = Console.ReadLine();
            Console.WriteLine("Please confirm password: ");
            admin.PasswordConfirm = Console.ReadLine();
            Console.WriteLine("Please enter your full name: ");
            admin.FullName = Console.ReadLine();
            Console.WriteLine("Please enter your phone number: ");
            admin.Phone = Console.ReadLine();
            return admin;
        }

        public Admin Login()
        {
            Admin admin = null;
            bool check;// default = false. 
            do
            {
                admin = GetLoginInformation();
                var errors = admin.CheckValidLogin();
                check = errors.Count > 0;
                // Check dữ liệu phía Client Nhập vào.
                // In ra thông báo khi thông tin nhập vào lỗi
                if (errors.Count <= 0) continue;
                foreach (var error in errors)
                {
                    Console.WriteLine(error.Value);
                }
                Console.WriteLine("Please reenter login information!");
            } while (check);
            
            // Check dữ liệu trong database 
            var adminExisting = _adminModel.FindByUsername(admin.UserName);
            if (adminExisting != null && HashUtil.ComparePasswordHash(admin.Password, adminExisting.Salt, adminExisting.PasswordHash))
            {
                Console.WriteLine("Found account with username: " + admin.UserName);
                Console.WriteLine(adminExisting.ToString());
                HashUtil.HashWithSHA1(admin.Password, adminExisting.Salt);
            }
            else
            {
                Console.WriteLine("Login fails!");
            }
            return null;
        }

        private Admin GetLoginInformation()
        {
            var admin = new Admin();
            Console.WriteLine("Please enter Username: ");
            admin.UserName = Console.ReadLine();
            Console.WriteLine("Please enter Password:");
            admin.Password = Console.ReadLine();
            return admin;
        }

        public void ShowListAdmin()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAdmin()
        {
            throw new System.NotImplementedException();
        }

        public void ShowListUser()
        {
            throw new System.NotImplementedException();
        }

        public void ApproveUser()
        {
            throw new System.NotImplementedException();
        }

        public void LockUser()
        {
            throw new System.NotImplementedException();
        }

        public void UnlockUser()
        {
            throw new System.NotImplementedException();
        }

        public void FindUserByAccountNumber()
        {
            throw new System.NotImplementedException();
        }

        public void SearchUserByPhone()
        {
            throw new System.NotImplementedException();
        }

        public void SearchUserByIdentityNumber()
        {
            throw new System.NotImplementedException();
        }

        public void SearchTransactionHistory()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAccount()
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePassword()
        {
            throw new System.NotImplementedException();
        }
    }
}