using NITHlibrary.Nith.Internals;
using NITHlibrary.Tools.Filters.ValueFilters;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NITHtester.Elements
{
    internal class HeadTrackerPlotter
    {
        private const int DefaultRadius = 180;
        private Button _btnCalibrate;
        private readonly ComboBox _cbxMode;
        private CheckBox _chkEnabled;
        private readonly Canvas _cnvPlot;
        private readonly Ellipse _dotPr;
        private readonly Ellipse _dotPy;
        private readonly HeadtrackerCenteringHelper _hTcenteringHelper;
        private readonly TextBlock _txtHTrawAccel;
        private readonly TextBlock _txtHTrawPosition;
        private readonly TextBox _txtPitchMultiplier;
        private readonly TextBox _txtRollMultiplier;
        private readonly TextBox _txtYawMultiplier;
        private HTvisualizationModes _visualizationMode = HTvisualizationModes.Position;
        private TextBox _txtFilterAlpha;
        private DoubleFilterMAexpDecaying yawFilter = new(1);
        private DoubleFilterMAexpDecaying pitchFilter = new(1);
        private DoubleFilterMAexpDecaying rollFilter = new(1);

        public HeadTrackerPlotter(Canvas cnvPlot, CheckBox chkEnabled, TextBox txtYawMultiplier, TextBox txtPitchMultiplier, TextBox txtRollMultiplier, Button btnCalibrate, TextBlock txtHTrawPosition, TextBlock txtHTrawAccel, ComboBox cbxMode, Ellipse dotPy, Ellipse dotPr, TextBox txtFilterAlpha)
        {
            _cnvPlot = cnvPlot;
            _chkEnabled = chkEnabled;
            _txtYawMultiplier = txtYawMultiplier;
            _txtPitchMultiplier = txtPitchMultiplier;
            _txtRollMultiplier = txtRollMultiplier;
            _btnCalibrate = btnCalibrate;
            _txtHTrawPosition = txtHTrawPosition;
            _txtHTrawAccel = txtHTrawAccel;
            _cbxMode = cbxMode;
            _dotPr = dotPr;
            _dotPy = dotPy;
            _txtFilterAlpha = txtFilterAlpha;

            _hTcenteringHelper = new HeadtrackerCenteringHelper();

            chkEnabled.Checked += ChkEnabled_Checked;
            chkEnabled.Unchecked += ChkEnabled_Unchecked;
            txtYawMultiplier.TextChanged += TxtYawMultiplier_TextChanged;
            txtPitchMultiplier.TextChanged += TxtPitchMultiplier_TextChanged;
            txtRollMultiplier.TextChanged += TxtRollMultiplier_TextChanged;
            btnCalibrate.Click += BtnCalibrate_Click;
            cbxMode.SelectionChanged += CbxMode_SelectionChanged;
            txtFilterAlpha.TextChanged += TxtFilterAlpha_TextChanged;

            // Populate Mode ComboBox
            foreach (string item in Enum.GetNames(typeof(HTvisualizationModes)))
            {
                cbxMode.Items.Add(item);
            }
            

            // Create lines
            DrawCenterLines(cnvPlot);

            // Initialize values
            cbxMode.SelectedIndex = 0; // Select first element as default
            txtYawMultiplier.Text = YawMultiplier.ToString("F0");
            txtPitchMultiplier.Text = PitchMultiplier.ToString("F0");
            txtRollMultiplier.Text = RollMultiplier.ToString("F0");
            txtFilterAlpha.Text = "100";
        }

        private void TxtFilterAlpha_TextChanged(object sender, TextChangedEventArgs e)
        {
            var alpha = Math.Clamp(float.Parse(_txtFilterAlpha.Text, CultureInfo.InvariantCulture) / 100, 0, 1f);
            yawFilter = new DoubleFilterMAexpDecaying(alpha);
            pitchFilter = new DoubleFilterMAexpDecaying(alpha);
            rollFilter = new DoubleFilterMAexpDecaying(alpha);
        }

        public bool Enabled { get; private set; } = false;

        public float PitchMultiplier { get; private set; } = 1;

        public float RollMultiplier { get; private set; } = 1;

        public float YawMultiplier { get; private set; } = 1;

        public void ReceiveAllNithValues(List<NithParameterValue> args)
        {
            _hTcenteringHelper.ParseAutomaticallyFromNithValues(args);
        }

        public void UpdateGraphics()
        {
            if (Enabled)
            {
                double pitch = 0;
                double yaw = 0;
                double roll = 0;

                // Decide which are to plot based on visualization mode
                switch (_visualizationMode)
                {
                    case HTvisualizationModes.Position:
                        pitch = _hTcenteringHelper.CenteredPosition.Pitch;
                        yaw = _hTcenteringHelper.CenteredPosition.Yaw;
                        roll = _hTcenteringHelper.CenteredPosition.Roll;
                        break;

                    case HTvisualizationModes.Acceleration:
                        pitch = _hTcenteringHelper.Acceleration.Pitch;
                        yaw = _hTcenteringHelper.Acceleration.Yaw;
                        roll = _hTcenteringHelper.Acceleration.Roll;
                        break;
                }

                // Filtering
                yawFilter.Push(yaw);
                yaw = yawFilter.Pull();
                pitchFilter.Push(pitch);
                pitch = pitchFilter.Pull();
                rollFilter.Push(roll);
                roll = rollFilter.Pull();

                // Normalizing
                double normalizedPitch = (_cnvPlot.ActualHeight / 2) + (pitch / (DefaultRadius * 2) * _cnvPlot.ActualHeight * PitchMultiplier);
                double normalizedYaw = (_cnvPlot.ActualWidth / 2) + (yaw / (DefaultRadius * 2) * _cnvPlot.ActualWidth * YawMultiplier);
                double normalizedRoll = (_cnvPlot.ActualWidth / 2) + (roll / (DefaultRadius * 2) * _cnvPlot.ActualWidth * RollMultiplier);

                // Invert the Y axis because in WPF, the Y coordinate increases downwards
                normalizedPitch = _cnvPlot.Height - normalizedPitch;

                // Set the position of the tracker dots
                // Pitch + Yaw dot
                Canvas.SetTop(_dotPy, normalizedPitch - _dotPy.ActualHeight / 2);
                Canvas.SetLeft(_dotPy, normalizedYaw - _dotPy.ActualWidth / 2);
                // Pitch + Roll dot
                Canvas.SetTop(_dotPr, normalizedPitch - _dotPr.ActualHeight / 2);
                Canvas.SetLeft(_dotPr, normalizedRoll - _dotPr.ActualWidth / 2);

                // Fill the raw data TextBlocks
                _txtHTrawPosition.Text =
                    "Y: " + _hTcenteringHelper.CenteredPosition.Yaw.ToString("F2") + "\n" +
                    "P: " + _hTcenteringHelper.CenteredPosition.Pitch.ToString("F2") + "\n" +
                    "R: " + _hTcenteringHelper.CenteredPosition.Roll.ToString("F2");
                _txtHTrawAccel.Text =
                    "Y: " + _hTcenteringHelper.Acceleration.Yaw.ToString("F2") + "\n" +
                    "P: " + _hTcenteringHelper.Acceleration.Pitch.ToString("F2") + "\n" +
                    "R: " + _hTcenteringHelper.Acceleration.Roll.ToString("F2");
            }
        }

        private void BtnCalibrate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _hTcenteringHelper.SetCenterToCurrentPosition();
        }

        private void CbxMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _visualizationMode = (HTvisualizationModes)Enum.Parse(typeof(HTvisualizationModes), _cbxMode.SelectedItem.ToString());
        }

        private void ChkEnabled_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            Enabled = true;
        }

        private void ChkEnabled_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            Enabled = false;
        }

        private void DrawCenterLines(Canvas cnvPlot)
        {
            // Create the horizontal line
            Line horizontalLine = new Line
            {
                Stroke = new SolidColorBrush(Color.FromRgb(0x22, 0x22, 0x22)),
                StrokeThickness = 2,
                X1 = 0,
                Y1 = cnvPlot.Height / 2,
                X2 = cnvPlot.Width,
                Y2 = cnvPlot.Height / 2
            };

            // Create the vertical line
            Line verticalLine = new Line
            {
                Stroke = new SolidColorBrush(Color.FromRgb(0x22, 0x22, 0x22)),
                StrokeThickness = 2,
                X1 = cnvPlot.Width / 2,
                Y1 = 0,
                X2 = cnvPlot.Width / 2,
                Y2 = cnvPlot.Height
            };

            // Put them in the backgrounds
            Panel.SetZIndex(horizontalLine, -1);
            Panel.SetZIndex(verticalLine, -1);

            // Add the lines to the canvas
            cnvPlot.Children.Add(horizontalLine);
            cnvPlot.Children.Add(verticalLine);
        }

        private void TxtPitchMultiplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PitchMultiplier = int.Parse(_txtPitchMultiplier.Text, CultureInfo.InvariantCulture);
            }
            catch
            {
                PitchMultiplier = DefaultRadius;
            }
        }

        private void TxtRollMultiplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                RollMultiplier = int.Parse(_txtRollMultiplier.Text, CultureInfo.InvariantCulture);
            }
            catch
            {
                RollMultiplier = DefaultRadius;
            }
        }

        private void TxtYawMultiplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                YawMultiplier = int.Parse(_txtYawMultiplier.Text, CultureInfo.InvariantCulture);
            }
            catch
            {
                YawMultiplier = DefaultRadius;
            }
        }
    }
}