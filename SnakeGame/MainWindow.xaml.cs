using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SnakeGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int score;
        private int length = 100;
        private int headSize = 30;
        private Point startingPoint = new Point(100, 100);
        private Point currentPosition = new Point();
        private DispatcherTimer timer;
        public List<Point> Foods = new List<Point>();
        public List<Point> SnakePoints = new List<Point>();
        Random rnd = new Random();
        Direction direction;
        public MainWindow()
        {
            InitializeComponent();

            myCanvas.Focus();
            timer = new DispatcherTimer();
            timer.Tick += timerTick;
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Start();
            currentPosition = startingPoint;
            for (int n = 0; n < 1; n++)
            {
                CreateRectangle(n);
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            CreateAnEllipse(currentPosition);
            var poss = RectanglePossition();
            if (currentPosition.X != poss.Item1 && currentPosition.Y != poss.Item2)
            {
                if (currentPosition.X <= 0 || currentPosition.Y <= 0 || currentPosition.X > 760 || currentPosition.Y > 380)
                {
                    GameOver();
                    this.Close();
                }
                if (direction == Direction.Left && currentPosition.X > 0)
                {
                    currentPosition.X -= 1;
                    CreateAnEllipse(currentPosition);
                }
                if (direction == Direction.Right && currentPosition.X < Application.Current.MainWindow.Width)
                {
                    currentPosition.X += 1;
                    CreateAnEllipse(currentPosition);
                }
                if (direction == Direction.Top && currentPosition.Y > 0)
                {
                    currentPosition.Y -= 1;
                    CreateAnEllipse(currentPosition);
                }
                if (direction == Direction.Buttom && currentPosition.Y < Application.Current.MainWindow.Height)
                {
                    currentPosition.Y += 1;
                    CreateAnEllipse(currentPosition);
                }
            }
            int n = 0;
            foreach (Point point in Foods)
            {
                if ((Math.Abs(point.X - currentPosition.X) < headSize) &&
                        (Math.Abs(point.Y - currentPosition.Y) < headSize))
                {
                    length += 10;
                    PlaySound();
                    score += 10;
                    Foods.RemoveAt(n);
                    myCanvas.Children.RemoveAt(n);
                    CreateRectangle(n);
                    break;
                }
                n++;
            }
            //for (int q = 0; q < (SnakePoints.Count - headSize * 2); q++)
            //{
            //    Point point = new Point(SnakePoints[q].X, SnakePoints[q].Y);
            //    if ((Math.Abs(point.X - currentPosition.X) < (headSize)) &&
            //         (Math.Abs(point.Y - currentPosition.Y) < (headSize)))
            //    {
            //        GameOver();
            //        break;
            //    }
            //}
        }

        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (direction != Direction.Right)
                {
                    direction = Direction.Left;
                }
            }
            if (e.Key == Key.Right)
            {
                if (direction != Direction.Left)
                {
                    direction = Direction.Right;
                }
            }
            if (e.Key == Key.Up)
            {
                if (direction != Direction.Buttom)
                {
                    direction = Direction.Top;
                }
            }
            if (e.Key == Key.Down)
            {
                if (direction != Direction.Top)
                {
                    direction = Direction.Buttom;
                }
            }
        }

        public void CreateRectangle(int index)
        {
            Point food = new Point(rnd.Next(5, 620), rnd.Next(5, 380));
            Rectangle rect = new Rectangle()
            {
                Width = 15,
                Height = 15,
                Fill = Brushes.Green,
                Stroke = Brushes.Red,
                StrokeThickness = 2,
            };

            var poss = RectanglePossition();
            Canvas.SetLeft(rect, food.X);
            Canvas.SetTop(rect, food.Y);
            myCanvas.Children.Insert(index, rect);
            Foods.Insert(index, food);
        }

        public Tuple<int, int> RectanglePossition()
        {
            int index = 0, index1 = 0;
            int[] a = new int[] { 150, 120, 370, 345, 368, 124, 340 };
            Random rnd = new Random();
            index = rnd.Next(a.Length);
            int[] b = new int[] { 127, 254, 370, 64, 225, 119, 329 };
            Random rnd1 = new Random();
            index1 = rnd1.Next(b.Length);
            return new Tuple<int, int>(a[index], b[index1]);
        }

        public void CreateAnEllipse(Point currentposition)
        {
            Ellipse newEllipse = new Ellipse();
            newEllipse.Fill = Brushes.Green;
            newEllipse.Width = 30;
            newEllipse.Height = 30;

            Canvas.SetTop(newEllipse, currentposition.Y);
            Canvas.SetLeft(newEllipse, currentposition.X);

            int count = myCanvas.Children.Count;
            myCanvas.Children.Add(newEllipse);
            SnakePoints.Add(currentposition);
            if (count > length)
            {
                myCanvas.Children.RemoveAt(count - length + 9);
                SnakePoints.RemoveAt(count - length);
            }
        }
        private void GameOver()
        {
            MessageBox.Show("You Lose! Your score is " + score.ToString(), "Game Over", MessageBoxButton.OK, MessageBoxImage.Hand);
            this.Close();
        }
        public void PlaySound()
        {
            var __mediaPlayer = new MediaPlayer();
            var executionDirectory = Environment.CurrentDirectory;
            var path = System.IO.Path.Combine(executionDirectory, "Rattle-Snake-Hissing-A1-www.fesliyanstudios.com.mp3");
            __mediaPlayer.Open(new Uri(path));
            __mediaPlayer.Play();
        }
    }
}



