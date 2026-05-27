using CobranzaCostas.ViewModels;

namespace CobranzaCostas.Views.Gestor;

public partial class GestorPage : ContentPage
{
    private readonly GestorViewModel _viewModel;

    public GestorPage(GestorViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Cargamos los datos desde Firestore al momento en que la pantalla se hace visible.
        // Esto asegura que siempre tengamos el avance más reciente.
        _viewModel.CargarAvanceCommand.Execute(null);
    }
}
