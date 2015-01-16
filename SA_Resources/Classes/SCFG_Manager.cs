/*
 * File     : SCFG_Manager.cs
 * Updated  : 15 January 2015
 * Author   : Patrick Paul
 * Synopsis : Configuration file (SCFG) manager class.
 *
 * This software is Copyright (c) 2013-2015, Stewart Audio Inc. and/or its licensors
 *
 */

using System;
using System.Reflection;
using System.IO;
using SA_Resources.DeviceManagement;
using SA_Resources.SAForms;

namespace SA_Resources.DeviceManagement
{
    public static class SCFG_Manager
    {
        /// <summary>
        /// Writes a device configuration (.scfg) to a file. Will overwrite existing file if it exists.
        /// </summary>
        /// <param name="outputFile">The output file path.</param>
        /// <param name="PARENT_FORM">The device's form.</param>
        /// <exception cref="SA_Resources.SCFG_Manager.SCFG_WRITE_EXCEPTION">
        /// IOException in SCFG_Manager.Write
        /// or
        /// UnauthorizedAccessException in SCFG_Manager.Write
        /// or
        /// Exception in SCFG_Manager.Write
        /// </exception>
        public static void Write(string outputFile, MainForm_Template PARENT_FORM)
        {
            try
            {
                if (File.Exists(outputFile))
                {
                    File.Delete(outputFile);
                }

                DeviceFamily dFamily = PARENT_FORM.GetDeviceFamily();

                using (StreamWriter writer = new StreamWriter(outputFile, true))
                {
                    Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version;

                    writer.WriteLine("DSPCCVERSION:" + currentVersion.Major + "." + currentVersion.Minor + "." + currentVersion.Build);
                    writer.WriteLine("DEVICE-ID:" + PARENT_FORM.GetDeviceID().ToString("X8") + ";");
                    writer.WriteLine("SERIAL:" + PARENT_FORM.SERIALNUM + ";");
                    writer.WriteLine("TIMESTAMP:" + DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + ";");

                    if (PARENT_FORM.IsAmplifier())
                    {
                        if (dFamily == DeviceFamily.FLX || dFamily == DeviceFamily.FLXNET)
                        {
                            int amplifierMode = PARENT_FORM.AmplifierMode;
                            writer.WriteLine("MODE:" + amplifierMode.ToString("00000000") + ";");
                        }

                        /* Note - these are declared to local variables first to prevent CS1690 Warnings */
                        bool sleepEnable = PARENT_FORM.SLEEP_ENABLE;
                        int sleepSeconds = PARENT_FORM.SLEEP_SECONDS;
                        int adcMin = PARENT_FORM.ADC_CALIBRATION_MIN;
                        int adcMax = PARENT_FORM.ADC_CALIBRATION_MAX;

                        writer.WriteLine("SLEEP_ENABLE:" + sleepEnable + ";");
                        writer.WriteLine("SLEEP_SECONDS:" + sleepSeconds.ToString("00000000") + ";");
                        writer.WriteLine("ADC_CALIBRATION_MIN:" + adcMin.ToString("00000000") + ";");
                        writer.WriteLine("ADC_CALIBRATION_MAX:" + adcMax.ToString("00000000") + ";");
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
                throw new SCFG_WRITE_EXCEPTION("IOException in SCFG_Manager.Write", io_ex);
            }
            catch (UnauthorizedAccessException access_ex)
            {
                throw new SCFG_WRITE_EXCEPTION("UnauthorizedAccessException in SCFG_Manager.Write", access_ex);
            }
            catch (Exception ex)
            {
                throw new SCFG_WRITE_EXCEPTION("Exception in SCFG_Manager.Write", ex);
            }

        }

        /// <summary>
        /// Reads device configuration file (.scfg) to a device form
        /// </summary>
        /// <param name="inputFile">The scfg file path.</param>
        /// <param name="PARENT_FORM">The device's form.</param>
        /// <exception cref="SA_Resources.SCFG_Manager.SCFG_READ_EXCEPTION">
        /// Loaded SCFG file does not match current device.
        /// or
        /// Unexpected token in configuration file on line  + linecounter
        /// or
        /// Did not find the start for Prefix # + i + .
        /// or
        /// IOException in SCFG_Manager.Read
        /// or
        /// IOException in SCFG_Manager.Read
        /// or
        /// Exception in SCFG_Manager.Read
        /// </exception>
        public static void Read(string inputFile, MainForm_Template PARENT_FORM)
        {
            try
            {
                using (StreamReader reader = new StreamReader(inputFile))
                {
                    bool presetStartFound = false;
                    string unprocessedLine = "";
                    int linecounter = 1;

                    while (!presetStartFound)
                    {
                        unprocessedLine = reader.ReadLine();

                        if (unprocessedLine == null)
                        {
                            // Catch the possible null reference exception here.
                            throw new SCFG_READ_EXCEPTION("Reached end of file before the start of a preset was found.");
                        }

                        if (unprocessedLine.StartsWith("DSPCCVERSION"))
                        {
                            // Consume for now
                        }
                        else if (unprocessedLine.StartsWith("DEVICE-ID"))
                        {
                            int device_id = Convert.ToInt32(unprocessedLine.Substring(16, 2), 16);

                            if (device_id != PARENT_FORM.GetDeviceID())
                            {
                                throw new SCFG_READ_EXCEPTION("Loaded SCFG file does not match current device.");
                            }
                        }
                        else if (unprocessedLine.StartsWith("SERIAL"))
                        {
                            // Consume for now
                        }
                        else if (unprocessedLine.StartsWith("TIMESTAMP"))
                        {
                            // Consume for now
                        }
                        else if (unprocessedLine.StartsWith("MODE"))
                        {
                            // Consume for now as bridge mode for FLX80-4 was removed due to technical limitations
                            // TODO - Re-evaluate this statement
                        }
                        else if (unprocessedLine.StartsWith("SLEEP_ENABLE"))
                        {
                            PARENT_FORM.SLEEP_ENABLE = unprocessedLine.Contains("True");
                        }
                        else if (unprocessedLine.StartsWith("SLEEP_SECONDS"))
                        {
                            PARENT_FORM.SLEEP_SECONDS = int.Parse(unprocessedLine.Substring(14, 8));
                        }
                        else if (unprocessedLine.StartsWith("ADC_CALIBRATION_MIN"))
                        {
                            PARENT_FORM.ADC_CALIBRATION_MIN = int.Parse(unprocessedLine.Substring(20, 8));
                        }
                        else if (unprocessedLine.StartsWith("ADC_CALIBRATION_MAX"))
                        {
                            PARENT_FORM.ADC_CALIBRATION_MAX = int.Parse(unprocessedLine.Substring(20, 8));
                        }
                        else if (unprocessedLine.StartsWith("PRESET"))
                        {
                            presetStartFound = true;
                        }
                        else
                        {
                            throw new SCFG_READ_EXCEPTION("Unexpected token in configuration file on line " + linecounter);
                        }

                        linecounter++;
                    }
                   

                    string PresetLine = "";

                    for (int i = 0; i < PARENT_FORM.GetNumPresets(); i++)
                    {
                        if (i > 0)
                        {
                            PresetLine = reader.ReadLine();

                            if (PresetLine == null)
                            {
                                throw new SCFG_READ_EXCEPTION("Encountered null reference when searching for Prefix #" + i + ".");
                            }

                            if (!PresetLine.StartsWith("PRESET"))
                            {
                                throw new SCFG_READ_EXCEPTION("Did not find the start for Prefix #" + i + ".");
                            }
                        }

                        PARENT_FORM.DSP_PROGRAMS[i].ReadFromFile(PARENT_FORM, reader);
                    }
                }
            }
            catch (IOException ioEx)
            {
                throw new SCFG_READ_EXCEPTION("IOException in SCFG_Manager.Read", ioEx);
            }
            catch (UnauthorizedAccessException accessEx)
            {
                throw new SCFG_READ_EXCEPTION("IOException in SCFG_Manager.Read", accessEx);
            }
            catch (Exception ex)
            {
                throw new SCFG_READ_EXCEPTION("Exception in SCFG_Manager.Read", ex);
            }
        }

        #region Exception Classes

        public class SCFG_WRITE_EXCEPTION : Exception
        {
            public SCFG_WRITE_EXCEPTION()
            {
            }

            public SCFG_WRITE_EXCEPTION(string message)
                : base(message)
            {
            }

            public SCFG_WRITE_EXCEPTION(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class SCFG_READ_EXCEPTION : Exception
        {
            public SCFG_READ_EXCEPTION()
            {
            }

            public SCFG_READ_EXCEPTION(string message)
                : base(message)
            {
            }

            public SCFG_READ_EXCEPTION(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        #endregion

    }
}