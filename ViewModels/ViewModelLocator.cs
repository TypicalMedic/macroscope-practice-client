using Microsoft.Extensions.DependencyInjection;

namespace ClientSide.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
    }
}
