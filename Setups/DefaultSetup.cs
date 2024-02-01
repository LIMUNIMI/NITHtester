using NITHdmis.NithSensors;
using NITHdmis.NithSensors.Wrappers.NithFaceCam;
using NITHdmis.Ports;
using NITHdmis.Template;
using NITHtester.Behaviors;
using NITHtester.Modules;
using System;
using System.Collections.Generic;

namespace NITHtester.Setups
{
    public class DefaultSetup : ISetup
    {
        public DefaultSetup(MainWindow instrumentWindow)
        {
            InstrumentWindow = instrumentWindow;
            Disposables = new List<IDisposable>();
        }

        private List<IDisposable> Disposables { get; set; }
        private MainWindow InstrumentWindow { get; set; }

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
            Rack.USBportManager = new USBportManager();
            Rack.USBportManager.Listeners.Add(Rack.NithModule);

            Rack.UDPportManager = new UDPportManager(20100);
            Rack.UDPportManager.Listeners.Add(Rack.NithModule);

            // Add disposables to list
            Disposables.Add(Rack.RenderingModule);
            Disposables.Add(Rack.NithModule);
            Disposables.Add(Rack.USBportManager);
            Disposables.Add(Rack.UDPportManager);

            // Make behaviors
            Rack.NithModule.SensorBehaviors.Add(new NBreadInput());
            Rack.NithModule.ErrorBehaviors.Add(new ErrorHandler(Rack.NithModule));

            // Preprocessors
            Rack.NithModule.Preprocessors.Add(new NithPreproccessor_FaceCam());

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