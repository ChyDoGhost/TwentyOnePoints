using System;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace TwentyOnePoints
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static Random random = new Random();
        int getValueCardPlayer = 0;
        int getValueCardComputer = 0;
        //Находим директорию с картами
        static string resourcesPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Resources\"));
        //Получаем все названия карт (full path)
        static string[] filesFromResources = Directory.GetFiles(resourcesPath);
        System.Windows.Controls.Image[] img = new System.Windows.Controls.Image[1];

        public int TakeACardPlayer()
        {
            //Выбираем случайную карту (full path)
            string getRandomCard = filesFromResources[random.Next(filesFromResources.Length - 1)];
            //imageGambler.Source = new BitmapImage(new Uri(getRandomCard, UriKind.Absolute));

            BitmapImage bitmapImage = new BitmapImage(new Uri(getRandomCard));
            img[0] = new System.Windows.Controls.Image();
            img[0].Source = bitmapImage;
            img[0].Width = 250;
            img[0].Height = 250;
            marginLeft += 25;
            Canvas.SetLeft(img[0], marginLeft);
            canvas1.Children.Add(img[0]);

            //Получаю имя файла
            string getFileName = Path.GetFileName(getRandomCard);
            //Обрезаю имя файла начиная с символа "_" (получаю значение карты)
            getValueCardPlayer += Convert.ToInt32(getFileName.Remove(getFileName.IndexOf("_")));

            return getValueCardPlayer;
        }

        int marginLeft = 0;

        public int TakeACardComputer()
        {

            //Выбираем случайную карту (full path)
            string getRandomCard = filesFromResources[random.Next(filesFromResources.Length - 1)];

            BitmapImage bitmapImage = new BitmapImage(new Uri(getRandomCard));
            img[0] = new System.Windows.Controls.Image();
            img[0].Source = bitmapImage;
            img[0].Width = 250;
            img[0].Height = 250;
            marginLeft += 25;
            Canvas.SetLeft(img[0], marginLeft);
            canvas2.Children.Add(img[0]);

            //Получаю имя файла
            string getFileName = Path.GetFileName(getRandomCard);
            //Обрезаю имя файла начиная с символа "_"
            getValueCardComputer += Convert.ToInt32(getFileName.Remove(getFileName.IndexOf("_")));

            return getValueCardComputer;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            int scorePlayer = TakeACardPlayer();
            int scoreComputer = TakeACardComputer();

            StringBuilder stringBuilderPlayer = new StringBuilder($"Ваш счет: {scorePlayer}");
            StringBuilder stringBuilderComputer = new StringBuilder($"Счет банкира: {scoreComputer}");

            label1.Content = stringBuilderPlayer;
            label2.Content = stringBuilderComputer;


            if ((scoreComputer > 21) || (scorePlayer == 21))
            {
                button1.IsEnabled = false;
                button2.IsEnabled = true;

                MessageBox.Show("Вы выиграли!", "Игра окончена!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if ((scorePlayer > 21) || (scoreComputer == 21))
            {
                button1.IsEnabled = false;
                button2.IsEnabled = true;
                MessageBox.Show("Вы проиграли!", "Игра окончена!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            button1.IsEnabled = true;

            getValueCardPlayer = 0;
            getValueCardComputer = 0;
            marginLeft = 0;

            label1.Content = String.Empty;
            label2.Content = String.Empty;

            canvas1.Children.Clear();
            canvas2.Children.Clear();

            button2.IsEnabled = false;

        }
    }
}
