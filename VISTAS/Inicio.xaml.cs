using InchiglemaS7.Modelos;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace InchiglemaS7.VISTAS;

public partial class Inicio : ContentPage
{
	
        private const string Url = "http://192.168.17.10:8080/moviles/post.php";
        private readonly HttpClient cliente = new HttpClient();
        private ObservableCollection<Estudiante> estud;

        public Inicio() 
    
	{
		InitializeComponent();
            Obtener();
	}
        public async void Obtener()
        {
            try
            {
                var content = await cliente.GetStringAsync(Url);
                List<Estudiante> mostrarEst = JsonConvert.DeserializeObject<List<Estudiante>>(content);
                estud = new ObservableCollection<Estudiante>(mostrarEst);
                listaEstudiantes.ItemsSource = estud;
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error o registrar la excepción para su posterior análisis.
                Console.WriteLine("Error al obtener los Datos:" + ex.Message);

            }
        }

    private void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
            if (e.SelectedItem != null)
            {
                var objetoestudiante = (Estudiante)e.SelectedItem;
                Navigation.PushAsync(new ActualizarEliminar(objetoestudiante));

                // Deseleccionar el elemento de la lista
                listaEstudiantes.SelectedItem = null;

            }
        }

    private void btnAdd_Clicked(object sender, EventArgs e)
    {
            Navigation.PushAsync(new VISTAS.Agregar());
    }
}