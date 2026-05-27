using CobranzaCostas.ViewModels;

namespace CobranzaCostas.Views.Director;

public partial class DirectorPage : ContentPage
{
    private readonly DirectorViewModel _viewModel;

    public DirectorPage(DirectorViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Carga los datos base de la dirección
        _viewModel.CargarDatosCommand.Execute(null);
    }
}