using TaskThree.Models;
using System.Linq;
using TaskThree.Export;
using System.Threading.Tasks;
using TaskThree.Services;
using TaskThree.Repositories;
using System.Windows;
using System.Collections.Generic;

namespace TaskThree.ViewModels
{
    class MainWindowViewModel
    {
        private const string _defaultExcelName = "records.xlsx";
        private const string _defaultXMLName = "records.xml";

        private IRepository _db;
        private IFileService _file;
        private IFileSelectionDialog _dialog;

        private RelayCommand _loadDataCommand, _exportToXMLCommand, _exportToExcelCommand;

        public Record FilterRecord { get; set; } = new Record() { Date = System.DateTime.Today };
        
        public MainWindowViewModel(IFileService file, IFileSelectionDialog dialog, IRepository db)
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
                    (_loadDataCommand = new RelayCommand(async obj =>
                    {
                        string fname = _dialog.OpenDialog();

                        if (fname != null)
                        {
                            var results = await _file.ReadAllAsync(fname);
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
                        _dialog.FileName = _defaultXMLName;
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
                        _dialog.FileName = _defaultExcelName;
                        ExportToFormatAsync(new ExcelExporter());
                    }));
            }
        }

        private async void ExportToFormatAsync(IExporter exp)
        {
            List<Record> records = new();

            if(!FilterRecord.IsValid())
            {
                MessageBox.Show("Введены некорректные данные");
                return;
            }

            await Task.Run(() =>
            {
                records = _db.GetRecordsWithFilter(FilterRecord).ToList();
           
            });

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
                await exp.ExportAsync(records, file);
        }
    }
}
