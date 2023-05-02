using Avalonia;
using Avalonia.Controls;

namespace NodeEditor.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();
        }
    }
}