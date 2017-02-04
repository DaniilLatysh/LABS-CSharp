using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPF_lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Figure> figureColection = new List<Figure>();

        public MainWindow()
        {
            InitializeComponent();


        }

        //-- Click on canvase ----

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Point mousePosition = e.GetPosition(this);  // Get mouse point  

                int border = int.Parse(Thck.Text.ToString());
                Color lineColor = Color.FromRgb(byte.Parse(rL.Text), byte.Parse(gL.Text), byte.Parse(bL.Text));
                Color backgroundColor = Color.FromRgb(byte.Parse(rB.Text), byte.Parse(gB.Text), byte.Parse(bB.Text));

                // Create new figure
                Figure figureObj = new Figure(mousePosition, border, lineColor, backgroundColor);

                figureColection.Add(figureObj);
                Canvas.Children.Add(figureObj.GetFigeure);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        //-- Click on button SAVE ----
        private void Save_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            try
            {
                Random rand = new Random();
                string fileName = "file", extension = ".d";

                if (File.Exists(fileName + extension))
                {
                    fileName += rand.Next(1, 1001);
                }

                using (FileStream fs = File.Create(fileName + extension))
                {
                    fs.Close();

                    StreamWriter sw = new StreamWriter(fileName + extension, true);
                    foreach (Figure f in figureColection)
                    {
                        sw.WriteLine(f.ToString());
                    }

                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //-- Click on button OPEN ----
        private void Open_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                Point mousePos = new Point();
                Color lineColor, backgroundColor;

                string line = "";
                int border = 0;
                openFileDialog.Filter = "Text Files (.d)|*.d"; // Only this extension

                if (openFileDialog.ShowDialog() == true)
                {
                    FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                    if (fileInfo.Extension.ToLower() == ".d")
                    {
                        FileName.Content = "File name: " + fileInfo.Name;
                        Canvas.Children.Clear(); // Clear canvase for drawing figure

                        using (StreamReader sr = fileInfo.OpenText())
                        {

                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] split = line.Split(' ');
                                mousePos.X = Convert.ToDouble(split[0]);
                                mousePos.Y = Convert.ToDouble(split[1]);
                                border = Convert.ToInt32(split[9]);
                                lineColor = (Color)ColorConverter.ConvertFromString(split[7]);
                                backgroundColor = (Color)ColorConverter.ConvertFromString(split[8]);

                                Figure figOpen = new Figure(mousePos, border, lineColor, backgroundColor);
                                Canvas.Children.Add(figOpen.GetFigeure);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: ", ex.Message);
            }
        }

        private void MousePosition_MouseMove(object sender, MouseEventArgs e)
        {
            MousePosition.Content = "Mouse position: " + e.GetPosition(this).ToString();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }


    public class Figure
    {
        private Polygon _figureObj;
        private Color _BackgroundColor;
        private Color _LineColor;
        private int _Border;
        private Point _MousePosition;
        
        public Figure(Point mousePosition, int border, Color lineColor, Color backgroundColor)
        {

            _BackgroundColor = backgroundColor;
            _LineColor = lineColor;
            _Border = border;
            _MousePosition = mousePosition;



            //---Points----
            _figureObj = new Polygon();

            Point pt2 = new Point(mousePosition.X -50, mousePosition.Y );
            Point pt3 = new Point(mousePosition.X -50, mousePosition.Y - 45);
            Point pt4 = new Point(mousePosition.X -45, mousePosition.Y - 50);
            Point pt5 = new Point(mousePosition.X , mousePosition.Y -50);

            PointCollection polygonPoints = new PointCollection();

            polygonPoints.Add(mousePosition);
            polygonPoints.Add(pt2);
            polygonPoints.Add(pt3);
            polygonPoints.Add(pt4);
            polygonPoints.Add(pt5);

            _figureObj.Points = polygonPoints;

            ColorBrush();
        }

        //---Color----
        private void ColorBrush()
        {
           
            SolidColorBrush lineBrush = new SolidColorBrush();
            lineBrush.Color = _LineColor;

            SolidColorBrush backBrush = new SolidColorBrush();
            backBrush.Color = _BackgroundColor;

            _figureObj.Stroke = lineBrush;
            _figureObj.Fill = backBrush;
            _figureObj.StrokeThickness = _Border;
        }

        
        public Polygon GetFigeure
        {
            get { return _figureObj; }
        }

        public Color LineColor
        {
            get { return _LineColor; }
            set { _LineColor = value; }
        }
        public Color BackgroundColor
        {
            get { return _BackgroundColor; }
            set { _BackgroundColor = value; }
        }
         
        public int Border
        {
            get { return _Border; }
            set { _Border = value; }
        }
        

        public override string ToString()
        {
            return _MousePosition.X.ToString() + " " + _MousePosition.Y.ToString() + " " + _figureObj.Points.ToString() + " " + _LineColor.ToString() + " " + _BackgroundColor.ToString() + " " + _Border.ToString();
        }


    }
}
