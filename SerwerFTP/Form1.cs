using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;

namespace SerwerFTP
{
    public partial class Form1 : Form
    {

        private TcpListener _listener;

        public Form1()
        {
            InitializeComponent();
            Start();
        }

        public void Start()
        {

            _listener = new TcpListener(IPAddress.Any, 21);
            _listener.Start();
            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);

        }

        public void Stop()
        {

            if (_listener != null)
            {
                _listener.Stop();
            }

        }


        private void HandleAcceptTcpClient(IAsyncResult result)
        {
            TcpClient client = _listener.EndAcceptTcpClient(result);
            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);

            NetworkStream stream = client.GetStream();

            using (StreamWriter writer = new StreamWriter(stream, Encoding.ASCII))
            using (StreamReader reader = new StreamReader(stream, Encoding.ASCII))
            {
                writer.WriteLine("Polaczono");
                writer.Flush();
                writer.WriteLine("Bede powtarzal za Toba. Wyslij pusta linie aby zakonczyc");
                writer.Flush();

                String line = null;

                while (!string.IsNullOrEmpty(line = reader.ReadLine()))
                {
                    writer.WriteLine("Echoing back: {0}", line);
                    writer.Flush();
                }

            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
