using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Dtree;
using System.Threading.Tasks;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class addmeaning : Page
    {
  
        public addmeaning(double x,double y)
        {
            this.InitializeComponent();
            ShowUp(x,y);
        }


        public void ShowUp(double x,double y)
        {
            add.IsOpen = true;
            add.HorizontalOffset = x+50;
            add.VerticalOffset = y + 50;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            addmore.addmeaning(text.Text);
            add.IsOpen = false;
        }

        public void Button_Click_1(object sender, RoutedEventArgs e)
        {
            add.IsOpen = false;
        }
    }
}
