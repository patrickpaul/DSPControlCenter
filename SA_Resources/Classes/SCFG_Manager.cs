using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SA_Resources;
using System.Linq;
using System.Globalization;
using System.IO;
using SA_Resources.Forms;

namespace SA_Resources
{
    public static class SCFG_Manager
    {

        public static void Write(string outputFile, MainForm_Template FORM_INSTANCE)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(outputFile))
            {
                file.WriteLine("DEVICE-ID:" + FORM_INSTANCE.GetDeviceID().ToString("X8") + ";");
                file.WriteLine("SERIAL:" + FORM_INSTANCE.SERIALNUM + ";");
                file.WriteLine("TIMESTAMP:" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + ";");
                for (int k = 0; k < 3; k++)
                {
                    file.WriteLine("PRESET" + (k + 1) + ";");

                    for (int i = 0; i < FORM_INSTANCE.GetNumInputChannels(); i++)
                    {
                        file.WriteLine("INPUT_" + (i + 1) + ":" + FORM_INSTANCE.PROGRAMS[k].inputs[i].Name + ";");
                    }

                    for (int j = 0; j < FORM_INSTANCE.GetNumOutputChannels(); j++)
                    {
                        file.WriteLine("OUTPUT_" + (j + 1) + ":" + FORM_INSTANCE.PROGRAMS[k].outputs[j].Name + ";");
                    }

                    for (int j = 0; j < FORM_INSTANCE.GetNumPhantomPowerChannels(); j++)
                    {
                        file.WriteLine("PHANTOM_" + (j + 1) + ":" + (FORM_INSTANCE.PROGRAMS[k].inputs[j].PhantomPower ? "00000001" : "00000000") + ";");
                    }


                    foreach (DSP_Setting single_setting in FORM_INSTANCE._settings[k])
                    {
                        file.WriteLine(single_setting.Index.ToString("D3") + "=" + single_setting.Value.ToString("X8") + ";");
                    }
                }
            }

            
        }

        public static int GetDeviceID(string inputFile)
        {
            string tempLine;

            using (System.IO.StreamReader file = new System.IO.StreamReader(inputFile))
            {
                while (file.Peek() >= 0)
                {
                    tempLine = file.ReadLine();

                    if (tempLine.Contains("DEVICE-ID:"))
                    {
                        return Convert.ToInt32(tempLine.Substring(16, 2), 16);
                    }
                } 
            }

            return 0;
        }

        public static void Read(string inputFile, MainForm_Template FORM_INSTANCE)
        {
            string tempLine = "";
            string channel_name = "";
            int cur_program = 0;

            using (System.IO.StreamReader file = new System.IO.StreamReader(inputFile))
            {
                int lineCount = 0, index = 0;
                UInt32 value = 0x00000000;
                while (file.Peek() >= 0)
                {
                    lineCount++;
                    tempLine = file.ReadLine();

                    if (tempLine.Contains("DEVICE-ID:"))
                    {
                        // TODO - CHECK HERE FOR VALID DEVICE-ID
                        continue;
                    }

                    if (tempLine.Contains("DEVICE-ID:"))
                    {
                        // TODO - CHECK HERE FOR VALID DEVICE-ID
                        continue;
                    }

                    if (tempLine.Contains("SERIAL:"))
                    {
                        // TODO - CHECK HERE FOR VALID DEVICE-ID
                        continue;
                    }

                    if (tempLine.Contains("TIMESTAMP:"))
                    {
                        // TODO - CHECK HERE FOR VALID DEVICE-ID
                        continue;
                    }

                    if (tempLine.Contains("PRESET") && tempLine.Substring(7, 1) == ";")
                    {
                        cur_program = int.Parse(tempLine.Substring(6, 1)) - 1;
                        //Console.WriteLine("Changing current program to " + cur_program);
                        continue;
                    }

                    if (tempLine.Substring(0, 5) == "INPUT")
                    {
                        index = int.Parse(tempLine.Substring(6, 1));
                        channel_name = tempLine.Substring(8, tempLine.Length - 9);
                        FORM_INSTANCE.PROGRAMS[cur_program].inputs[index - 1].Name = channel_name;
                        continue;
                    }

                    if (tempLine.Substring(0, 7) == "PHANTOM")
                    {
                        index = int.Parse(tempLine.Substring(8, 1));
                        if (tempLine.Substring(17, 1) == "1")
                        {
                            FORM_INSTANCE.PROGRAMS[cur_program].inputs[index - 1].PhantomPower = true;
                        }
                        else
                        {
                            FORM_INSTANCE.PROGRAMS[cur_program].inputs[index - 1].PhantomPower = false;
                        }
                        continue;
                    }

                    if (tempLine.Substring(0, 6) == "OUTPUT")
                    {
                        index = int.Parse(tempLine.Substring(7, 1));
                        channel_name = tempLine.Substring(9, tempLine.Length - 10);
                        FORM_INSTANCE.PROGRAMS[cur_program].outputs[index - 1].Name = channel_name;
                        continue;
                    }

                    if ((tempLine.Length != 13) || (tempLine.IndexOf('=') != 3) || (tempLine.IndexOf(';') != 12))
                    {
                        throw new Exception("Invalid format encountered on line " + lineCount);
                    }
                    else
                    {
                        index = int.Parse(tempLine.Substring(0, 3));
                        bool parsedSuccessfully = UInt32.TryParse(tempLine.Substring(4, 8), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out value);
                        if (!parsedSuccessfully)
                        {
                            throw new Exception("Invalid value encountered on line " + lineCount);
                        }
                        else
                        {
                            FORM_INSTANCE._settings[cur_program][index].Value = value;
                        }
                    }
                }
            }


        }

    }
}
