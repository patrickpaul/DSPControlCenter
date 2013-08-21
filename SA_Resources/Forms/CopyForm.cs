using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SA_Resources;

namespace SA_Resources.Forms
{
    // TODO - This form must know the number of channels
    public partial class CopyForm : Form
    {
        private CopyFormType FORM_TYPE;
        private int CH_NUMBER;

        private MainForm_Template PARENT_FORM;

        public CopyForm(MainForm_Template _parentForm, int channel_number, CopyFormType formType = CopyFormType.Filter3)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;

            CH_NUMBER = channel_number;

            FORM_TYPE = formType;

            ((PictureCheckbox)Controls.Find("pchkChannel" + channel_number, true).First()).Enabled = false;

            pchkItem1.Visible = true;
            pchkItem2.Visible = true;
            pchkItem3.Visible = true;
            pchkItem4.Visible = true;
            pchkItem5.Visible = true;
            pchkItem6.Visible = true;

            switch (formType)
            {
                case CopyFormType.Filter3:
                    pchkItem1.Text = " " + FilterToDescription(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][0]);
                    pchkItem1.Checked = (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][0].Type != FilterType.None);

                    pchkItem2.Text = " " + FilterToDescription(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][1]);
                    pchkItem2.Checked = (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][1].Type != FilterType.None);

                    pchkItem3.Text = " " + FilterToDescription(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][2]);
                    pchkItem3.Checked = (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][2].Type != FilterType.None);

                    pchkItem4.Visible = false;
                    pchkItem5.Visible = false;
                    pchkItem6.Visible = false;
                break;

                case CopyFormType.Filter6:
                    pchkItem1.Text = " " + FilterToDescription(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][3]);
                    
                    pchkItem1.Checked = (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][3].Type != FilterType.None);

                    pchkItem2.Text = " " + FilterToDescription(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][4]);
                    pchkItem2.Checked = (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][4].Type != FilterType.None);

                    pchkItem3.Text = " " + FilterToDescription(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][5]);
                    pchkItem3.Checked = (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][5].Type != FilterType.None);

                    pchkItem4.Text = " " + FilterToDescription(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][6]);
                    pchkItem4.Checked = (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][6].Type != FilterType.None);

                    pchkItem5.Text = " " + FilterToDescription(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][7]);
                    pchkItem5.Checked = (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][7].Type != FilterType.None);

                    pchkItem6.Text = " " + FilterToDescription(PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][8]);
                    pchkItem6.Checked = (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][8].Type != FilterType.None);
                break;

                case CopyFormType.InputConfiguration:
                    // 3 Items are name, input type, phantom power
                    // TODO - show extra warning for phantom power
                    pchkItem4.Visible = false;
                    pchkItem5.Visible = false;
                    pchkItem6.Visible = false;
                break;

                case CopyFormType.Compressor:
                    pchkItem1.Text = " Compressor Configuration from CH " + channel_number;
                    pchkItem1.Checked = true;

                    pchkItem2.Visible = false;
                    pchkItem3.Visible = false;
                    pchkItem4.Visible = false;
                    pchkItem5.Visible = false;
                    pchkItem6.Visible = false;
                break;

                case CopyFormType.Limiter :
                    pchkItem1.Text = " Limiter Configuration from CH " + channel_number;
                    pchkItem1.Checked = true;

                    pchkItem2.Visible = false;
                    pchkItem3.Visible = false;
                    pchkItem4.Visible = false;
                    pchkItem5.Visible = false;
                    pchkItem6.Visible = false;
                break;

                case CopyFormType.Delay :
                    // 1 Item is delay in ms

                    pchkItem1.Text = " Delay Value of " + (PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Delay * 1000).ToString("F1") + "ms";
                    pchkItem1.Checked = true;
                    pchkItem2.Visible = false;
                    pchkItem3.Visible = false;
                    pchkItem4.Visible = false;
                    pchkItem5.Visible = false;
                    pchkItem6.Visible = false;
                break;

                default :
                    // Gain
                    pchkItem2.Visible = false;
                    pchkItem3.Visible = false;
                    pchkItem4.Visible = false;
                    pchkItem5.Visible = false;
                    pchkItem6.Visible = false;
                break;
            }

            pchkItem1.Invalidate();
            pchkItem2.Invalidate();
            pchkItem3.Invalidate();
            pchkItem4.Invalidate();
            pchkItem5.Invalidate();
            pchkItem6.Invalidate();

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (FORM_TYPE == CopyFormType.Delay)
            {
                double copy_value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Delay;

                for (int i = 1; i <= 4; i++)
                {
                    if (((PictureCheckbox)Controls.Find("pchkChannel" + i.ToString(), true).First()).Checked == true)
                    {
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[i - 1].Delay = copy_value;
                    }
                }

                PARENT_FORM.UpdateTooltips();
                this.Close();
            }

            if (FORM_TYPE == CopyFormType.Compressor)
            {
                CompressorConfig copy_value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][0];

                for (int i = 1; i <= 4; i++)
                {
                    if (((PictureCheckbox)Controls.Find("pchkChannel" + i.ToString(), true).First()).Checked == true)
                    {
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[i - 1][0] = copy_value;
                    }
                }

                PARENT_FORM.UpdateTooltips();
                this.Close();
            }

            if (FORM_TYPE == CopyFormType.Limiter)
            {
                CompressorConfig copy_value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[CH_NUMBER - 1][1];

                for (int i = 1; i <= 4; i++)
                {
                    if (((PictureCheckbox)Controls.Find("pchkChannel" + i.ToString(), true).First()).Checked == true)
                    {
                        PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].compressors[i - 1][1] = copy_value;
                    }
                }

                PARENT_FORM.UpdateTooltips();
                this.Close();
            }

            if (FORM_TYPE == CopyFormType.Filter3)
            {
                int i, j;
                //double copy_value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Delay;
                for (i = 0; i < 3; i++)
                {
                    if (((PictureCheckbox)Controls.Find("pchkItem" + (i+1).ToString(), true).First()).Checked == true)
                    {
                        // Going to copy this filter

                        for (j = 0; j < 4; j++)
                        {
                            if (((PictureCheckbox)Controls.Find("pchkChannel" + (j+1).ToString(), true).First()).Checked == true)
                            {
                                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[j][i] = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i];
                            }
                        }
                    }
                }

                PARENT_FORM.UpdateTooltips();
                this.Close();
            }

            if (FORM_TYPE == CopyFormType.Filter6)
            {
                int i, j;
                //double copy_value = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].delays[CH_NUMBER - 1].Delay;
                for (i = 3; i < 9; i++)
                {
                    if (((PictureCheckbox)Controls.Find("pchkItem" + (i - 2).ToString(), true).First()).Checked == true)
                    {
                        // Going to copy this filter

                        for (j = 0; j < 4; j++)
                        {
                            if (((PictureCheckbox)Controls.Find("pchkChannel" + (j + 1).ToString(), true).First()).Checked == true)
                            {
                                PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[j][i] = PARENT_FORM.PROGRAMS[PARENT_FORM.CURRENT_PROGRAM].filters[CH_NUMBER - 1][i];
                            }
                        }
                    }
                }

                PARENT_FORM.UpdateTooltips();
                this.Close();
            }


        }

        private string FilterFrequencyString(double frequency)
        {
            if(frequency < 1000) {
                return frequency.ToString("F0") + "Hz";
            } else {
                return (frequency/1000).ToString("F2") + "kHz";
            }

        }
        private string FilterToDescription(FilterConfig in_filter)
        {
            switch (in_filter.Type)
            {
                case FilterType.FirstOrderLowPass:
                    return "Low Pass at " + FilterFrequencyString(in_filter.Filter.CenterFrequency);

                case FilterType.SecondOrderLowPass:
                    return "Low Pass at " + FilterFrequencyString(in_filter.Filter.CenterFrequency);

                case FilterType.FirstOrderHighPass:
                    return "High Pass at " + FilterFrequencyString(in_filter.Filter.CenterFrequency);

                case FilterType.SecondOrderHighPass:
                    return "High Pass at " + FilterFrequencyString(in_filter.Filter.CenterFrequency);

                case FilterType.LowShelf:
                    return "Low Shelf at " + FilterFrequencyString(in_filter.Filter.CenterFrequency);

                case FilterType.HighShelf:
                    return "High Shelf at " + FilterFrequencyString(in_filter.Filter.CenterFrequency);

                case FilterType.Peak:
                    return "Peak at " + FilterFrequencyString(in_filter.Filter.CenterFrequency);

                case FilterType.Notch:
                    return "Notch at " + FilterFrequencyString(in_filter.Filter.CenterFrequency);

                default:
                    return "Not used";
            }

        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
