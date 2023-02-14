using System.Windows.Controls;


namespace TODOGO
{
    /// <summary>
    /// Логика взаимодействия для CalendarPage.xaml
    /// </summary>
    public partial class CalendarPage : Page
    {
        public CalendarPage(AppViewModel dc)
        {
            InitializeComponent();
            DataContext = dc;
        }
    }
}
