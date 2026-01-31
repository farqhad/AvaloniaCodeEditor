using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows;
using Avalonia.Platform.Storage;
using Avalonia.Controls;
using WForms = System.Windows.Forms;
using Avalalalal.Views;
using Avalalalal.Messages;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;
using System.IO;

namespace Avalalalal.ViewModels
{
	partial class IDELayoutViewModel : ViewModelBase
	{
		[ObservableProperty]
		private string? _filePath;
		[ObservableProperty]
		private string? _errorMessage;
		[ObservableProperty]
		private ObservableCollection<string> _files = new ObservableCollection<string>();
		private string? _selectedFile;
		public static string? CurrentSelectedPath { get; set; }
		[ObservableProperty]
		private string? _code;

		public string SelectedFile {
			get => _selectedFile; 
			set {
				_selectedFile = value;

				SelectFileDisplayCode($"{CurrentSelectedPath}\\{value}");
			}
		}

		private void FillFileList(FolderBrowserDialog dialog) {
			var files = Directory.GetFiles(dialog.SelectedPath);

			foreach (var file in files)
			{
				Files.Add(Path.GetFileName(file));
			}
		}


		private void SelectFileDisplayCode(string path)
		{
			if (CurrentSelectedPath != null)
			{
				Code = File.ReadAllText(path);
			}
		}

		public void CreateNewFile(string? FileName)
		{
			Files.Clear();

			var pickFolderDialog = new FolderBrowserDialog();

			if (pickFolderDialog.ShowDialog() == DialogResult.OK)
			{
				FilePath = pickFolderDialog.SelectedPath;
			}

			if (!File.Exists($"{FilePath}\\{FileName}"))
			{
				File.Create($"{FilePath}\\{FileName}");
			}
			else {
				ErrorMessage = "ERROR: File Already Exists!\n";
			}

			FillFileList(pickFolderDialog);

			CurrentSelectedPath = FilePath;
		}

		[RelayCommand]
		private void FileSaveChanges() {
			if (CurrentSelectedPath != null)
			{
				File.WriteAllText(CurrentSelectedPath, Code);
			}
		}

		[RelayCommand]
		private void CreateNewFileDialog() {
			ErrorMessage = string.Empty;

			var viewModel = new EnterNewFileNameViewModel(CreateNewFile);
			var message = new ViewModelChangedMessage(viewModel);

			WeakReferenceMessenger.Default.Send<ViewModelChangedMessage>(message);

		}

        [RelayCommand]
		private void CloseTheApp() {
			System.Environment.Exit(1);
		}
	}
}
