namespace Task1.View.DialogService
{
    public interface IDialogService
    {
        string FilePath { get; set; }

        string FileFormat { get; set; }

        bool OpenFileDialog();

        bool SaveFileDialog();

        void ShowMessage(string message);
    }
}
