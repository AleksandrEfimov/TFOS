using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFOS
{
    public partial class FormAttributeComparator : Form
    {
        public FormAttributeComparator()
        {
            InitializeComponent();
        }

        public string tbResult
        {
            get { return ""; }
            set { tbResultView.Text = value; }
        }
        
        private void FormAttributeComparator_Load(object sender, EventArgs e)
        {
            
            
        }
    }
}
