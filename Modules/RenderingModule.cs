using NITHtester.Elements;
using System.Windows.Threading;

namespace NITHtester.Modules
{
    public class RenderingModule : IDisposable
    {
        public RenderingModule(MainWindow mainWindow)
        {
            MainWindow = mainWindow;
            DispatcherTimer = new DispatcherTimer();
            DispatcherTimer.Interval = new TimeSpan(10000); // 10000 nanoseconds = one millisecond
            DispatcherTimer.Tick += DispatcherUpdate;
        }

        private DispatcherTimer DispatcherTimer { get; set; }
        private MainWindow MainWindow { get; set; }

        public void Dispose()
        {
            DispatcherTimer.Stop();
        }

        /// <summary>
        /// Starts the rendering timer
        /// </summary>
        public void StartRendering()
        {
            DispatcherTimer.Start();
        }

        /// <summary>
        /// Stops the rendering timer
        /// </summary>
        public void StopRendering()
        {
            DispatcherTimer.Stop();
        }

        /// <summary>
        /// This method will be called every time the dispatcher timer is triggered, to update graphics.
        /// </summary>
        private void DispatcherUpdate(object sender, EventArgs e)
        {
            // MainWindow.indConnection.Fill = Rack.USBreceiver.IsConnected ? Rack.YES_BRUSH : Rack.NO_BRUSH;
            MainWindow.txtParametersAndValues.Text = Rack.DataManagerModule.ArgumentsString;
            MainWindow.txtSensorName.Text = Rack.DataManagerModule.SensorName;
            MainWindow.txtSensorVersion.Text = Rack.DataManagerModule.SensorVersion;
            MainWindow.txtStatusCode.Text = Rack.DataManagerModule.StatusCode;
            MainWindow.txtExtraData.Text = Rack.DataManagerModule.ExtraData;

            MainWindow.txtRawStrings.Text = Rack.DataManagerModule.RawString;
            MainWindow.txtErrors.Text = Rack.DataManagerModule.ErrorsString;

            MainWindow.txtTestPanel.Text = Rack.TestString;

            // Update graphics of external modules
            UpdateGauges();
            Rack.HeadTrackerPlotter.UpdateGraphics();
        }

        private void UpdateGauges()
        {
            foreach(BindableGauge gauge in Rack.BindableGauges)
            {
                gauge.UpdateGraphics();
            }
        }
    }
}