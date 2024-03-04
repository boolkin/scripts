using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

// https://so.parthpatel.net/winforms
namespace SampleApp
{
    public class MainForm : Form
    {
        private TextBox textBox1;
        private Button btnHello;
        private Label label1;

        // The form's constructor: this initializes the form and its controls.
        public MainForm()
        {
            // mainform
            this.Text = "Выбрать файл";
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);

            // texbox
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientSize = new System.Drawing.Size(300, 100);
            this.Controls.Add(this.textBox1);
            this.ResumeLayout(false);
            this.PerformLayout();
            textBox1.Text = ShowNetworkInterfaces();

            // label
            label1 = new System.Windows.Forms.Label();
            label1.Text = "Скопируй IP адрес из поля выше";
            label1.Size = new Size(label1.PreferredWidth, label1.PreferredHeight);
            label1.Location = new Point(80, 30);
            this.Controls.Add(label1);

            // button
            btnHello = new Button();
            btnHello.Location = new Point(100, 70);
            btnHello.Name = "btnHello";
            btnHello.Size = new Size(130, 30);
            btnHello.Text = "Выбрать файл";
            btnHello.Click += new EventHandler(btnHello_Click);
            this.Controls.Add(btnHello);
        }
        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }
        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) Console.WriteLine(file);
        }

        private void btnHello_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                try
                {
                    byte[] byteArr = File.ReadAllBytes(file);
                    CreateListener(byteArr);
                    Console.WriteLine("listener stoppppppppp");
                }
                catch (IOException)
                {
                }
            }
        }
        // This is the main entry point for the application.
        // All C# applications have one and only one of these methods.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }


        public static string ShowNetworkInterfaces()
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            if (nics == null || nics.Length < 1)
            {
                return " No network interfaces found.";
            }
            foreach (NetworkInterface adapter in nics)
            {
                string descr = adapter.Description.ToLower();
                string stat = adapter.OperationalStatus.ToString().ToLower();
                if (descr.Contains("virtual") | descr.Contains("soft") | stat.Contains("down")) continue;
                Console.WriteLine(adapter.Description);
                Console.WriteLine(String.Empty.PadLeft(adapter.Description.Length, '='));

                IPInterfaceProperties properties = adapter.GetIPProperties();
                UnicastIPAddressInformationCollection uniCast = properties.UnicastAddresses;
                if (uniCast != null)
                {
                    foreach (UnicastIPAddressInformation uni in uniCast)
                    {
                        if (uni.Address.AddressFamily != AddressFamily.InterNetwork) continue;
                        string str = uni.Address + ":80";
                        Console.WriteLine(str);
                        return str;
                    }
                }
            }
            return "No connected interfaces";
        }
        public static void CreateListener(byte[] buffer)
        {
            int qq = 0;
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://*:80/");
            listener.Start();

            while (qq < 2)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                Console.WriteLine(DateTime.Now.Ticks.ToString());
                if (request.RawUrl.Contains("favicon")) Console.WriteLine("favic qq {0}", DateTime.Now.Ticks.ToString());
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                Console.WriteLine(buffer.Length);
                //response.Close();
                qq++;
            }
            listener.Stop();
        }
    }
}