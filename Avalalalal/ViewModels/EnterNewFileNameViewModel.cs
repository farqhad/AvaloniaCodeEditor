using Avalalalal.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Avalalalal.ViewModels;

namespace Avalalalal.ViewModels
{
	partial class EnterNewFileNameViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string? _fileName;
		private Action<string>? _create;

		public EnterNewFileNameViewModel(Action<string>? create) {
			_create = create;
		}

		partial void OnFileNameChanged(string? value)
		{
			CreateCommand.NotifyCanExecuteChanged();
		}

		[RelayCommand(CanExecute = nameof(IsFileNameEmpty))]
		private void Create() {
			_create?.Invoke(FileName);

			var viewModel = App.ServiceProvider.GetService<IDELayoutViewModel>()!;
			var message = new ViewModelChangedMessage(viewModel);

			WeakReferenceMessenger.Default.Send<ViewModelChangedMessage>(message);
		}

		private bool IsFileNameEmpty() => !string.IsNullOrEmpty(FileName);


		[RelayCommand]
		private void BackButton() {
			var viewModel = App.ServiceProvider.GetService<IDELayoutViewModel>()!;
			var message = new ViewModelChangedMessage(viewModel);

			WeakReferenceMessenger.Default.Send<ViewModelChangedMessage>(message);
		}
	}
}
