using NITHdmis.NithSensors;
using NITHtester.Elements;
using System.Collections.Generic;
using System.Windows.Media;

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

        public static List<NithArgumentValue> NithValues { get; set; }

        public static bool Paused { get; set; } = false;

        public static Statuses PauseStatus { get; set; } = Statuses.Connected_Playing;

        public static int Port
        {
            get { return port; }
            set
            {
                if (value < 1) port = 1;
                else port = value;
            }
        }

        public static RenderingModule RenderingModule { get; set; }
    }
}