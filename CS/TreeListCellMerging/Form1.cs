using System;
using System.Windows.Forms;

namespace TreeListCellMerging
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



     
        private void Form1_Load(object sender, EventArgs e)
        {
            this.departmentsTableAdapter.Fill(this.departmentsDataSet.Departments);
            treeList1.ExpandAll();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            treeList1.ShowPrintPreview();
          
        }
    }
}