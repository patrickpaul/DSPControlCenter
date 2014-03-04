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
                    
                    
                    case DSP_Primitive_Types.BiquadFilter:
                        /*
                    PrimitiveButton = (PictureButton)(PARENT_FORM.Controls.Find("btnCH" + (SinglePrimitive.Channel + 1) + "Delay", true).FirstOrDefault());

                    if (PrimitiveButton != null)
                    {
                        PrimitiveButton.MouseClick += new MouseEventHandler(PARENT_FORM.btnDelay_MouseClick);
                    }
*/
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

                    default:


                        break;

                }



            }
        }




    }
}
