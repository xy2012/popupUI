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
using System.Threading.Tasks;
//using MASTER_CONTROLL;
using Dtree;
using System.Resources;
using Windows.UI.Xaml.Media.Imaging;
using System.Text;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public class addmore
    {
        public static void  addmeaning(string s)
        {
            Popup.add_temp = s;
            operation.addmeaning(Popup.ptn,s);
        }

    }

    public sealed partial class Popup : Page
    {
        int pointerstate = 0;
        double PX, PY, subX, subY;
        int flag_move = 0;
        public int style_cnt = 1;
        private bool closed_flag = false;
        public  Popup()
        {
            this.InitializeComponent();
            mborder.AddHandler(PointerPressedEvent, new PointerEventHandler(Popup_PointerPressed), true);
            mborder.AddHandler(PointerReleasedEvent, new PointerEventHandler(WordPopup_PointerReleased), true);
            mborder.AddHandler(PointerMovedEvent, new PointerEventHandler(WordPopup_PointerMoved), true);
            mborder.AddHandler(PointerExitedEvent, new PointerEventHandler(WordPopup_PointerExited), true);
            mborder.AddHandler(PointerEnteredEvent, new PointerEventHandler(WordPopup_PointerEntered), true);
            mborder.AddHandler(PointerPressedEvent, new PointerEventHandler(WordPopup_PointerPressed), true);
        }

        void WordPopup_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            flag_move = 1;
            Windows.UI.Xaml.Input.Pointer Poi1 = e.Pointer;
            Border mborde_ = sender as Border;
            if (Poi1.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                Windows.UI.Input.PointerPoint ptrPt1 = e.GetCurrentPoint(mborde_);
                subX = ptrPt1.Position.X;
                subY = ptrPt1.Position.Y;
                /*
                if (pointerstate == 1)
                {
                    if (subX < 25)
                    {
                        WordPopup.HorizontalOffset = WordPopup.HorizontalOffset + subX - PX-50;
                        WordPopup.VerticalOffset = WordPopup.VerticalOffset + subY - PY;
                        PX = subX-50;
                        PY = subY;
                    }
                    if (subY < 25)
                    {
                        WordPopup.HorizontalOffset = WordPopup.HorizontalOffset + subX - PX;
                        WordPopup.VerticalOffset = WordPopup.VerticalOffset + subY - PY-50;
                        PX = subX;
                        PY = subY-50;
                    }
                    if (subX > WordPopup.ActualWidth - 25)
                    {
                        WordPopup.HorizontalOffset = WordPopup.HorizontalOffset + subX - PX +50;
                        WordPopup.VerticalOffset = WordPopup.VerticalOffset + subY - PY;
                        PX = subX+50;
                        PY = subY;
                    }
                    if (subY > WordPopup.ActualHeight - 35)
                    {
                        WordPopup.HorizontalOffset = WordPopup.HorizontalOffset + subX - PX;
                        WordPopup.VerticalOffset = WordPopup.VerticalOffset + subY - PY + 50;
                        PX = subX;
                        PY = subY+50;
                    }
                   
                }
                */
                if (pointerstate == 1)
                {
                    //      if (subX - PX > 2 || subY - PY > 2 || PX - subX > 2 || PY - subY > 2)
                    //       {
                    WordPopup.HorizontalOffset = WordPopup.HorizontalOffset + subX - PX;
                    WordPopup.VerticalOffset = WordPopup.VerticalOffset + subY - PY;
                    //     }
                    PX = ptrPt1.Position.X;
                    PY = ptrPt1.Position.Y;
                }
                e.Handled = true;
            }
        }

        void WordPopup_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            ((Border)sender).ReleasePointerCapture(e.Pointer);
            if (flag_move == 1)
            {
                if (subX - PX > 2 || subY - PY > 2 || PX - subX > 2 || PY - subY > 2)
                {
                    WordPopup.HorizontalOffset = WordPopup.HorizontalOffset + subX - PX;
                    WordPopup.VerticalOffset = WordPopup.VerticalOffset + subY - PY;
                }
            }
            pointerstate = 0;
        }

        //Can only get capture on PointerPressed (i.e. touch down, mouse click, pen press)
        void WordPopup_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            pointerstate = 1;
            flag_move = 0;
        }

        void WordPopup_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            pointerstate = 0;
        }

        void WordPopup_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            pointerstate = 0;
        }

        public void Popup_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Windows.UI.Xaml.Input.Pointer Poi = e.Pointer;
            Border mborde_ = sender as Border;
            if (Poi.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Mouse)
            {
                Windows.UI.Input.PointerPoint ptrPt = e.GetCurrentPoint(mborde_);

                PX = ptrPt.Position.X;
                PY = ptrPt.Position.Y;
                subX = 0;
                subY = 0;
                e.Handled = true;
            }
        }

        public String word;
        public static int count = 0;

        public void GetWord(string tmp_word)
        {
            word = tmp_word;
            this.ShowUpWord();
        }

        public void ShowUpWord()
        {
            WordPopup.IsOpen = true;
            Showup.Text = "meaning";
        }

        public void changetxt(string a)
        {
            textBox1.Text = a;
        }
        public  void addbutton(string b, string a, int i)
        {
            Button btn = new Button();
            this.shiyi.Children.Add(btn);
            btn.Content = a;
            btn.Name = i + b;
            btn.Click += new RoutedEventHandler(click_button);
            btn.Foreground = textBox1.Foreground;
            ImageBrush imageBrush = new ImageBrush();
            btn.Background = imageBrush;//去背景

        }

        private char ch;
        private bool[] tap = new bool[13];
        public static int[] tap_ = new int[10];
        public void click_button(object sender, RoutedEventArgs e)
        {
            if (tap[11] == true)
            {
                this.todataset.Content = "update";
                tap[11] = false;
            }

            Button mbt = (Button)sender;
            string shiyi_full = mbt.Name.Remove(0,1);
            ch = mbt.Name[0];
            int i_ch = Convert.ToInt16(ch)-48;
            int c;
            if (tap[i_ch] == false)
            {
                this.Showup.Text = shiyi_full;
               
                for (c = 0; c < 10; c++)
                    tap[c] = false;
                tap[i_ch] = true;
                tap_[i_ch]++;
                operation.uprank(ptn, i_ch, 4);
            }
            else
            {
                this.Showup.Text = "meaning";
                for(c=0;c<10;c++)
                       tap[c] = false;
            }
        }
        public void getorder(object sender, RoutedEventArgs e)
        {
            closed_flag = true;
            operation.clear(ptn);
            ptn = null;
            this.WordPopup.IsOpen = false;
           
        }
        
         private void Add(object sender, RoutedEventArgs e)
        {
            string s = operation.gettree2string(ptn);
            /*

                        if (tap[11] == false)
                        {

                            string s = operation.gettree2string(ptn);
                            MESSAGE message_temp = new MESSAGE();
                            message_temp.parti = PARTICIPANT.U_M;
                            message_temp.method_u_m = METHOD_U_M.method_add_to_vocabularybook;
                            message_temp.in_message.wordbook_str = "default";
                            message_temp.in_message.word_str = s;

                            SPECIFIC_COMMUNICATION communicate = new SPECIFIC_COMMUNICATION();
                            communicate.message = message_temp;
                            communicate.send_messageAsync();



                        }
                        else
                        {
                            string s = this.textBox1.Text;
                            operation.clear(ptn);
                            string[] split = s.Split(new char[]{' '});
                            string word_temp = split[0].Substring(2);
                            MESSAGE message_temp = new MESSAGE();
                            message_temp.parti = PARTICIPANT.U_M;
                            message_temp.method_u_m = METHOD_U_M.method_delete_from_vocabularybook;
                            message_temp.in_message.wordbook_str = "default";
                            message_temp.in_message.word_str = word_temp;

                            SPECIFIC_COMMUNICATION communicate = new SPECIFIC_COMMUNICATION();
                            communicate.message = message_temp;
                            communicate.send_messageAsync();
                        }
                        */

        }
       
        public void Stylechange_Click(object sender, RoutedEventArgs e)
        {
            string groundpicture = "ms-appx:///backlog/" + style_cnt + ".jpg";
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(groundpicture, UriKind.RelativeOrAbsolute)
                    );
                log.Background = imageBrush;
            if (style_cnt < 11)
                style_cnt++;
            else
                style_cnt = 1;
        }

        public static string temp;
        public static string re_tem;
        public static string re_tem_;
        public static bool flags_tap=false;
        public static bool flags_ = false;
        public static int k_cnt;

        public static string add_temp="0";
        

        /*
        private void update_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
           
            if (flags_tap == false)
            {
                if (tap[12] == false)
                {
                    if (textBox1.Text.Length > 15)
                    {
                        addbutton("more. " + textBox1.Text, "more. " + textBox1.Text.Substring(0, 15), k_cnt);
                        
                    }
                    else
                    {
                        addbutton("more. " + textBox1.Text, "more. " + textBox1.Text, k_cnt);
                        
                    }
                    re_tem_ = k_cnt + "more. " + textBox1.Text;
                    re_tem = textBox1.Text;
                    k_cnt = k_cnt + 1;
                    textBox1.Text = temp;
                    btn.Content = "sure?";
                    tap[12] = true;     
                    this.Showup.Text = "戳 cancel 可以取消操作哦!";
                }
                else if(tap[12]==true)
                {
                    operation.addmeaning(ptn, re_tem);
                    btn.Content = "sure!";
                    this.Showup.Text = "meaning";
                    tap[12] = false;
                    
                }
            }
        }
        */

        public async void update_Click(object sender, RoutedEventArgs e)
        {
            double x = WordPopup.HorizontalOffset;
            double y = WordPopup.VerticalOffset;
            addmeaning mess = new addmeaning(x,y);
        }

        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            if (add_temp != "0")
            {
                if (add_temp.Length > 15)
                {
                    addbutton("more. " + add_temp, "more. " + add_temp.Substring(0, 15), k_cnt);

                }
                else
                {
                    addbutton("more. " + add_temp, "more. " + add_temp, k_cnt);
                }
                k_cnt = k_cnt + 1;
                add_temp = "0";
            }
        }

        /*
        private void cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            flags_tap = true;
            if (tap[12] == true)
            {

                Button btn = (Button)sender;
                foreach (Control ctl in this.shiyi.Children)
                {
                    if (ctl is Button)
                    {
                        if (ctl.Name == re_tem_)
                        {
                            ctl.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
            this.selfupdate.Content = "Uself";
            this.Showup.Text = "meaning";
            tap[12] = false;
            k_cnt = k_cnt - 1;
        }
*/
        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (flags_tap==true)
            {
                flags_tap = false;
                tap[12] = false;
            }
        }

        public static MLNode<string> ptn = null;
        public Task<string> wakeUI(string a,double x,double y)
        {
            GetWord(a);
            WordPopup.HorizontalOffset = x;
            WordPopup.VerticalOffset = y;
            if (a[0] == '0' && a[1] == '0')
            {
                this.todataset.Content = "add";
                tap[11] = false;
            }
            else
            {
                this.todataset.Content = "delete";
                tap[11] = true;
            }
            ptn = operation.getstring2tree(a, this);
           
            for (int i = 0; i < operation.K_cnt; i++)
            {
                tap_[i] = operation.K_cnt - i;
            }
            temp = this.textBox1.Text;
            tap[12] = false;
            k_cnt = operation.K_cnt;
            return Task<string>.Run(() =>
            {
                return "00";
            });
        }

       

        public async Task<string> check_closes()
        {
            while (!closed_flag) { }
            return "closed";

        }

    }
   
}
