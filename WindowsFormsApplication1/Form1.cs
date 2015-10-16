using System;
using System.Windows.Forms;
using RegawMOD.Android;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        AndroidController android;
        Device device;
        string serial;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            android = AndroidController.Instance;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //label1.Text = "clicked";
          

            //Always call UpdateDeviceList() before using AndroidController on devices to get the most updated list
            android.UpdateDeviceList();

            if (android.HasConnectedDevices)
            {
                serial = android.ConnectedDevices[0];
                device = android.GetConnectedDevice(serial);
                //this.textBox2.Text = device.SerialNumber;
                //AdbCommand adbCmd = Adb.FormAdbShellCommand(this.device, false, "dumpsys", "battery");
                
                AdbCommand adbCmd = Adb.FormAdbCommand(textBoxCommand.Text);
                string result = Adb.ExecuteAdbCommand(adbCmd);
                textBoxOutput.AppendText(result+"\r\n");
            }
            else
            {
                this.textBoxOutput.Text = "Error - No Devices Connected";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Apk Files (.apk)|*.apk|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Assign the cursor in the Stream to the Form's Cursor property.
                textBoxApk.Text = openFileDialog1.FileName;
            }
        }



        private void buttonInstall_Click(object sender, EventArgs e)
        {
            serial = android.ConnectedDevices[0];
            device = android.GetConnectedDevice(serial);
            string apkFileName = textBoxApk.Text;
            AdbCommand adbCmd = Adb.FormAdbCommand("install " + apkFileName);
            string result = Adb.ExecuteAdbCommand(adbCmd);
            textBoxOutput.AppendText(result + "\r\n");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBoxOutput.Clear();
        }
    }
}
