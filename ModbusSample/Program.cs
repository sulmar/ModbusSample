using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ModbusSample
{
    class Program
    {
        static void Main(string[] args)
        {
            CommunicationTest();
        }

        private static void CommunicationTest()
        {
            TcpClient client = new TcpClient("127.0.0.1", 502);
            ModbusIpMaster master = ModbusIpMaster.CreateIp(client);

            // read five input values
            ushort startAddress = 0;
            ushort numInputs = 16;
            bool[] inputs = master.ReadCoils(startAddress, numInputs);


            // Odczyt cyfrowych
            var digitalInputs = master.ReadInputs(startAddress, 16);

            /// Odczyt analogowych
            var analogInputs = master.ReadInputRegisters(0, 16);

            var numbers = new ushort[] { 55, 66 };
            master.WriteMultipleRegisters(0, numbers);


            // Zapis
            master.WriteSingleCoil(18, true);




            // Zapis do Holding Registers
            master.WriteSingleRegister(1, 254);

            var holding = master.ReadHoldingRegisters(0, 2);

            var digitalInputs2 = master.ReadInputs(startAddress, 16);

            for (int i = 0; i < numInputs; i++)
                Console.WriteLine("Input {0}={1}", startAddress + i, inputs[i] ? 1 : 0);
        }
    }
}
