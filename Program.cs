namespace CDA
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // Obs³u¿ wyj¹tek tutaj, np. zapisz informacje do logów lub poinformuj u¿ytkownika.
            MessageBox.Show("Wyst¹pi³ b³¹d: " + e.Exception.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}