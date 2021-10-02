using System;
using System.Collections;
using System.Collections.Generic;
using MBBank.Util;

namespace MBBank.Entity
{
    public class Account
    {
        public string AccountNumber { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string PasswordConfirm { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public double Balance { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cccd { get; set; }
        public string CityId { get; set; }
        public int DistrickId { get; set; }
        public string wardId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public DateTime Dob { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        
        public Account()
        {
            AccountNumber = Guid.NewGuid().ToString();
            //Generate giá trị cho created at, updated at, account number,
        }
        
        /*
         * Hàm kiểm tra lỗi dữ liệu nhập vào
         * Nếu lỗi sẽ hiện ra thông báo
         */
        public Dictionary<string, string> CheckValid()
        {
            var errors = new Dictionary<string, string>();// Lưu dữ liệu theo dạng Key - value  
            if (string.IsNullOrEmpty(this.Username))
            {
                errors.Add("username", "Username can not be null or empty");
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                errors.Add("password", "Password can not be null or empty");
            }

            if (!this.Password.Equals(this.PasswordConfirm))
            {
                errors.Add("confirmPassword", "Password and PasswordConfirm are not matched!");
            }

            if (string.IsNullOrEmpty(this.Address))
            {
                errors.Add("address", "address can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.Email))
            {
                errors.Add("email", "email can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.Cccd))
            {
                errors.Add("cccd", "cccd can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.wardId))
            {
                errors.Add("wardId", "wardId can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.CityId))
            {
                errors.Add("cityId", "cityId can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.FirstName))
            {
                errors.Add("firstName", "firstName can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.LastName))
            {
                errors.Add("lastName", "lastName can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.PhoneNumber))
            {
                errors.Add("phoneNumber", "phoneNumber can not be null or empty");
            }
            return errors;
        }
        public override string ToString()
        {
            return $"Username:{Username}, Password:{Password}, PasswordHash:{PasswordHash}";
        }

        public void EncryptPassword()
        {
            //3.1 Tạo muối.
            Salt = HashUtil.RandomString(7);
            //3.2 Băm và trộn password với muối.
            PasswordHash = HashUtil.HashWithSHA1(Password, Salt);
        }

        public void GenerateAccountNumber()
        {
            AccountNumber = Guid.NewGuid().ToString();
        }

        public Dictionary<string, string> CheckValidLogin()
        {
            var errors = new Dictionary<string, string>();// Lưu dữ liệu theo dạng Key - value  
            if (string.IsNullOrEmpty(this.Username))
            {
                errors.Add("username", "Username can not be null or empty");
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                errors.Add("password", "Password can not be null or empty");
            }
            return errors;
        }

        public Dictionary<string, string> CheckValidUpdate()
        {
          var errors = new Dictionary<string, string>();// Lưu dữ liệu theo dạng Key - value  
            if (string.IsNullOrEmpty(this.Username))
            {
                errors.Add("username", "Username can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.Address))
            {
                errors.Add("address", "address can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.Email))
            {
                errors.Add("email", "email can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.Cccd))
            {
                errors.Add("cccd", "cccd can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.wardId))
            {
                errors.Add("wardId", "wardId can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.CityId))
            {
                errors.Add("cityId", "cityId can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.FirstName))
            {
                errors.Add("firstName", "firstName can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.LastName))
            {
                errors.Add("lastName", "lastName can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.PhoneNumber))
            {
                errors.Add("phoneNumber", "phoneNumber can not be null or empty");
            }

            return errors;
        }

        public Dictionary<string, string> CheckValidUpdatePassword()
        {
            var errors = new Dictionary<string, string>();// Lưu dữ liệu theo dạng Key - value  
            if (string.IsNullOrEmpty(this.Password))
            {
                errors.Add("password", "Password can not be null or empty");
            }
            
            if (string.IsNullOrEmpty(this.PasswordConfirm))
            {
                errors.Add("confirmPassword", "ConfirmPassword can not be null or empty");
            }
            
            if (!this.Password.Equals(this.PasswordConfirm))
            {
                errors.Add("confirmPassword", "Password and PasswordConfirm are not matched!");
            }

            return errors;
        }

        public void EncryptPasswordUpdate(string salt)
        {
            PasswordHash = HashUtil.HashWithSHA1(Password, salt);
        }
    }
}