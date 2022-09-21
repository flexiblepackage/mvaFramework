using System.ComponentModel;
using System.IO.Ports;


namespace MVAFW.TestItemColls.Camera.Utility
{
    public class ESP8266 : BasicTestItem
    {
        public ESP8266()
        {
            COM = "COM4";
            Baud = 115200;
            RelayMode = relaymode.Relay1_Open;
        }

        [Category("Relay Setting")]
        public string COM { get; set; }
        [Category("Relay Setting")]
        public int Baud { get; set; }
        [Category("Relay Setting")]
        public enum relaymode : uint
        {
            Relay1_Close = 0xA00101A2,
            Relay1_Open = 0xA00100A1,
            Relay2_Close = 0xA00201A3,
            Relay2_Open = 0xA00200A2,
        }
        public relaymode RelayMode { get; set; }

        public override void doTest()
        {
            base.doTest();

            byte[] data = { (byte)((uint)RelayMode >> 24), (byte)((uint)RelayMode >> 16 & 0xff), (byte)((uint)RelayMode >> 8 & 0xff), (byte)((uint)RelayMode & 0xff) };

            var sp = new SerialPort(COM, Baud, Parity.None, 8, StopBits.One);
            sp.Handshake = Handshake.None;
            sp.Open();
            sp.Write(data, 0, data.Length);
            sp.Close();

            Values[0] = RelayMode.ToString();
        }
    }
}
