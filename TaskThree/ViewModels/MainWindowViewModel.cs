﻿using TaskThree.Models;
using System.Collections.Generic;
using System.Linq;
using TaskThree.Export;
using System.Threading.Tasks;
using TaskThree.Services;
using TaskThree.Repositories;

namespace TaskThree.ViewModels
{
    class MainWindowViewModel
    {
        private IRepository _db;
        private RelayCommand _loadDataCommand, _exportToXMLCommand, _exportToExcelCommand;
        private IFileService _file;
        private IDialogService _dialog;

        public List<Record> Records { get; set; }
        public Record FilterRecord { get; set; }
        
        public MainWindowViewModel(IFileService file, IDialogService dialog, IRepository db)
        {
            _file = file;
            _dialog = dialog;
            _db = db;
            FilterRecord = new Record();
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
            await Task.Run(() =>
            {
                Records = _db.GetRecordsWithFilter(FilterRecord).ToList();
                exp.Export(Records, _dialog.SaveDialog());
            });
        }
    }
}
