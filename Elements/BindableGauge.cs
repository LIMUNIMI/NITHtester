using NITHlibrary.Nith.Internals;
using NITHlibrary.Tools.Filters.ValueFilters;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace NITHtester.Elements
{
    internal class BindableGauge
    {
        private ComboBox cbxArg;
        private CheckBox chkUseProp;
        private TextBox txtFilterAlpha;
        private TextBox txtMax;
        private TextBox txtMin;
        private TextBox txtOffset;
        private DoubleFilterMAexpDecaying filter;

        public BindableGauge(ProgressBar prbValue, ComboBox cbxArg, TextBox txtMin, TextBox txtMax, TextBox txtOffset, CheckBox chkUseProp, TextBox txtFilterAlpha)
        {
            this.txtFilterAlpha = txtFilterAlpha;
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

            // Initialize param
            Parameter = NithParameters.NaP;

            // Initialize txts
            txtMin.Text = Min.ToString("F0");
            txtMax.Text = Max.ToString("F0");
            txtOffset.Text = Offset.ToString("F0");
            cbxArg.SelectedIndex = 0;
            txtFilterAlpha.Text = "100";

            // Assign events
            txtMin.TextChanged += TxtMin_TextChanged;
            txtMax.TextChanged += TxtMax_TextChanged;
            txtOffset.TextChanged += TxtOffset_TextChanged;
            cbxArg.SelectionChanged += CbxArg_SelectionChanged;
            chkUseProp.Checked += ChkUseProp_Checked;
            chkUseProp.Unchecked += ChkUseProp_Unchecked;
            txtFilterAlpha.TextChanged += TxtFilterAlpha_TextChanged;

            // Initialize the filter
            filter = new DoubleFilterMAexpDecaying(1);
        }

        public NithParameters Parameter { get; private set; }

        public double Max { get; private set; } = 100;

        public double Min { get; private set; } = 0;

        public NithParameterValue NithParamVal { get; private set; } = new NithParameterValue(NithParameters.NaP, "");

        public double Offset { get; private set; } = 0;

        public ProgressBar PrbGauge { get; private set; }

        public bool UseProp { get; private set; } = false;

        public void ReceiveArgs(List<NithParameterValue> values)
        {
            foreach (NithParameterValue v in values)
            {
                if (v.Parameter == Parameter)
                {
                    NithParamVal = v;
                }
            }
        }

        public void UpdateGraphics()
        {
            double val;
            bool safe = false;
            try
            {
                if (UseProp && NithParamVal.Type == NithDataTypes.BaseAndMax)
                {
                    val = NithParamVal.Normalized + Offset;
                    safe = true;
                }
                else
                {
                    val = NithParamVal.BaseAsDouble + Offset;
                    safe = true;
                }
            }
            catch
            {
                // Absolutely ignorable, it simply means the stored NithParameterValue is not valid. In this case: show nothing. Set Value to 0
                val = 0;
                safe = false;
            }

            if (safe && !double.IsNaN(val))
            {
                filter.Push(val);
                val = filter.Pull();
                PrbGauge.Value = val;
            }
            
        }

        private void CbxArg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parameter = (NithParameters)Enum.Parse(typeof(NithParameters), cbxArg.SelectedItem.ToString());
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

        private void TxtFilterAlpha_TextChanged(object sender, TextChangedEventArgs e)
        {
            var val = Math.Clamp(float.Parse(txtFilterAlpha.Text, CultureInfo.InvariantCulture) / 100, 0, 1f);
            filter = new DoubleFilterMAexpDecaying(val);
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