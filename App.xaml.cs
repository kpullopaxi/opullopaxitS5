using opullopaxitS5.Utils;
using System.IO;

namespace opullopaxitS5
{
    public partial class App : Application
    {
        public static PersonRepository personrepo { get; private set; }

        public App()
        {
            InitializeComponent();

            // Inicializar personrepo
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "personas.db3");
            personrepo = new PersonRepository(dbPath);

            MainPage = new NavigationPage(new Views.vHome());
        }
    }
}
