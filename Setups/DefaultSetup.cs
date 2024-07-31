using NITHlibrary.Nith.Module;
using NITHlibrary.Nith.PortDetector;
using NITHlibrary.Nith.Wrappers.NithFaceCam;
using NITHlibrary.Tools.Ports;
using NITHtester.Behaviors;
using NITHtester.Modules;
using NITHtester.Test;

namespace NITHtester.Setups
{
    public class DefaultSetup(MainWindow instrumentWindow) : ISetup
    {
        private List<IDisposable> Disposables { get; set; } = new();
        private MainWindow InstrumentWindow { get; set; } = instrumentWindow;

        /// <summary>
        /// Launches the setup actions for the instrument
        /// </summary>
        public void Setup()
        {
            // Make modules
            Rack.MappingModule = new MappingModule();
            Rack.RenderingModule = new RenderingModule(InstrumentWindow);
            Rack.NithModule = new NithModule();
            Rack.DataManagerModule = new DataManagerModule();

            // Make ports
            Rack.USBreceiver = new USBreceiver();
            Rack.USBreceiver.Listeners.Add(Rack.NithModule);

            Rack.UDPreceiver = new UDPreceiver(20100);
            Rack.UDPreceiver.Listeners.Add(Rack.NithModule);

            Rack.PortDetector = new NithUSBportDetector();
            Rack.PortDetector.Behaviors.Add(new BUSBreceiver_ConnectToPort(Rack.USBreceiver));

            // Add disposables to list
            Disposables.Add(Rack.RenderingModule);
            Disposables.Add(Rack.NithModule);
            Disposables.Add(Rack.USBreceiver);
            Disposables.Add(Rack.UDPreceiver);

            // Make Behaviors
            Rack.NithModule.SensorBehaviors.Add(new NBreadInput());
            Rack.NithModule.ErrorBehaviors.Add(new ErrorHandler(Rack.NithModule));

            // Test
            Rack.NithModule.SensorBehaviors.Add(new BlinkBehaviorTestBug());
            //Rack.MouseModule = new MouseModule();
            //Rack.NithModule.SensorBehaviors.Add(new NithBehavior_GazeToMouse(Rack.MouseModule, true));

            // Preprocessors
            Rack.NithModule.Preprocessors.Add(new NithPreprocessor_FaceCam());

            // You will probably want to leave this at the end!
            Rack.RenderingModule.StartRendering();
        }

        /// <summary>
        /// This method will dispose all the disposable modules. Call on program exit.
        /// </summary>
        public void Dispose()
        {
            foreach (IDisposable disposable in Disposables)
            {
                disposable.Dispose();
            }
        }
    }
}