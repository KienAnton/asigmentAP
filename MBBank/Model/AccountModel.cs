using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MBBank.Entity;
using MBBank.Util;
using MySql.Data.MySqlClient;

namespace MBBank.Model
{
    public class AccountModel: IAccountModel
    {
        private readonly string _insertCommand = $"INSERT INTO accounts " +
                                                 $"(account_number, username, password_hash, salt, email, firstname, lastname, cccd, phonenumber, cityId, districkId, wardId, dob) " +
                                                 $"VALUES (@account_number, @username, @password_hash, @salt, @email, @firstname, lastname, @cccd, @phonenumber, @cityId, @districkId, @wardId, @dob)";
        private readonly string _selectByIdCommand = $"SELECT * FROM accounts WHERE account_number = @account_number";
        private readonly string _selectByUsernameCommand = $"SELECT * FROM accounts WHERE username = @username";
        private readonly string _updateCommand = $"UPDATE accounts SET username = @username, email = @email, firstname = @firstname, lastname = @lastname, cccd = @cccd, phonenumber = @phonenumber, " +
                                                 $"cityId = @cityId, districkId = @districkId, wardId = @wardId, Dob = @Dob WHERE account_number = @account_number";
        private readonly string _updatePasswordCommand = $"UPDATE accounts SET password_hash = @password_hash WHERE account_number = @account_number";
        private readonly string _depositCommand = $"UPDATE accounts SET balance = @balance WHERE account_number = @account_number";
        private readonly string _withDrawCommand = $"UPDATE accounts SET balance = @balance WHERE account_number = @account_number";
        private readonly string _truyCommand = $"UPDATE accounts SET balance = @balance WHERE account_number = @account_number";
        public Account Save(Account account)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    //cnn.Open();
                    var mySqlCommand =
                        new MySqlCommand(_insertCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@account_number", account.AccountNumber);
                    mySqlCommand.Parameters.AddWithValue("@username", account.Username);
                    mySqlCommand.Parameters.AddWithValue("@password_hash", account.PasswordHash);
                    mySqlCommand.Parameters.AddWithValue("@salt", account.Salt);
                    mySqlCommand.Parameters.AddWithValue("@email", account.Email);
                    mySqlCommand.Parameters.AddWithValue("@firstname", account.FirstName);
                    mySqlCommand.Parameters.AddWithValue("@lastname", account.LastName);
                    mySqlCommand.Parameters.AddWithValue("@cccd", account.Cccd);
                    mySqlCommand.Parameters.AddWithValue("@phonenumber", account.PhoneNumber);
                    mySqlCommand.Parameters.AddWithValue("@cityId", account.CityId);
                    mySqlCommand.Parameters.AddWithValue("@districkId", account.DistrickId);
                    mySqlCommand.Parameters.AddWithValue("@wardId", account.wardId);
                    mySqlCommand.Parameters.AddWithValue("@dob", account.Dob);
                    mySqlCommand.Prepare();
                    var result = mySqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return account;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }

        public Account Update(string accountNumber, Account updateAccount)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    //cnn.Open();
                    var mySqlCommand =
                        new MySqlCommand(_updateCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@username", updateAccount.Username);
                    mySqlCommand.Parameters.AddWithValue("@email", updateAccount.Email);
                    mySqlCommand.Parameters.AddWithValue("@firstname", updateAccount.FirstName);
                    mySqlCommand.Parameters.AddWithValue("@lastname", updateAccount.LastName);
                    mySqlCommand.Parameters.AddWithValue("@cccd", updateAccount.Cccd);
                    mySqlCommand.Parameters.AddWithValue("@phonenumber", updateAccount.PhoneNumber);
                    mySqlCommand.Parameters.AddWithValue("@cityId", updateAccount.CityId);
                    mySqlCommand.Parameters.AddWithValue("@districkId", updateAccount.DistrickId);
                    mySqlCommand.Parameters.AddWithValue("@wardId", updateAccount.wardId);
                    mySqlCommand.Parameters.AddWithValue("@dob", updateAccount.Dob);
                    mySqlCommand.Parameters.AddWithValue("@account_number", accountNumber);
                    mySqlCommand.Parameters.AddWithValue("@updateAt", updateAccount.UpdatedAt);
                    mySqlCommand.Prepare();
                    var result = mySqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return updateAccount;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }

        public bool Delete(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public Account FindByAccountNumber(string accountNumber)
        {
            try
            {
                using ( var cnn = ConnectionHelper.GetInstance())
                {
                    //cnn.Open();
                    var sqlCmd = new MySqlCommand(_selectByIdCommand, cnn);
                    sqlCmd.Parameters.AddWithValue("@account_number", accountNumber);
                    sqlCmd.Prepare();
                    using (var reader = sqlCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var account = new Account()
                            {
                                AccountNumber = reader.GetString("account_number"),
                                Username = reader.GetString("username"),
                                PasswordHash = reader.GetString("password_hash"),
                                Salt = reader.GetString("salt"),
                                Balance = reader.GetDouble("balance")
                            };
                            return account;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }
        

        public Account FindByUsername(string username)
        {
            try
            {
                using ( var cnn = ConnectionHelper.GetInstance())
                {
                    //cnn.Open();
                    var sqlCmd = new MySqlCommand(_selectByUsernameCommand, cnn);
                    sqlCmd.Parameters.AddWithValue("@username", username);
                    sqlCmd.Prepare();
                    var reader = sqlCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        var account = new Account()
                        {
                            AccountNumber = reader.GetString("account_number"),
                            Username = reader.GetString("username"),
                            PasswordHash = reader.GetString("password_hash"),
                            Salt = reader.GetString("salt")
                        };
                        return account;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }

        public List<Account> FindAll(int page, int limit)
        {
            throw new NotImplementedException();
        }

        public List<Account> SearchByPhone(string keyword, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public List<Account> SearchByIdentityNumber(string keyword, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public List<TransactionHistory> FindTransactionHistoryByAccountNumber(string accountNumber, DateTime startTime, DateTime endTime, int page,
            int limit)
        {
            throw new NotImplementedException();
        }

        public TransactionHistory Deposit(string accountNumber, double balance)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    //cnn.Open();
                    var mySqlCommand =
                        new MySqlCommand(_depositCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@balance", balance);
                    mySqlCommand.Parameters.AddWithValue("@account_number", accountNumber);
                    mySqlCommand.Prepare();
                    var result = mySqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine("Success action");
                        ;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }

        public TransactionHistory Withdraw(string accountNumber, double balance)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    //cnn.Open();
                    var mySqlCommand =
                        new MySqlCommand(_withDrawCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@balance", balance);
                    mySqlCommand.Parameters.AddWithValue("@account_number", accountNumber);
                    mySqlCommand.Prepare();
                    var result = mySqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }

        public TransactionHistory Transfer(string sendAccountNumber, string receiveAccountNumber, int page, int limit)
        {
            throw new NotImplementedException();
        }

        public Account UpdatePassword(string accountNumber, Account accountUpdate)
        {
            try
            {
                using (var cnn = ConnectionHelper.GetInstance())
                {
                    //cnn.Open();
                    var mySqlCommand =
                        new MySqlCommand(_updatePasswordCommand, cnn);
                    mySqlCommand.Parameters.AddWithValue("@password_hash", accountUpdate.PasswordHash);
                    mySqlCommand.Parameters.AddWithValue("@account_number", accountNumber);
                    mySqlCommand.Prepare();
                    var result = mySqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return accountUpdate;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return null;
        }
    }
}