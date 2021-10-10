using System;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace SHPlayer
{
    /// <summary>
    /// Логика работы страницы воспроизведения файла.
    /// Уже при запуске страницы в неё необходимо передать путь к файлу.
    /// 
    /// by ShpriZZ
    /// </summary>
    public partial class PlayerFrame : Page
    {
        // Формат отображаемого времени.
        string format = @"hh\:mm\:ss";

        // Картинки для кнопок «Pause» и «Play».
        BitmapImage playImage = new BitmapImage(new Uri("pack://application:,,,/SHPlayer;component/Res/Play.png"));
        BitmapImage pausImage = new BitmapImage(new Uri("pack://application:,,,/SHPlayer;component/Res/Pause.png"));

        TimeSpan duration;     // Полная продолжительность звукового файла.
        MediaPlayer player;    // Переменная для медиа-плеера.
        DispatcherTimer timer; // Таймер, благодаря которому изменяется текущее время и таймлайн.
        
        // Конструктор страницы принимает путь к файлу, чтобы сразу же его запустить.
        public PlayerFrame(string fname)
        {
            // Настройка Медиа-плеера.
            player = new MediaPlayer();
            player.Open(new Uri(fname, UriKind.RelativeOrAbsolute));
            player.MediaOpened += (s, e) => Opened();
            player.MediaEnded += (s, e) => Stop();

            InitializeComponent();
        }

        // Процедура выполняемая при открытии файла.
        void Opened()
        {
            // Отображается полная длительность звукового файла.
            duration = player.NaturalDuration.TimeSpan;
            lLength.Content = duration.ToString(format);

            player.Play(); // Начинаем воспроизведение.

            // Запускаем таймер, который двигает таймлайн.
            timer = new DispatcherTimer();
            timer.Tick += (s, a) => Tick();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            timer.Start();
        }

        // Процедура выполняемя при нажатии стоп.
        void Stop()
        {
            // Отключаем таймер.
            // Изменяем картинку на «Play».
            timer.IsEnabled = false;
            iPlay.Source = playImage;
            
            // Остановка плеера.
            player.Stop();
            Tick();
        }

        // Процедура, вызываемая при паузе.
        void Pause()
        {
            // Отключаем таймер.
            // Изменяем картинку на «Play».
            timer.IsEnabled = false;
            iPlay.Source = playImage;

            // Активируем паузу.
            player.Pause();
        }

        // Процедура, вызываемая при продолжении воспроизведения..
        void Play()
        {
            // Включаем таймер.
            // Изменяем картинку на «Pause».
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
