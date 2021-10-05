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
using System.Windows.Threading;

namespace SHPlayer
{
    /// <summary>
    /// Логика взаимодействия для PlayerFrame.xaml
    /// </summary>
    public partial class PlayerFrame : Page
    {
        string format = @"hh\:mm\:ss";

        BitmapImage playImage = new BitmapImage(new Uri("pack://application:,,,/SHPlayer;component/Res/Play.png"));
        BitmapImage pausImage = new BitmapImage(new Uri("pack://application:,,,/SHPlayer;component/Res/Pause.png"));

        MediaPlayer player;
        DispatcherTimer timer;
        TimeSpan duration;

        public PlayerFrame(string fname)
        {

            player = new MediaPlayer();
            player.Open(new Uri(fname, UriKind.RelativeOrAbsolute));
            player.MediaOpened += (s, e) => Opened();
            player.MediaEnded += (s, e) => Stop();

            InitializeComponent(); 
        }

        void Opened()
        {
            duration = player.NaturalDuration.TimeSpan;
            lLength.Content = duration.ToString(format);

            player.Play();

            timer = new DispatcherTimer();
            timer.Tick += (s, a) => Tick();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Start();
        }

        void Stop()
        {
            timer.IsEnabled = false;
            iPlay.Source = playImage;

            player.Stop();

            Tick();
        }

        void Pause()
        {
            timer.IsEnabled = false;
            iPlay.Source = playImage;

            player.Pause();
        }

        void Play()
        {
            timer.IsEnabled = true;
            iPlay.Source = pausImage;

            player.Play();
        }

        void Tick()
        {
            TimeSpan position = player.Position;

            double ts = duration.TotalSeconds;
            double ns = position.TotalSeconds;
            rActiveTimeLine.Width = GetTimeLineLenght(ts, ns);

            lPosition.Content = position.ToString(format);
        }

        double GetTimeLineLenght(double allSecond, double nowSecond)
        {
            double maxWidth = rStaticTimeLine.Width;
            double onePix = maxWidth / allSecond;
            return onePix * nowSecond;
        }

        #region Логика работы кнопок.
        void TimeLineMouseUp(object sender, MouseEventArgs e)
        {
            double x = e.GetPosition(rStaticTimeLine).X;
            double pixtime = duration.TotalSeconds / rStaticTimeLine.Width;
            double res = pixtime * x;

            player.Position = new TimeSpan(0, 0, 0, (int)res, (int)(res/1000));

            Tick();
        }

        // Изменение цвета кнопок заголовка, когда курсор заходит на кнопку.
        void btnPlayUp(object sender, MouseEventArgs e)
        {
            if (timer.IsEnabled) Pause();
            else Play();
        }

        // Изменение цвета кнопок заголовка, когда курсор заходит на кнопку.
        void btnStopUp(object sender, MouseEventArgs e)
        {
            if (player.Position.TotalSeconds > 0) Stop();
        }
        #endregion

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
    }
}
