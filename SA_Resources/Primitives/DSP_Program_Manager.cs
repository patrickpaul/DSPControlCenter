using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SA_Resources.SAControls;
using SA_Resources.SAForms;
using SA_Resources.USB;

namespace SA_Resources.DSP.Primitives
{
    public class DSP_Program_Manager
    {
        public List<DSP_Primitive> PRIMITIVES;
        public List<UInt32> READ_VALUE_CACHE;
        public UInt32[] WRITE_VALUE_CACHE = new UInt32[768];
        private const int NUM_PAGES = 12;

        public int next_available_offset = 0;

        public string Name;
        public int Index;

        public DSP_Program_Manager(int program_index, string in_name = "")
        {
            Index = program_index;

            Name = in_name;
            PRIMITIVES = new List<DSP_Primitive>();
            READ_VALUE_CACHE = new List<UInt32>();


        }

        public void RegisterNewPrimitive(int offset,DSP_Primitive in_primitive)
        {
            
            if(offset < next_available_offset)
            {
                throw new Exception("Offset overlap in Primitive Manager. Attempted to add at offset " + offset + " where next available is  " + next_available_offset);
                return;
            }

            next_available_offset = offset + in_primitive.Num_Values;

            in_primitive.SetOffset(offset);
            PRIMITIVES.Add(in_primitive);
        }

        public int LookupIndex(DSP_Primitive_Types in_type, int in_ch, int in_positiona, int in_positionb = -1)
        {
            return PRIMITIVES.FindIndex(
                prim => 
                    (prim.Type == in_type) && 
                    (prim.Channel == in_ch) && 
                    (prim.PositionA == in_positiona)
                );

        }

        public DSP_Primitive LookupPrimitive(DSP_Primitive_Types in_type, int in_ch, int in_positiona, int in_positionb = -1)
        {
            return PRIMITIVES.Find(
                prim =>
                    (prim.Type == in_type) &&
                    (prim.Channel == in_ch) &&
                    (prim.PositionA == in_positiona)
                );

        }

        public void PrintIndexUsage()
        {
            foreach (DSP_Primitive singlePrimitive in PRIMITIVES)
            {
                Console.WriteLine(singlePrimitive.Offset + "-" + (singlePrimitive.Offset + singlePrimitive.Num_Values-1) + " =" + singlePrimitive.Name);
            }

        }

        public void ReloadFromSettingsList(List<DSP_Setting> settingsList)
        {
            foreach(DSP_Primitive singlePrimitive in PRIMITIVES)
            {
                Console.WriteLine("[ERROR] ReloadFromSettingsList - Not yet implemented.");
                //singlePrimitive.UpdateFromReadValues(settingsList.GetRange(singlePrimitive.Offset, singlePrimitive.Num_Values));

            }
        }

