using System;
using System.Collections.Generic;
using MBBank.Entity;

namespace MBBank.Model
{
    public interface IAccountModel
    {
        Account Save(Account account); // Đăng ký tài khoản
        Account Update(string accountNumber, Account updateAccount);// thay đổi tài khoản, lock, active....
        bool Delete(string accountNumber);// Đóng tài khoản.
        Account FindByAccountNumber(string accountAccountNumber);// Tìm tài khoản theo số tài khoản
        Account FindByUsername(string username);// Tìm tài khoản theo tên tài khoản
        List<Account> FindAll(int page, int limit);
        // Lọc theo số điện thoại, có thể nhập một phần điện thoại để search.
        List<Account> SearchByPhone(string keyword, int page, int limit);
        // Lọc theo căn cước công dân/ cmnd
        List<Account> SearchByIdentityNumber(string keyword, int page, int limit);
        // Sao kê
        List<TransactionHistory> FindTransactionHistoryByAccountNumber(
            string accountNumber,
            DateTime startTime,
            DateTime endTime,
            int page,
            int limit
        );
        TransactionHistory Deposit(string accountNumber, double amount);
        TransactionHistory Withdraw(string accountNumber, double amount);
        TransactionHistory Transfer(
            string sendAccountNumber,
            string receiveAccountNumber,
            int page,
            int limit
        );

        Account UpdatePassword(string accountNumber, Account accountUpdate);
    }
}