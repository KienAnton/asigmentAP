namespace MBBank.View
{
    public interface IUserMenu
    {
        void Menu();
        void GenerateGuestMenu();
        void GenerateUserMenu();
        void GenerateChoiceUserOrAdmin();
        void GenerateMenuRegister();
        void GenerateAdminMenu();

    }
}