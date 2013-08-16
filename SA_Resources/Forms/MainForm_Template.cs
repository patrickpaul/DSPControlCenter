using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SA_Resources.Forms
{
    public partial class MainForm_Template : Form
    {
        public List<DSP_Setting>[] _settings = new List<DSP_Setting>[3];
        public List<DSP_Setting>[] _cached_settings = new List<DSP_Setting>[3];

        public List<UInt32>[] _gain_meters = new List<UInt32>[4];
        public List<UInt32>[] _comp_meters = new List<UInt32>[2];

        public ProgramConfig[] PROGRAMS = new ProgramConfig[3];

        

        /* Settings to put into demo modes */
        public bool DisableComms = false;
        public readonly bool _vsDebug = System.Diagnostics.Debugger.IsAttached;

        /* Settings Initialization */

        public bool form_loaded = false;

        public int num_channels = 4;
        public int num_phantom = 4;

        public PIC_Bridge _PIC_Conn;

        public int CURRENT_PROGRAM = 0;
        public int NUM_PROGRAMS = 1;



        // TODO - Move all DEVICE ID's to a global list
        public int DEVICE_ID = 0x20;
        public string SERIALNUM = "";

        public string CONFIGFILE = "";


        public MainForm_Template()
        {
            InitializeComponent();

        }


        public void InitializePrograms()
        {
            for (int p = 0; p < NUM_PROGRAMS; p++)
            {
                PROGRAMS[p] = new ProgramConfig();
            }

        }

        public virtual void UpdateTooltips()
        {

            

        }

        public virtual void AddItemToQueue(LiveQueueItem itemToAdd)
        {
        }
    }
}
