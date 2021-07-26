
namespace TaskThree.Services
{
    interface IDialogService
    {
        string OpenDialog();
        string SaveDialog();
        string FileName { get; set; }
    }
}
