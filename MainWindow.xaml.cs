using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpforder
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        List<drink> drinks = new List<drink>();
        string takeout;
        public MainWindow()
        {
            InitializeComponent();

            drinks.Add(new drink() { Name = "咖啡", Size = "大杯", Prize = 60 });
            drinks.Add(new drink() { Name = "咖啡", Size = "中杯", Prize = 60 });
            drinks.Add(new drink() { Name = "紅茶", Size = "大杯", Prize = 30 });
            drinks.Add(new drink() { Name = "紅茶", Size = "中杯", Prize = 20 });
            drinks.Add(new drink() { Name = "綠茶", Size = "大杯", Prize = 30 });
            drinks.Add(new drink() { Name = "綠茶", Size = "中杯", Prize = 20 });

            DisplayDrinks(drinks);
        }
        private void DisplayDrinks(List<drink> mydrink)
        {
            foreach (drink d in mydrink)
            {
                StackPanel sp = new StackPanel();
                CheckBox cb = new CheckBox();
                //TextBox tb = new TextBox();
                Slider sl = new Slider();
                Label lb = new Label();

                cb.Content = d.Name + d.Size + d.Prize;
                //tb.Width = 100;
                sl.Width = 50;
                sl.Value = 0;
                sl.Minimum = 0;
                sl.Maximum = 10;
                sl.Width = 100;
                sl.TickPlacement = TickPlacement.BottomRight;
                sl.IsSnapToTickEnabled = true;

                sp.Orientation = Orientation.Horizontal;
                sp.Children.Add(cb);
                sp.Children.Add(sl);
                sp.Children.Add(lb);

                //sp.Children.Add(tb);
                lb.Width = 50;
                lb.Content = "0";
                drink_menu.Children.Add(sp);
                Binding myBinding = new Binding("Value");
                myBinding.Source = sl;
                lb.SetBinding(ContentProperty, myBinding);
            }
        }

        

        private void rb_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.IsChecked == true) takeout = rb.Content.ToString();
            MessageBox.Show(takeout);
        }
    }
}
