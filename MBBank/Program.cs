using System;
using MBBank.Controller;
using MBBank.Entity;
using MBBank.Util;
using MBBank.View;

namespace MBBank
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var userView = new UserView();
            userView.Menu();

            // UserController userController = new UserController();
            // Console.WriteLine("Dang Nhap");
            // Console.WriteLine("Nhap thong tin thay doi.");
            // userController.UpdateInformation();


            // AdminController adminController = new AdminController();
            // adminController.Login();


            // IUserController userController = new UserController();
            // var check= userController.Login() != null;
            // if (check)
            // {
            //     Console.WriteLine("kie:"+check);
            // }
            // Account loggedInAccount;
            // bool isAdmin = false;
            // while (true)
            // {
            //     Console.WriteLine("1. Register.");
            //     Console.WriteLine("2. Login.");
            //     Console.WriteLine("3. About.");
            //     Console.WriteLine("4. Enter your choice.");
            //     IUserController userController = new UserController();
            //     loggedInAccount = userController.Login();
            //     if (isAdmin)
            //     {
            //         // show thong tin admin
            //     }
            //     else if (loggedInAccount != null)
            //     {
            //     }
            // }
            //
            // IUserController userController = new UserController();
            // userController.Register();

        }
    }
}