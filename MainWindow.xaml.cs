using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const double FINISH = 970; // координата точки ФИНИШ
        const double START = 15; // координата точки СТАРТ
        int finishPosition = 0; // счетчик позиции кнопки на ФИНИШЕ

        Thread btnThread1;
        Thread btnThread2;
        Thread btnThread3;
        Thread btnThread4;

        Random rnd;

        public delegate void Helper(Button button, Thread thread);
        Helper helper;
        public MainWindow()
        {
            helper = new Helper(Motion);
            rnd = new Random();
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {              
            btnThread1 = new Thread(MoveBtn1)
            {
                IsBackground = true
            };
            btnThread1.Start();

            btnThread2 = new Thread(MoveBtn2)
            {
                IsBackground = true
            };
            btnThread2.Start();

            btnThread3 = new Thread(MoveBtn3)
            {
                IsBackground = true
            };
            btnThread3.Start();

            btnThread4 = new Thread(MoveBtn4)
            {
                IsBackground = true
            };
            btnThread4.Start();           

            BtnStart.IsEnabled = false;
            BtnRestart.IsEnabled = false;
        }

        void Motion(Button button, Thread thread)
        {
            double currentPosition;           
            currentPosition = Canvas.GetLeft(button);
            Canvas.SetLeft(button, currentPosition + rnd.Next(3,19));
            if (currentPosition >= FINISH)
            {
                finishPosition++;
                Canvas.SetLeft(button, FINISH);
                button.Content = $"Finished #{finishPosition}";
                thread.Name = "Fin";
                if (finishPosition == 4)
                    BtnRestart.IsEnabled = true;
            }
        }
        private void MoveBtn1(object obj)
        {
            while (!(btnThread1.Name == "Fin"))
            {
                Application.Current.Dispatcher.BeginInvoke(helper, Btn1, btnThread1);
                Thread.Sleep(150);
            }
        }
        private void MoveBtn2(object obj)
        {
            while (!(btnThread2.Name == "Fin"))
            {              
                Application.Current.Dispatcher.BeginInvoke(helper, Btn2, btnThread2);
                Thread.Sleep(150);
            }
        }
        private void MoveBtn3(object obj)
        {
            while (!(btnThread3.Name == "Fin"))
            {                
                Application.Current.Dispatcher.BeginInvoke(helper, Btn3, btnThread3);
                Thread.Sleep(150);
            }
        }
        private void MoveBtn4(object obj)
        {
            while (!(btnThread4.Name == "Fin"))
            {
                Application.Current.Dispatcher.BeginInvoke(helper, Btn4, btnThread4);
                Thread.Sleep(150);
            }
        }
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(Btn1, START);
            Canvas.SetLeft(Btn2, START);
            Canvas.SetLeft(Btn3, START);
            Canvas.SetLeft(Btn4, START);
            Btn1.Content = "Button 1"; 
            Btn2.Content = "Button 2";
            Btn3.Content = "Button 3";
            Btn4.Content = "Button 4";
            BtnStart.IsEnabled = true;
            finishPosition = 0;
        }
    }
}