        public void AttachUI_Events(MainForm_Template PARENT_FORM)
        {

            PictureButton PrimitiveButton = null;
            Label PrimitiveLabel = null;

            foreach (DSP_Primitive SinglePrimitive in PRIMITIVES)
            {
                switch (SinglePrimitive.Type)
                {
                    case DSP_Primitive_Types.Compressor:
                    case DSP_Primitive_Types.Limiter:

                        PrimitiveButton = (PictureButton)(PARENT_FORM.Controls.Find("btnCompressor" + (SinglePrimitive.Channel) + (SinglePrimitive.PositionA), true).FirstOrDefault());

                        if (PrimitiveButton != null)
                        {
                            PrimitiveButton.MouseClick += new MouseEventHandler(PARENT_FORM.btnComp_MouseClick);
                        }

                    break;

                    case DSP_Primitive_Types.StandardGain:

                    PrimitiveButton = (PictureButton)(PARENT_FORM.Controls.Find("btnGain" + SinglePrimitive.Channel + SinglePrimitive.PositionA, true).FirstOrDefault());

                    if (PrimitiveButton != null)
                    {
                        PrimitiveButton.MouseClick += new MouseEventHandler(PARENT_FORM.btnStandardGain_MouseClick);
                    }

                    break;
                    
                    case DSP_Primitive_Types.Pregain:
                        
                    PrimitiveButton = (PictureButton)(PARENT_FORM.Controls.Find("btnGain" + SinglePrimitive.Channel + SinglePrimitive.PositionA, true).FirstOrDefault());

                    if (PrimitiveButton != null)
                    {
                        PrimitiveButton.MouseClick += new MouseEventHandler(PARENT_FORM.btnPregain_MouseClick);
                    }

                    break;
                    
                    
                    case DSP_Primitive_Types.BiquadFilter:

                        if (SinglePrimitive.PositionA == 0)
                        {
                            PrimitiveButton = (PictureButton)(PARENT_FORM.Controls.Find("btnCH" + (SinglePrimitive.Channel) + "PreFilters", true).FirstOrDefault());

                            if (PrimitiveButton != null)
                            {
                                PrimitiveButton.MouseClick += new MouseEventHandler(PARENT_FORM.btnFilter_MouseClick);
                            }
                        }
                        else if (SinglePrimitive.PositionA == 3)
                        {
                            PrimitiveButton = (PictureButton)(PARENT_FORM.Controls.Find("btnCH" + (SinglePrimitive.Channel) + "PostFilters", true).FirstOrDefault());

                            if (PrimitiveButton != null)
                            {
                                PrimitiveButton.MouseClick += new MouseEventHandler(PARENT_FORM.btnFilter_MouseClick);
                            }
                        }
                    break;
                    
                    
                    case DSP_Primitive_Types.Delay:

                        PrimitiveButton = (PictureButton)(PARENT_FORM.Controls.Find("btnCH" + (SinglePrimitive.Channel + 1) + "Delay", true).FirstOrDefault());

                        if (PrimitiveButton != null)
                        {
                            PrimitiveButton.MouseClick += new MouseEventHandler(PARENT_FORM.btnDelay_MouseClick);
                        }

                    break;

                    case DSP_Primitive_Types.Input:

                    PrimitiveLabel = (Label)(PARENT_FORM.Controls.Find("lblCH" + (SinglePrimitive.Channel + 1) + "Input", true).FirstOrDefault());

                    if (PrimitiveLabel != null)
                    {
                        PrimitiveLabel.MouseClick += new MouseEventHandler(PARENT_FORM.lblInput_MouseClick);
                    }

                    break;

                    case DSP_Primitive_Types.Output:

                    PrimitiveLabel = (Label)(PARENT_FORM.Controls.Find("lblCH" + (SinglePrimitive.Channel + 1) + "Output", true).FirstOrDefault());

                    if (PrimitiveLabel != null)
                    {
                        PrimitiveLabel.MouseClick += new MouseEventHandler(PARENT_FORM.lblOutput_MouseClick);
                    }

                    break;

                    case DSP_Primitive_Types.Ducker4x4:

                    PrimitiveButton = (PictureButton)(PARENT_FORM.Controls.Find("pbtnDucker", true).FirstOrDefault());

                        if (PrimitiveButton != null)
                        {
                            PrimitiveButton.MouseClick += new MouseEventHandler(PARENT_FORM.pbtnDucker4x4_MouseClick);
                        }

                    break;

                    default:


                        break;

                }



            }
        }

