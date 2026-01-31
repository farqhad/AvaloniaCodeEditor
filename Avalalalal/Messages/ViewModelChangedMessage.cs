using Avalalalal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avalalalal.Messages
{
	public class ViewModelChangedMessage
	{
		public ViewModelChangedMessage(ViewModelBase viewModel) {
			ViewModel = viewModel;
		}

		public ViewModelBase ViewModel { get; }
	}
}
