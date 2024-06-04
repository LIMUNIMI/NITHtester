using NITHlibrary.Tools.Ports;
using NITHtester.Elements;
using NITHtester.Modules;
using NITHtester.Setups;
using System.Windows;
using System.Windows.Controls;

namespace NITHtester
{
    /// <summary>
    /// Interaction logic for the instrument window
    /// </summary>
    public partial class MainWindow : Window
    {
        private ISetup Setup;

        public MainWindow()
        {
            InitializeComponent();
        }

        public List<ComboBox> GaugesList { get; set; }

        private SupportedPortTypes PortType { get; set; } = SupportedPortTypes.USB;

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (IsConnected())
            {
                DisconnectAll();
            }
            else
            {
                switch (PortType)
                {
                    case SupportedPortTypes.USB:
                        Rack.USBreceiver.Connect();
                        break;

                    case SupportedPortTypes.UDP:
                        Rack.UDPreceiver.Connect();
                        break;
                }
            }

            UpdateIndicators();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            Rack.Paused = !Rack.Paused;

            if (Rack.Paused)
            {
                Rack.RenderingModule.StopRendering();
            }
            else
            {
                Rack.RenderingModule.StartRendering();
            }

            UpdateIndicators();
        }

        private void btnUSBPortMinus_Click(object sender, RoutedEventArgs e)
        {
            DisconnectAll();
            Rack.USBreceiver.Port--;
            UpdateIndicators();
        }

        private void btnUSBPortPlus_Click(object sender, RoutedEventArgs e)
        {
            DisconnectAll();
            Rack.USBreceiver.Port++;
            UpdateIndicators();
        }

        private void btnUSBPortScan_Click(object sender, RoutedEventArgs e)
        {
            if (PortType == SupportedPortTypes.USB)
            {
                Rack.PortDetector.Scan();
            }
        }

        private void DisconnectAll()
        {
            Rack.USBreceiver?.Disconnect();
            Rack.UDPreceiver?.Disconnect();
        }

        private void InitializeGauges()
        {
            Rack.BindableGauge1 = new BindableGauge(prbGauge1, lstGauge1, txtMinGauge1, txtMaxGauge1, txtOffGauge1, chkProp1);
            Rack.BindableGauge2 = new BindableGauge(prbGauge2, lstGauge2, txtMinGauge2, txtMaxGauge2, txtOffGauge2, chkProp2);
            Rack.BindableGauge3 = new BindableGauge(prbGauge3, lstGauge3, txtMinGauge3, txtMaxGauge3, txtOffGauge3, chkProp3);
            Rack.BindableGauge4 = new BindableGauge(prbGauge4, lstGauge4, txtMinGauge4, txtMaxGauge4, txtOffGauge4, chkProp4);
            Rack.BindableGauge5 = new BindableGauge(prbGauge5, lstGauge5, txtMinGauge5, txtMaxGauge5, txtOffGauge5, chkProp5);

            Rack.BindableGauges = new List<BindableGauge>
            {
                Rack.BindableGauge1,
                Rack.BindableGauge2,
                Rack.BindableGauge3,
                Rack.BindableGauge4,
                Rack.BindableGauge5,
            };
        }

        private void InitializeHeadTrackerPlotter()
        {
            Rack.HeadTrackerPlotter = new HeadTrackerPlotter(
                cnvHeadTrackerPlot,
                chkHTenabled,
                txtHTyawMultiplier,
                txtHTpitchMultiplier,
                txtHTrollMultiplier,
                btnHTcalibrate,
                txtHTrawPosition,
                txtHTrawAccel,
                cbxHTargBind,
                dotPitchYaw,
                dotPitchRoll);
        }

        private bool IsConnected()
        {
            bool connected = false;

            switch (PortType)
            {
                case SupportedPortTypes.USB:
                    connected = Rack.USBreceiver.IsConnected;
                    break;

                case SupportedPortTypes.UDP:
                    connected = Rack.UDPreceiver.IsConnected;
                    break;
            }

            return connected;
        }

        private void rbtUDP_Checked(object sender, RoutedEventArgs e)
        {
            DisconnectAll();
            PortType = SupportedPortTypes.UDP;
        }

        private void rbtUSB_Checked(object sender, RoutedEventArgs e)
        {
            DisconnectAll();
            PortType = SupportedPortTypes.USB;
        }

        private void TxtUDPPort_OnTextChanged(object sender, TextChangedEventArgs e)
        {

            if (isSetup)
            {
                try
                {
                    if (IsConnected())
                    {
                        Rack.UDPreceiver.Disconnect();
                    }
                    Rack.UDPreceiver.Port = int.Parse(txtUDPPort.Text);
                }
                catch
                {
                    // Ignore, just don't crash
                }
            }
        }

        private void UpdateIndicators()
        {
            txtUSBPort.Text = Rack.USBreceiver.Port.ToString();
            txtUDPPort.Text = Rack.UDPreceiver.Port.ToString();
            switch (PortType)
            {
                case SupportedPortTypes.USB:
                    indConnection.Fill = Rack.USBreceiver.IsConnected ? Rack.YES_BRUSH : Rack.NO_BRUSH;
                    break;

                case SupportedPortTypes.UDP:
                    indConnection.Fill = Rack.UDPreceiver.IsConnected ? Rack.YES_BRUSH : Rack.NO_BRUSH;
                    break;
            }

            btnPause.Content = Rack.Paused ? "▶ Play" : "|| Pause";
        }

        private bool isSetup = false;
        /// <summary>
        /// This method will be called when the window finished loading. A good moment to call a setup
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Setup = new DefaultSetup(this);
            Setup.Setup();
            isSetup = true;

            InitializeGauges();
            InitializeHeadTrackerPlotter();
            UpdateIndicators();
        }
    }
}