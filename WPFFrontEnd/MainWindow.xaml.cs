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
using WPFFrontEnd.UI;

namespace WPFFrontEnd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                switch (button.Tag.ToString())
                {
                    case "Opt1":
                        DynamicContent.Content = new ListOptionControl();
                        break;

                    case "Opt2":
                        DynamicContent.Content = new AddOptionControl();
                        break;

                    case "Opt3":
                        DynamicContent.Content = new TextBlock
                        {
                            Text = "Contenu3",
                            FontSize = 24
                        };
                        break;

                    default:
                        DynamicContent.Content = null;
                        break;
                }
            }
        }
    }
}