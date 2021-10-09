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

namespace SHPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string FileName 
        {
            set
            {
                fileName = value;
                ToPlayerFrame();
            }
        }
        string fileName;
        Frame nowFrame;

        public MainWindow()
        {
            InitializeComponent();
            FMain.Content = new StartFrame(this);
            nowFrame = (Frame)FMain.Content;
        }

        void ToPlayerFrame()
        {
            FMain.Content = new PlayerFrame(fileName);
            nowFrame = (Frame)FMain.Content;

            var splits = fileName.Split('\\');
            string mname = splits[splits.Length-1];

            if (mname.Length > 40)
                mname = mname.Substring(0, 38) + "...";

            lHeader.Content = mname;
        }

        void ToInfoFrame(object sender, MouseButtonEventArgs e)
        {
            FMain.Content = new InfoFrame();

            iHelp.Visibility = Visibility.Hidden;
            iBack.Visibility = Visibility.Visible;
        }

        void ToBackFrame(object sender, MouseButtonEventArgs e)
        {
            FMain.Content = nowFrame;

            iBack.Visibility = Visibility.Hidden;
            iHelp.Visibility = Visibility.Visible;
        }

        #region Нажатие и отпускание кнопок.

        // Изменение цвета кнопок заголовка, когда курсор заходит на кнопку.
        void btnMouseLeave(object sender, MouseEventArgs e)
        {
            (sender as Image).Opacity = 1;
        }

        // Изменение цвета кнопок заголовка, когда курсор заходит на кнопку.
        void btnMouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Image).Opacity = 0.85;
        }

        // Изменение цвета кнопок заголовка, когда курсор заходит на кнопку.
        void btnMouseDown(object sender, MouseEventArgs e)
        {
            (sender as Image).Opacity = 0.6;
        }

        #endregion

        #region События работы с заголовком формы.

        void rHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        void iClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        void iHide_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion
    }
}
