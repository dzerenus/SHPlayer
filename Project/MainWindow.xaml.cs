using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace SHPlayer
{
    /// <summary>
    /// Оболочка для MP3 плеера by ShpriZZ.
    /// Это мой первый тестовый проект в рамках кодерского челленджа.
    /// Я не использовал здесь каких-либо паттернов оформления, так как программа очень простая.
    /// 
    /// Telegram: https://t.me/ShpriZZ
    /// GitHub: https://github.com/a-ShpriZZ-a
    /// </summary>
    public partial class MainWindow : Window
    {
        // Переменная под путь выбранного пользователем файла.
        // Когда пользователь указывает на стартовой странице путь к файлу,
        // вызывается процедура, которая изменияет страницу на фрейме.
        public string FileName { set => ToPlayerFrame(value); }

        // Глобальная переменная для страницы плеера.
        // Она необходима, чтобы была возможность вернуться со страницы
        // с информацие о программе на страницу воспроизведения, не прерывая его.
        private PlayerFrame pFrame;

        public MainWindow()
        {
            InitializeComponent();
            FMain.Content = new StartFrame(this);
        }

        // Процедура смена страницы Frame на страницу с плеером.
        void ToPlayerFrame(string fileName)
        {
            // Меняем страницу фрейма на аудиоплеер и сохраняем.
            pFrame = new PlayerFrame(fileName);
            FMain.Content = pFrame;

            // Получаем из пути имя файла.
            string[] splits = fileName.Split('\\');
            string mname = splits[splits.Length-1];

            // Если имя слишком длинное, обрезаем его.
            if (mname.Length > 40)
                mname = mname.Substring(0, 38) + "...";

            // Ставим полученное имя файла в заголовк программы.
            lHeader.Content = mname;
        }

        // Процедура перехода на страницу и нформацией о программе.
        // Вызывается при нажатии на соответствующую кнопку в окне.
        void ToInfoFrame(object sender, MouseButtonEventArgs e)
        {
            FMain.Content = new InfoFrame();

            // Меняем кнопки «Назад» и «Справка» местами.
            iHelp.Visibility = Visibility.Hidden;
            iBack.Visibility = Visibility.Visible;
        }

        // Нажатие на кнопку возвращения со страницы справки.
        void ToBackFrame(object sender, MouseButtonEventArgs e)
        {
            // Если пользователь открывал справку со страницы плеера, возвращаемся на неё.
            // Иначе открываем стартовую страницу.
            if (pFrame != null) FMain.Content = pFrame;
            else FMain.Content = new StartFrame(this);

            // Меняем кнопки «Назад» и «Справка» местами.
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

        // Возможность перемещать форму при зажатии левой кнопки мыши на заголовке.
        void rHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        // Выход из программы при нажатии на крестик.
        void iClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        // Сворачивание окна при нажатии на кнопку.
        void iHide_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        #endregion
    }
}
