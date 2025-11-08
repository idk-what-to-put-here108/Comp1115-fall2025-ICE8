using System;

namespace ICE8
{

    enum Career
    {
        Army,
        Psion,
        Rogue,
        Telepath
    }
    public partial class SelectionForm : Form
    {
        string[] Careers = Enum.GetNames<Career>();
        int[][] CareerStats =
        [
            [35, 35, 30, 30, 25, 25], // Army
            [30, 35, 30, 25, 35, 25], // Psion
            [35, 30, 30, 35, 25, 25], // Rogue
            [25, 30, 30, 35, 25, 35]  // Telepath
        ];
        TextBox[] PrimaryStatTextBoxes;
        TextBox[] SecondaryStatTextBoxes;

        public SelectionForm()
        {
            InitializeComponent();

            comboBox_career.Items.Clear();
            comboBox_career.Items.AddRange(Careers);

            // Initialize the PrimaryStatTextBoxes array
            PrimaryStatTextBoxes =
            [
                TextBox_AGL,
                TextBox_STR,
                TextBox_VGR,
                TextBox_PER,
                TextBox_INT,
                TextBox_WIL
            ];

            // Initialize the SecondaryStatTextBoxes array
            SecondaryStatTextBoxes =
            [
                TextBox_AWA,
                TextBox_TOU,
                TextBox_RES
            ];
        }

        private void Button_Generate_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Random generation is destructive, Are you sure?", "Confirm Random", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                foreach (var stat in PrimaryStatTextBoxes)
                {
                    stat.Text = Roll6d10DropLowest().ToString();
                }

            }

            ComputeSecondaryAttributes();
        }
        private void ComputeSecondaryAttributes()
        {
            TextBox_AWA.Text = (Convert.ToInt32(TextBox_AGL.Text) + Convert.ToInt32(TextBox_PER.Text)).ToString();
            TextBox_TOU.Text = (Convert.ToInt32(TextBox_STR.Text) + Convert.ToInt32(TextBox_VGR.Text)).ToString();
            TextBox_RES.Text = (Convert.ToInt32(TextBox_INT.Text) + Convert.ToInt32(TextBox_WIL.Text)).ToString();
        }


        /// <summary>
        /// This method rolls 5d10 and returns the total
        /// </summary>
        /// <returns>An integer value between 5 and 50</returns>


        int Roll6d10DropLowest()
        {
            Random random = new Random();
            int[] rolls = new int[6];
            for (int die = 0; die < 6; die++)
            {
                rolls[die] = random.Next(1, 11);
            }
            Array.Sort(rolls);
            int total = 0;
            for (int die = 1; die < 6; die++)
            {
                total += rolls[die];
            }
            return total;
        }
        private void comboBox_career_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_career.SelectedIndex < 0) { return; }
            for (int attribute = 0; attribute < PrimaryStatTextBoxes.Length; attribute++)
            {
                PrimaryStatTextBoxes[attribute].Text = CareerStats[comboBox_career.SelectedIndex][attribute].ToString();
            }
            ComputeSecondaryAttributes();
        }

        private void TextBox_AWA_TextChanged(object sender, EventArgs e)
        {

        }

        private void Reset_Button_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Confirm Reset", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                comboBox_career.SelectedIndex = -1;
                foreach (var stat in PrimaryStatTextBoxes)
                {
                    stat.Text = string.Empty;
                }

                foreach (var stat in SecondaryStatTextBoxes)
                {
                    stat.Text = string.Empty;
                }
            }
        }

        private void ShowRandomButton_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowRandomButton.Checked)
            {
                Button_Random.Show();
            }
            else
            {
                Button_Random.Hide();
            }
        }

        private void Submit_Button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

