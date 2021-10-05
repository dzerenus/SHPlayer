using Microsoft.Win32;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SHPlayer
{
    /// <summary>
    /// Логика взаимодействия для StartFrame.xaml
    /// </summary>
    public partial class StartFrame : Page
    {
        DoubleAnimation hoverAnim;
        DoubleAnimation leaveAnim;

        MainWindow mWindow;

        public StartFrame(MainWindow main)
        {
            mWindow = main;

            hoverAnim = new DoubleAnimation();
            hoverAnim.From = 1;
            hoverAnim.To = 0.6;
            hoverAnim.Duration = TimeSpan.FromMilliseconds(100);

            leaveAnim = new DoubleAnimation();
            leaveAnim.From = 0.6;
            leaveAnim.To = 1;
            leaveAnim.Duration = TimeSpan.FromMilliseconds(100);

            InitializeComponent();
        }

        private void LeftMouseEnter(object sender, MouseEventArgs e)
        {
            hoverAnim.From = lChose.Opacity;

            lChose.BeginAnimation(OpacityProperty, hoverAnim);
            rChose.BeginAnimation(OpacityProperty, hoverAnim);
        }

        private void LeftMouseLeave(object sender, MouseEventArgs e)
        {
            leaveAnim.From = lChose.Opacity;

            lChose.BeginAnimation(OpacityProperty, leaveAnim);
            rChose.BeginAnimation(OpacityProperty, leaveAnim);
        }

        private void lChose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var opf = new OpenFileDialog();
            opf.Filter = "MP3 File|*.mp3|WAV File|*.wav";
            opf.Title = "Chose sound file";

            bool? res = opf.ShowDialog();

            if (res == true) mWindow.FileName = opf.FileName;
        }
    }
}
