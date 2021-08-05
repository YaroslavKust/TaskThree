using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace TaskThree.Models
{
    class Record: INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        private string _error = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public string this[string propertyName]
        {
            get
            {
                string cityOrCountry = "^[a - zA - Zа - яА - ЯЁё \\-]+$";
                string fullName = "^[A-Za-zА-Яа-яЁё]+\\-?[A-Za-zА-Яа-яЁё]+$";

                _error = string.Empty;
                switch (propertyName)
                {
                    case "FirstName":
                        if (FirstName != string.Empty)
                            if (!CheckFormat(FirstName, fullName))
                            {
                                _error = "Имя должно состоять из одного слова или двух, разделенных тире!";
                            }
                        break;

                    case "LastName":
                        if (LastName != string.Empty)
                            if (!CheckFormat(LastName, fullName))
                            {
                                _error = "Фамилия должна состоять из одного слова или двух, разделенных тире!";
                            }
                        break;

                    case "SurName":
                        if (SurName != string.Empty)
                            if (!CheckFormat(SurName, fullName))
                            {
                                _error = "Отчество должно состоять из одного слова или двух, разделенных тире!";
                            }
                        break;

                    case "City":
                        if (City != string.Empty)
                            if (!CheckFormat(City, cityOrCountry))
                            {
                                _error = "Название города не может содержать цифры и спецсимволы!";
                            }
                        break;

                    case "Country":
                        if (Country != string.Empty)
                            if (!CheckFormat(Country, cityOrCountry))
                            {
                                _error = "Название страны не может содержать цифры и спецсимволы!";
                            }
                        break;
                }

                return _error;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        private bool CheckFormat(string s, string format)
        {
            Regex regex = new Regex(format);
            return regex.IsMatch(s);
        }

        public bool IsValid() => _error == string.Empty;
    }
}
