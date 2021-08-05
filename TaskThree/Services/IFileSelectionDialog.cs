
namespace TaskThree.Services
{
    interface IFileSelectionDialog
    {
        string OpenDialog();
        string SaveDialog();
        string FileName { get; set; }
    }
}
