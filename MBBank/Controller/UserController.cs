using System;
using System.Collections.Generic;
using MBBank.Entity;
using MBBank.Model;
using MBBank.Util;

namespace MBBank.Controller
{
    public class UserController : IUserController
    {
        private IAccountModel _model;

        public UserController()
        {
            _model = new AccountModel();
        }

        /*
         * Đăng ký tài khoản ngân hàng
         */
        public Account Register()
        {
            bool check; // default = false.  
            Account account = null;
            do
            {
                account = GetAccountInformation(); //1. Nhập dữ liệu.
                var errors = account.CheckValid(); //2. Validate dữ liệu.
                check = errors.Count > 0;
                if (CheckExistUsername(account.Username))
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

            // Lỗi trùng tài khoản không thuộc về người dùng
            while (CheckExistAccountNumber(account.AccountNumber))
            {
                account.GenerateAccountNumber(); // trùng thì generate lại
            }

            // Check unique username
            var existingUsername = _model.FindByUsername(account.Username);
            Console.WriteLine(account.ToString()); // In ra thông tin vừa nhập
            account.EncryptPassword(); //3. Mã hóa.
            var result = _model.Save(account); //4. Lưu vào database 
            if (result == null) return null;
            Console.WriteLine("Register success!");
            return result;
        }

        private bool CheckExistUsername(string username)
        {
            return _model.FindByUsername(username) != null;
        }

        private bool CheckExistAccountNumber(string accountAccountNumber)
        {
            return _model.FindByAccountNumber(accountAccountNumber) != null;
        }

        private Account GetAccountInformation()
        {
            var account = new Account();
            Console.WriteLine("Please enter Username: ");
            account.Username = Console.ReadLine();
            Console.WriteLine("Please enter Password:");
            account.Password = Console.ReadLine();
            Console.WriteLine("Please confirm password: ");
            account.PasswordConfirm = Console.ReadLine();
            Console.WriteLine("Please enter email: ");
            account.Email = Console.ReadLine();
            Console.WriteLine("Please enter your first name: ");
            account.FirstName = Console.ReadLine();
            Console.WriteLine("Please enter your last name: ");
            account.LastName = Console.ReadLine();
            Console.WriteLine("Please enter your cccd: ");
            account.Cccd = Console.ReadLine();
            Console.WriteLine("Please enter your phone number: ");
            account.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Please enter your cityId: ");
            account.CityId = Console.ReadLine();
            Console.WriteLine("Please enter your districkId: ");
            account.DistrickId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter your birthday: ");
            account.Dob = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Please enter your address: ");
            account.Address = Console.ReadLine();
            Console.WriteLine("Please enter your wardId: ");
            account.wardId = Console.ReadLine();
            return account;
        }

        public void ShowBankInformation()
        {
            throw new System.NotImplementedException();
        }

        /*
         * Đăng nhập vào tài khoản ngân hàng
         */
        public Account Login()
        {
            Account account = null;
            bool check; // default = false. 
            do
            {
                account = GetLoginInformation();
                var errors = account.CheckValidLogin();
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
            var accountExisting = _model.FindByUsername(account.Username);
            if (accountExisting != null &&
                HashUtil.ComparePasswordHash(account.Password, accountExisting.Salt, accountExisting.PasswordHash))
            {
                Console.WriteLine("Login success!");
                return accountExisting;
            }
            else
            {
                Console.WriteLine("Login fails!");
            }

            return null;
        }

        private Account GetLoginInformation()
        {
            var account = new Account();
            Console.WriteLine("Please enter your name: ");
            account.Username = Console.ReadLine();
            Console.WriteLine("Please enter your password: ");
            account.Password = Console.ReadLine();
            return account;
        }

        /*
         * Rút tiền trong tài khoản
         */
        public TransactionHistory WithDraw(string accountNumber)
        {
            double mount;
            Account account = new Account();
            Console.WriteLine("Nhap so tien muon rut.");
            var balance = _model.FindByAccountNumber(accountNumber).Balance;
            mount = Convert.ToDouble(Console.ReadLine());
            if (balance > mount)
            {
                balance -= mount;
                Console.WriteLine("Rut tien thanh cong, so tien ban da rut la: {0} VND", mount);
                var result = _model.Withdraw(accountNumber, balance);
                return result;
            }
            else
            {
                Console.WriteLine("Giao dich khong thanh cong, do so du cua ban khong du de thuc hien");
            }
            return null;
        }

        /*
         * Gửi tiền vào ngân hàng
         */
        public TransactionHistory Deposit(string accountNumber)
        {
            double mount;
            Account account = new Account();
            Console.WriteLine("Nhap so tien muon gui vao tai khoan.");
            var balance = _model.FindByAccountNumber(accountNumber).Balance;
            mount = Convert.ToDouble(Console.ReadLine());
            if (mount > 0)
            {
                balance += mount;
                mount = 0;
                Console.WriteLine("Nap thanh cong!");
                var result = _model.Deposit(accountNumber, balance);
                return result;
            }
            else
            {
                Console.WriteLine("Khong nap duoc tien vao tai khoan");
            }

            return null;
        }
            //Thuc hien chuen tiem
        public void Transfer(string accountNumber)
        {
            double mount;
            var account = new Account();
            var transactionHistory = new TransactionHistory();
            var balance = _model.FindByAccountNumber(accountNumber).Balance;
            Console.WriteLine("Nhap so tien muon chuyen.");
            mount = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Nhap so tai khoan nguoi nhan:");
            transactionHistory.ReceiverAccountNumber = Console.ReadLine();

            if (mount > 0)
            {
                balance -= mount;
                Console.WriteLine("Nap thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong nap duoc tien vao tai khoan");
            }
        }
        public Account UpdateInformation(String accountNumber)
        {
            bool check; // default = false.  
            Account accountUpdate = null;
            do
            {
                accountUpdate = GetUpdateInformation(); //1. Nhập dữ liệu.
                var errors = accountUpdate.CheckValidUpdate(); //2. Validate dữ liệu.
                check = errors.Count > 0;
                // In ra thông báo khi thông tin nhập vào lỗi
                if (errors.Count <= 0) continue;
                foreach (var error in errors)
                {
                    Console.WriteLine(error.Value);
                }

                Console.WriteLine("Please try again enter account information!");
            } while (check);

            //var existingAccount = _model.FindByUsername(account.Username);
            var result = _model.Update(accountNumber, accountUpdate); //4. Lưu vào database 
            if (result == null) return null;
            Console.WriteLine("Update success!");
            return result;
        }

        public Account UpdatePassword(string accountNumber, string salt)
        {
            bool check; // default = false.  
            Account accountUpdate = null;
            do
            {
                accountUpdate = GetUpdatePassword(); //1. Nhập dữ liệu.
                var errors = accountUpdate.CheckValidUpdatePassword(); //2. Validate dữ liệu.
                check = errors.Count > 0;
                // In ra thông báo khi thông tin nhập vào lỗi
                if (errors.Count <= 0) continue;
                foreach (var error in errors)
                {
                    Console.WriteLine(error.Value);
                }

                Console.WriteLine("Please try again enter account information!");
            } while (check);

            accountUpdate.EncryptPasswordUpdate(salt); //3. Mã hóa.
            //var existingAccount = _model.FindByUsername(account.Username);
            var result = _model.UpdatePassword(accountNumber, accountUpdate); //4. Lưu vào database 
            if (result == null) return null;
            Console.WriteLine("Update success!");
            return result;
        }

        private static Account GetUpdatePassword()
        {
            var account = new Account();
            Console.WriteLine("Please enter new password: ");
            account.Password = Console.ReadLine();
            Console.WriteLine("Please enter confirm new password: ");
            account.PasswordConfirm = Console.ReadLine();
            return account;
        }

        private Account GetUpdateInformation()
        {
            var account = new Account();
            Console.WriteLine("Please enter Username: ");
            account.Username = Console.ReadLine();
            Console.WriteLine("Please enter email: ");
            account.Email = Console.ReadLine();
            Console.WriteLine("Please enter your first name: ");
            account.FirstName = Console.ReadLine();
            Console.WriteLine("Please enter your last name: ");
            account.LastName = Console.ReadLine();
            Console.WriteLine("Please enter your cccd: ");
            account.Cccd = Console.ReadLine();
            Console.WriteLine("Please enter your phone number: ");
            account.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Please enter your cityId: ");
            account.CityId = Console.ReadLine();
            Console.WriteLine("Please enter your districkId: ");
            account.DistrickId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter your birthday: ");
            account.Dob = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Please enter your address: ");
            account.Address = Console.ReadLine();
            Console.WriteLine("Please enter your wardId: ");
            account.wardId = Console.ReadLine();
            return account;
        }

        public void CheckInformation()
        {
            
        }

        public void LockTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void UnlockTransaction()
        {
            throw new System.NotImplementedException();
        }

        public void CheckTransactionHistory()
        {
            throw new System.NotImplementedException();
        }
    }
}