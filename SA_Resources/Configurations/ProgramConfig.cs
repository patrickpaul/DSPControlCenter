namespace SA_Resources.Configurations
{
    public class ProgramConfig
    {
        public string Name;

        public InputConfig[] inputs = new InputConfig[4];
        public OutputConfig[] outputs = new OutputConfig[4];
        public FilterConfig[][] filters = new FilterConfig[4][];

        public int[] pregains = new int[4]; 

        public GainConfig[][] gains = new GainConfig[4][];
        public CompressorConfig[][] compressors = new CompressorConfig[4][];
        public DelayConfig[] delays = new DelayConfig[4];
        public GainConfig[][] crosspoints = new GainConfig[6][];

        public DuckerConfig ducker = new DuckerConfig();

        public ProgramConfig(string _name = "")
        {
            this.Name = _name; 
            

            for (int i = 0; i < 4; i++)
            {

                filters[i] = new FilterConfig[9];
                crosspoints[i] = new GainConfig[4];
                gains[i] = new GainConfig[4];
                inputs[i] = new InputConfig(i);
                outputs[i] = new OutputConfig(i);



                compressors[i] = new CompressorConfig[2];

                compressors[i][0] = new CompressorConfig(i+1,CompressorType.Compressor);
                compressors[i][1] = new CompressorConfig(i+1,CompressorType.Limiter);

                delays[i] = new DelayConfig();

                for (int j = 0; j < 4; j++)
                {
                    gains[i][j] = new GainConfig();
                    crosspoints[i][j] = new GainConfig();

                    if (i == j)
                    {
                        crosspoints[i][j].Gain = 0;
                    }
                    else
                    {
                        crosspoints[i][j].Muted = true;
                    }
                }
            }

            for (int j = 4; j < 6; j++)
            {
                crosspoints[j] = new GainConfig[4];

                for (int k = 0; k < 4; k++)
                {
                    crosspoints[j][k] = new GainConfig(0, true);
                }
            }

            inputs[0].Name = "Input #1";
            inputs[1].Name = "Input #2";
            inputs[2].Name = "Input #3";
            inputs[3].Name = "Input #4";

            outputs[0].Name = "Output #1";
            outputs[1].Name = "Output #2";
            outputs[2].Name = "Output #3";
            outputs[3].Name = "Output #4";
        }

    }
}
