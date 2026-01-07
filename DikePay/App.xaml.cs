namespace DikePay
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();

            // Esto le dice a MAUI: "Usa el motor de navegación Shell"
            MainPage = new AppShell();
        }

        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new MainPage()) { Title = "DikePay" };
        //}
    }
}
