using NITHlibrary.Nith.Internals;
using NITHlibrary.Nith.Module;
using NITHtester.Elements;
using System.Windows.Media;
using NITHlibrary.Nith.PortDetector;
using NITHlibrary.Tools.Ports;

namespace NITHtester.Modules
{
    /// <summary>
    /// This will contain all the modules
    /// </summary>
    internal static class Rack
    {
        public static readonly SolidColorBrush NO_BRUSH = new SolidColorBrush(Colors.DarkRed);
        public static readonly SolidColorBrush YES_BRUSH = new SolidColorBrush(Colors.DarkGreen);
        private static int port = 1;

        public static BindableGauge BindableGauge1 { get; internal set; }
        public static BindableGauge BindableGauge2 { get; internal set; }
        public static BindableGauge BindableGauge3 { get; internal set; }
        public static BindableGauge BindableGauge4 { get; internal set; }
        public static BindableGauge BindableGauge5 { get; internal set; }
        public static List<BindableGauge> BindableGauges { get; internal set; }
        public static DataManagerModule DataManagerModule { get; set; }
        public static HeadTrackerPlotter HeadTrackerPlotter { get; set; }
        public static MappingModule MappingModule { get; set; }
        public static NithModule NithModule { get; set; }

        public static List<NithParameterValue> NithValues { get; set; }

        public static bool Paused { get; set; } = false;

        public static Statuses PauseStatus { get; set; } = Statuses.Connected_Playing;

        public static RenderingModule RenderingModule { get; set; }
        public static USBreceiver USBreceiver { get; set; }
        public static UDPreceiver UDPreceiver { get; set; }

        public static NithUSBportDetector PortDetector { get; set; }
        public static string TestString { get; set; } = "";

        // Test
        //public static MouseModule MouseModule { get; set; }
    }
}