using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanSetupSoftware
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Chrome_btn_Click(object sender, EventArgs e)
        {
            InstallHWMonitor();
        }


        private void InstallHWMonitor()
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process process = new Process
                {
                    StartInfo = processStartInfo
                };

                process.Start();

                // Execute winget command to install HWMonitor using PowerShell
                process.StandardInput.WriteLine("winget install --id CPUID.HWMonitor --accept-source-agreements");
                process.StandardInput.WriteLine("Y");
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    MessageBox.Show("HWMonitor has been installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("HWMonitor is already installed.", "Installation Skipped", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during installation: " + ex.Message, "Installation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process process = new Process
                {
                    StartInfo = processStartInfo
                };
                string a;
                a = textBox1.Text;
                process.Start();
                process.StandardInput.WriteLine("winget search "+a);
                process.StandardInput.WriteLine("Y");
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                // Split the output into individual lines
                string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                // Clear existing rows from the DataGridView
                dataGridView1.Rows.Clear();

                // Parse each line and add it as a row to the DataGridView
                foreach (string line in lines)
                {
                    string[] values = line.Split('\t');
                    dataGridView1.Rows.Add(values);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred during installation: " + ex.Message, "Installation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}


