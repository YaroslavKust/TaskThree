using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskThree.Export;
using TaskThree.Models;
using TaskThree.Repositories;
using TaskThree.Services;

namespace TaskThree.ViewModels
{
    class MainWindowViewModel
    {
        private const string DefaultExcelName = "records.xlsx";
        private const string DefaultXmlName = "records.xml";

        private IRepository _db;
        private IFileService _file;
        private IFileSelectionDialog _dialog;

        private RelayCommand _loadDataCommand, _exportToXmlCommand, _exportToExcelCommand;

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
                return _loadDataCommand ??= new RelayCommand(async _ =>
                {
                    string fileName = _dialog.OpenDialog();

                    if (fileName == null) return;

                    try
                    {
                        var results = await _file.ReadAllAsync(fileName);
                        await _db.AddRecordsAsync(results);
                    }
                    catch (InvalidDataException e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }

                    MessageBox.Show("Данные успешно записаны");
                });
            }
        }

        public RelayCommand ExportToXmlCommand
        {
            get
            {
                return _exportToXmlCommand ??= new RelayCommand(_ => 
                {
                    _dialog.FileName = DefaultXmlName;
                    ExportToFormatAsync(new XmlExporter());
                });
            }
        }

        public RelayCommand ExportToExcelCommand
        {
            get
            {
                return _exportToExcelCommand ??= new RelayCommand(_ =>
                {
                    _dialog.FileName = DefaultExcelName;
                    ExportToFormatAsync(new ExcelExporter());
                });
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
