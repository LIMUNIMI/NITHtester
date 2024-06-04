using NITHlibrary.Nith.Internals;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NITHtester.Elements
{
    internal class HeadTrackerPlotter
    {
        private const int DEFAULT_RADIUS = 180;
        private Button btnCalibrate;
        private ComboBox cbxMode;
        private CheckBox chkEnabled;
        private Canvas cnvPlot;
        private Ellipse dotPR;
        private Ellipse dotPY;
        private HeadtrackerCenteringHelper HTcenteringHelper;
        private TextBlock txtHTrawAccel;
        private TextBlock txtHTrawPosition;
        private TextBox txtPitchMultiplier;
        private TextBox txtRollMultiplier;
        private TextBox txtYawMultiplier;
        private HTvisualizationModes visualizationMode = HTvisualizationModes.Position;

        public HeadTrackerPlotter(Canvas cnvPlot, CheckBox chkEnabled, TextBox txtYawMultiplier, TextBox txtPitchMultiplier, TextBox txtRollMultiplier, Button btnCalibrate, TextBlock txtHTrawPosition, TextBlock txtHTrawAccel, ComboBox cbxMode, Ellipse dotPY, Ellipse dotPR)
        {
            this.cnvPlot = cnvPlot;
            this.chkEnabled = chkEnabled;
            this.txtYawMultiplier = txtYawMultiplier;
            this.txtPitchMultiplier = txtPitchMultiplier;
            this.txtRollMultiplier = txtRollMultiplier;
            this.btnCalibrate = btnCalibrate;
            this.txtHTrawPosition = txtHTrawPosition;
            this.txtHTrawAccel = txtHTrawAccel;
            this.cbxMode = cbxMode;
            this.dotPR = dotPR;
            this.dotPY = dotPY;

            HTcenteringHelper = new HeadtrackerCenteringHelper();

            chkEnabled.Checked += ChkEnabled_Checked;
            chkEnabled.Unchecked += ChkEnabled_Unchecked;
            txtYawMultiplier.TextChanged += TxtYawMultiplier_TextChanged;
            txtPitchMultiplier.TextChanged += TxtPitchMultiplier_TextChanged;
            txtRollMultiplier.TextChanged += TxtRollMultiplier_TextChanged;
            btnCalibrate.Click += BtnCalibrate_Click;
            cbxMode.SelectionChanged += CbxMode_SelectionChanged;

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
        }

        public bool Enabled { get; private set; } = false;

        public float PitchMultiplier { get; private set; } = 1;

        public float RollMultiplier { get; private set; } = 1;

        public float YawMultiplier { get; private set; } = 1;

        public void ReceiveAllNithValues(List<NithArgumentValue> args)
        {
            HTcenteringHelper.ParseAutomaticallyFromNithValues(args);
        }

        public void UpdateGraphics()
        {
            if (Enabled)
            {
                double pitch = 0;
                double yaw = 0;
                double roll = 0;

                // Decide which are to plot based on visualization mode
                switch (visualizationMode)
                {
                    case HTvisualizationModes.Position:
                        pitch = HTcenteringHelper.CenteredPosition.Pitch;
                        yaw = HTcenteringHelper.CenteredPosition.Yaw;
                        roll = HTcenteringHelper.CenteredPosition.Roll;
                        break;

                    case HTvisualizationModes.Acceleration:
                        pitch = HTcenteringHelper.Acceleration.Pitch;
                        yaw = HTcenteringHelper.Acceleration.Yaw;
                        roll = HTcenteringHelper.Acceleration.Roll;
                        break;
                }

                double normalizedPitch = (cnvPlot.ActualHeight / 2) + (pitch / (DEFAULT_RADIUS * 2) * cnvPlot.ActualHeight * PitchMultiplier);
                double normalizedYaw = (cnvPlot.ActualWidth / 2) + (yaw / (DEFAULT_RADIUS * 2) * cnvPlot.ActualWidth * YawMultiplier);
                double normalizedRoll = (cnvPlot.ActualWidth / 2) + (roll / (DEFAULT_RADIUS * 2) * cnvPlot.ActualWidth * RollMultiplier);

                // Invert the Y axis because in WPF, the Y coordinate increases downwards
                normalizedPitch = cnvPlot.Height - normalizedPitch;

                // Set the position of the tracker dots
                // Pitch + Yaw dot
                Canvas.SetTop(dotPY, normalizedPitch - dotPY.ActualHeight / 2);
                Canvas.SetLeft(dotPY, normalizedYaw - dotPY.ActualWidth / 2);
                // Pitch + Roll dot
                Canvas.SetTop(dotPR, normalizedPitch - dotPR.ActualHeight / 2);
                Canvas.SetLeft(dotPR, normalizedRoll - dotPR.ActualWidth / 2);

                // Fill the raw data TextBlocks
                txtHTrawPosition.Text =
                    "Y: " + HTcenteringHelper.CenteredPosition.Yaw.ToString("F2") + "\n" +
                    "P: " + HTcenteringHelper.CenteredPosition.Pitch.ToString("F2") + "\n" +
                    "R: " + HTcenteringHelper.CenteredPosition.Roll.ToString("F2");
                txtHTrawAccel.Text =
                    "Y: " + HTcenteringHelper.Acceleration.Yaw.ToString("F2") + "\n" +
                    "P: " + HTcenteringHelper.Acceleration.Pitch.ToString("F2") + "\n" +
                    "R: " + HTcenteringHelper.Acceleration.Roll.ToString("F2");
            }
        }

        private void BtnCalibrate_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            HTcenteringHelper.SetCenterToCurrentPosition();
        }

        private void CbxMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            visualizationMode = (HTvisualizationModes)Enum.Parse(typeof(HTvisualizationModes), cbxMode.SelectedItem.ToString());
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
            Canvas.SetZIndex(horizontalLine, -1);
            Canvas.SetZIndex(verticalLine, -1);

            // Add the lines to the canvas
            cnvPlot.Children.Add(horizontalLine);
            cnvPlot.Children.Add(verticalLine);
        }

        private void TxtPitchMultiplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PitchMultiplier = int.Parse(txtPitchMultiplier.Text, CultureInfo.InvariantCulture);
            }
            catch
            {
                PitchMultiplier = DEFAULT_RADIUS;
            }
        }

        private void TxtRollMultiplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                RollMultiplier = int.Parse(txtRollMultiplier.Text, CultureInfo.InvariantCulture);
            }
            catch
            {
                RollMultiplier = DEFAULT_RADIUS;
            }
        }

        private void TxtYawMultiplier_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                YawMultiplier = int.Parse(txtYawMultiplier.Text, CultureInfo.InvariantCulture);
            }
            catch
            {
                YawMultiplier = DEFAULT_RADIUS;
            }
        }
    }
}