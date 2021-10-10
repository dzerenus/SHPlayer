using System;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace SHPlayer
{
    /// <summary>
    /// Стартовая страница позволяет пользователю выбрать файл,
    /// который он хочет воспроизвести.
    /// Путь к выбранному файлу передаётся в главную форму.
    /// 
    /// by ShpriZZ
    /// </summary>
    public partial class StartFrame : Page
    {
        // Анимации наведения курсора на кнопку и ухода с неё.
        DoubleAnimation hoverAnim;
        DoubleAnimation leaveAnim;

        MainWindow mWindow;

        // Конструктор формы принимает главное окно, чтобы была
        // возможность вернуть путь к выбранному пользователем файлу. 
        public StartFrame(MainWindow main)
        {
            mWindow = main;

            // Настройка анимации наведения курсора.
            hoverAnim = new DoubleAnimation();
            hoverAnim.From = 1;
            hoverAnim.To = 0.6;
            hoverAnim.Duration = TimeSpan.FromMilliseconds(100);

            // Настройка анимации ухода курсора.
            leaveAnim = new DoubleAnimation();
            leaveAnim.From = 0.6;
            leaveAnim.To = 1;
            leaveAnim.Duration = TimeSpan.FromMilliseconds(100);

            InitializeComponent();
        }

        // При наведении курсора на кнопку выбора происходит соответствующая анимация.
        private void LeftMouseEnter(object sender, MouseEventArgs e)
        {
            hoverAnim.From = lChose.Opacity;

            lChose.BeginAnimation(OpacityProperty, hoverAnim);
            rChose.BeginAnimation(OpacityProperty, hoverAnim);
        }

        // При убирании курсора с кнопки выбора так же происходит анимация.
        private void LeftMouseLeave(object sender, MouseEventArgs e)
        {
            leaveAnim.From = lChose.Opacity;

            lChose.BeginAnimation(OpacityProperty, leaveAnim);
            rChose.BeginAnimation(OpacityProperty, leaveAnim);
        }

        // Открытие диалогового окна и выбор файла для воспроизведения.
        private void lChose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "MP3 File|*.mp3|WAV File|*.wav";
            opf.Title = "Chose sound file";

            bool? res = opf.ShowDialog();

            // Если файл выбран, передать путь к нему в главную форму.
            if (res == true) mWindow.FileName = opf.FileName;
        }
    }
}
