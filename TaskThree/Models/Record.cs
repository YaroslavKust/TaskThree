using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskThree.Models
{
    class Record: INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
