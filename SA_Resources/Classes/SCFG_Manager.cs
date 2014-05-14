using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SA_Resources;
using System.Globalization;
using System.IO;
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
                    string SerialLine = reader.ReadLine();
                    string TimestampLine = reader.ReadLine();

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
                Console.WriteLine("Exception in SCFG_Manager.Write: " + ex.Message);
            }


        }

    }
}
