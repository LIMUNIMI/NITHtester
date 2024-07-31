using NITHlibrary.Nith.Behaviors;
using NITHlibrary.Nith.Internals;
using NITHlibrary.Nith.Module;
using NITHtester.Modules;

namespace NITHtester.Behaviors
{
    internal class ErrorHandler : ANithErrorToStringBehavior
    {
        private string lastErrorString = "";

        public ErrorHandler(NithModule nithModule) : base(nithModule)
        {
        }

        protected override void ErrorActions(string errorString, NithErrors error)
        {

            if(errorString != lastErrorString)
            {
                Rack.DataManagerModule.AppendErrorLine("[" + DateTime.Now.ToString("HH:mm ss:fff") + "] " + errorString);
            }

            lastErrorString = errorString;
        }
    }
}
