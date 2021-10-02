using MBBank.Entity;

namespace MBBank.Controller
{
    /*
     * Trong các dự án lớn thường sẽ bổ sung thêm tầng service.
     * Các bussiness phức tạp sẽ được sử lý tại tầng bussiness
     * Tần controller chỉ đơn giản làm nhiệm vụ validate 
     */
    public interface IUserController
    {
        /*
         * - Yêu cầu người dùng nhập thông tin tài khoản,
         * - Kiểm tra thông tin tài khoản: check input, check tồn tại.
         * - Tạo muối, mã hóa password, khởi tạo một số thông tin tự động: ngày tạo, ngày update, status 
         * - Lưu vào database
         */
        Account Register();
        /*
         * Xem thông tin ngân hàng, có thể đọc từ một file txt hoặc hard code 
         */
        void ShowBankInformation();
        /*
         * Đăng nhập hệ thống.
         * - Yêu cầu người dùng nhập thông tin username và password
         * - Trả về những thông tin tài khoản nếu đang nhập thành công
         */
        Account Login();
        
        //User login 
        TransactionHistory WithDraw(string accountNumber);
        TransactionHistory Deposit(string accountNumber);
        void Transfer(string accountNumber);
        Account UpdateInformation(string accountNumber);
        Account UpdatePassword(string accountNumber, string salt);
        void CheckInformation();
        void LockTransaction(); 
        void UnlockTransaction();
        void CheckTransactionHistory();
    }
}