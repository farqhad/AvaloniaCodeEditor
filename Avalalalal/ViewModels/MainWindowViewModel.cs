using CommunityToolkit.Mvvm.Messaging;
using Avalalalal.Messages;
using Microsoft.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avalalalal.ViewModels
{
	public partial class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			WeakReferenceMessenger.Default.Register<ViewModelChangedMessage>(this, (sender, message) =>
			{
				CurrentViewModel = message.ViewModel;
			});

			var viewModel = App.ServiceProvider.GetService<IDELayoutViewModel>()!;
			var message = new ViewModelChangedMessage(viewModel);

			WeakReferenceMessenger.Default.Send<ViewModelChangedMessage>(message);
		}

		[ObservableProperty]
		private ViewModelBase? _currentViewModel;
	}
}
