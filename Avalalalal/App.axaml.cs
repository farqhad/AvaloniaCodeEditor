using Avalalalal.ViewModels;
using Avalalalal.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Avalalalal
{
	public partial class App : Application
	{
		static App()
		{
			ServiceCollection = new ServiceCollection();

			{
				ServiceCollection.AddSingleton<MainWindow>();

				ServiceCollection.AddSingleton<MainWindowViewModel>();
				ServiceCollection.AddSingleton<IDELayoutViewModel>();
				ServiceCollection.AddTransient<EnterNewFileNameViewModel>();
			}

			ServiceProvider = ServiceCollection.BuildServiceProvider();
		}

		public static ServiceCollection ServiceCollection { get; }
		public static ServiceProvider ServiceProvider { get; }

		public override void Initialize()
		{
			AvaloniaXamlLoader.Load(this);
		}

		public override void OnFrameworkInitializationCompleted()
		{
			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
			{
				// Line below is needed to remove Avalonia data validation.
				// Without this line you will get duplicate validations from both Avalonia and CT
				BindingPlugins.DataValidators.RemoveAt(0);
				desktop.MainWindow = new MainWindow
				{
					DataContext = new MainWindowViewModel(),
				};
			}

			base.OnFrameworkInitializationCompleted();
		}
	}
}