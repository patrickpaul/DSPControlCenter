using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SA_Resources.SAControls;
using SA_Resources.SAForms;

namespace SA_Resources.DSP.Primitives
{
    public class DSP_Program_Manager
    {
        public List<DSP_Primitive> PRIMITIVES;

        public int next_available_offset = 0;

        public string Name;

        public DSP_Program_Manager(string in_name = "")
        {
            Name = in_name;
            PRIMITIVES = new List<DSP_Primitive>();
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

                                if (print_labels)
                                {
                                    file.WriteLine("StandardGain at " + singlePrimitive.Channel + "-" + singlePrimitive.PositionA);
                                }
                                
                                foreach (UInt32 singleValue in ((DSP_Primitive_StandardGain) singlePrimitive).Values)
                                {
                                    
                                    file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }

                                break;

                            case DSP_Primitive_Types.Pregain:

                                if (print_labels)
                                {
                                    file.WriteLine("Pregain at " + singlePrimitive.Channel + "-" + singlePrimitive.PositionA);
                                }

                                foreach (UInt32 singleValue in ((DSP_Primitive_Pregain)singlePrimitive).Values)
                                {

                                    file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }

                                break;


                            case DSP_Primitive_Types.BiquadFilter:

                                if (print_labels)
                                {
                                    file.WriteLine("Filter at " + singlePrimitive.Channel + "-" + singlePrimitive.PositionA);
                                }

                                foreach (UInt32 singleValue in ((DSP_Primitive_BiquadFilter) singlePrimitive).Values)
                                {

                                    file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                    index_counter++;
                                }


                            break;

                            case DSP_Primitive_Types.Compressor:
                            case DSP_Primitive_Types.Limiter:

                                if (print_labels)
                                {
                                    file.WriteLine("Compressor/Limiter at " + singlePrimitive.Channel + "-" + singlePrimitive.PositionA);
                                }
                            
                            foreach (UInt32 singleValue in ((DSP_Primitive_Compressor)singlePrimitive).Values)
                            {

                                file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                index_counter++;
                            }


                            break;

                            case DSP_Primitive_Types.Ducker4x4:

                                if (print_labels)
                                {
                                    file.WriteLine("Ducker at " + singlePrimitive.Channel + "-" + singlePrimitive.PositionA);
                            
                                }
                            
                            foreach (UInt32 singleValue in ((DSP_Primitive_Ducker4x4)singlePrimitive).Values)
                            {

                                file.WriteLine(index_counter.ToString("0000") + ":" + singleValue.ToString("X8"));
                                index_counter++;
                            }


                            break;

                            case DSP_Primitive_Types.MixerCrosspoint:

                                if (print_labels)
                                {
                                    file.WriteLine("MixerCrosspoint at " + singlePrimitive.Channel + "-" + singlePrimitive.PositionA);

                                }
                            
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




    }
}
