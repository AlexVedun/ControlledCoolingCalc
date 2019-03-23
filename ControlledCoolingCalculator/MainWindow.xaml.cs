using Models.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlledCoolingCalculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<String> ModelList { get; set; }
        public Steel Calculator { get; set; }
        private _10CrSiNiCu _10CrSiNiCu = null;
        private K60 K60 = null;
        private string thickness;
        private string tempWater;
        private string tempBeginCooling;
        private string tempEndCooling;
        private string sectionCount;
        private string coolingRate;
        private string ratio;
        private string waterFlowDown;
        private string waterFlowUp;
        private string waterFlowDownManual;
        private string waterFlowUpManual;
        private string rollerSpeed;
        private bool isWaterFlowDownManual;
        private bool isWaterFlowUpManual;
        

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            ModelList = new ObservableCollection<string>
            {
                "10ХСНД",
                "К60"
            };
            Calculator = null;
        }

        private void ModelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            switch ((string)combo.SelectedValue)
            {
                case "10ХСНД":
                    if (_10CrSiNiCu == null)
                    {
                        _10CrSiNiCu = new _10CrSiNiCu();
                    }
                    Calculator = _10CrSiNiCu;
                    break;
                case "К60":
                    if (K60 == null)
                    {
                        K60 = new K60();
                    }
                    Calculator = K60;
                    break;
            }
            Ratio = Calculator.ratio.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Calculator != null)
            {
                //Calculator.thickness = double.Parse(Thickness);
                //Calculator.tempWater = double.Parse(TempWater);
                //Calculator.tempBeginCooling = double.Parse(TempBeginCooling);
                //Calculator.tempEndCooling = double.Parse(TempEndCooling);
                //Calculator.sectionCount = int.Parse(SectionCount);
                //Calculator.coolingRate = double.Parse(CoolingRate);
                //Calculator.ratio = double.Parse(Ratio);
                Calculator.thickness = GetDouble(Thickness, 0);
                Calculator.tempWater = GetDouble(TempWater, 0);
                Calculator.tempBeginCooling = GetDouble(TempBeginCooling, 0);
                Calculator.tempEndCooling = GetDouble(TempEndCooling, 0);
                Calculator.sectionCount = int.Parse(SectionCount);
                Calculator.coolingRate = GetDouble(CoolingRate, 0);
                Calculator.ratio = GetDouble(Ratio, 0);
                Calculator.Calculate(IsWaterFlowDownManual, WaterFlowDownManual != null ? double.Parse(WaterFlowDownManual) : 0, IsWaterFlowUpManual, WaterFlowUpManual != null ? double.Parse(WaterFlowUpManual) : 0);
                if (!IsWaterFlowDownManual)
                {
                    //WaterFlowDown = Calculator.waterFlowDown.ToString();
                    WaterFlowDown = Math.Round(Calculator.waterFlowDown).ToString();
                }
                if (!IsWaterFlowUpManual)
                {
                    //WaterFlowUp = Calculator.waterFlowUp.ToString();
                    WaterFlowUp = Math.Round(Calculator.waterFlowUp).ToString();
                }
                RollerSpeed = Calculator.rollerSpeed.ToString("F2");
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Thickness { get => thickness; set { thickness = value; NotifyPropertyChanged(); } }
        public string TempWater { get => tempWater; set { tempWater = value; NotifyPropertyChanged(); } }
        public string TempBeginCooling { get => tempBeginCooling; set { tempBeginCooling = value; NotifyPropertyChanged(); } }
        public string TempEndCooling { get => tempEndCooling; set { tempEndCooling = value; NotifyPropertyChanged(); } }
        public string SectionCount { get => sectionCount; set { sectionCount = value; NotifyPropertyChanged(); } }
        public string CoolingRate { get => coolingRate; set { coolingRate = value; NotifyPropertyChanged(); } }
        public string Ratio { get => ratio; set { ratio = value; NotifyPropertyChanged(); } }
        public string WaterFlowDown { get => waterFlowDown; set { waterFlowDown = value; NotifyPropertyChanged(); } }
        public string WaterFlowUp { get => waterFlowUp; set { waterFlowUp = value; NotifyPropertyChanged(); } }
        public string WaterFlowDownManual { get => waterFlowDownManual; set { waterFlowDownManual = value; NotifyPropertyChanged(); } }
        public string WaterFlowUpManual { get => waterFlowUpManual; set { waterFlowUpManual = value; NotifyPropertyChanged(); } }
        public string RollerSpeed { get => rollerSpeed; set { rollerSpeed = value; NotifyPropertyChanged(); } }
        public bool IsWaterFlowDownManual { get => isWaterFlowDownManual; set { isWaterFlowDownManual = value; NotifyPropertyChanged(); } }
        public bool IsWaterFlowUpManual { get => isWaterFlowUpManual; set { isWaterFlowUpManual = value; NotifyPropertyChanged(); } }

        public static double GetDouble(string value, double defaultValue)
        {
            double result;

            // Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                // Then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                // Then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }
            return result;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                calculateButton.Focus();                
                calculateButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textBox.SelectAll();
        }
    }
}
