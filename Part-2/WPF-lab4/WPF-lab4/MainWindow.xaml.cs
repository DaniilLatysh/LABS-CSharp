using System;
using System.Threading;
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
        private static Label lb = new Label();
        private static Bomb b = new Bomb();
        private static bool flag = true;
        
        private MediaPlayer playAircraft = new MediaPlayer();


        public MainWindow()
        {
            InitializeComponent();
            MoveLocation();
            Layout.Children.Add(lb);
            lb.Visibility = Visibility.Hidden;

            playAircraft.Open(new Uri(@"D:\POIS\ОАПЯВУ\C#\Labs\Part-2\WPF-lab4\WPF-lab4\Source\danilagame.mp3", UriKind.Absolute));
            playAircraft.Volume = 0.1;
            playAircraft.Play();
        }


        private void MoveLocation()
        {
            ThicknessAnimation ta = new ThicknessAnimation();
            Thickness endLock = new Thickness(-12000, 0, 0, 0);
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

            lb.Width = 200;
            lb.Height = 100;
            lb.Content = "Aircraft position: " + AircraftImage.Margin;
            lb.Margin = new Thickness(-700, -400, 0, 0);


        }


        private void AircraftImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Bomb(e);
        }


        private void Bomb(MouseButtonEventArgs e)
        {
            if (b.Flag) {
                Point pt = e.GetPosition(this);

                bomb = b.addBomb(pt, Place, TankImage, TankImage2, TankImage3, AircraftImage, Layout);

                Layout.Children.Add(b.addBomb(pt, Place, TankImage, TankImage2, TankImage3, AircraftImage, Layout));
            }
        }

        /*
         * Click on Aircraft
         */
        private void AircraftImage_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            if (!flag)
            {
                flag = true;
                lb.Visibility = Visibility.Visible;
            }
            else {
                flag = false;
                lb.Visibility = Visibility.Hidden;
            }
            
        }
    }

    class Bomb
    {

        private static Label lb = new Label();
        
        private MediaPlayer playExpl = new MediaPlayer();
        private Image Place, TankImage, TankImage2, TankImage3, exp, bomb, AircraftImage;
        private Grid Layout;
        private double speed;
        private bool _flag = true, flag;

        public Image addBomb(Point pt, Image Place, Image TankImage, Image TankImage2, Image TankImage3, Image AircraftImage, Grid Layout)
        {
            this.Place = Place;
            this.TankImage = TankImage;
            this.TankImage2 = TankImage2;
            this.TankImage3 = TankImage3;
            this.Layout = Layout;
            this.AircraftImage = AircraftImage;
            

            int bombHeight = 900;
            int flyHeight = 500;
            double timeFromSeconds = 1;
            /**
             * Create bomb
             */
            _flag = false;
            bomb = new Image();

            bomb.Margin = new Thickness(200, pt.Y - flyHeight, 380, 0);
            bomb.Source = new BitmapImage(new Uri(@"D:\POIS\ОАПЯВУ\C#\Labs\Part-2\WPF-lab4\WPF-lab4\Source\bomb.png"));
            bomb.Width = 20;

            bomb.MouseLeftButtonDown += Bomb_MouseLeftButtonDown;

            /**
             * Animation
             */

            ThicknessAnimation ta = new ThicknessAnimation();
            Thickness endLock = new Thickness(AircraftImage.Margin.Left - 500, bombHeight - pt.Y, AircraftImage.Margin.Left, 0);
            ta.By = endLock;
            ta.Duration = TimeSpan.FromSeconds(timeFromSeconds);
            ta.Completed += new EventHandler(Ta_Completed);
            bomb.BeginAnimation(Image.MarginProperty, ta);

            speed = (AircraftImage.Margin.Left - 500 - bomb.Margin.Left - Place.Margin.Left) / timeFromSeconds;



            return bomb;
        }

        public bool Flag {
            get
            {
                return _flag;
            }
        }

        private void Bomb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            

            Layout.Children.Remove(lb);
            lb.Width = 200;
            lb.Height = 100;

            
            lb.Content = "Bomb speed: " + -speed;
            lb.Margin = new Thickness(-700, -300, 0, 0);
            Layout.Children.Add(lb);
            lb.Visibility = Visibility.Hidden;
            if (!flag)
            {
                flag = true;
                lb.Visibility = Visibility.Visible;
            }
            else
            {
                flag = false;
                lb.Visibility = Visibility.Hidden;
            }
        }

        /**
         *    Complite Animation   
         */

        private void Ta_Completed(object sender, EventArgs e)
        {

            playExpl.Stop();
            playExpl.Open(new Uri(@"D:\POIS\ОАПЯВУ\C#\Labs\Part-2\WPF-lab4\WPF-lab4\Source\bolshoy_vzryv.mp3", UriKind.Absolute));
            playExpl.Volume = 0.6;


            exp = new Image();
            exp.Source = new BitmapImage(new Uri(@"D:\POIS\ОАПЯВУ\C#\Labs\Part-2\WPF-lab4\WPF-lab4\Source\Exp.png"));
            exp.Width = 100;
            exp.Margin = new Thickness(bomb.Margin.Left - Place.Margin.Left - 1100, bomb.Margin.Top, 0, 0);


            ThicknessAnimation ta = new ThicknessAnimation();
            Thickness endLock = new Thickness(bomb.Margin.Left - Place.Margin.Left - 7000, bomb.Margin.Top, 0, 0);
            ta.By = endLock;
            ta.Duration = TimeSpan.FromSeconds(8);
            exp.BeginAnimation(Image.MarginProperty, ta);


            if (bomb.Margin.Left < -6200 && bomb.Margin.Left > -6600)
            {
                TankImage.Visibility = Visibility.Hidden;
                bomb.Visibility = Visibility.Hidden;
                playExpl.Play();


            }
            else
            if (bomb.Margin.Left < -7000 && bomb.Margin.Left > -7200)
            {
                TankImage2.Visibility = Visibility.Hidden;
                bomb.Visibility = Visibility.Hidden;
                playExpl.Play();
            }
            else
            if (bomb.Margin.Left < -7800 && bomb.Margin.Left > -8000)
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

            Layout.Children.Add(exp);
            _flag = true;
        
        }
    }

   



}
