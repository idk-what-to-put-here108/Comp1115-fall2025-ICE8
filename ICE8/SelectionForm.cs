using System;

namespace ICE8
{
    public partial class SelectionForm : Form
    {
        public SelectionForm()
        {
            InitializeComponent();
        }
        
        private void Button_Generate_Click(object sender, EventArgs e)
        {
            TextBox_AGL.Text = Roll5d10().ToString();
            TextBox_STR.Text = Roll5d10().ToString();
            TextBox_VGR.Text = Roll5d10().ToString();
            TextBox_PER.Text = Roll5d10().ToString();
            TextBox_INT.Text = Roll5d10().ToString();
            TextBox_WIL.Text = Roll5d10().ToString();
           
        }

        /// <summary>
        /// This method rolls 5d10 and returns the total
        /// </summary>
        /// <returns>An integer value between 5 and 50</returns>
        int Roll5d10()
        {
            Random random = new Random();
            int total = 0;
            for (int die = 0; die < 5; die++)
            {
                total += random.Next(1, 11);
            }
            return total;
        }
    }
}

