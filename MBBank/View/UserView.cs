using System;
using System.Runtime.InteropServices;
using MBBank.Controller;
using MBBank.Entity;

namespace MBBank.View
{
    public class UserView: IUserMenu
    {
        private IUserController userController = new UserController();
        private IAdminController adminController = new AdminController();
        private Account account = new Account();
        public void Menu()
        {
            while (true){
                Console.WriteLine("—— Ngan hang Spring HeroBank ——");
                Console.WriteLine("1. Dang ky tai khoan");
                Console.WriteLine("2. Dang nhap he thong");
                Console.WriteLine("3. Thoat");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Nhap lua chon cua ban (1-3): ");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice) {
                    case 1:
                        Console.WriteLine("1. Nguoi dung \n2. Admin");
                        var option = Convert.ToInt32(Console.ReadLine());
                        switch (option)
                        {
                            case 1:
                                userController.Register();
                                break;
                            case 2:
                                adminController.Register();
                                break;
                        }
                        break;
                    case 2:
                       GenerateChoiceUserOrAdmin();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose between 0-2");
                        break;
                }
                if (choice == 0){
                    break;
                }
            }
        }

        public void GenerateGuestMenu()
        {
            throw new System.NotImplementedException();
        }

        
        public void GenerateUserMenu()
        {
            while (true){
                Console.WriteLine("—— Ngân Hàng Spring Hero Bank ——");
                Console.WriteLine("Chao mung ban da quay tro lai, vui long chon thao tac.");
                Console.WriteLine("1. Gui tien");
                Console.WriteLine("2. Rut tien.");
                Console.WriteLine("3. Chuyen khoan.");
                Console.WriteLine("4. Truy van so du.");
                Console.WriteLine("5. Thay doi thong tin ca nhan.");
                Console.WriteLine("6. Thay doi thong tin mat khau.");
                Console.WriteLine("7. Lich su giao dich.");
                Console.WriteLine("8. Quay lai.");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Nhap lua chon cua ban (1-8): ");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice) {
                    case 1:
                        userController.Deposit(account.AccountNumber);
                        break;
                    case 2:
                        userController.WithDraw(account.AccountNumber);
                        break;
                    case 3:
                        Console.WriteLine("Chuyen khoan.");
                        break;
                    case 4:
                        Console.WriteLine("Truy van so du");
                        break;
                    case 5:
                        Console.WriteLine("Vui long nhap thong tin thay doi!");
                        userController.UpdateInformation(account.AccountNumber);
                        break;
                    case 6:
                        Console.WriteLine("Thay doi thong tin mat khau");
                        userController.UpdatePassword(account.AccountNumber, account.Salt);
                        break;
                    case 7:
                        Console.WriteLine("Lich su giao dich");
                        break;
                    case 8:
                        GenerateChoiceUserOrAdmin();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose between 1-8");
                        break;
                }
                if (choice == 0){
                    break;
                }
            }
        }

        public void GenerateChoiceUserOrAdmin()
        {
            bool isLoginUser = false;
            bool isLoginAdmin = false;
            while (true){
                Console.WriteLine("—— Ngan hang Spring Hero Bank ——");
                Console.WriteLine("Chao mung Ban quay lai. vui long chon quyen truy cap.");
                Console.WriteLine("1. User");
                Console.WriteLine("2. Admin.");
                Console.WriteLine("3. Quay lai.");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Nhap lua chon cua ban (1-3): ");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice) {
                    case 1:
                        if (isLoginUser)
                        {
                            Console.WriteLine("Dang nhap tai khoan thanh cong!");
                        }
                        else
                        {
                            account = userController.Login();
                            if (account !=null)
                            {
                                GenerateUserMenu();
                                Console.WriteLine($"Username:{account.Username}");
                                isLoginUser = true;
                            }
                            else
                            {
                                Console.WriteLine("Sai mat khau hoac password, vui long dang nhap lai!");
                            }
                        }
                        break;
                    case 2:
                        if (isLoginAdmin)
                        {
                            Console.WriteLine("Dang nhap quyen admin thanh cong!");
                        }
                        else
                        {
                            if (adminController.Login() !=null)
                            {
                                GenerateUserMenu();
                                isLoginAdmin = true;
                            }
                        }
                        break;
                    case 3: 
                        Menu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose between 1-8");
                        break;
                }
                if (choice == 0){
                    break;
                }
            }
        }

        public void GenerateMenuRegister()
        {
            throw new NotImplementedException();
        }

        public void GenerateAdminMenu()
        {
            while (true){
                Console.WriteLine("—— Ngân Hàng Spring Hero Bank ——");
                Console.WriteLine("Chao mung ban quay lai, vui long chon thao tac!");
                Console.WriteLine("1. Danh sach nguoi dung");
                Console.WriteLine("2. Danh sach lich su giao dich.");
                Console.WriteLine("3. Tim nguoi dung theo ten.");
                Console.WriteLine("4. Tim kiem nguoi dung theo so tai khoan.");
                Console.WriteLine("5. Tim kiem nguoi dung theo so dien thoai.");
                Console.WriteLine("6. Them nguoi dung moi.");
                Console.WriteLine("7. Khoa mo tai khoan nguoi dung.");
                Console.WriteLine("8. Tim kiem lich su giao dich theo so tai khoan.");
                Console.WriteLine("9. Thay doi thong tin tai khoan.");
                Console.WriteLine("10.Thay doi thong tin mat khau.");
                Console.WriteLine("11.Thoat.");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("Nhap lua chon cua ban (1-11): ");
                var choice = Convert.ToInt32(Console.ReadLine());
                switch (choice) {
                    case 1:
                        Console.WriteLine("Gui tien.");
                        break;
                    case 2:
                        Console.WriteLine("Rut tien.");
                        break;
                    case 3:
                        Console.WriteLine("Chuyen khoan.");
                        break;
                    case 4:
                        Console.WriteLine("Truy van so du");
                        break;
                    case 5:
                        Console.WriteLine("Thay doi thong tin ca nhan");
                        break;
                    case 6:
                        Console.WriteLine("Thay doi thong tin mat khau");
                        break;
                    case 7:
                        Console.WriteLine("Lich su giao dich");
                        break;
                    case 8:
                        GenerateChoiceUserOrAdmin();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose between 1-8");
                        break;
                }
                if (choice == 0){
                    break;
                }
            }
        }
    }
}