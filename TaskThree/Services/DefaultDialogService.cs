using Microsoft.Win32;

namespace TaskThree.Services
{
    class DefaultDialogService: IFileSelectionDialog
    {
        public string OpenDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "csv files(*.csv)| *.csv";
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return null;
        }

        public string SaveDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = FileName;
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return null;
        }

        public string FileName { get; set; }
    }
}
