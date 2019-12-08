using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;

namespace ControlledCoolingCalculator
{
    /// <summary>
    /// Логика взаимодействия для ManualWindow.xaml
    /// </summary>
    public partial class ManualWindow : Window
    {
        public ManualWindow()
        {
            string manual = "Manual.xps";

            InitializeComponent();

            System.Windows.Resources.StreamResourceInfo res =  Application.GetResourceStream(new Uri(manual, UriKind.Relative));
            MemoryStream ms = new MemoryStream();
            res.Stream.CopyTo(ms);
            Package pkg = Package.Open(ms);
            string strMemoryPackageName = string.Format("memorystream://{0}.xps", manual);
            Uri packageUri = new Uri(strMemoryPackageName);
            PackageStore.AddPackage(packageUri, pkg);
            XpsDocument doc = new XpsDocument(pkg, CompressionOption.Maximum, strMemoryPackageName);
            this.documentViewer.Document = doc.GetFixedDocumentSequence();
        }
    }
}
