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
        public double Threshold, Ratio, Attack, Release;
        public bool SoftKnee, Bypassed;
        public CompressorType Type;

        public CompressorConfig(CompressorType t = CompressorType.Compressor)
        {
            Threshold = -20;
            Ratio = 100;
            Attack = 0.01; // 10ms
            Release = 0.01; // 10ms
            SoftKnee = false;
            Bypassed = true;
            Type = t;
        }
        
        
        public CompressorConfig(double th, double rat, double a, double rel, bool sk, bool bypassed, CompressorType t = CompressorType.Compressor)
        {
            Threshold = th;
            Ratio = rat;
            Attack = a;
            Release = rel;
            SoftKnee = sk;
            Bypassed = bypassed;
            Type = t;
        }

        public void QueueChange(MainForm_Template PARENT_FORM, int SETTINGS_INDEX)
        {
            int ADDR_THRESHOLD =    SETTINGS_INDEX;
            int ADDR_KNEE =         SETTINGS_INDEX + 1;
            int ADDR_RATIO =        SETTINGS_INDEX + 2;
            int ADDR_ATTACK =       SETTINGS_INDEX + 3;
            int ADDR_RELEASE =      SETTINGS_INDEX + 4;
            int ADDR_BYPASS =       SETTINGS_INDEX + 5;


            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_THRESHOLD, DSP_Math.double_to_MN(this.Threshold, 9, 23)));

            if (this.SoftKnee)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_KNEE, 0x03000000));
            }
            else
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_KNEE, 0x00000000));
            }

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_RATIO, DSP_Math.comp_ratio_to_value(this.Ratio)));

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_ATTACK, DSP_Math.comp_attack_to_value(this.Attack))); 

            PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_RELEASE, DSP_Math.comp_release_to_value(this.Release))); 

            if (this.Bypassed)
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_BYPASS, 0x00000001));
            }
            else
            {
                PARENT_FORM.AddItemToQueue(new LiveQueueItem(ADDR_BYPASS, 0x00000000));
            }

        }

        public bool Equals(CompressorConfig compareConfig)
        {

            if(compareConfig == null)
            {
                return false;
            }

            if(Threshold != compareConfig.Threshold || Ratio != compareConfig.Ratio || Attack != compareConfig.Attack)
            {
                return false;
            }

            if(Release != compareConfig.Release || SoftKnee != compareConfig.SoftKnee || Bypassed != compareConfig.Bypassed)
            {
                return false;
            }

            if(Type != compareConfig.Type)
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
