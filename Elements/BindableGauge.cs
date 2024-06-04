using NITHlibrary.Nith.Internals;
using System.Globalization;
using System.Windows.Controls;

namespace NITHtester.Elements
{
    internal class BindableGauge
    {
        private ComboBox cbxArg;
        private CheckBox chkUseProp;
        private TextBox txtMax;
        private TextBox txtMin;
        private TextBox txtOffset;

        public BindableGauge(ProgressBar prbValue, ComboBox cbxArg, TextBox txtMin, TextBox txtMax, TextBox txtOffset, CheckBox chkUseProp)
        {
            this.cbxArg = cbxArg;
            this.txtMin = txtMin;
            this.txtMax = txtMax;
            this.txtOffset = txtOffset;
            this.chkUseProp = chkUseProp;
            PrbGauge = prbValue;

            // Fill the arguments checkbox
            foreach (string item in Enum.GetNames(typeof(NithParameters)))
            {
                cbxArg.Items.Add(item);
            }

            // Assign events
            txtMin.TextChanged += TxtMin_TextChanged;
            txtMax.TextChanged += TxtMax_TextChanged;
            txtOffset.TextChanged += TxtOffset_TextChanged;
            cbxArg.SelectionChanged += CbxArg_SelectionChanged;
            chkUseProp.Checked += ChkUseProp_Checked;
            chkUseProp.Unchecked += ChkUseProp_Unchecked;

            // Initialize txts
            txtMin.Text = Min.ToString("F0");
            txtMax.Text = Max.ToString("F0");
            txtOffset.Text = Offset.ToString("F0");
            cbxArg.SelectedIndex = 0;
        }

        public NithParameters Argument { get; private set; } = NithParameters.NaA;

        public double Max { get; private set; } = 100;

        public double Min { get; private set; } = 0;

        public NithArgumentValue NithArgVal { get; private set; } = new NithArgumentValue(NithParameters.NaA, "");
        public double Offset { get; private set; } = 0;

        public ProgressBar PrbGauge { get; private set; }

        public bool UseProp { get; private set; } = false;

        public void ReceiveArgs(List<NithArgumentValue> values)
        {
            foreach (NithArgumentValue v in values)
            {
                if (v.Argument == Argument)
                {
                    NithArgVal = v;
                }
            }
        }

        public void UpdateGraphics()
        {
            try
            {
                if (UseProp && NithArgVal.Type == NithDataTypes.BaseAndMax)
                {
                    PrbGauge.Value = NithArgVal.Proportional + Offset;
                }
                else
                {
                    PrbGauge.Value = double.Parse(NithArgVal.Base, CultureInfo.InvariantCulture) + Offset;
                }
            }
            catch
            {
                // Absolutely ignorable, it simply means the stored NithArgumentValue is not valid. In this case: show nothing. Set Value to 0
                PrbGauge.Value = 0;
            }
        }

        private void CbxArg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Argument = (NithParameters)Enum.Parse(typeof(NithParameters), cbxArg.SelectedItem.ToString());
        }

        private void ChkUseProp_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            UseProp = true;
            Max = 100;
            PrbGauge.Maximum = Max;
        }

        private void ChkUseProp_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            UseProp = false;
            TxtMax_TextChanged(null, null);
        }

        private void TxtMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UseProp)
            {
                try
                {
                    Max = double.Parse(txtMax.Text, CultureInfo.InvariantCulture);
                }
                catch
                {
                    Max = 0;
                }
                PrbGauge.Maximum = Max;
            }
        }

        private void TxtMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Min = double.Parse(txtMin.Text, CultureInfo.InvariantCulture);
            }
            catch
            {
                Min = 0;
            }
            PrbGauge.Minimum = Min;
        }

        private void TxtOffset_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Offset = double.Parse(txtOffset.Text, CultureInfo.InvariantCulture);
            }
            catch
            {
                Offset = 0;
            }
        }
    }
}