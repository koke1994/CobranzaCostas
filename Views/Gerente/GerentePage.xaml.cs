using CobranzaCostas.ViewModels;

namespace CobranzaCostas.Views.Gerente;

public partial class GerentePage : ContentPage
{
    private readonly GerenteViewModel _viewModel;

    public GerentePage(GerenteViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Cargamos los datos desde Firestore apenas la pantalla se hace visible
        _viewModel.CargarCompromisoCommand.Execute(null);
    }
}
