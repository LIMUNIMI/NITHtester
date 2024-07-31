using NITHlibrary.Nith.Internals;
using NITHlibrary.Tools.Mappers;
using NITHlibrary.Tools.Types;

namespace NITHtester.Elements
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete("This class is deprecated. Use the NithPreprocessor_HeadTrackerCalibrator instead, by adding it to the NithModule's preprocessors.")]
    public class HeadtrackerCenteringHelper
    {
        private double _accP = 0;
        private double _accR = 0;
        private double _accY = 0;
        private double _posP = 0;
        private double _posR = 0;
        private double _posY = 0;
        private AngleBaseChanger pitchBaseChanger;
        private AngleBaseChanger rollBaseChanger;
        private AngleBaseChanger yawBaseChanger;

        public HeadtrackerCenteringHelper()
        {
            pitchBaseChanger = new AngleBaseChanger();
            yawBaseChanger = new AngleBaseChanger();
            rollBaseChanger = new AngleBaseChanger();
        }

        public Polar3DData Acceleration { get; set; }

        public Polar3DData CenteredPosition
        {
            get
            {
                return new Polar3DData { Yaw = yawBaseChanger.Transform(Position.Yaw), Pitch = pitchBaseChanger.Transform(Position.Pitch), Roll = rollBaseChanger.Transform(Position.Roll) };
            }
        }

        public Polar3DData Position { get; set; }
        public double Velocity { get; set; }

        public Polar3DData GetCenter()
        {
            return new Polar3DData
            {
                Yaw = yawBaseChanger.Delta,
                Pitch = pitchBaseChanger.Delta,
                Roll = rollBaseChanger.Delta
            };
        }

        public void ParseAutomaticallyFromNithValues(List<NithParameterValue> args)
        {
            _posY = _posP = _posR = 0;
            _accY = _accP = _accR = 0;

            foreach (NithParameterValue arg in args)
            {
                switch (arg.Parameter)
                {
                    case NithParameters.head_pos_yaw: _posY = arg.BaseAsDouble; break;
                    case NithParameters.head_pos_pitch: _posP = arg.BaseAsDouble; break;
                    case NithParameters.head_pos_roll: _posR = arg.BaseAsDouble; break;
                    case NithParameters.head_acc_yaw: _accY = arg.BaseAsDouble; break;
                    case NithParameters.head_acc_pitch: _accP = arg.BaseAsDouble; break;
                    case NithParameters.head_acc_roll: _accR = arg.BaseAsDouble; break;
                    default: break;
                }
            }

            Position = new Polar3DData { Yaw = _posY, Pitch = _posP, Roll = _posR };
            Acceleration = new Polar3DData { Yaw = _accY, Pitch = _accP, Roll = _accR };
        }

        public void SetCenterToCurrentPosition()
        {
            pitchBaseChanger.Delta = Position.Pitch;
            yawBaseChanger.Delta = Position.Yaw;
            rollBaseChanger.Delta = Position.Roll;
        }
    }
}