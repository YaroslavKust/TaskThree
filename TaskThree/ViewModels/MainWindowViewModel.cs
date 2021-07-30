using TaskThree.Models;
using System.Linq;
using TaskThree.Export;
using System.Threading.Tasks;
using TaskThree.Services;
using TaskThree.Repositories;
using System.Windows;

namespace TaskThree.ViewModels
{
    class MainWindowViewModel
    {
        private IRepository _db;
        private IFileService _file;
        private IDialogService _dialog;

        private RelayCommand _loadDataCommand, _exportToXMLCommand, _exportToExcelCommand;

        public Record FilterRecord { get; set; } = new Record() { Date = System.DateTime.Today };
        
        public MainWindowViewModel(IFileService file, IDialogService dialog, IRepository db)
        {
            _file = file;
            _dialog = dialog;
            _db = db;
        }

        public RelayCommand LoadDataCommand
        {
            get
            {
                return _loadDataCommand ??
                    (_loadDataCommand = new RelayCommand(obj =>
                    {
                        string fname = _dialog.OpenDialog();

                        if (fname != null)
                        {
                            var results = _file.ReadAll(fname);
                            _db.AddRecordsAsync(results);
                        }
                    }));
            }
        }

        public RelayCommand ExportToXMLCommand
        {
            get
            {
                return _exportToXMLCommand ??
                    (_exportToXMLCommand = new RelayCommand(obj => 
                    {
                        _dialog.FileName = "records.xml";
                        ExportToFormatAsync(new XMLExporter());
                    }));
            }
        }

        public RelayCommand ExportToExcelCommand
        {
            get
            {
                return _exportToExcelCommand ??
                    (_exportToExcelCommand = new RelayCommand(obj =>
                    {
                        _dialog.FileName = "records.xlsx";
                        ExportToFormatAsync(new ExcelExporter());
                    }));
            }
        }

        private async void ExportToFormatAsync(IExporter exp)
        {
            if(!FilterRecord.IsValid())
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }

            await Task.Run(() =>
            {
                var records = _db.GetRecordsWithFilter(FilterRecord).ToList();
                if (records.Count == 0)
                {
                    var result = MessageBox.Show("Записи не найдены, все равно создать документ?",
                        "Подтверждение", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.No)
                    {
                        return;
                    }
                }

                string file = _dialog.SaveDialog();
                if (!string.IsNullOrWhiteSpace(file))
                    exp.Export(records, file);
            });
        }
    }
}
