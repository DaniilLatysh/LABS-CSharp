using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace WPF_lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Image bomb;
        private static Bomb b = new Bomb();
        
        private MediaPlayer playAircraft = new MediaPlayer();


        public MainWindow()
        {
            InitializeComponent();
            MoveLocation();
            


            playAircraft.Open(new Uri(@"D:\POIS\ОАПЯВУ\C#\Labs\WPF-lab4\WPF-lab4\Source\danilagame.mp3", UriKind.Absolute));
            playAircraft.Volume = 0.1;
            playAircraft.Play();
        }

        public void Aircraft()
        {

        }

        private void MoveLocation()
        {


            ThicknessAnimation ta = new ThicknessAnimation();
            Thickness endLock = new Thickness(-12300, 0, 0, 0);
            ta.By = endLock;
            ta.Duration = TimeSpan.FromSeconds(20);
            Place.BeginAnimation(Image.MarginProperty, ta);
            TankImage.BeginAnimation(Image.MarginProperty, ta);
            TankImage2.BeginAnimation(Image.MarginProperty, ta);
            TankImage3.BeginAnimation(Image.MarginProperty, ta);

        }



        private void AircraftImage_MouseMove(object sender, MouseEventArgs e)
        {
            int flyHeight = 550;

            Point pt = e.GetPosition(this);
            AircraftImage.Margin = new Thickness(Place.Margin.Left, pt.Y - flyHeight, Place.Margin.Left + 100, 0);


        }

        private void AircraftImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Bomb(e);

        }


        private void Bomb(MouseButtonEventArgs e)
        {
            
            Point pt = e.GetPosition(this);

            bomb = b.addBomb(pt, Place, TankImage, TankImage2, TankImage3, AircraftImage, Layout);

            Layout.Children.Add(b.addBomb(pt, Place, TankImage, TankImage2, TankImage3, AircraftImage, Layout));

        }


       


    }

    class Bomb
    {
        private Image bomb;

        private static Image exp;
        private MediaPlayer playExpl = new MediaPlayer();
        private Image Place, TankImage, TankImage2, TankImage3;
        private Grid Layout;
        public Image addBomb(Point pt, Image Place, Image TankImage, Image TankImage2, Image TankImage3, Image AircraftImage, Grid Layout)
        {
            this.Place = Place;
            this.TankImage = TankImage;
            this.TankImage2 = TankImage2;
            this.TankImage3 = TankImage3;
            this.Layout = Layout;

            int bombHeight = 900;
            int flyHeight = 500;


            bomb = new Image();

            bomb.Margin = new Thickness(200, pt.Y - flyHeight, 380, 0);
            bomb.Source = new BitmapImage(new Uri(@"D:\POIS\ОАПЯВУ\C#\Labs\WPF-lab4\WPF-lab4\Source\bomb.png"));
            bomb.Width = 20;


            ThicknessAnimation ta = new ThicknessAnimation();
            Thickness endLock = new Thickness(AircraftImage.Margin.Left - 500, bombHeight - pt.Y, AircraftImage.Margin.Left, 0);
            ta.By = endLock;
            ta.Duration = TimeSpan.FromSeconds(2);
            ta.Completed += new EventHandler(Ta_Completed);
            bomb.BeginAnimation(Image.MarginProperty, ta);

            return bomb;
        }
        
        /*--
         ---    Complite Animation   
         */       

        private void Ta_Completed(object sender, EventArgs e)
        {

            playExpl.Stop();
            playExpl.Open(new Uri(@"D:\POIS\ОАПЯВУ\C#\Labs\WPF-lab4\WPF-lab4\Source\bolshoy_vzryv.mp3", UriKind.Absolute));
            playExpl.Volume = 0.6;


            exp = new Image();
            exp.Source = new BitmapImage(new Uri(@"D:\POIS\ОАПЯВУ\C#\Labs\WPF-lab4\WPF-lab4\Source\Exp.png"));
            exp.Width = 100;
            exp.Margin = new Thickness(bomb.Margin.Left - Place.Margin.Left - 1600, bomb.Margin.Top, 0, 0);


            ThicknessAnimation ta = new ThicknessAnimation();
            Thickness endLock = new Thickness(bomb.Margin.Left - Place.Margin.Left - 7000, bomb.Margin.Top, 0, 0);
            ta.By = endLock;
            ta.Duration = TimeSpan.FromSeconds(8);
            exp.BeginAnimation(Image.MarginProperty, ta);


            if (bomb.Margin.Left < -5500 && bomb.Margin.Left > -6200)
            {
                TankImage.Visibility = Visibility.Hidden;
                bomb.Visibility = Visibility.Hidden;
                playExpl.Play();


            }
            else
            if (bomb.Margin.Left < -6600 && bomb.Margin.Left > -7000)
            {
                TankImage2.Visibility = Visibility.Hidden;
                bomb.Visibility = Visibility.Hidden;
                playExpl.Play();
            }
            else
            if (bomb.Margin.Left < -7400 && bomb.Margin.Left > -7700)
            {
                TankImage3.Visibility = Visibility.Hidden;
                bomb.Visibility = Visibility.Hidden;
                playExpl.Play();
            }
            else
            {
                bomb.Visibility = Visibility.Hidden;
                playExpl.Play();
            }

            bomb.Visibility = Visibility.Hidden;

            Layout.Children.Add(exp);
        
        }
    }

   



}
