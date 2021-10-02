using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using MBBank.Util;
using Org.BouncyCastle.Asn1.Crmf;

namespace MBBank.Entity
{
    public class Admin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeletedAt { get; set; }

        public Admin()
        {
            this.CreatedAt = DateTime.Now;
        }
        
        public override string ToString()
        {
            return $"Username:{UserName}, Password:{Password}, PasswordHash:{PasswordHash}";
        }

        public void EncryptPassword()
        {
            //3.1 Tạo muối.
            Salt = HashUtil.RandomString(7);
            //3.2 Băm và trộn password với muối.
            PasswordHash = HashUtil.HashWithSHA1(Password, Salt);
        }

        public Dictionary<string, string> CheckValid()
        {
            var errors = new Dictionary<string, string>();// Lưu dữ liệu theo dạng Key - value  
            if (string.IsNullOrEmpty(this.UserName))
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
            
            if (string.IsNullOrEmpty(this.Phone))
            {
                errors.Add("phone", "phone can not be null or empty!");
            }
            return errors;
        }

        public Dictionary<string, string> CheckValidLogin()
        {
            var errors = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(this.UserName))
            {
                errors.Add("username", "Username can not be null or empty");
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                errors.Add("password", "Password can not be null or empty");
            }

            return errors;
        }
    }
}