using System.Windows;
using Microsoft.Win32;

namespace Task1.View.DialogService
{
    public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }

        public string FileFormat { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Csv file (*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;

                return true;
            }

            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Csv file (*.csv)|*.csv|Xml file (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                FileFormat = System.IO.Path.GetExtension(saveFileDialog.FileName);
                return true;
            }

            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
