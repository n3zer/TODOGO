using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Mvvm;

namespace TODOGO
{
    /// <summary>
    /// Логика взаимодействия для SideMenuItem.xaml
    /// </summary>
    public partial class SideMenuItem :Button
    {
        public SideMenuItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        



        public Path Icon
        {
            get { return (Path)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Path), typeof(SideMenuItem), new PropertyMetadata(null));



        
        public string TextContent
        {
            get { return (string)GetValue(TextContentProperty); }
            set { SetValue(TextContentProperty, value); }
        }
        public static readonly DependencyProperty TextContentProperty = DependencyProperty.Register("TextContent", typeof(string),
        typeof(SideMenuItem), new FrameworkPropertyMetadata(null));



        public Page Page
        {
            get { return (Page)GetValue(PageProperty); }
            set { SetValue(PageProperty, value); }
        }

        public static readonly DependencyProperty PageProperty =
            DependencyProperty.Register("Page", typeof(Page), typeof(SideMenuItem), new PropertyMetadata(null));







    }
}
