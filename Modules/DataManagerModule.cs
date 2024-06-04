using System.Collections.Generic;
using System.Linq;

namespace NITHtester.Modules
{
    internal class DataManagerModule
    {
        private const int MAX_ERROR_LINES = 100;
        private const int MAX_RAW_LINES = 100;
        public string ArgumentsString { get; set; }
        public string ErrorsString { get; private set; } = "";
        public string RawString { get; private set; } = "";
        public string SensorName { get; set; } = "";
        public string SensorVersion { get; set; } = "";
        public string StatusCode { get; set; } = "";
        public string ExtraData { get; set; } = "";
        private Queue<string> ErrorQueue { get; set; } = new Queue<string>();
        private Queue<string> RawQueue { get; set; } = new Queue<string>();

        internal void AppendErrorLine(string line)
        {
            ErrorQueue.Enqueue(line);
            if (ErrorQueue.Count > MAX_ERROR_LINES) { ErrorQueue.Dequeue(); }
            ErrorsString = string.Join("\n", ErrorQueue.Reverse().ToArray());
        }

        internal void AppendRawLine(string line)
        {
            RawQueue.Enqueue(line);
            if (RawQueue.Count > MAX_RAW_LINES) { RawQueue.Dequeue(); }
            RawString = string.Join("\n", RawQueue.Reverse().ToArray());
        }

        internal void ClearErrorQueue()
        {
            ErrorQueue.Clear();
        }

        internal void ClearRawQueue()
        {
            RawQueue.Clear();
        }
    }
}