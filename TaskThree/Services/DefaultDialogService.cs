using Microsoft.Win32;

namespace TaskThree.Services
{
    class DefaultDialogService: IDialogService
    {
        public string OpenDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*.csv";
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return null;
        }

        public string SaveDialog()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
                return dialog.FileName;
            return null;
        }
    }
}
