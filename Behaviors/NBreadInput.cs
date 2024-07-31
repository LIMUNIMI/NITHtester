using NITHlibrary.Nith.Internals;
using NITHtester.Elements;
using NITHtester.Modules;

namespace NITHtester.Behaviors
{
    internal class NBreadInput : INithSensorBehavior
    {
        private const int MIN_ARG_LENGTH = 15;
        private string argumentStr = "";

        public void HandleData(NithSensorData nithData)
        {
            Rack.DataManagerModule.SensorName = nithData.SensorName;
            Rack.DataManagerModule.SensorVersion = nithData.Version;
            Rack.DataManagerModule.StatusCode = nithData.StatusCode.ToString();
            Rack.DataManagerModule.ExtraData = nithData.ExtraData;

            argumentStr = "";
            foreach (NithParameterValue val in nithData.Values)
            {
                argumentStr += AddWhiteSpaces(val.Parameter.ToString());
                argumentStr += "v: ";
                if (val.Type == NithDataTypes.OnlyBase)
                {
                    argumentStr += val.Base;
                }
                else if (val.Type == NithDataTypes.BaseAndMax)
                {
                    argumentStr += val.Base + " / " + val.Max + "\tn: " + val.Normalized.ToString("F2");
                }
                argumentStr += "\n";
            }

            // Update argumentStr and raw lines
            Rack.DataManagerModule.ArgumentsString = argumentStr;
            Rack.DataManagerModule.AppendRawLine("[" + DateTime.Now.ToString("HH:mm ss:fff") + "] " + nithData.RawLine);

            // Position the nithData args in Rack for access
            Rack.NithValues = nithData.Values;
            
            // Set the gauges to receive values
            foreach(BindableGauge gauge in Rack.BindableGauges)
            {
                gauge.ReceiveArgs(nithData.Values);
            }

            // Head tracker plotter receive values automatically
            Rack.HeadTrackerPlotter.ReceiveAllNithValues(nithData.Values);
        }

        private string AddWhiteSpaces(string input)
        {
            if (input.Length < MIN_ARG_LENGTH)
            {
                int numberOfSpaces = MIN_ARG_LENGTH - input.Length;
                string whiteSpaces = new string(' ', numberOfSpaces);
                return input + whiteSpaces;
            }
            else
            {
                return input;
            }
        }
    }
}