using System.Windows;
using TaskThree.ViewModels;
using TaskThree.Services;
using TaskThree.Repositories;

namespace TaskThree
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel vm = new MainWindowViewModel(new CSVFileService(), new DefaultDialogService(), new SQLServerRepository());
            DataContext = vm;
        }
    }
}
