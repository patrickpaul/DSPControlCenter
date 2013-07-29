using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources;
using SA_Resources.Filters;

namespace FLX80_4
{
    public partial class ConfigurationPrintout : Form
    {
        private const int LOW_PASS = 0;
        private const int HIGH_PASS = 1;
        private const int LOW_SHELF = 2;
        private const int HIGH_SHELF = 3;
        private const int PEAK = 4;
        private const int NOTCH = 5;
        private const int SECOND_LOW_PASS = 6;
        private const int SECOND_HIGH_PASS = 7;

        private MainForm ParentForm;
        public ConfigurationPrintout(MainForm _parent)
        {
            InitializeComponent();

            ParentForm = _parent;
        }

        private void ConfigurationPrintout_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < ParentForm.num_channels; i++)
            {
                //txtConfig.Text += "Channel " + (i + 1).ToString("N0") + Environment.NewLine;


                txtConfig.Text += "Channel " + (i + 1).ToString("N0") + System.Environment.NewLine;

                PrintInput(ParentForm.inputs[i]);

                
                txtConfig.Text += "Input Gain" + Environment.NewLine;
                PrintGain(ParentForm.gains[i][0]);

                txtConfig.Text += "Filters" + Environment.NewLine;

                for (int j = 0; j < 3; j++)
                {
                    if (ParentForm.filters[i][j] == null || ParentForm.filters[i][j].Filter == null)
                    {
                        txtConfig.Text += "Filter " + (j + 1) + " - Not Used" + Environment.NewLine;
                    }
                    else
                    {
                        PrintFilter(ParentForm.filters[i][j].Filter);
                    }
                }
                txtConfig.Text += Environment.NewLine; 

                txtConfig.Text += "Compressor" + Environment.NewLine; 
                PrintCompressor(ParentForm.compressors[i][0]);

                txtConfig.Text += "Pre-mix Gain" + Environment.NewLine;
                PrintGain(ParentForm.gains[i][1]);

                txtConfig.Text += "Post-mix Gain" + Environment.NewLine;
                PrintGain(ParentForm.gains[i][2]);

                txtConfig.Text += "Limiter" + Environment.NewLine; 
                PrintCompressor(ParentForm.compressors[i][1]);
                txtConfig.Text += "Output Gain" + Environment.NewLine;
                PrintGain(ParentForm.gains[i][3]);

                txtConfig.Text += System.Environment.NewLine;
                txtConfig.Text += System.Environment.NewLine;
            }

