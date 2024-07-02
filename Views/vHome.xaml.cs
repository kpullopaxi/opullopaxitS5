using opullopaxitS5.Models;

namespace opullopaxitS5.Views
{
    public partial class vHome : ContentPage
    {
        public vHome()
        {
            InitializeComponent();
        }

        private void btnInsertar_Clicked(object sender, EventArgs e)
        {
            status.Text = "";
            App.personrepo.AddNewPerson(txtNombre.Text);
            status.Text = App.personrepo.statusMessage;
            txtNombre.Text = ""; // Limpiar la entrada despu�s de a�adir
            txtId.Text = ""; // Limpiar el campo ID
            LoadPeople(); // Cargar la lista de personas despu�s de a�adir
        }

        private void btnActualizar_Clicked(object sender, EventArgs e)
        {
            status.Text = "";
            if (int.TryParse(txtId.Text, out int id))
            {
                App.personrepo.UpdatePerson(id, txtNombre.Text);
                status.Text = App.personrepo.statusMessage;
                txtNombre.Text = ""; // Limpiar la entrada despu�s de actualizar
                txtId.Text = ""; // Limpiar el campo ID
                LoadPeople(); // Cargar la lista de personas despu�s de actualizar
            }
            else
            {
                status.Text = "ID inv�lido";
            }
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            status.Text = "";
            if (int.TryParse(txtId.Text, out int id))
            {
                App.personrepo.DeletePerson(id);
                status.Text = App.personrepo.statusMessage;
                txtNombre.Text = ""; // Limpiar la entrada despu�s de eliminar
                txtId.Text = ""; // Limpiar el campo ID
                LoadPeople(); // Cargar la lista de personas despu�s de eliminar
            }
            else
            {
                status.Text = "ID inv�lido";
            }
        }

        private void btnListar_Clicked(object sender, EventArgs e)
        {
            status.Text = "";
            LoadPeople(); // Cargar la lista de personas al hacer clic en "Listar"
        }

        private void LoadPeople()
        {
            List<Persona> people = App.personrepo.GetAllPeople();
            ListaPersonas.ItemsSource = people;
        }
    }
}
