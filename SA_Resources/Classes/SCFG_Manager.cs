using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SA_Resources;
using System.Globalization;
using System.IO;
using SA_Resources.SADevices;
using SA_Resources.SAForms;

namespace SA_Resources
{
    public static class SCFG_Manager
    {

        public static void Write(string outputFile, MainForm_Template PARENT_FORM)
        {
            try
            {
                if (File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }

                Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

                using (StreamWriter writer = new StreamWriter(outputFile, true))
                {
                    writer.WriteLine("DSPCCVERSION:" + currentVersion.Major + "." + currentVersion.Minor + "." + currentVersion.Build);
                    writer.WriteLine("DEVICE-ID:" + PARENT_FORM.GetDeviceID().ToString("X8") + ";");
                    writer.WriteLine("SERIAL:" + PARENT_FORM.SERIALNUM + ";");
                    writer.WriteLine("TIMESTAMP:" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + ";");

                    if (PARENT_FORM.GetDeviceFamily() == DeviceFamily.FLX)
                    {
                        int mode = PARENT_FORM.AmplifierMode;
                        bool sleep_enable = PARENT_FORM.SLEEP_ENABLE;
                        int sleep_seconds = PARENT_FORM.SLEEP_SECONDS;
                        int adc_min = PARENT_FORM.ADC_CALIBRATION_MIN;
                        int adc_max = PARENT_FORM.ADC_CALIBRATION_MAX;

                        writer.WriteLine("MODE:" + mode.ToString("00000000") + ";");
                        writer.WriteLine("SLEEP_ENABLE:" + sleep_enable + ";");
                        writer.WriteLine("SLEEP_SECONDS:" + sleep_seconds.ToString("00000000") + ";");
                        writer.WriteLine("ADC_CALIBRATION_MIN:" + adc_min.ToString("00000000") + ";");
                        writer.WriteLine("ADC_CALIBRATION_MAX:" + adc_max.ToString("00000000") + ";");
                    }

                    if (PARENT_FORM.GetDeviceFamily() == DeviceFamily.DSP100)
                    {
                        bool sleep_enable = PARENT_FORM.SLEEP_ENABLE;
                        int sleep_seconds = PARENT_FORM.SLEEP_SECONDS;
                        int adc_min = PARENT_FORM.ADC_CALIBRATION_MIN;
                        int adc_max = PARENT_FORM.ADC_CALIBRATION_MAX;

                        writer.WriteLine("SLEEP_ENABLE:" + sleep_enable + ";");
                        writer.WriteLine("SLEEP_SECONDS:" + sleep_seconds.ToString("00000000") + ";");
                        writer.WriteLine("ADC_CALIBRATION_MIN:" + adc_min.ToString("00000000") + ";");
                        writer.WriteLine("ADC_CALIBRATION_MAX:" + adc_max.ToString("00000000") + ";");
                    }

                    for (int i = 0; i < PARENT_FORM.GetNumPresets(); i++)
                    {
                        PARENT_FORM.DSP_PROGRAMS[i].Write_Program_To_Cache();
                        PARENT_FORM.DSP_PROGRAMS[i].Write_Cache_To_File(writer);
                    }
                }

            }
            catch (IOException io_ex)
            {
                Console.WriteLine("IOException in SCFG_Manager.Write: " + io_ex.Message);
            }
            catch (UnauthorizedAccessException access_ex)
            {
                Console.WriteLine("UnauthorizedAccessException in SCFG_Manager.Write: " + access_ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SCFG_Manager.Write: " + ex.Message);
            }

        }

        public static void Read(string inputFile, MainForm_Template PARENT_FORM)
        {
            try
            {
                using (StreamReader reader = new StreamReader(inputFile))
                {
                    string VersionLine = reader.ReadLine();
                    string DeviceLine = reader.ReadLine();

                    
                    int device_id = Convert.ToInt32(DeviceLine.Substring(16, 2), 16);

                    if (device_id != PARENT_FORM.GetDeviceID())
                    {
                        throw new Exception("Loaded SCFG file does not match current device.");
                    }

                    string SerialLine = reader.ReadLine();
                    string TimestampLine = reader.ReadLine();

                    if (PARENT_FORM.GetDeviceFamily() == DeviceFamily.FLX)
                    {
                        string ModeLine = reader.ReadLine();

                        

                        string SleepEnableLine = reader.ReadLine();
                        string SleepSecondsLine = reader.ReadLine();
                        string ADCMinLine = reader.ReadLine();
                        string ADCMaxLine = reader.ReadLine();

                        int new_mode = int.Parse(ModeLine.Substring(5, 8));
                        bool sleep_enable = SleepEnableLine.Contains("True");
                        int sleep_seconds = int.Parse(SleepSecondsLine.Substring(14, 8));
                        int adc_min = int.Parse(ADCMinLine.Substring(20, 8));
                        int adc_max = int.Parse(ADCMaxLine.Substring(20, 8));

                        PARENT_FORM.AmplifierMode = new_mode;
                        PARENT_FORM.SLEEP_ENABLE = sleep_enable;
                        PARENT_FORM.SLEEP_SECONDS = sleep_seconds;
                        PARENT_FORM.ADC_CALIBRATION_MIN = adc_min;
                        PARENT_FORM.ADC_CALIBRATION_MAX = adc_max;

                        PARENT_FORM.SetBridgeMode(new_mode);

                    }

                    if (PARENT_FORM.GetDeviceFamily() == DeviceFamily.DSP100)
                    {
                        string SleepEnableLine = reader.ReadLine();
                        string SleepSecondsLine = reader.ReadLine();
                        string ADCMinLine = reader.ReadLine();
                        string ADCMaxLine = reader.ReadLine();

                        bool sleep_enable = SleepEnableLine.Contains("True");
                        int sleep_seconds = int.Parse(SleepSecondsLine.Substring(14, 8));
                        int adc_min = int.Parse(ADCMinLine.Substring(20, 8));
                        int adc_max = int.Parse(ADCMaxLine.Substring(20, 8));
                        PARENT_FORM.SLEEP_ENABLE = sleep_enable;
                        PARENT_FORM.SLEEP_SECONDS = sleep_seconds;
                        PARENT_FORM.ADC_CALIBRATION_MIN = adc_min;
                        PARENT_FORM.ADC_CALIBRATION_MAX = adc_max;

                    }

                    string PresetLine = "";

                    for (int i = 0; i < PARENT_FORM.GetNumPresets(); i++)
                    {
                        PresetLine = reader.ReadLine();
                        PARENT_FORM.DSP_PROGRAMS[i].ReadFromFile(PARENT_FORM, reader);
                    }
                }

            }
            catch (IOException io_ex)
            {
                Console.WriteLine("IOException in SCFG_Manager.Write: " + io_ex.Message);
            }
            catch (UnauthorizedAccessException access_ex)
            {
                Console.WriteLine("UnauthorizedAccessException in SCFG_Manager.Write: " + access_ex.Message);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Loaded SCFG file"))
                {
                    throw new Exception(ex.Message);
                }
                Console.WriteLine("Exception in SCFG_Manager.Write: " + ex.Message);
            }


        }

    }
}
