using TaskThree.Models;
using System.Collections.Generic;
using Microsoft.Win32;
using System.IO;
using System.Text;
using System;
using System.Linq;

namespace TaskThree.ViewModels
{
    class MainWindowViewModel
    {
        private TaskThreeContext _db;
        private RelayCommand _loadDataCommand;

        public List<Record> Records { get; set; }
        
        public MainWindowViewModel()
        {
            _db = new TaskThreeContext();
            Records = _db.Records.ToList();
            //Records.Add(new Record { Date = System.DateTime.Now, City = "a", Country = "a", FirstName = "a", LastName = "a", SurName = "a" });
        }

        public RelayCommand LoadDataCommand
        {
            get
            {
                return _loadDataCommand ??
                    (_loadDataCommand = new RelayCommand(obj =>
                    {
                        string fname;
                        OpenFileDialog fileDialog = new OpenFileDialog();

                        if (fileDialog.ShowDialog() == true)
                        {
                            fname = fileDialog.FileName;
                            StreamReader reader = new StreamReader(fname, Encoding.UTF8);

                            string line;
                            while((line = reader.ReadLine()) != null)
                            {
                                string[] data = line.Split(';');
                                Record rec = new Record()
                                {
                                    Date = DateTime.Parse(data[0]),
                                    FirstName = data[1],
                                    LastName = data[2],
                                    SurName = data[3],
                                    City = data[4],
                                    Country = data[5]
                                };

                                _db.Add(rec);
                                _db.SaveChanges();
                            }

                        }

                    }));
            }
        }
    }
}
