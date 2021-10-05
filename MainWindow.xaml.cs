using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Wpforder
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        List<drink> drinks = new List<drink>();
        List<orderitem> order = new List<orderitem>();
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
            //MessageBox.Show(takeout);
        }

        private void button1_click(object sender, RoutedEventArgs e)
        {

            order.Clear();
            for (int i=0; i < drink_menu.Children.Count; i++)
            {
                StackPanel sp = drink_menu.Children[i] as StackPanel;
                CheckBox cb = sp.Children[0] as CheckBox;
                Slider sl = sp.Children[1] as Slider;
                int quantity = Convert.ToInt32(sl.Value);
                
                if (cb.IsChecked == true &&quantity !=0)
                {
                    int price = drinks[i].Prize;
                    order.Add(new orderitem() { index = i , Quantity = quantity, SubTotal = quantity * price});
                }
            }
            
            int total = 0;
            string message="";
            int sellprice = 0;
            Tb_order.Text = "";
            Tb_order.Text += $"您所訂購的飲料是{takeout} ,訂購清單如下:\n";
            int j = 1;
            foreach (orderitem oi in order)
            {
                total += oi.SubTotal;
                 
                if (total >= 500)
                {
                    message = "訂購滿500元以上8折";
                    sellprice = Convert.ToInt32(Math.Round(Convert.ToDouble(total) * 0.8));
                }
                else if (total >= 300)
                {
                    message = "訂購滿300元以上85折";
                    sellprice = Convert.ToInt32(Math.Round(Convert.ToDouble(total) * 0.85));
                }
                else if (total >= 200)
                {
                    message = "訂購滿200元以上9折";
                    sellprice = Convert.ToInt32(Math.Round(Convert.ToDouble(total) * 0.9));
                }
                else
                {
                    message = "訂購未滿200元不打折";
                    sellprice = total;

                }
                Tb_order.Text += $"第{j}項:{drinks[oi.index].Name}{drinks[oi.index].Size}" +
                    $"，每杯{drinks[oi.index].Prize}元，總共{oi.Quantity}杯，小計{oi.SubTotal}元\n";
                j++;

            }
            Tb_order.Text += $"訂購合計{total}元，{message}，售價{sellprice}元";

        }
    }
}
