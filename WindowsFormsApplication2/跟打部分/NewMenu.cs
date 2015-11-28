using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class NewMenu : MenuStrip
    {
        private Color _themeColor = Color.Gray;
        public NewMenu()
       {
           //InitializeComponent();
           this.Renderer = new CustomProfessionalRenderer(_themeColor);
        }
        public Color ThemeColor
        {
           get { return _themeColor; }
           set
          {
              _themeColor = value;
              this.Renderer = new CustomProfessionalRenderer(_themeColor);
           }
        }

    }
}
