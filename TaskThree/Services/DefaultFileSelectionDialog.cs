using Microsoft.Win32;

namespace TaskThree.Services
{
    class DefaultFileSelectionDialog: IFileSelectionDialog
    {
        public string OpenDialog()
        {
            OpenFileDialog dialog = new() {Filter = "csv files(*.csv)| *.csv"};
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return null;
        }

        public string SaveDialog()
        {
            SaveFileDialog dialog = new(){FileName = FileName};
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return null;
        }

        public string FileName { get; set; }
    }
}
