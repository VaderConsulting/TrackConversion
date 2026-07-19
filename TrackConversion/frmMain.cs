namespace TrackConversion
{
    public partial class frmMain : Form
    {
        string LastFolder = "";

        public frmMain()
        {
            InitializeComponent();
        }

        private void BtnAddInputFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgBrowse = new OpenFileDialog();

            if (LastFolder.Length > 0)
            {
                dlgBrowse.InitialDirectory = LastFolder;
            }
            else
            {
                dlgBrowse.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            dlgBrowse.DefaultExt = ".csv";
            dlgBrowse.Multiselect = true;
            dlgBrowse.Filter = "CSV Files (*.csv)|*.csv";

            DialogResult unused1 = dlgBrowse.ShowDialog();

            if (dlgBrowse.FileNames.Length > 0 && dlgBrowse.FileNames[0] != null && dlgBrowse.FileNames[0].Length > 0)
            {
                LastFolder = Path.GetDirectoryName(dlgBrowse.FileNames[0]) + "";

                foreach (string Filename in dlgBrowse.FileNames)
                {
                    if (Filename.Length > 0 && !lstInputFiles.Items.Contains(Filename))
                    {
                        int unused = lstInputFiles.Items.Add(Filename);
                    }
                }
            }

            btnConvert.Enabled = lstInputFiles.Items.Count > 0;
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            // https://www8.garmin.com/xmlschemas/GpxExtensionsv3.xsd
            string[] Colours = { "Black", "DarkRed", "DarkGreen", "DarkYellow", "DarkBlue", "DarkMagenta", "DarkCyan", "LightGray", "Red", "Green", "Yellow", "Blue", "Magenta", "Cyan", "White" };
            int ColourIndex = 0;

            try
            {
                foreach (string Filename in lstInputFiles.Items)
                {
                    string Subject = Path.GetFileName(Path.GetFileNameWithoutExtension(Filename));
                    string OutputFilename = Path.Combine(Path.GetDirectoryName(Filename) + "", Path.GetFileNameWithoutExtension(Filename)) + ".gpx";

                    string Result = TracPlus_RockAIR.ToGPX(Filename, chkReverse.Checked, Colours[ColourIndex], chkZeroInvalidData.Checked);

                    using (StreamWriter writer = File.CreateText(OutputFilename))
                    {
                        writer.Write(Result.ToString());
                    }

                    ColourIndex++;
                    if (ColourIndex > Colours.GetUpperBound(0))
                    {
                        ColourIndex = 0;
                    }
                }

                DialogResult unused1 = MessageBox.Show("Conversion complete.\r\n\r\nCheck input folder(s) for converted files.", "Progress", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                DialogResult unused = MessageBox.Show($"An error occurred during the conversion:\r\n\r\n{ex}\r\n\r\nCheck file and folder permissions.", "Progress", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRemoveInputFile_Click(object sender, EventArgs e)
        {
            for (int Index = 0; Index < lstInputFiles.SelectedItems.Count; Index++)
            {
                lstInputFiles.Items.Remove(lstInputFiles.SelectedItems[Index]);

                Index--;
            }

            btnConvert.Enabled = lstInputFiles.Items.Count > 0;
        }

        private void LstInputFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveInputFile.Enabled = lstInputFiles.SelectedItems.Count > 0;
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {

        }
    }
}
