using TaskThree.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using TaskThree.Export;
using System.Threading.Tasks;
using TaskThree.Services;
using Microsoft.EntityFrameworkCore;

namespace TaskThree.ViewModels
{
    class MainWindowViewModel
    {
        private TaskThreeContext _db;
        private RelayCommand _loadDataCommand, _exportToXMLCommand, _exportToExcelCommand;
        private IFileService _file;
        private IDialogService _dialog;

        public List<Record> Records { get; set; }
        public Record FilterRecord { get; set; }
        
        public MainWindowViewModel(IFileService file, IDialogService dialog)
        {
            _file = file;
            _dialog = dialog;
            _db = new TaskThreeContext();
            FilterRecord = new Record();
        }

        public async Task FilterDataAsync()
        {
            Records = await _db.Records.Where(r =>
                FilterRecord.Date == DateTime.MinValue || r.Date == FilterRecord.Date
                    &&
                    String.IsNullOrWhiteSpace(FilterRecord.FirstName) || r.FirstName == FilterRecord.FirstName
                    &&
                    String.IsNullOrWhiteSpace(FilterRecord.LastName) || r.LastName == FilterRecord.LastName
                    &&
                    String.IsNullOrWhiteSpace(FilterRecord.SurName) || r.SurName == FilterRecord.SurName
                    &&
                    String.IsNullOrWhiteSpace(FilterRecord.City) || r.City == FilterRecord.City
                    &&
                    String.IsNullOrWhiteSpace(FilterRecord.Country) || r.Country == FilterRecord.Country
            ).ToListAsync();
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
                            _db.AddRangeAsync(results);
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
                        ExportToFormatAsync(new ExcelExporter());
                    }));
            }
        }

        private async void ExportToFormatAsync(IExporter exp)
        {
            await FilterDataAsync();
            await Task.Run(() =>
            {
                exp.Export(Records, _dialog.SaveDialog());
            });
        }
    }
}
