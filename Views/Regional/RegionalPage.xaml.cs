using CobranzaCostas.ViewModels;

namespace CobranzaCostas.Views.Regional;

public partial class RegionalPage : ContentPage
{
    private readonly RegionalViewModel _viewModel;

    public RegionalPage(RegionalViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Carga los datos base de la región
        _viewModel.CargarDatosCommand.Execute(null);
    }
}