using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SA_Resources.SAControls
{
    public partial class GainFader : PictureBox
    {
        public GainFader()
        {
            this.BackgroundImage = SA_Resources.GlobalResources.ui_meter_base;
            this.AutoSize = false;
            this.Size = new Size(73, 302);
        }
    }
}
