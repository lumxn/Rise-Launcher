using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;

namespace RiseLauncher
{
    public partial class Form1 : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,
          int nTopRect,
          int nRightRect,
          int nBottomRect,
          int nWidthEllipse,
          int nHeightEllipse
      );

        public Point mouseLocation;
        private string selectedFilePath;
        private const string UsernameSettingKey = "Username";
        public Form1()
        {
            InitializeComponent();
            UpdateVersionFromURL();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 80, 80));
            // TRANSPARENT
            panel1.BackColor = Color.FromArgb(100, 0, 0, 0);
            panel2.BackColor = Color.FromArgb(100, 0, 0, 0);
            // ROUNDING
            panel1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel1.Width, panel1.Height, 20, 20));
            panel2.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, panel2.Width, panel2.Height, 20, 20));
            button1.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button1.Width, button1.Height, 20, 20));

            if (Properties.Settings.Default.LastSelectedFilePath != null)
            {
                selectedFilePath = Properties.Settings.Default.LastSelectedFilePath;
            }

        }

        private void UpdateVersionFromURL()
        {
            try
            {
                string versionUrl = "https://raw.githubusercontent.com/lumxn/riselaunchervalues/main/version";

                string cacheBustingUrl = versionUrl + "?timestamp=" + DateTime.Now.Ticks;

                using (WebClient webClient = new WebClient())
                {
                    string versionText = webClient.DownloadString(cacheBustingUrl);
                    label5.Text = $"{versionText.Trim()}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching version information: " + ex.Message);
            }
        }

        private void UpdateChangelogFromURL()
        {
            try
            {
                string changelogUrl = "https://raw.githubusercontent.com/lumxn/riselaunchervalues/main/changelog";

                string cacheBustingUrl = changelogUrl + "?timestamp=" + DateTime.Now.Ticks;
                using (WebClient webClient = new WebClient())
                {
                    string changelogText = webClient.DownloadString(cacheBustingUrl).Trim();
                    label6.Text = changelogText;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching changelog: " + ex.Message);
            }
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateVersionFromURL();
            UpdateChangelogFromURL();
            string savedUsername = Properties.Settings.Default[UsernameSettingKey]?.ToString();
            if (!string.IsNullOrEmpty(savedUsername))
            {
                label3.Text = $"Welcome back, {savedUsername}!";
            }

        }

        private void mouse_Down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(e.X, e.Y);
        }

        private void mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePoint = Control.MousePosition;
                mousePoint.Offset(-mouseLocation.X, -mouseLocation.Y);
                Location = mousePoint;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;

                Properties.Settings.Default.LastSelectedFilePath = selectedFilePath;
                Properties.Settings.Default.Save();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                if (System.IO.File.Exists(selectedFilePath))
                {
                    try
                    {
                        string workingDirectory = System.IO.Path.GetDirectoryName(selectedFilePath);

                        Process.Start(new ProcessStartInfo
                        {
                            FileName = selectedFilePath,
                            WorkingDirectory = workingDirectory
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("The specified file does not exist.");
                }
            }
            else
            {
                MessageBox.Show("Please select a file first.");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            try
            {
                string githubUrl = "https://raw.githubusercontent.com/lumxn/riselaunchervalues/main/beta";

                string cacheBustingUrl = githubUrl + "?timestamp=" + DateTime.Now.Ticks;

                using (WebClient webClient = new WebClient())
                {
                    string workuploadLink = webClient.DownloadString(cacheBustingUrl).Trim();

                    Process.Start(new ProcessStartInfo(workuploadLink) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening download link: " + ex.Message);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                string username = Microsoft.VisualBasic.Interaction.InputBox("Enter your username:", "Username Prompt", "");

                label3.Text = $"Welcome back, {username}!";

                Properties.Settings.Default[UsernameSettingKey] = username;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating username: " + ex.Message);
            }
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Hide();
        }
    }
}
