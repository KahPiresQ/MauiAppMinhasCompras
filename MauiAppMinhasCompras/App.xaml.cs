using System.IO;                       // Path.Combine
using MauiAppMinhasCompras.Helpers;    // SQLiteDatabaseHelper

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        // Acesso global ao banco
        public static SQLiteDatabaseHelper Db { get; private set; } = null!;

        public App()
        {
            InitializeComponent();

            // Garante uma única conexão (Hot Reload às vezes reinicia)
            if (Db == null)
            {
                var path = Path.Combine(FileSystem.AppDataDirectory, "minhas_compras.db3");
                Db = new SQLiteDatabaseHelper(path);
            }

            // Abre direto a tela de lista (simples, sem Shell)
            MainPage = new NavigationPage(new Views.ListaProduto());
        }
    }
}