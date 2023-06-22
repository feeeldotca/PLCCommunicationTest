using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PLCCommunicationTest
{
    public partial class Form1 : Form
    {
        // Use Modbus Slave to test

        public Form1()
        {
            InitializeComponent();

            // continiounsly get value from registers

            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 502);

            Modbus.Device.ModbusIpMaster master = Modbus.Device.ModbusIpMaster.CreateIp(tcpClient);
            Task.Run(async () =>
            {
                while (true) {
                    await Task.Delay(500);
                    // read slave 1, address 0, 1 byte data a time continously
                    ushort[] values = master.ReadInputRegisters(1, 0, 1); 
                    textBox1.Text = values[0].ToString(); 
                }
            });
            
        }
    }
}