            //txtConfig.Select(0, 0);
        }
        
        private void PrintInput(InputConfig _input)
        {
            txtConfig.Text += "Label: " + _input.Name + System.Environment.NewLine;
            txtConfig.Text += "Input Type: ";

            if (_input.Type == InputType.Line)
            {
                txtConfig.Text += "Line Level" + System.Environment.NewLine;
            }
            else
            {
                txtConfig.Text += "Microphone" + System.Environment.NewLine;
            }

            txtConfig.Text += "Phantom Power: " + _input.PhantomPower.ToString() + System.Environment.NewLine;

            txtConfig.Text += System.Environment.NewLine;

            

        }

        private void PrintGain(GainConfig _gain)
        {
            txtConfig.Text += "Level: " + _gain.Gain.ToString("N1") + "dB" + System.Environment.NewLine;
            txtConfig.Text += "Muted: " + _gain.Muted.ToString() + System.Environment.NewLine;
            txtConfig.Text += System.Environment.NewLine;
        }

        private void PrintCompressor(CompressorConfig _comp)
        {
            txtConfig.Text += "Threshold: " + _comp.Threshold.ToString("N1") + "dB" + System.Environment.NewLine;
            
            if(_comp.Type == CompressorType.Compressor) {
                txtConfig.Text += "Ratio: " + _comp.Ratio.ToString("N0") + ":1" + System.Environment.NewLine;
            }
            txtConfig.Text += "Soft Knee: " + _comp.SoftKnee.ToString() + System.Environment.NewLine;

            txtConfig.Text += "Attack: " + _comp.Attack.ToString("N3") + System.Environment.NewLine;
            txtConfig.Text += "Release: " + _comp.Release.ToString("N3") + System.Environment.NewLine;

            txtConfig.Text += "Bypassed: " + _comp.Bypassed.ToString() + System.Environment.NewLine;
            
            txtConfig.Text += System.Environment.NewLine;

            // thresh, ratio, soft knee, attack, release, bypassed
        }
        
        
        private void PrintFilter(BiquadFilter _filter)
        {
            if (_filter.FilterType == LOW_PASS || _filter.FilterType == SECOND_LOW_PASS)
            {
                txtConfig.Text += "Low Pass Filter: ";
                txtConfig.Text += _filter.CenterFrequency.ToString("#.") + "Hz, ";
                if (_filter.FilterType == LOW_PASS)
                {
                    txtConfig.Text += "6dB/Octave";
                }
                else
                {
                    txtConfig.Text += "12dB/Octave";
                }
                
                txtConfig.Text += System.Environment.NewLine;

            }
            else if (_filter.FilterType == HIGH_PASS || _filter.FilterType == SECOND_HIGH_PASS)
            {
                txtConfig.Text += "High Pass Filter: ";
                txtConfig.Text += _filter.CenterFrequency.ToString("#.") + "Hz, ";
                if (_filter.FilterType == HIGH_PASS)
                {
                    txtConfig.Text += "6dB/Octave";
                }
                else
                {
                    txtConfig.Text += "12dB/Octave";
                }

                txtConfig.Text += System.Environment.NewLine;
            }else if(_filter.FilterType == LOW_SHELF){
                txtConfig.Text += "Low Shelf Filter: ";
                txtConfig.Text += _filter.CenterFrequency.ToString("#.") + "Hz, ";
                txtConfig.Text += _filter.Gain.ToString("#.#") + " dB, ";
                txtConfig.Text += "Q " + _filter.QValue.ToString("#.###");
                txtConfig.Text += System.Environment.NewLine;

            }else if(_filter.FilterType == HIGH_SHELF){
                txtConfig.Text += "High Shelf Filter: ";
                txtConfig.Text += _filter.CenterFrequency.ToString("#.") + "Hz, ";
                txtConfig.Text += _filter.Gain.ToString("#.#") + " dB, ";
                txtConfig.Text += "Q " + _filter.QValue.ToString("#.###");
                txtConfig.Text += System.Environment.NewLine;

            }else if(_filter.FilterType == PEAK){
                txtConfig.Text += "Peak Filter: ";
                txtConfig.Text += _filter.CenterFrequency.ToString("#.") + "Hz, ";
                txtConfig.Text += _filter.Gain.ToString("#.#") + " dB, ";
                txtConfig.Text += "Q " + _filter.QValue.ToString("#.###");
                txtConfig.Text += System.Environment.NewLine;

            }else if(_filter.FilterType == NOTCH){
                txtConfig.Text += "Notch Filter: ";
                txtConfig.Text += _filter.CenterFrequency.ToString("#.") + "Hz, ";
                txtConfig.Text += "Q " + _filter.QValue.ToString("#.###");
                txtConfig.Text += System.Environment.NewLine;
            }
        }


        public void AddFormattedLine(string text, bool MakeBold = false)
        {
            int SelectionStart = txtConfig.TextLength;
            txtConfig.AppendText(text + System.Environment.NewLine); 
            int SelectionLength = text.Length;

            txtConfig.Select(SelectionStart,SelectionLength);

            if (MakeBold)
            {
                txtConfig.SelectionFont = new Font(txtConfig.Font, FontStyle.Bold);
            }
            else
            {
                txtConfig.SelectionFont = new Font(txtConfig.Font, FontStyle.Regular);
            }

            //if(MakeBold)
            //{
            //    txtConfig.SelectionFont = new Font(txtConfig.Font, FontStyle.Bold);
            //}
        }

    }
}
