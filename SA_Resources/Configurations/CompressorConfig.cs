using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources.Forms;

namespace SA_Resources
{

    public enum CompressorType
    {
        Compressor,
        Limiter
    }

    public class CompressorConfig: ICloneable
    {
        public double _Threshold, _Ratio, _Attack, _Release;
        public bool _SoftKnee, _Bypassed;
        public CompressorType _Type;
        public int NUM_DSP_SETTINGS = 6;
        public UInt32 Threshold_Value, Ratio_Value, Attack_Value, Release_Value, SoftKnee_Value,Bypassed_Value,Type_Value;

        public int _CHANNEL;

        public CompressorConfig(int in_CH, CompressorType t = CompressorType.Compressor)
        {
            _Threshold = -20;
            _Ratio = 100;
            _Attack = 0.01; // 10ms
            _Release = 0.01; // 10ms
            _SoftKnee = false;
            _Bypassed = true;
            _Type = t;
            _CHANNEL = in_CH;
        }


        public CompressorConfig(double th, double rat, double a, double rel, bool sk, bool bypassed, int in_CH, CompressorType t = CompressorType.Compressor)
        {
            _Threshold = th;
            _Ratio = rat;
            _Attack = a;
            _Release = rel;
            _SoftKnee = sk;
            _Bypassed = bypassed;
            _Type = t;
            _CHANNEL = in_CH;
        }

        public double Threshold
        {
            get { return this._Threshold; }
            set
            {
                this._Threshold = value;
                this.Threshold_Value = DSP_Math.double_to_MN(value, 9, 23);
            }

        }

        public double Ratio
        {
            get { return this._Ratio; }
            set
            {
                this._Ratio = value;
                this.Ratio_Value = DSP_Math.comp_ratio_to_value(value);
            }

        }

        public double Attack
        {
            get { return this._Attack; }
            set
            {
                this._Attack = value;
                this.Attack_Value = DSP_Math.comp_attack_to_value(value);
            }

        }

        public double Release
        {
            get { return this._Release; }
            set
            {
                this._Release = value;
                this.Release_Value = DSP_Math.comp_release_to_value(value);
            }

        }

        public bool SoftKnee
        {
            get { return this._SoftKnee; }
            set
            {
                this._SoftKnee = (bool)value;
                this.SoftKnee_Value = (_SoftKnee) ? (UInt32)0x03000000 : (UInt32)0x00000000;
            }
        }

        public bool Bypassed
        {
            get { return this._Bypassed; }
            set
            {
                this._Bypassed = (bool)value;
                this.Bypassed_Value = (_Bypassed) ? (UInt32)0x00000001 : (UInt32)0x00000000;
            }

        }

        public void UpdateFromSettings(MainForm_Template PARENT_FORM, int PROGRAM_INDEX, int SETTINGS_OFFSET)
        {
            this.Threshold = DSP_Math.MN_to_double_signed(PARENT_FORM._settings[PROGRAM_INDEX][SETTINGS_OFFSET].Value, 9, 23);
            this.SoftKnee = (PARENT_FORM._settings[PROGRAM_INDEX][SETTINGS_OFFSET + 1].Value == 0x03000000);
            this.Ratio = DSP_Math.value_to_comp_ratio(PARENT_FORM._settings[PROGRAM_INDEX][SETTINGS_OFFSET + 2].Value);
            this.Attack = DSP_Math.value_to_comp_attack(PARENT_FORM._settings[PROGRAM_INDEX][SETTINGS_OFFSET + 3].Value);
            this.Release = DSP_Math.value_to_comp_release(PARENT_FORM._settings[PROGRAM_INDEX][SETTINGS_OFFSET + 4].Value);
            this.Bypassed = (PARENT_FORM._settings[PROGRAM_INDEX][SETTINGS_OFFSET + 5].Value == 0x00000001);

            ((PictureButton)PARENT_FORM.Controls.Find("btnCH" + _CHANNEL.ToString() + "Compressor", true).First()).Overlay1Visible = this.Bypassed;
            ((PictureButton)PARENT_FORM.Controls.Find("btnCH" + _CHANNEL.ToString() + "Compressor", true).First()).Invalidate();
                
        }

        public void QueueChange(MainForm_Template PARENT_FORM, int SETTINGS_INDEX)
        {
            int ADDR_THRESHOLD = SETTINGS_INDEX;
            int ADDR_KNEE = SETTINGS_INDEX + 1;
            int ADDR_RATIO = SETTINGS_INDEX + 2;
            int ADDR_ATTACK = SETTINGS_INDEX + 3;
            int ADDR_RELEASE = SETTINGS_INDEX + 4;
            int ADDR_BYPASS = SETTINGS_INDEX + 5; 
            
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_THRESHOLD, this.Threshold_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_KNEE, this.SoftKnee_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_RATIO, this.Ratio_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_ATTACK, this.Attack_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_RELEASE, this.Release_Value));
            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_BYPASS, this.Bypassed_Value));

        }

        public bool Equals(CompressorConfig compareConfig)
        {

            if(compareConfig == null)
            {
                return false;
            }

            if(_Threshold != compareConfig.Threshold || _Ratio != compareConfig.Ratio || _Attack != compareConfig.Attack)
            {
                return false;
            }

            if(_Release != compareConfig.Release || _SoftKnee != compareConfig.SoftKnee || _Bypassed != compareConfig.Bypassed)
            {
                return false;
            }

            if (_Type != compareConfig._Type)
            {
                return false;
            }

            return true;

        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
