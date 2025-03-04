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



namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string disp = ""; // хранит в себе то, что выводится на дисплей 
        private char mathSymbol = ' '; //хранит в себе последний математический символ
        private double lastNum = 0; //хранит в себе последнее чисто, которое было введено 


        public MainWindow()
        {
            InitializeComponent();
        }

        private void СhangeOfSign_Click(object sender, RoutedEventArgs e)
        {
            string[] nums = disp.Split(mathSymbol);
            string firstNum = nums[0].Replace(",", "."); // переменная храние значение - первое число на дисплее
            if (nums.Length == 2 && nums[1] != "") // если на дисплее 2 числа
            {
                string secondNum = nums[1].Replace(".", ",");// переменная храние значение - второе число на дисплее
         
                if (secondNum.StartsWith("(-")) { // если надо убрать скоробочку, так как она уже есть
                    secondNum = nums[nums.Length - 1].Replace("(", "").Replace(")", "").Replace("-", "");  
                    disp = firstNum + mathSymbol + secondNum.ToString();
                }
                else if (mathSymbol == '+') // если был знак плюс, то заменяем его на минус, так как + и - равно -
                {
                    mathSymbol = '–';
                    disp = firstNum + mathSymbol + secondNum.ToString();
                }
                else if (mathSymbol == '–') // если был знак минус, то заменяем его на плюс, так как - и - равно +
                {
                    mathSymbol = '+';
                    disp = firstNum + mathSymbol + secondNum.ToString();
                }
                else { // если был знак * или /, то знак не меняем, но добавляем во второму числу знак минуса и кнопки (- )     
                    disp = firstNum + mathSymbol + "(-" + secondNum.ToString() + ")"; 
                }                        
            }
            else if (nums.Length == 1) // если на дисплее только одно число без знаков
            {
                if (nums[nums.Length - 1].StartsWith("(-")) disp = nums[nums.Length - 1].Replace("(", "").Replace(")", "").Replace("-", "");
                else disp = "(-" + firstNum.ToString() + ")";
            }
            display.Text = disp;
            Equality.Focus();
        }

        private void Dot_Click(object sender, RoutedEventArgs e) { AddDot(); }

        private void AddDot() // функция добавления точки 
        {
            if (disp == "") { disp = "0";}
            if (disp[disp.Length - 1] == '.')
            { // если уже есть, то отмена точки
                disp = disp.Remove(disp.Length - 1);
                display.Text = disp;
                return;
            }
            string[] nums = disp.Split(mathSymbol);
            string[] Value = nums[nums.Length - 1].Split('.');
            if (Value.Length > 1)
            {
                return;
            }
            if (string.IsNullOrEmpty(disp) || disp[disp.Length - 1] == mathSymbol) disp = disp + "0."; // если перед точкой нет цифр, то ставим 0
            else if (disp[disp.Length - 1] == ')') // если последний символ скобочка, то точка ставится перед скоробочкой
            {
                disp = disp.Remove(disp.Length - 1);
                disp += ".0)";
            }
            else disp = disp + ".";
            display.Text = disp;
            Equality.Focus();
        }

        private bool IsValidNumberLength(string lastNum) // функция проверки максимального кол-ва символов в числе
        {
            return (lastNum.Contains("-") && lastNum.Contains("(") && lastNum.Length < 11) ||
                   (lastNum.Contains("-") && !lastNum.Contains("(") && lastNum.Length < 9) ||
                   (!lastNum.Contains("-") && lastNum.Contains("(") && lastNum.Length < 10) ||
                   (!lastNum.Contains("-") && !lastNum.Contains("(") && lastNum.Length < 8);
        }
        private void Number_Click(object sender, RoutedEventArgs e)
        {
            if (disp == "0") disp = string.Empty; // если на экране только 0, то удаляем 0
            Button bt = (Button)sender;
            string num = bt.Content.ToString();
            AddNumber(num);
            Equality.Focus();
        }

        private void AddNumber(string num) // функция добавления числа на дисплей
        {

            string[] nums = disp.Split(mathSymbol);
            string[] Value = nums[nums.Length - 1].ToString().Replace(",", ".").Split('.');
            if (disp == "0" ) { disp = disp.Substring(0, disp.Length - 1); }
            // по условию - на число не должно состоят более чем из 8 цифр до запятой и 3 после запятой
            if (!string.IsNullOrEmpty(disp) && disp[disp.Length - 1] == ')' && Value.Length > 1 && Value[1].Length < 4)
            {
                disp = disp.Substring(0, disp.Length - 1); // удаляем последний символ
                AddSymbolOnDisplay(num);
                disp += ")";
                display.Text = disp;
            }
            if (!string.IsNullOrEmpty(disp) && disp[disp.Length - 1] == ')' && Value.Length == 1 &&  Value[0].Length < 10)
            {
                disp = disp.Substring(0, disp.Length - 1); // удаляем последний символ
                AddSymbolOnDisplay(num);
                disp += ")";
                display.Text = disp;
            }
            else if (Value.Length > 1 && Value[1].Length < 3) AddSymbolOnDisplay(num);  // по условию - на число не должно состоят более чем из 3 цифр после запятой                  
            else if (Value.Length == 1 && Value[0].Length < 8) AddSymbolOnDisplay(num); // по условию - на число не должно состоят более чем из 8 цифр до запятой                     
        }

        private string MathOperation(double firstNum, double secondNum) // функция математических операций
        {
            if (mathSymbol.ToString() == "+") disp = (Math.Round(firstNum + secondNum, 3)).ToString().Replace(",", ".");
            else if (mathSymbol.ToString() == "–") disp = (Math.Round(firstNum - secondNum, 3)).ToString().Replace(",", ".");
            else if (mathSymbol.ToString() == "*") disp = (Math.Round(firstNum * secondNum, 3)).ToString().Replace(",", ".");
            else if (mathSymbol.ToString() == "/") disp = (Math.Round(firstNum / secondNum, 3)).ToString().Replace(",", ".");
            return disp;
        }
        private void AC_Click(object sender, RoutedEventArgs e)
        {
            disp = "";
            display.Text = "";
            mathSymbol = ' ';
        }

        private void AddSymbolOnDisplay(string bt) // функция добавления цифры или символа на экран
        {
            disp = disp + bt;
            display.Text = disp;
        }
        private void C_Click(object sender, RoutedEventArgs e)
        {
            if (disp != "")
            {
                disp = disp.Remove(disp.Length - 1);
                display.Text = disp;
            }
        }
        private void Symbol_Click(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            AddSymbol(bt.Content.ToString());
            Equality.Focus();
        }

        private void AddSymbol(string bt)
        {
            if (string.IsNullOrEmpty(disp)) return;
            if (disp[disp.Length - 1] == '+' || disp[disp.Length - 1] == '–' || disp[disp.Length - 1] == '/' || disp[disp.Length - 1] == '*') // если на дисплее последние символы из +-*/, то просто меняем его
            {
                disp = disp.Remove(disp.Length - 1);
                display.Text = disp;
                mathSymbol = Convert.ToChar(bt);
                AddSymbolOnDisplay(bt);
                return;
            }
            else if (disp[disp.Length - 1] == '.') // если на дисплее последний символ "." , то добвляем 0 в конец
            {
                disp += "0";
            }
            else if (disp[disp.Length - 1] == ')' && disp[disp.Length - 2] == '.') // если на дисплее последние символы ".)", то меняем их на .0)
            {
                disp = disp.Remove(disp.Length - 1);
                disp += "0)";
            }

            if (disp.Contains(mathSymbol)) Calculation(); // если нужно что-то посчитать            
            mathSymbol = Convert.ToChar(bt);
            AddSymbolOnDisplay(bt);
        }

        private void Equality_Click(object sender, RoutedEventArgs e) { Calculation();  }

        private void Calculation() // функция корректного отображения в textbox
        {
            if (string.IsNullOrEmpty(disp)) return;
            string[] nums = disp.Split(mathSymbol);
            double firstNum = Convert.ToDouble(nums[0].Replace(".", ",").Replace("(", "").Replace(")", "")); // очищаем первое число от ( и ), меняет знак разделитель

            if (nums.Length == 2 && nums[1] != "") // если на дисплее 2 числа, то считаем их
            {
                double secondNum = Convert.ToDouble(nums[1].Replace(".", ",").Replace("(", "").Replace(")", ""));
                disp = MathOperation(firstNum, secondNum);
                lastNum = secondNum;
            }
            else if (nums.Length == 2 && nums[1] == "") // если на дисплее 1 число и заканчивается каким-то математическим знаком, то считаем число между собой
            {
                disp = MathOperation(firstNum, firstNum);
                lastNum = firstNum;
            }
            else if (nums.Length == 1) // если на дисплее только одно число без знаков, то считаем его и то, что есть в памяти
            {
                disp = MathOperation(firstNum, lastNum);
            }
            nums = disp.Split(mathSymbol);

            if (disp[0] == '-') disp = "(" + disp + ")"; // если нужно добавить скобочки
            if (disp == "∞" || disp == "NaN" || disp == "не число") { disp = ""; }
            display.Text = disp;
            Equality.Focus();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D0 || e.Key == Key.NumPad0) AddNumber("0");
            else if (e.Key == Key.D1 || e.Key == Key.NumPad1) AddNumber("1");
            else if (e.Key == Key.D2 || e.Key == Key.NumPad2) AddNumber("2");
            else if (e.Key == Key.D3 || e.Key == Key.NumPad3) AddNumber("3");
            else if (e.Key == Key.D4 || e.Key == Key.NumPad4) AddNumber("4");
            else if (e.Key == Key.D5 || e.Key == Key.NumPad5) AddNumber("5");
            else if (e.Key == Key.D6 || e.Key == Key.NumPad6) AddNumber("6");
            else if (e.Key == Key.D7 || e.Key == Key.NumPad7) AddNumber("7");
            else if (e.Key == Key.D8 || e.Key == Key.NumPad8) AddNumber("8");
            else if (e.Key == Key.D9 || e.Key == Key.NumPad9) AddNumber("9");
            else if (e.Key == Key.OemPlus || e.Key == Key.Add) AddSymbol("+");
            else if (e.Key == Key.OemMinus || e.Key == Key.Subtract) AddSymbol("–");
            else if (e.Key == Key.Divide) AddSymbol("/");
            else if (e.Key == Key.Multiply) AddSymbol("*");
            else if (e.Key == Key.Decimal) { AddDot(); }
            else if (e.Key == Key.Enter ) { Calculation(); }
        }
    }
}
