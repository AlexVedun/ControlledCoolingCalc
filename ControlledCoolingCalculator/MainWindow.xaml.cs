using Models.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
        private _10Mn2VNb _10Mn2VNb = null;
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
        private string rollingEndTemp;
        private bool isWaterFlowDownManual;
        private bool isWaterFlowUpManual;
        private string correctWaterFlowDown;
        private string correctWaterFlowUp;
        private string changeCoolingRate;
        private string changeTempEndCooling;


        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            ModelList = new ObservableCollection<string>
            {
                "10ХСНД",
                "К60",
                "10Г2ФБ"
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
                case "10Г2ФБ":
                    if (_10Mn2VNb == null)
                    {
                        _10Mn2VNb = new _10Mn2VNb();
                    }
                    Calculator = _10Mn2VNb;                    
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
                //Calculator.rollingEndTemp = GetDouble(RollingEndTemp, 0);
                Calculator.Calculate(IsWaterFlowDownManual, WaterFlowDownManual != null ? double.Parse(WaterFlowDownManual) : 0, IsWaterFlowUpManual, WaterFlowUpManual != null ? double.Parse(WaterFlowUpManual) : 0);
                TempBeginCooling = Math.Round(Calculator.tempBeginCooling).ToString();
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
        public string RollingEndTemp { get => rollingEndTemp; set { rollingEndTemp = value; NotifyPropertyChanged(); } }
        public bool IsWaterFlowDownManual { get => isWaterFlowDownManual; set { isWaterFlowDownManual = value; NotifyPropertyChanged(); } }
        public bool IsWaterFlowUpManual { get => isWaterFlowUpManual; set { isWaterFlowUpManual = value; NotifyPropertyChanged(); } }
        public string CorrectWaterFlowDown { get => correctWaterFlowDown; set { correctWaterFlowDown = value; NotifyPropertyChanged(); } }
        public string CorrectWaterFlowUp { get => correctWaterFlowUp; set { correctWaterFlowUp = value; NotifyPropertyChanged(); } }
        public string ChangeCoolingRate { get => changeCoolingRate; set { changeCoolingRate = value; NotifyPropertyChanged(); } }
        public string ChangeTempEndCooling { get => changeTempEndCooling; set { changeTempEndCooling = value; NotifyPropertyChanged(); } }

        public bool IsNotCorrectS1IsManual { get => (bool)!CorrectS1IsManual.IsChecked; }

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

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //double changeCoolingRate = GetDouble(ChangeCoolingRateTextBox.Text, 0);
            //double changeTempEndCooling = GetDouble(ChangeTempEndCoolingTextBox.Text, 0);
            //double waterFlowUp = IsWaterFlowUpManual ? GetDouble(WaterFlowUpManual, 0) : GetDouble(WaterFlowUp, 0);
            //double waterFlowDown = IsWaterFlowDownManual ? GetDouble(WaterFlowDownManual, 0) : GetDouble(WaterFlowDown, 0);
            //double correctWaterFlowUp = waterFlowUp * (changeCoolingRate * 0.12 - changeTempEndCooling * 0.035);
            //double correctWaterFlowDown = waterFlowDown * (changeCoolingRate * 0.12 - changeTempEndCooling * 0.035);
            //CorrectWaterFlowUpTextBox.Text = correctWaterFlowUp.ToString("F2");
            //CorrectWaterFlowDownTextBox.Text = correctWaterFlowDown.ToString("F2");
            //double changeCoolingRate = GetDouble(ChangeCoolingRate, 0);
            //double changeTempEndCooling = GetDouble(ChangeTempEndCooling, 0);
            //double waterFlowUp = IsWaterFlowUpManual ? GetDouble(WaterFlowUpManual, 0) : GetDouble(WaterFlowUp, 0);
            //double waterFlowDown = IsWaterFlowDownManual ? GetDouble(WaterFlowDownManual, 0) : GetDouble(WaterFlowDown, 0);
            //double correctWaterFlowUp = waterFlowUp * (changeCoolingRate * 0.12 - changeTempEndCooling * 0.035);
            //double correctWaterFlowDown = waterFlowDown * (changeCoolingRate * 0.12 - changeTempEndCooling * 0.035);
            //CorrectWaterFlowUp = correctWaterFlowUp.ToString("F2");
            //CorrectWaterFlowDown = correctWaterFlowDown.ToString("F2");
            //TextBox textBox = sender as TextBox;
            //string answerUp;
            //string answerDown;
            //double correction;
            //double waterFlowUp = 0;
            //double waterFlowDown = 0;
            //if (textBox.Text != "")
            //{
            //    correction = GetDouble(textBox.Text, 0);
            //    switch (textBox.Name)
            //    {
            //        case "CorrectCoolingRatePlusTextBox":
            //        case "CorrectCoolingRateMinusTextBox":                        
            //            waterFlowUp = correction / 0.0065;
            //            waterFlowDown = waterFlowUp * 2.337;                        
            //            break;
            //        case "CorrectTempEndCoolingPlusTextBox":                        
            //        case "CorrectTempEndCoolingMinusTextBox":                        
            //            waterFlowUp = correction / 0.0994;
            //            waterFlowDown = waterFlowUp * 2.337;                        
            //            break;
            //    }
            //    answerUp = Math.Round(waterFlowUp).ToString();
            //    answerDown = Math.Round(waterFlowDown).ToString();

            //}
            //else
            //{
            //    answerUp = "";
            //    answerDown = "";
            //}
            //switch (textBox.Name)
            //{
            //    case "CorrectCoolingRatePlusTextBox":
            //        CorrectWaterFlowUpPlusTextBox.Text = answerUp;
            //        CorrectWaterFlowDownPlusTextBox.Text = answerDown;
            //        break;
            //    case "CorrectCoolingRateMinusTextBox":
            //        CorrectWaterFlowUpMinusTextBox.Text = answerUp;
            //        CorrectWaterFlowDownMinusTextBox.Text = answerDown;
            //        break;
            //    case "CorrectTempEndCoolingPlusTextBox":
            //        CorrectWaterFlowUpTKOMinusTextBox.Text = answerUp;
            //        CorrectWaterFlowDownTKOMinusTextBox.Text = answerDown;
            //        break;
            //    case "CorrectTempEndCoolingMinusTextBox":
            //        CorrectWaterFlowUpTKOPlusTextBox.Text = answerUp;
            //        CorrectWaterFlowDownTKOPlusTextBox.Text = answerDown;
            //        break;
            //}
        }

        private void CorrectS2RollingEndTempChange_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            double beginWaterFlowUp = GetDouble(CorrectS2BeginWaterFlowUp.Text, 0);
            double beginWaterFlowDown = GetDouble(CorrectS2BeginWaterFlowDown.Text, 0);
            double rollingEndTempChange = GetDouble(textBox.Text, 0);

            switch(textBox.Name)
            {
                case "CorrectS2RollingEndTempIncreaseBy":
                    if (textBox.Text != "")
                    {
                        CorrectS2RollingEndTempDecreaseBy.IsEnabled = false;
                    }
                    else
                    {
                        CorrectS2RollingEndTempDecreaseBy.IsEnabled = true;
                    }
                    CorrectS2NewWaterFlowUp.Text = Math.Round(beginWaterFlowUp - 3.5 * rollingEndTempChange).ToString();
                    CorrectS2NewWaterFlowDown.Text = Math.Round(beginWaterFlowDown - 3.5 * rollingEndTempChange).ToString();
                    break;
                case "CorrectS2RollingEndTempDecreaseBy":
                    if (textBox.Text != "")
                    {
                        CorrectS2RollingEndTempIncreaseBy.IsEnabled = false;
                    }
                    else
                    {
                        CorrectS2RollingEndTempIncreaseBy.IsEnabled = true;
                    }
                    CorrectS2NewWaterFlowUp.Text = Math.Round(beginWaterFlowUp + 3.5 * rollingEndTempChange).ToString();
                    CorrectS2NewWaterFlowDown.Text = Math.Round(beginWaterFlowDown + 3.5 * rollingEndTempChange).ToString();
                    break;
            }
        }

        private void CorrectS1IsManual_Checked(object sender, RoutedEventArgs e)
        {
            CorrectS1NewRollerSpeed.IsEnabled = false;
        }

        private void CorrectS1IsManual_Unchecked(object sender, RoutedEventArgs e)
        {
            CorrectS1NewRollerSpeed.IsEnabled = true;
        }

        private void CorrectS1ChangeCoolingRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            double beginRollerSpeed = GetDouble(CorrctS1BeginRollerSpeed.Text, 0);
            double coolingRateChange = GetDouble(textBox.Text, 0);
            double beginWaterFlowUp = GetDouble(CorrectS1BeginWaterFlowUp.Text, 0);
            double beginWaterFlowDown = GetDouble(CorrectS1BeginWaterFlowDown.Text, 0);
            double newRollerSpeed = 0;

            switch (textBox.Name)
            {
                case "CorrectS1IncreaseCoolingRate":
                    if (textBox.Text != "")
                    {
                        CorrectS1DecreaseCoolingRate.IsEnabled = false;
                    }
                    else
                    {
                        CorrectS1DecreaseCoolingRate.IsEnabled = true;
                    }
                    if (!(bool)CorrectS1IsManual.IsChecked)
                    {
                        newRollerSpeed = beginRollerSpeed + 0.055 * coolingRateChange;
                        CorrectS1NewRollerSpeed.Text = newRollerSpeed.ToString("F2");
                    }
                    else
                    {
                        newRollerSpeed = GetDouble(CorrectS1ManualRollerSpeed.Text, 0);
                    }
                    break;
                case "CorrectS1DecreaseCoolingRate":
                    if (textBox.Text != "")
                    {
                        CorrectS1IncreaseCoolingRate.IsEnabled = false;
                    }
                    else
                    {
                        CorrectS1IncreaseCoolingRate.IsEnabled = true;
                    }
                    if (!(bool)CorrectS1IsManual.IsChecked)
                    {
                        newRollerSpeed = beginRollerSpeed - 0.055 * coolingRateChange;
                        CorrectS1NewRollerSpeed.Text = newRollerSpeed.ToString("F2");
                    }
                    else
                    {
                        newRollerSpeed = GetDouble(CorrectS1ManualRollerSpeed.Text, 0);
                    }
                    break;
            }
            CorrectS1NewWaterFlowUp.Text = Math.Round(beginWaterFlowUp * newRollerSpeed / beginRollerSpeed).ToString();
            CorrectS1NewWaterFlowDown.Text = Math.Round(beginWaterFlowDown * newRollerSpeed / beginRollerSpeed).ToString();
        }

        private void CorrectS1ManualRollerSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            CorrectS1IncreaseCoolingRate.Clear();
            CorrectS1DecreaseCoolingRate.Clear();
            double newRollerSpeed = GetDouble(CorrectS1ManualRollerSpeed.Text, 0);
            double beginRollerSpeed = GetDouble(CorrctS1BeginRollerSpeed.Text, 0);
            double beginWaterFlowUp = GetDouble(CorrectS1BeginWaterFlowUp.Text, 0);
            double beginWaterFlowDown = GetDouble(CorrectS1BeginWaterFlowDown.Text, 0);
            CorrectS1NewWaterFlowUp.Text = Math.Round(beginWaterFlowUp * newRollerSpeed / beginRollerSpeed).ToString();
            CorrectS1NewWaterFlowDown.Text = Math.Round(beginWaterFlowDown * newRollerSpeed / beginRollerSpeed).ToString();
        }

        private void menuHelp_Click(object sender, RoutedEventArgs e)
        {
            string manual = "Manual.docx";

            if (!File.Exists(manual))
            {
                System.Windows.Resources.StreamResourceInfo res =
                    Application.GetResourceStream(new Uri(manual, UriKind.Relative));
                var outputFile = new System.IO.FileStream(manual, System.IO.FileMode.OpenOrCreate);

                res.Stream.CopyTo(outputFile);
                res.Stream.Close();
                outputFile.Close();
            }
            Process.Start(manual);
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();

            aboutWindow.ShowDialog();
        }
    }
}
