using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace TaskThree.Models
{
    class Record : INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string SurName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        private string _error = string.Empty;

        public string this[string propertyName]
        {
            get
            {
                _error = string.Empty;
                switch (propertyName)
                {
                    case "FirstName":
                        if (FirstName != string.Empty)
                            if (!CheckNameFormat(FirstName))
                                _error = "Имя должно состоять из одного слова или двух, разделенных тире!";
                        break;

                    case "LastName":
                        if (LastName != string.Empty)
                            if (!CheckNameFormat(LastName))
                                _error = "Фамилия должна состоять из одного слова или двух, разделенных тире!";
                        break;

                    case "SurName":
                        if (SurName != string.Empty)
                            if (!CheckNameFormat(SurName))
                                _error = "Отчество должно состоять из одного слова или двух, разделенных тире!";
                        break;

                    case "City":
                        if (City != string.Empty)
                            if (!CheckPlaceFormat(City))
                                _error = "Название города не может содержать цифры и спецсимволы!";
                        break;

                    case "Country":
                        if (Country != string.Empty)
                            if (!CheckPlaceFormat(Country))
                                _error = "Название страны не может содержать цифры и спецсимволы!";
                        break;
                }

                return _error;
            }
        }

        public string Error => null;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public static bool CheckNameFormat(string s)
        {
            string fullNameFormat = "^[A-Za-zА-Яа-яЁё]+\\-?[A-Za-zА-Яа-яЁё]+$";
            return new Regex(fullNameFormat).IsMatch(s);
        }

        public static bool CheckPlaceFormat(string s)
        {
            string cityOrCountryFormat = "^[a-zA-Zа-яА-ЯЁё \\-]+$";
            return new Regex(cityOrCountryFormat).IsMatch(s);
        }

        public bool IsValid()
        {
            return _error == string.Empty;
        }

    }
}