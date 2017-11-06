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
            _listener.BeginAcceptTcpClient(HandleAcceptTcpClient, _listener);
            TcpClient client = _listener.EndAcceptTcpClient(result);

            ClientConnection connection = new ClientConnection(client);

            ThreadPool.QueueUserWorkItem(connection.HandleClient, client);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
