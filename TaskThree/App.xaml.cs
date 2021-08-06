using Autofac;
using System.Windows;
using TaskThree.Repositories;
using TaskThree.Services;
using TaskThree.ViewModels;

namespace TaskThree
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DefaultDialogService>().As<IFileSelectionDialog>();
            builder.RegisterType<CsvFileService>().As<IFileService>();
            builder.RegisterType<RecordRepository>().As<IRepository>();
            builder.RegisterType<MainWindowViewModel>().AsSelf();
            var container = builder.Build();

            var viewmodel = container.Resolve<MainWindowViewModel>();
            var view = new MainWindow() { DataContext = viewmodel };
            view.Show();

        }
    }
}
