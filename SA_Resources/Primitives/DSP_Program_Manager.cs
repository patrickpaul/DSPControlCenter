using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SA_Resources.SAControls;
using SA_Resources.SAForms;
using SA_Resources.USB;

namespace SA_Resources.DSP.Primitives
{
    public class DSP_Program_Manager
    {
        public List<DSP_Primitive> PRIMITIVES;
        public List<UInt32> FLASH_READ_VALUES;

        private const int NUM_PAGES = 12;

        public int next_available_offset = 0;

        public string Name;
        public int Index;

        public DSP_Program_Manager(int program_index, string in_name = "")
        {
            Index = program_index;

            Name = in_name;
            PRIMITIVES = new List<DSP_Primitive>();
            FLASH_READ_VALUES = new List<UInt32>();


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

        public void SaveToFile(string outputFile)
        {
            bool print_labels = false;
            Int16 index_counter = 0;
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(outputFile))
                {

                    foreach (DSP_Primitive singlePrimitive in PRIMITIVES)
                    {
                        switch (singlePrimitive.Type)
                        {
                            case DSP_Primitive_Types.StandardGain:

       
                                
                                foreach (UInt32 singleValue in ((DSP_Primitive_StandardGain) singlePrimitive).Values)
                                {
                                    
                                    file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }

                                break;

                            case DSP_Primitive_Types.Pregain:

                                foreach (UInt32 singleValue in ((DSP_Primitive_Pregain)singlePrimitive).Values)
                                {

                                    file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }

                                break;


                            case DSP_Primitive_Types.BiquadFilter:

                    
                                foreach (UInt32 singleValue in ((DSP_Primitive_BiquadFilter) singlePrimitive).Values)
                                {

                                    file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }


                            break;

                            case DSP_Primitive_Types.Compressor:
                            case DSP_Primitive_Types.Limiter:

                     
                            foreach (UInt32 singleValue in ((DSP_Primitive_Compressor)singlePrimitive).Values)
                            {

                                file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                index_counter++;
                            }


                            break;

                            case DSP_Primitive_Types.Ducker4x4:

       
                            
                            foreach (UInt32 singleValue in ((DSP_Primitive_Ducker4x4)singlePrimitive).Values)
                            {

                                file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                index_counter++;
                            }


                            break;

                            case DSP_Primitive_Types.MixerCrosspoint:

                            foreach (UInt32 singleValue in ((DSP_Primitive_MixerCrosspoint)singlePrimitive).Values)
                            {

                                file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                index_counter++;
                            }


                            break;
                            
                            
                            case DSP_Primitive_Types.Delay:

                                if (print_labels)
                                {
                                    file.WriteLine("Delay at " + singlePrimitive.Channel + "-" + singlePrimitive.PositionA);
  
                                }
                            
                            foreach (UInt32 singleValue in ((DSP_Primitive_Delay)singlePrimitive).Values)
                            {

                                file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                index_counter++;
                            }


                            break;



                        }



                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in SaveToFile]: " + ex.Message);
            }


        }

        public bool ReadFromDevice(MainForm_Template PARENT_FORM)
        {
            try
            {
                DSP_Primitive_Input InputPrimitive;
                DSP_Primitive_Output OutputPrimitive;
                DSP_Primitive_Pregain PregainPrimitive;

                if (PARENT_FORM._PIC_Conn.getRTS())
                {
                    FLASH_READ_VALUES.Clear();

                    for (int i = 0; i < NUM_PAGES; i++)
                    {
                        //Console.WriteLine("Streaming page " + i + " to index  " + (i*64));
                        PARENT_FORM._PIC_Conn.StreamReadPage(this.Index, i, ref FLASH_READ_VALUES, i*64);
                    }

                    //Console.WriteLine("Done reading values! Now to load them....");

                    int primitive_value_offset = 0;

                    List<UInt32> Single_Primitive_Values = new List<uint>();

                    Single_Primitive_Values.Clear();
                    DSP_Primitive_BiquadFilter SingleFilterPrimitive;

                    
                    foreach (DSP_Primitive singlePrimitive in PRIMITIVES)
                    {
                        Single_Primitive_Values.Clear();
                        
                        if (singlePrimitive.Type == DSP_Primitive_Types.BiquadFilter)
                        {

                            SingleFilterPrimitive = (DSP_Primitive_BiquadFilter) singlePrimitive;
                            Single_Primitive_Values.Add(FLASH_READ_VALUES[512 + SingleFilterPrimitive.Plainfilter_Offset]);
                            Single_Primitive_Values.Add(FLASH_READ_VALUES[576 + SingleFilterPrimitive.Plainfilter_Offset]);
                            Single_Primitive_Values.Add(FLASH_READ_VALUES[640 + SingleFilterPrimitive.Plainfilter_Offset]);
                        }
                        else
                        {
                            Single_Primitive_Values = FLASH_READ_VALUES.GetRange(primitive_value_offset, singlePrimitive.Num_Values);
 
                        }

                        singlePrimitive.UpdateFromReadValues(Single_Primitive_Values);

                        primitive_value_offset += singlePrimitive.Num_Values;
                    }

                    // Input settings - Name, Pregain, Phantom Power

                    for (int i = 0; i < PARENT_FORM.GetNumInputChannels(); i++)
                    {
                        InputPrimitive = (DSP_Primitive_Input)PARENT_FORM.DSP_PROGRAMS[this.Index].LookupPrimitive(DSP_Primitive_Types.Input, i, 0);
                        PregainPrimitive = (DSP_Primitive_Pregain)PARENT_FORM.DSP_PROGRAMS[this.Index].LookupPrimitive(DSP_Primitive_Types.Pregain, i, 0);
                        
                        InputPrimitive.PhantomPower = ((FLASH_READ_VALUES[569] & 0x01) == 1);

                        InputPrimitive.Pregain = FLASH_READ_VALUES[InputPrimitive.PregainOffset];

                        FLASH_READ_VALUES[569] >>= 1;

                        InputPrimitive.LoadNameFromValues(FLASH_READ_VALUES.GetRange(InputPrimitive.NameOffset, 5));

                        PregainPrimitive.Gain = PregainPrimitive.Gain - InputPrimitive.Pregain;
                        PregainPrimitive.Pregain = (int)InputPrimitive.Pregain;

                    }

                    // Output settings - Name
                    for (int i = 0; i < PARENT_FORM.GetNumOutputChannels(); i++)
                    {
                        OutputPrimitive = (DSP_Primitive_Output)PARENT_FORM.DSP_PROGRAMS[this.Index].LookupPrimitive(DSP_Primitive_Types.Output, i, 0);
 
                        OutputPrimitive.LoadNameFromValues(FLASH_READ_VALUES.GetRange(OutputPrimitive.NameOffset, 5));

                     }


                    //Console.WriteLine("Done with program read");

                    return true;
                }
                else
                {
                    Console.WriteLine("[EXCEPTION in DSP_Program_Manager.ReadFromDevice]: Did not get RTS");
                    return false;
                }
            }
            catch (Exception ex)
            {
                 Console.WriteLine("[EXCEPTION in DSP_Program_Manager.ReadFromDevice]: " + ex.Message);
                return false;
            }


        }




    }
}
