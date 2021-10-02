using MBBank.Entity;

namespace MBBank.Controller
{
    public interface IAdminController
    {
        Admin Register(); // Đăng ký tài khoản
        Admin Login();// Đăng nhập
        void ShowListAdmin();// Hiển thị danh sách Admin
        void UpdateAdmin();// Thay đổi thông tin Admin 
        void ShowListUser();// Hiển thị danh sách người dùng
        void ApproveUser(); // Bắt nhập user id, tìm kiếm theo id, show trạng thái, hỏi là active hay không
        void LockUser();// Khóa tài khoản người dùng
        void UnlockUser();// Mở khóa tài khoản người dùng
        void FindUserByAccountNumber();// Tìm kiếm người dùng theo số tài khoản
        void SearchUserByPhone();// Tìm kiếm người dùng theo số điện thoại
        void SearchUserByIdentityNumber();//Tìm kiếm người dùng theo cccd
        void SearchTransactionHistory(); // Tìm kiếm lịch sử dao dịch theo số tài khoản.
        void UpdateAccount();// Thay đổi thông tin tài khoản
        void UpdatePassword(); //Thay đổi mật khẩu




    }
}