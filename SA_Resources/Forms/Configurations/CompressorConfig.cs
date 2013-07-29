using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SA_Resources
{

    public enum CompressorType
    {
        Compressor,
        Limiter
    }

    public class CompressorConfig
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
    }
}