        public void Write_Program_To_Cache(int num_channels = 4)
        {
            bool print_labels = false;
            Int16 index_counter = 0;

            

            int offset_counter = 0;
            int plain_offset_counter = 0;

            for (int i = 0; i < 768; i++)
            {
                WRITE_VALUE_CACHE[i] = 0x11111111;
            }

            try
            {
       
                    foreach (DSP_Primitive singlePrimitive in PRIMITIVES)
                    {
                        offset_counter = 0;

                        switch (singlePrimitive.Type)
                        {
                            case DSP_Primitive_Types.StandardGain:

                                offset_counter = singlePrimitive.Offset;
                                
                                foreach (UInt32 singleValue in ((DSP_Primitive_StandardGain) singlePrimitive).Values)
                                {
                                    WRITE_VALUE_CACHE[offset_counter++] = singleValue;
                                    //file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }

                                break;

                            case DSP_Primitive_Types.Pregain:

                                offset_counter = singlePrimitive.Offset;

                                foreach (UInt32 singleValue in ((DSP_Primitive_Pregain)singlePrimitive).Values)
                                {
                                    WRITE_VALUE_CACHE[offset_counter++] = singleValue;
                                    //file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }

                                break;


                            case DSP_Primitive_Types.BiquadFilter:

                                DSP_Primitive_BiquadFilter RecastFilter = ((DSP_Primitive_BiquadFilter) singlePrimitive);
                                RecastFilter.Recalculate_Values();

                                offset_counter = singlePrimitive.Offset;

                                foreach (UInt32 singleValue in ((DSP_Primitive_BiquadFilter) singlePrimitive).Values)
                                {
                                    WRITE_VALUE_CACHE[offset_counter++] = singleValue;
                                    //file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }

                                WRITE_VALUE_CACHE[512 + RecastFilter.Plainfilter_Offset] = RecastFilter.Package_Value;
                                WRITE_VALUE_CACHE[576 + RecastFilter.Plainfilter_Offset] = RecastFilter.Package_Gain_Value;
                                WRITE_VALUE_CACHE[640 + RecastFilter.Plainfilter_Offset] = RecastFilter.Package_Q_Value;

                            break;

                            case DSP_Primitive_Types.Compressor:
                            case DSP_Primitive_Types.Limiter:

                            offset_counter = singlePrimitive.Offset;

                            foreach (UInt32 singleValue in ((DSP_Primitive_Compressor)singlePrimitive).Values)
                            {
                                WRITE_VALUE_CACHE[offset_counter++] = singleValue;
                                //file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                index_counter++;
                            }


                            break;

                            case DSP_Primitive_Types.Ducker4x4:

                                DSP_Primitive_Ducker4x4 RecastDucker = ((DSP_Primitive_Ducker4x4)singlePrimitive);

                                offset_counter = singlePrimitive.Offset;
                                RecastDucker.RecalculateRouters();

                                foreach (UInt32 singleValue in ((DSP_Primitive_Ducker4x4)singlePrimitive).Values)
                                {
                                    WRITE_VALUE_CACHE[offset_counter++] = singleValue;
                                    //file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }

                                WRITE_VALUE_CACHE[RecastDucker.PlainValue_Offset] = RecastDucker.Ducker_Package;
                            break;

                            case DSP_Primitive_Types.MixerCrosspoint:

                                offset_counter = singlePrimitive.Offset;

                                foreach (UInt32 singleValue in ((DSP_Primitive_MixerCrosspoint)singlePrimitive).Values)
                                {
                                    WRITE_VALUE_CACHE[offset_counter++] = singleValue;
                                    //file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }


                            break;
                            
                            
                            case DSP_Primitive_Types.Delay:

                            offset_counter = singlePrimitive.Offset;
                            
                            foreach (UInt32 singleValue in ((DSP_Primitive_Delay)singlePrimitive).Values)
                            {
                                WRITE_VALUE_CACHE[offset_counter++] = singleValue;

                                //file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                index_counter++;
                            }


                            break;

                            case DSP_Primitive_Types.Input:

                            DSP_Primitive_Input RecastInput = ((DSP_Primitive_Input)singlePrimitive);

                                int name_offset = RecastInput.NameOffset;
                                RecastInput.NameToValues();

                                WRITE_VALUE_CACHE[RecastInput.PregainOffset] = RecastInput.Pregain;

                                WRITE_VALUE_CACHE[name_offset++] = RecastInput.NameValues[0];
                                WRITE_VALUE_CACHE[name_offset++] = RecastInput.NameValues[1];
                                WRITE_VALUE_CACHE[name_offset++] = RecastInput.NameValues[2];
                                WRITE_VALUE_CACHE[name_offset++] = RecastInput.NameValues[3];
                                WRITE_VALUE_CACHE[name_offset] = RecastInput.NameValues[4];

                            break;


                            case DSP_Primitive_Types.Output:

                            DSP_Primitive_Output RecastOutput = ((DSP_Primitive_Output)singlePrimitive);

                            int output_name_offset = RecastOutput.NameOffset;
                            RecastOutput.NameToValues();

                            WRITE_VALUE_CACHE[output_name_offset++] = RecastOutput.NameValues[0];
                            WRITE_VALUE_CACHE[output_name_offset++] = RecastOutput.NameValues[1];
                            WRITE_VALUE_CACHE[output_name_offset++] = RecastOutput.NameValues[2];
                            WRITE_VALUE_CACHE[output_name_offset++] = RecastOutput.NameValues[3];
                            WRITE_VALUE_CACHE[output_name_offset] = RecastOutput.NameValues[4];

                            break;



                        }


                    }


                    /* CONFIGURE FILTERS FOR FUTURE INPUT CHANNELS 5-8 */
                    for (int filter_counter = 130; filter_counter < 190; )
                    {
                        WRITE_VALUE_CACHE[filter_counter++] = 0x20000000;
                        WRITE_VALUE_CACHE[filter_counter++] = 0x00000000;
                        WRITE_VALUE_CACHE[filter_counter++] = 0x00000000;
                        WRITE_VALUE_CACHE[filter_counter++] = 0x00000000;
                        WRITE_VALUE_CACHE[filter_counter++] = 0x00000000;

                    }
                    int f_counter = 0;

                    // TODO - This should be for DSP 4X4 ONLY
                    /* FROM DSP COMM TOOL - Fill in Bridge Router, Generators, HP, and Mutes
                     */
                    WRITE_VALUE_CACHE[470] = 0x00000001;
                    WRITE_VALUE_CACHE[471] = 0x00000002;
                    WRITE_VALUE_CACHE[472] = 0x00000003;
                    WRITE_VALUE_CACHE[473] = 0x00000004;
                    WRITE_VALUE_CACHE[474] = 0xFC58FE28;
                    WRITE_VALUE_CACHE[475] = 0x05555555;
                    WRITE_VALUE_CACHE[476] = 0xFAD5B33C;
                    WRITE_VALUE_CACHE[477] = 0x00000001;
                    WRITE_VALUE_CACHE[478] = 0x00000000;

                    /* BLANKS AFTER MUTE_INPUTS */

                    for (f_counter = 479; f_counter < 512; f_counter++)
                    {
                        WRITE_VALUE_CACHE[f_counter] = 0xFFFFFFFF;
                    }

                    /* OUTFILTER PACKAGE CHANNEL 5-6 */
                    for (int filter_counter = 548; filter_counter < 560; filter_counter++)
                    {
                        WRITE_VALUE_CACHE[filter_counter] = 0x00000000;
                    }

 
                    for (f_counter = 568; f_counter < 576; f_counter++)
                    {
                        WRITE_VALUE_CACHE[f_counter] = 0xFFFFFFFF;
                    }


                    /* PRESET LABEL */
                    // TODO - REMOVE THIS
                    WRITE_VALUE_CACHE[568] = 0x00000000;
                    WRITE_VALUE_CACHE[569] = 0x00000000;
                    WRITE_VALUE_CACHE[560] = 0x00000000;
                    WRITE_VALUE_CACHE[561] = 0x00000000;
                    WRITE_VALUE_CACHE[562] = 0x00000000;

                    /* PREGAIN CHANNELS 5-6 */
                    WRITE_VALUE_CACHE[564] = 0x00000000;
                    WRITE_VALUE_CACHE[565] = 0x00000000;

                    /* FILTER GAINS CHANNELS 5+6 AND BLANKS */
                    for (f_counter = 612; f_counter < 640; f_counter++)
                    {
                        WRITE_VALUE_CACHE[f_counter] = 0xFFFFFFFF;
                    }


                    /* FILTER Qs CHANNELS 5+6 AND BLANKS */
                    for (f_counter = 676; f_counter < 704; f_counter++)
                    {
                        WRITE_VALUE_CACHE[f_counter] = 0xFFFFFFFF;
                    }

        
                    /* POST CHANNEL LABELS*/

                    for (f_counter = 744; f_counter < 768; f_counter++)
                    {
                        WRITE_VALUE_CACHE[f_counter] = 0xFFFFFFFF;
                    }

                    UInt32 PhantomMask = 0;
                    DSP_Primitive_Input InputPrimitive = null;

                    for (int i = num_channels; i > 0; i--)
                    {
                        PhantomMask <<= 1;

                        InputPrimitive = (DSP_Primitive_Input)this.LookupPrimitive(DSP_Primitive_Types.Input, i - 1, 0);

                        if (InputPrimitive.PhantomAvailable)
                        {
                            if (InputPrimitive.PhantomPower)
                            {
                                PhantomMask |= 1;
                            }
                        }
                    }

                    WRITE_VALUE_CACHE[566] = PhantomMask;
                    
                    
                    
                    


                    
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in SaveToFile]: " + ex.Message);
            }


        }

        public void Write_Cache_To_File(StreamWriter writer)
        {
            try
            {
                writer.WriteLine("PRESET"+this.Index.ToString("00"));
                for (int i = 0; i < 768; i++)
                {
                    writer.WriteLine(i.ToString("0000") + ":" + WRITE_VALUE_CACHE[i].ToString("X8"));
                }            
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in Write_Cache_To_File]: " + ex.Message);
            }
        }

        public bool SendToDevice(MainForm_Template PARENT_FORM)
        {
            try
            {


            }
            catch (Exception ex)
            {
                
            }

            return false;
        }


        public void Read_File_Into_Cache(StreamReader reader)
        {
            try
            {
                READ_VALUE_CACHE.Clear();

                UInt32 parsed_value;

                for (int i = 0; i < 768; i++)
                {
       
                    bool parsedSuccessfully = UInt32.TryParse(reader.ReadLine().Substring(5, 8), NumberStyles.HexNumber, CultureInfo.CurrentCulture, out parsed_value);
                    
                    if (!parsedSuccessfully)
                    {
                        throw new Exception("Invalid value encountered in preset " + this.Index + " on line " + i);
                    }
                    else
                    {
                        READ_VALUE_CACHE.Add(parsed_value);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in Read_File_Into_Cache]: " + ex.Message);
            }

        }

        public void Read_Device_Into_Cache(MainForm_Template PARENT_FORM)
        {
            try
            {
                if (PARENT_FORM._PIC_Conn.getRTS())
                {
                    READ_VALUE_CACHE.Clear();

                    for (int i = 0; i < NUM_PAGES; i++)
                    {
                        PARENT_FORM._PIC_Conn.StreamReadPage(this.Index, i, ref READ_VALUE_CACHE, i * 64);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in Read_Device_Into_Cache]: " + ex.Message);
            }
        }

        public void Load_Cache_To_Program(MainForm_Template PARENT_FORM)
        {
            DSP_Primitive_Input InputPrimitive;
            DSP_Primitive_Output OutputPrimitive;
            DSP_Primitive_Pregain PregainPrimitive;
            DSP_Primitive_Ducker4x4 DuckerPrimitive;

            List<UInt32> Single_Primitive_Values = new List<uint>();

            Single_Primitive_Values.Clear();
            DSP_Primitive_BiquadFilter SingleFilterPrimitive;


            foreach (DSP_Primitive singlePrimitive in PRIMITIVES)
            {
                Single_Primitive_Values.Clear();

                if (singlePrimitive.Type == DSP_Primitive_Types.Input || singlePrimitive.Type == DSP_Primitive_Types.Output)
                {
                    continue;
                }

                if (singlePrimitive.Type == DSP_Primitive_Types.BiquadFilter)
                {

                    SingleFilterPrimitive = (DSP_Primitive_BiquadFilter)singlePrimitive;
                    Single_Primitive_Values.Add(READ_VALUE_CACHE[512 + SingleFilterPrimitive.Plainfilter_Offset]);
                    Single_Primitive_Values.Add(READ_VALUE_CACHE[576 + SingleFilterPrimitive.Plainfilter_Offset]);
                    Single_Primitive_Values.Add(READ_VALUE_CACHE[640 + SingleFilterPrimitive.Plainfilter_Offset]);
                }
                else
                {
                    Single_Primitive_Values = READ_VALUE_CACHE.GetRange(singlePrimitive.Offset, singlePrimitive.Num_Values);

                }

                singlePrimitive.UpdateFromReadValues(Single_Primitive_Values);

                //primitive_value_offset += singlePrimitive.Num_Values;
            }

            // Input settings - Name, Pregain, Phantom Power

            for (int i = 0; i < PARENT_FORM.GetNumInputChannels(); i++)
            {
                InputPrimitive = (DSP_Primitive_Input)PARENT_FORM.DSP_PROGRAMS[this.Index].LookupPrimitive(DSP_Primitive_Types.Input, i, 0);
                PregainPrimitive = (DSP_Primitive_Pregain)PARENT_FORM.DSP_PROGRAMS[this.Index].LookupPrimitive(DSP_Primitive_Types.Pregain, i, 0);

                InputPrimitive.PhantomPower = ((READ_VALUE_CACHE[566] & 0x01) == 1);

                InputPrimitive.Pregain = READ_VALUE_CACHE[InputPrimitive.PregainOffset];

                READ_VALUE_CACHE[566] >>= 1;

                InputPrimitive.LoadNameFromValues(READ_VALUE_CACHE.GetRange(InputPrimitive.NameOffset, 5));

                PregainPrimitive.Gain = PregainPrimitive.Gain - InputPrimitive.Pregain;
                PregainPrimitive.Pregain = (int)InputPrimitive.Pregain;

            }

            // Output settings - Name
            for (int i = 0; i < PARENT_FORM.GetNumOutputChannels(); i++)
            {
                OutputPrimitive = (DSP_Primitive_Output)PARENT_FORM.DSP_PROGRAMS[this.Index].LookupPrimitive(DSP_Primitive_Types.Output, i, 0);

                OutputPrimitive.LoadNameFromValues(READ_VALUE_CACHE.GetRange(OutputPrimitive.NameOffset, 5));

            }


            DuckerPrimitive = (DSP_Primitive_Ducker4x4)PARENT_FORM.DSP_PROGRAMS[this.Index].LookupPrimitive(DSP_Primitive_Types.Ducker4x4, 0, 0);

            DuckerPrimitive.UpdateFromPlainValues(READ_VALUE_CACHE[DuckerPrimitive.PlainValue_Offset]);

        }



        public bool ReadFromFile(MainForm_Template PARENT_FORM, StreamReader reader)
        {
            try
            {
                Read_File_Into_Cache(reader);


                Load_Cache_To_Program(PARENT_FORM);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in ReadFromFile]:" + ex.Message);
            }
            
            return false;
        }


        public bool ReadFromDevice(MainForm_Template PARENT_FORM)
        {
            try
            {
                Read_Device_Into_Cache(PARENT_FORM);


                Load_Cache_To_Program(PARENT_FORM);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in ReadFromDevice]:" + ex.Message);
            }

            return false;


        }




    }
}
