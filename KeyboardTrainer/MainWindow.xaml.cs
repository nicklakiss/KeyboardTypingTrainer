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

namespace KeyboardTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected int _counter = 0;
        NoErrors noErrors = new NoErrors();
        LittleErrors littleErrors = new LittleErrors();
        Errors errors = new Errors();

        private void Connect(string style)
        {
            Uri uri = new Uri(style, UriKind.Relative);
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(uri);
            if (Application.Current.Resources.MergedDictionaries.Count > 1) Application.Current.Resources.MergedDictionaries.RemoveAt(Application.Current.Resources.MergedDictionaries.Count - 1);
            Application.Current.Resources.MergedDictionaries.Add(res);
        }


        private void ButtonHide_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            noErrors = null;
        }


        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) DragMove();
        }

        private string[] _words =
        {
            "apple", "banana", "cherry", "orange", "peach", "grapes"
        };
        private string _currentWord;
        private Random _random = new();

        private string GenerateWord() => _words[_random.Next(_words.Length)];

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            WordTextBlock.Text = GenerateWord();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (WordTextBlock.Text != "Welcome")
            {
            
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift) return;

            if ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
            {
                //MessageBox.Show($"{e.Key.ToString()}+Shift was pressed");
                if (e.Key.ToString()[0] == WordTextBlock.Text[0])
                {
                    
                    WordTextBlock.Text = WordTextBlock.Text.Remove(0, 1);
                }
                else 
                {
                    _counter++;
                    StatisticsTextBlock.Text = "Errors: " + _counter.ToString();
                }
                
            }
            else
            {
                //MessageBox.Show($"{e.Key.ToString()} was pressed");
                if (e.Key.ToString().ToLower()[0] == WordTextBlock.Text[0])
                {
                    WordTextBlock.Text = WordTextBlock.Text.Remove(0, 1);
                }
                else 
                {
                    _counter++;
                    StatisticsTextBlock.Text = "Errors: " + _counter.ToString();
                }
            }

            if (WordTextBlock.Text == String.Empty)
            {                
                if (_counter == 0)
                {
                    noErrors.Show();
                }
                else if (_counter < 3)
                {
                   littleErrors.Show();
                }
                else   
                {
                    errors.Show();
                }


            }
            }
        }

        private void DayTheme_Click(object sender, RoutedEventArgs e)
        {
                Connect("DayStyle.xaml");
        }

        private void NightTheme_Click(object sender, RoutedEventArgs e)
        {
                Connect("NightStyle.xaml");
        }
    }
}
