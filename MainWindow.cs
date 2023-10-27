using NITHdmis.Template;
using NITHtester.Elements;
using NITHtester.Modules;
using NITHtester.Setups;
using System;
using System.Collections.Generic;
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

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            Rack.NithModule.Connect(Rack.Port);
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

        private void btnSensorPortMinus_Click(object sender, RoutedEventArgs e)
        {
            Rack.Port--;
            Rack.NithModule.Disconnect();
            UpdateIndicators();
        }

        private void btnSensorPortPlus_Click(object sender, RoutedEventArgs e)
        {
            Rack.Port++;
            Rack.NithModule.Disconnect();
            UpdateIndicators();
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

        private void UpdateIndicators()
        {
            txtSensorPort.Text = Rack.Port.ToString();
            indConnection.Fill = Rack.NithModule.IsConnectionOk ? Rack.YES_BRUSH : Rack.NO_BRUSH;
            btnPause.Content = Rack.Paused ? "▶ Play" : "|| Pause";
        }

        /// <summary>
        /// This method will be called when the window finished loading. A good moment to call a setup
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Setup = new DefaultSetup(this);
            Setup.Setup();

            InitializeGauges();
            InitializeHeadTrackerPlotter();
            UpdateIndicators();
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
    }
}