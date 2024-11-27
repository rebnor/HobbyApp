using HobbyApp.VM;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HobbyApp
{
    /// <summary>
    /// Initializes the main window, sets up the ViewModel, and loads data asynchronously.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var viewModel = new HobbiesViewModel();
            DataContext = viewModel;
            _ = viewModel.LoadAsync();
        }
    }
}