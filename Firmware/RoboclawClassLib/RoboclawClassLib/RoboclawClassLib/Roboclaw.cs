using System;
using System.IO;
using System.IO.Ports;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32.SafeHandles;
using USBLib;

namespace RoboclawClassLib
{
    class Commands
    {
        public const int M1FORWARD = 0;
        public const int M1BACKWARD = 1;
        public const int SETMINMB = 2;
        public const int SETMAXMB = 3;
        public const int M2FORWARD = 4;
        public const int M2BACKWARD = 5;
        public const int M17BIT = 6;
        public const int M27BIT = 7;
        public const int MIXEDFORWARD = 8;
        public const int MIXEDBACKWARD = 9;
        public const int MIXEDRIGHT = 10;
        public const int MIXEDLEFT = 11;
        public const int MIXEDFB = 12;
        public const int MIXEDLR = 13;

        public const int GETM1ENC = 16;
        public const int GETM2ENC = 17;
        public const int GETM1SPEED = 18;
        public const int GETM2SPEED = 19;
        public const int RESETENC = 20;
        public const int GETVERSION = 21;
        public const int SETM1ENCCOUNT = 22;
        public const int SETM2ENCCOUNT = 23;
        public const int GETMBATT = 24;
        public const int GETLBATT = 25;
        public const int SETMINLB = 26;
        public const int SETMAXLB = 27;
        public const int SETM1PID = 28;
        public const int SETM2PID = 29;
        public const int GETM1ISPEED = 30;
        public const int GETM2ISPEED = 31;
        public const int M1DUTY = 32;
        public const int M2DUTY = 33;
        public const int MIXEDDUTY = 34;
        public const int M1SPEED = 35;
        public const int M2SPEED = 36;
        public const int MIXEDSPEED = 37;
        public const int M1SPEEDACCEL = 38;
        public const int M2SPEEDACCEL = 39;
        public const int MIXEDSPEEDACCEL = 40;
        public const int M1SPEEDDIST = 41;
        public const int M2SPEEDDIST = 42;
        public const int MIXEDSPEEDDIST = 43;
        public const int M1SPEEDACCELDIST = 44;
        public const int M2SPEEDACCELDIST = 45;
        public const int MIXEDSPEEDACCELDIST = 46;
        public const int GETBUFFERS = 47;
        public const int GETPWMS = 48;
        public const int GETCURRENTS = 49;
        public const int MIXEDSPEED2ACCEL = 50;
        public const int MIXEDSPEED2ACCELDIST = 51;
        public const int M1DUTYACCEL = 52;
        public const int M2DUTYACCEL = 53;
        public const int MIXEDDUTYACCEL = 54;
        public const int READM1PID = 55;
        public const int READM2PID = 56;
        public const int SETMAINVOLTAGES = 57;
        public const int SETLOGICVOLTAGES = 58;
        public const int GETMINMAXMAINVOLTAGES = 59;
        public const int GETMINMAXLOGICVOLTAGES = 60;
        public const int SETM1POSPID = 61;
        public const int SETM2POSPID = 62;
        public const int READM1POSPID = 63;
        public const int READM2POSPID = 64;
        public const int M1SPEEDACCELDECCELPOS = 65;
        public const int M2SPEEDACCELDECCELPOS = 66;
        public const int MIXEDSPEEDACCELDECCELPOS = 67;
        public const int SETM1DEFAULTACCEL = 68;
        public const int SETM2DEFAULTACCEL = 69;
        public const int SETM1DITHER = 70;	//deprecated
        public const int SETM2DITHER = 71;	//deprecated
        public const int GETM1DITHER = 72;	//deprecated
        public const int GETM2DITHER = 73;	//deprecated
        public const int SETPINFUNCTIONS = 74;	//roboclaw only
        public const int GETPINFUNCTIONS = 75;	//roboclaw only
        public const int SETDEADBAND = 76;
        public const int GETDEADBAND = 77;
        public const int GETENCODERS = 78;
        public const int GETISPEEDS = 79;
        public const int RESTOREDEFAULTS = 80;
        public const int GETDEFAULTACCEL = 81;
        public const int GETTEMP = 82;
        public const int GETTEMP2 = 83;

        public const int GETERROR = 90;
        public const int GETENCODERMODE = 91;
        public const int SETM1ENCODERMODE = 92;
        public const int SETM2ENCODERMODE = 93;
        public const int WRITENVM = 94;
        public const int READNVM = 95;

        public const int SETCONFIG = 98;
        public const int GETCONFIG = 99;
        public const int SETCTRLSMODE = 100;
        public const int GETCTRLSMODE = 101;
        public const int SETCTRL1 = 102;
        public const int SETCTRL2 = 103;
        public const int GETCTRLS = 104;
        public const int SETAUTO1 = 105;
        public const int SETAUTO2 = 106;
        public const int GETAUTOS = 107;

        public const int SETM1LR = 128;
        public const int SETM2LR = 129;
        public const int GETM1LR = 130;
        public const int GETM2LR = 131;
        public const int CALIBRATELR = 132;
        public const int SETM1MAXCURRENT = 133;
        public const int SETM2MAXCURRENT = 134;
        public const int GETM1MAXCURRENT = 135;
        public const int GETM2MAXCURRENT = 136;
        public const int SETDOUT = 137;
        public const int GETDOUTS = 138;
        public const int SETPRIORITY = 139;
        public const int GETPRIORITY = 140;
        public const int SETADDRESSMIXED = 141;
        public const int GETADDRESSMIXED = 142;
        public const int SETSIGNAL = 143;
        public const int GETSIGNALS = 144;
        public const int SETSTREAM = 145;
        public const int GETSTREAMS = 146;
        public const int GETSIGNALSDATA = 147;
        public const int SETPWMMODE = 148;
        public const int GETPWMMODE = 149;
        public const int SETNODEID = 150;
        public const int GETNODEID = 151;

        public const int RESETSTOP = 200;
        public const int SETESTOPLOCK = 201;
        public const int GETESTOPLOCK = 202;

        public const int SETSCRIPTAUTORUN = 246;
        public const int GETSCRIPTAUTORUN = 247;
        public const int STARTSCRIPT = 248;
        public const int STOPSCRIPT = 249;

        public const int READEEPROM = 252;
        public const int WRITEEEPROM = 253;
        public const int READSCRIPT = 254;
    }

    public class CanFestival
    {
        public static System.IO.Ports.SerialPort canPort;

        public static string m_canopencomport = "";
        private static bool m_canopenismcp = false;

        private static Object tx_mutex = new Object();
        private static List<UInt16> received = new List<UInt16>();
        private static UInt16 rx_last;

        public static byte canReceive_driver(IntPtr inst, ref UInt16 cob_id, ref byte rtr, ref byte len, ref UInt64 data)
        {
            try
            {
                int count = canPort.BytesToRead;
                for (int i = 0; i < count; i++)
                {
                    UInt16 val = (UInt16)canPort.ReadByte();
                    if (rx_last == 0xFF)
                    {
                        if (val == 0x00)
                            received.Add(0x100);
                        else
                            if (val == 0x01)
                                received.Add(0xFF);
                            else
                            {
                                received = new List<UInt16>();
                                return 1;
                            }
                        rx_last = 0;
                    }
                    else
                    {
                        if (val != 0xFF)
                            received.Add(val);
                        rx_last = val;
                    }
                }
            }
            catch (Exception)
            {
                canDisconnect();
                return 1;
            }

            while (received.Count > 0 && received[0] != 0x100)
                received.RemoveAt(0);
            if (received.Count < 4)
                return 1;

            rtr = (byte)(received[1] >> 6 & 1);
            len = (byte)(received[1] & 0xF);
            cob_id = (ushort)(received[2] << 8 | received[3]);
            if (rtr == 0)
            {
                //NORMAL
                if (received.Count < (4 + len))
                    return 1;

                received.RemoveAt(0);
                received.RemoveAt(0);
                received.RemoveAt(0);
                received.RemoveAt(0);

                data = 0;
                for (int i = 0; i < len; i++)
                {
                    data |= (UInt64)received[0] << (8 * i);
                    received.RemoveAt(0);
                }
            }
            else
            {
                //RTR
                received.RemoveAt(0);
                received.RemoveAt(0);
                received.RemoveAt(0);
                received.RemoveAt(0);
            }

            return 0;
        }

        public static byte canSend_driver(IntPtr inst, UInt16 cob_id, byte rtr, byte len, UInt64 data)
        {
            byte[] packet = new byte[23];
            packet[0] = 0xFF;
            packet[1] = 0x00;
            packet[2] = (byte)((rtr != 0 ? 0x40 : 0x00) | len);
            int index = 3;
            packet[index++] = (byte)(cob_id >> 8);
            if (packet[index - 1] == 0xFF)
                packet[index++] = 0x01;
            packet[index++] = (byte)(cob_id & 0xFF);
            if (packet[index - 1] == 0xFF)
                packet[index++] = 0x01;
            for (int i = 0; i < len; i++)
            {
                packet[index++] = (byte)((data >> (8 * i)) & 0xFF);
                if (packet[index - 1] == 0xFF)
                    packet[index++] = 0x01;
            }

            try
            {
                lock (tx_mutex)
                {
                    canPort.Write(packet, 0, index);
                }
            }
            catch (Exception)
            {
                canDisconnect();
                return 1;
            }

            return 0;
        }

        public static IntPtr canOpen_driver(string busname)
        {
            canPort = new System.IO.Ports.SerialPort(busname, 250000);
            canPort.Open();
            GC.SuppressFinalize(canPort.BaseStream);
            canPort.ReadTimeout = 1;

            return (IntPtr)1;
        }

        public static int canClose_driver(IntPtr inst)
        {
            try
            {
                canPort.Close();
            }
            catch (Exception) { }
            try
            {
                canPort.Dispose();
            }
            catch (Exception) { }
            canPort = null;
            m_canopenismcp = false;
            m_canopencomport = "";
            return 1;
        }

        public static byte canReceive(IntPtr inst, ref UInt16 cob_id, ref byte rtr, ref byte len, ref UInt64 data)
        {
            if (m_canopenismcp)
            {
                return 0;
            }
            else
            {
                return canReceive_driver(inst, ref cob_id, ref rtr, ref len, ref data);
            }
        }

        public static byte canSend(IntPtr inst, UInt16 cob_id, byte rtr, byte len, UInt64 data)
        {
            if (m_canopenismcp)
            {
                return 0;
            }
            else
            {
                return canSend_driver(inst, cob_id, rtr, len, data);
            }
        }
        public static IntPtr canOpen(string busname)
        {
            if (m_canopenismcp)
            {
                return (IntPtr)0;
            }
            else
            {
                return canOpen_driver(busname);
            }
        }
        public static int canClose(IntPtr inst)
        {
            if (m_canopenismcp)
            {
                return 0;
            }
            else
            {
                return canClose_driver(inst);
            }
        }
        public static byte canChangeBaudRate(IntPtr fd, string baud)
        {
            if (m_canopenismcp)
            {
                return 0;
            }
            else
            {
                return 0;   //UVCCM does not support baudrate change
            }
        }

        public delegate byte canReceiveCallback(IntPtr inst, ref UInt16 cob_id, ref byte rtr, ref byte len, ref UInt64 data);
        public delegate byte canSendCallback(IntPtr inst, UInt16 cob_id, byte rtr, byte len, UInt64 data);
        public delegate IntPtr canOpenCallback(string busname);
        public delegate int canCloseCallback(IntPtr inst);
        public delegate byte canChangeBaudRateCallback(IntPtr fd, string baud);

        private static canReceiveCallback m_canReceive;
        private static canSendCallback m_canSend;
        private static canOpenCallback m_canOpen;
        private static canCloseCallback m_canClose;
        private static canChangeBaudRateCallback m_canChangeBaudRate;

        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canSetFuncs(canReceiveCallback canReceive, canSendCallback canSend, canOpenCallback canOpen, canCloseCallback canClose, canChangeBaudRateCallback canChangeBaudRate);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canConnect(String comport);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void canDisconnect();
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void canUpdateNodeList();
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern string canGetNodeName(byte node_id);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canReadString(byte nodeid, UInt16 index, byte subindex, StringBuilder buffer, UInt32 len);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canRead32(byte nodeid, UInt16 index, byte subindex, ref UInt32 value);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canRead16(byte nodeid, UInt16 index, byte subindex, ref UInt16 value);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canRead8(byte nodeid, UInt16 index, byte subindex, ref byte value);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canWriteString(byte nodeid, UInt16 index, byte subindex, string buffer, UInt32 len);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canWrite32(byte nodeid, UInt16 index, byte subindex, UInt32 value);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canWrite16(byte nodeid, UInt16 index, byte subindex, UInt16 value);
        [DllImport("CanFestival-3.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte canWrite8(byte nodeid, UInt16 index, byte subindex, byte value);

        public static bool canEnable(string comport, bool ismcp)
        {
            m_canopenismcp = ismcp;
            m_canopencomport = string.Copy(comport);

            m_canReceive = new canReceiveCallback(canReceive);
            m_canSend = new canSendCallback(canSend);
            m_canOpen = new canOpenCallback(canOpen);
            m_canClose = new canCloseCallback(canClose);
            m_canChangeBaudRate = new canChangeBaudRateCallback(canChangeBaudRate);

            if (canSetFuncs(m_canReceive, m_canSend, m_canOpen, m_canClose, m_canChangeBaudRate) != 0)
            {
                if (canConnect(m_canopencomport) != 0)
                    return true;
            }
            return false;
        }

        public static void FindNodes(ref List<string> values)
        {
            if (m_canopencomport != "")
            {
                if (m_canopenismcp)
                {
                    //MCP CANOpen Master
                }
                else
                {
                    //Gridconnect CANUSB adapter
                    canUpdateNodeList();
                    for (byte node = 0; node < 128; node++)
                    {
                        string name = canGetNodeName(node);
                        if (name != "")
                        {
                            values.Add(name + " (NODE" + node + ")");
                        }
                    }
                }
            }
        }
    }

    public class SerialPort
    {
        const Int64 INVALID_HANDLE_VALUE = -1;

        private static Object log_mutex = new Object();
        public static void Log(string logMessage)
        {
            lock (log_mutex)
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    w.Write("Log Entry : ");
                    w.Write("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    w.WriteLine("  : {0}", logMessage);
                }
            }
        }

        public static Dictionary<string, System.IO.Ports.SerialPort> serialPorts = new Dictionary<string, System.IO.Ports.SerialPort>();
        public static Dictionary<string, int> references = new Dictionary<string, int>();

        private string m_comport;
        private int m_baudrate;
        private bool isopen = false;

        public Object getlock()
        {
            if (serialPorts.ContainsKey(m_comport))
                return serialPorts[m_comport];
            Debug.WriteLine("getlock() Failed");
            return null;
        }

        public SerialPort(string comport, int baudrate)
        {
            m_comport = comport;
            m_baudrate = baudrate;
            if (!serialPorts.ContainsKey(m_comport))
            {
                serialPorts.Add(m_comport, new System.IO.Ports.SerialPort(m_comport, m_baudrate));
                references.Add(m_comport,0);
            }
        }

        ~SerialPort()
        {
            //serialPorts.Remove(m_comport);
            //references.Remove(m_comport);
        }

        private static Object lockdevices = new Object();
        public static List<USBDeviceInfo> GetDevices()
        {
            lock(lockdevices){
			    List<USBDeviceInfo> devices = USB.GetUSBDevices();

			    foreach (USBDeviceInfo device in devices)
			    {
                    if (device.Description == "USB Roboclaw Solo 30A" ||
                        device.Description == "USB Roboclaw Solo 60A" ||
                        device.Description == "USB Roboclaw 2x5A" ||
                        device.Description == "USB Roboclaw 2x7A" ||
                        device.Description == "USB Roboclaw 2x15A" ||
			    	    device.Description == "USB Roboclaw 2x30A" ||
				        device.Description == "USB Roboclaw 2x45A" ||
    				    device.Description == "USB Roboclaw 2x60A" ||
	    			    device.Description == "USB Roboclaw 2x60HV60" ||
		    		    device.Description == "USB Roboclaw 2x60HV80" ||
			    	    device.Description == "USB Roboclaw 2x80HV60" ||
				        device.Description == "USB Roboclaw 2x80HV80" ||
                        device.Description == "USB Roboclaw 2x120HV60" ||
    				    device.Description == "USB Roboclaw 2x120HV80" ||
                        device.Description == "USB Roboclaw 2x160A" ||
                        device.Description == "USB Roboclaw 2x160HV60" ||
		    		    device.Description == "USB Roboclaw 2x160HV80" ||
                        device.Description == "USB Roboclaw 2x200HV60" ||
                        device.Description == "USB Roboclaw 2x200HV80" ||
                        device.Description == "MCP230 2x30A" ||
                        device.Description == "MCP233 2x30A" ||
                        device.Description == "MCP236 2x30A" ||
                        device.Description == "MCP238 2x30A" ||
					    device.Description == "MCP260 2x60A" ||
					    device.Description == "MCP263 2x60A" ||
					    device.Description == "MCP266 2x60A" ||
					    device.Description == "MCP268 2x60A" ||
					    device.Description == "MCP2123 2x120A" ||
					    device.Description == "MCP2126 2x120A" ||
					    device.Description == "MCP2128 2x120A" ||
					    device.Description == "MCP2160 2x160A" ||
					    device.Description == "MCP2163 2x160A" ||
					    device.Description == "MCP2166 2x160A" ||
					    device.Description == "MCP2168 2x160A")
				    {
					    System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"\((COM\d+)\)");  // "..... (COMxxx)" -> COMxxxx
					    if (rgx.Matches(device.Name).Count > 0)
					    {
                            try
                            {
                                string raw = rgx.Match(device.Name).ToString();
                                System.Text.RegularExpressions.Regex rgx2 = new System.Text.RegularExpressions.Regex(@"(COM\d+)");  // "..... (COMxxx)" -> COMxxxx
                                device.DeviceID = rgx2.Match(raw).ToString();
                            }
                            catch (Exception)
                            {
                            }
					    }
				    }
			    }

                return devices;
            }
        }

        public bool IsOpen()
        {
            return isopen;
        }

        public bool IsValidPort()
        {
            var usbDevices = GetDevices();   //Get all USB to Serial adapters
            foreach (var usbDevice in usbDevices)
            {
                if (usbDevice.DeviceID == m_comport)
                    return true;
            }
            return false;
        }
        
        public bool Open()
        {
            if (m_comport.Substring(0, 4) == "NODE")
                return true;

            if (!System.IO.Ports.SerialPort.GetPortNames().Contains(m_comport))
            {
                Debug.WriteLine("Invalid Port");
                return false;
            }
            if (serialPorts.ContainsKey(m_comport))
            {
                lock (getlock())
                {
                    if (!serialPorts[m_comport].IsOpen)
                    {
                        Debug.WriteLine("Opening Port");
                        serialPorts[m_comport].Open();
                        Debug.WriteLine("Port Opened");
                        GC.SuppressFinalize(serialPorts[m_comport].BaseStream);
                        serialPorts[m_comport].RtsEnable = true;
                        //serialPorts[m_comport].WriteTimeout = 5000;
                        serialPorts[m_comport].ReadTimeout = 100;
                        System.Threading.Thread.Sleep(250);
                        serialPorts[m_comport].DiscardInBuffer();
                        serialPorts[m_comport].DiscardOutBuffer();
                    }
                    if (!isopen)
                    {
                        references[m_comport]++;
                    }
                    isopen = true;
                    return true;
                }
            }
            isopen = false;
            return false;
        }

        public void Close()
        {
            if (isopen)
                references[m_comport]--;
            isopen = false;
            if (serialPorts.ContainsKey(m_comport))
            {
                if (references[m_comport] == 0)
                {
                    try
                    {
                        Debug.WriteLine("Closing Port");
                        serialPorts[m_comport].Close();
                        serialPorts[m_comport].Dispose();
                    }
                    catch (Exception)
                    {
                    }
                    Debug.WriteLine("Port Closed");
                }
            }
        }

        public int BytesToRead()
        {
            if (serialPorts.ContainsKey(m_comport))
            {
                return serialPorts[m_comport].BytesToRead;
            }
            return 0;
        }

        public int ReadByte()
        {
            if (serialPorts.ContainsKey(m_comport))
            {
                return serialPorts[m_comport].ReadByte();
            }
            return 0;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            if (serialPorts.ContainsKey(m_comport))
            {
                return serialPorts[m_comport].Read(buffer,offset,count);
            }
            return 0;
        }

        public int Read(char[] buffer, int offset, int count)
        {
            if (serialPorts.ContainsKey(m_comport))
            {
                return serialPorts[m_comport].Read(buffer, offset, count);
            }
            return 0;
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            if (serialPorts.ContainsKey(m_comport))
            {
                serialPorts[m_comport].Write(buffer, offset, count);
            }
        }

        public void DiscardInBuffer()
        {
            if (serialPorts.ContainsKey(m_comport))
            {
                serialPorts[m_comport].DiscardInBuffer();
            }
        }

        public void ReadTimeout(int timeout)
        {
            if (serialPorts.ContainsKey(m_comport))
            {
                serialPorts[m_comport].ReadTimeout = timeout;
            }
        }

        public void RtsEnable(bool state)
        {
            if (serialPorts.ContainsKey(m_comport))
            {
                serialPorts[m_comport].RtsEnable = state;
            }
        }
    }

    public class Roboclaw : SerialPort
    {
        public byte m_address;
        public string m_comport;
        public int m_baudrate;

        private UInt16 m_crc;

        public Roboclaw(string comport, int baudrate, byte address) : base(comport, baudrate)
        {
            m_comport = comport;
            m_baudrate = baudrate;
            m_address = address;
        }

        ~Roboclaw()
        {
            CloseUnit();
        }

        //

        private static Object lockdevices = new Object();
        public static List<USBDeviceInfo> GetGridConnect()
        {
            lock (lockdevices)
            {
                List<USBDeviceInfo> devices = USB.GetFTDIDevices();

                foreach (USBDeviceInfo device in devices)
                {
                    
                    System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex(@"\((COM\d+)\)");  // "..... (COMxxx)" -> COMxxxx
                    if (rgx.Matches(device.Name).Count > 0)
                    {
                        try
                        {
                            string raw = rgx.Match(device.Name).ToString();
                            System.Text.RegularExpressions.Regex rgx2 = new System.Text.RegularExpressions.Regex(@"(COM\d+)");  // "..... (COMxxx)" -> COMxxxx
                            device.DeviceID = rgx2.Match(raw).ToString();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                return devices;
            }
        }

        public static List<string> Find(bool alldevices = true)
        {
            List<string> values = new List<string>();

            //This code will autodetect USB Roboclaw boards
            var usbDevices = GetDevices();   //Get all USB to Serial adapters
            foreach (var usbDevice in usbDevices)
            {
                //Check if this is a USB Roboclaw
                if (usbDevice.DeviceID != "" && usbDevice.DeviceID.Substring(0,3)=="COM")
                {
                    values.Add(usbDevice.Description + " (" + usbDevice.DeviceID + ")");
                }
            }

            if (alldevices)
            {
                CanFestival.FindNodes(ref values);
            }

            return values;
        }

        public bool IsUnitOpen()
        {
            if (!canIsNode(m_address))
            {
                return IsOpen();
            }
            else
            {
                return true;
            }
        }

        public bool OpenUnit()
        {
            if (!canIsNode(m_address))
            {
                if (Open())
                {
                    return true;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public void CloseUnit()
        {
            if (!canIsNode(m_address))
            {
                try
                {
                    lock (getlock())
                    {
                        if (IsOpen())
                            Close();
                    }
                }
                catch (Exception)
                {
                    //Thread.Sleep(10);
                }
            }
            else
            {
                //do nothing to close CAN node
            }
        }

        private void crc_clear()
		{
			m_crc = 0;
		}

		private UInt16 crc_update (byte data)
		{
			int i;
			m_crc = (UInt16)(m_crc ^ ((UInt16)data << 8));
			for (i=0; i<8; i++)
			{
				if ((m_crc & 0x8000)!=0)
					m_crc = (UInt16)((m_crc << 1) ^ 0x1021);
				else
					m_crc <<= 1;
			}
			return m_crc;
		}

		private UInt16 crc_get()
		{
			return m_crc;
		}

        private void DumpGarbage()
        {
            lock (getlock())
            {
                Int32 count = BytesToRead();
                byte[] buffer = new byte[count];
                Read(buffer, 0, count);
            }
        }

        private bool ReadLenCmd(ref byte len, byte address, byte cmd, ref ArrayList args)
        {
            if (!canIsNode(m_address))
            {
                byte[] arr;

                lock (getlock())
                {
                    DumpGarbage();

                    Write(new byte[] { address, cmd }, 0, 2);

                    crc_clear();
                    crc_update(address);
                    crc_update(cmd);
                    try
                    {
                        len = (byte)ReadByte();
                    }
                    catch (TimeoutException)
                    {
                        DiscardInBuffer();
                        return false;
                    }
                    crc_update(len);

                    //calculate number of bytes per group
                    byte groupsize = 0;
                    foreach (Object obj in args)
                    {
                        if (obj.GetType() == typeof(UInt32[]))
                        {
                            groupsize += 4;
                        }
                        else if (obj.GetType() == typeof(Int32[]))
                        {
                            groupsize += 4;
                        }
                        else if (obj.GetType() == typeof(UInt16[]))
                        {
                            groupsize += 2;
                        }
                        else if (obj.GetType() == typeof(Int16[]))
                        {
                            groupsize += 2;
                        }
                        else if (obj.GetType() == typeof(byte[]))
                        {
                            groupsize++;
                        }
                    }

                    arr = new byte[len * groupsize];

                    try
                    {
                        for (uint i = 0; i < len * groupsize; i++)
                        {
                            arr[i] = (byte)ReadByte();
                            crc_update(arr[i]);
                        }
                        UInt16 ccrc = (UInt16)(ReadByte() << 8);
                        ccrc |= (UInt16)ReadByte();
                        if (crc_get() != ccrc)
                            return false;
                    }
                    catch (TimeoutException)
                    {
                        DiscardInBuffer();
                        return false;
                    }
                }

                UInt32 index = 0;
                for (int i = 0; i < len; i++)
                {
                    foreach (Object obj in args)
                    {
                        if (obj.GetType() == typeof(UInt32[]))
                            ((UInt32[])obj)[i] = (UInt32)(arr[index++] << 24 | arr[index++] << 16 | arr[index++] << 8 | arr[index++]);
                        else if (obj.GetType() == typeof(Int32[]))
                            ((Int32[])obj)[i] = (Int32)(arr[index++] << 24 | arr[index++] << 16 | arr[index++] << 8 | arr[index++]);
                        else if (obj.GetType() == typeof(UInt16[]))
                            ((UInt16[])obj)[i] = (UInt16)(arr[index++] << 8 | arr[index++]);
                        else if (obj.GetType() == typeof(Int16[]))
                            ((Int16[])obj)[i] = (Int16)(arr[index++] << 8 | arr[index++]);
                        else if (obj.GetType() == typeof(byte[]))
                            ((byte[])obj)[i] = (byte)(arr[index++]);
                        else
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                //calculate number of bytes per group
                if (CanFestival.canRead8(m_address, (UInt16)(0x2000 + cmd), 1, ref len) == 0)
                    return false;

                for (byte group = 0; group < len; group++)
                {
                    int index = 2;
                    if (!canIsNode(m_address)) throw new Exception();
                    if (CanFestival.canWrite8(m_address,
                                            (UInt16)(0x2000 + cmd),
                                            1,
                                            group)==0)
                        return false;
                    for (int i = 0; i < args.Count; i++)
                    {
                        if (!canIsNode(m_address)) throw new Exception();
                        if (args[i].GetType() == typeof(UInt32[]))
                        {
                            UInt32 value=0;
                            while (CanFestival.canRead32(m_address, (UInt16)(0x2000 + cmd), (byte)index, ref value) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }
                            index++;
                            ((UInt32[])args[i])[group] = (UInt32)value;
                        }
                        else if (args[i].GetType() == typeof(Int32[]))
                        {
                            UInt32 value=0;
                            while (CanFestival.canRead32(m_address, (UInt16)(0x2000 + cmd), (byte)index, ref value) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }
                            index++;
                            ((Int32[])args[i])[group] = (Int32)value;
                        }
                        else if (args[i].GetType() == typeof(UInt16[]))
                        {
                            UInt16 value=0;
                            while (CanFestival.canRead16(m_address, (UInt16)(0x2000 + cmd), (byte)index, ref value) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }
                            index++;
                            ((UInt16[])args[i])[group] = (UInt16)value;
                        }
                        else if (args[i].GetType() == typeof(Int16[]))
                        {
                            UInt16 value=0;
                            while (CanFestival.canRead16(m_address, (UInt16)(0x2000 + cmd), (byte)index, ref value) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }
                            index++;
                            ((Int16[])args[i])[group] = (Int16)value;
                        }
                        else if (args[i].GetType() == typeof(byte[]))
                        {
                            byte value=0;
                            while (CanFestival.canRead8(m_address, (UInt16)(0x2000 + cmd), (byte)index, ref value) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }
                            index++;
                            ((byte[])args[i])[group] = (byte)value;
                        }
                    }
                }

                return true;
            }
        }

        private bool ReadCmd(byte address, byte cmd, ref ArrayList args)
        {
            if (!canIsNode(m_address))
            {
                uint len = 0;
                foreach (Object obj in args)
                {
                    if (obj.GetType() == typeof(UInt32))
                        len += 4;
                    else if (obj.GetType() == typeof(Int32))
                        len += 4;
                    else if (obj.GetType() == typeof(UInt16))
                        len += 2;
                    else if (obj.GetType() == typeof(Int16))
                        len += 2;
                    else if (obj.GetType() == typeof(byte))
                        len++;
                }

                byte[] arr = new byte[len];

                lock (getlock())
                {
                    DumpGarbage();

                    UInt16 ccrc;

                    DiscardInBuffer();
                    crc_clear();
                    crc_update(address);
                    crc_update(cmd);

                    Write(new byte[] { address, cmd }, 0, 2);

                    for (Int32 i = 0; i < len; i++)
                    {
                        try
                        {
                            arr[i] = (byte)ReadByte();
                            crc_update(arr[i]);
                        }
                        catch (TimeoutException)
                        {
                            DiscardInBuffer();
                            //Trace.WriteLine("ReadCmd Read Data Timeout:" + cmd.ToString() + " " + i.ToString());
                            return false;
                        }
                    }

                    try
                    {
                        ccrc = (UInt16)(ReadByte() << 8);
                        ccrc |= (UInt16)ReadByte();
                    }
                    catch (TimeoutException)
                    {
                        DiscardInBuffer();
                        return false;
                    }

                    if (crc_get() != ccrc)
                    {
                        DiscardInBuffer();
                        return false;
                    }
                }

                uint index = 0;
                for (int i = 0; i < args.Count; i++)
                {
                    if (args[i].GetType() == typeof(UInt32))
                        args[i] = (UInt32)(arr[index++] << 24 | arr[index++] << 16 | arr[index++] << 8 | arr[index++]);
                    else if (args[i].GetType() == typeof(Int32))
                        args[i] = (Int32)(arr[index++] << 24 | arr[index++] << 16 | arr[index++] << 8 | arr[index++]);
                    else if (args[i].GetType() == typeof(UInt16))
                        args[i] = (UInt16)(arr[index++] << 8 | arr[index++]);
                    else if (args[i].GetType() == typeof(Int16))
                        args[i] = (Int16)(arr[index++] << 8 | arr[index++]);
                    else if (args[i].GetType() == typeof(byte))
                        args[i] = (byte)(arr[index++]);
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                int index = 0;
                if (args.Count > 1)
                    index++;
                for (int i = 0; i < args.Count; i++)
                {
                    if (!canIsNode(m_address)) throw new Exception();
                    if (args[i].GetType() == typeof(UInt32))
                    {
                        UInt32 value = 0;
                        while (CanFestival.canRead32(m_address, (UInt16)(0x2000 + cmd), (byte)index, ref value) == 0)
                        {
                            if (!canIsNode(m_address)) throw new Exception();
                        }
                        index++;
                        args[i] = (UInt32)value;
                    }
                    else if (args[i].GetType() == typeof(Int32))
                    {
                        UInt32 value = 0;
                        while (CanFestival.canRead32(m_address, (UInt16)(0x2000 + cmd), (byte)index, ref value) == 0)
                        {
                            if (!canIsNode(m_address)) throw new Exception();
                        }
                        index++;
                        args[i] = (Int32)value;
                    }
                    else if (args[i].GetType() == typeof(UInt16))
                    {
                        UInt16 value = 0;
                        while (CanFestival.canRead16(m_address, (UInt16)(0x2000 + cmd), (byte)index, ref value) == 0)
                        {
                            if (!canIsNode(m_address)) throw new Exception();
                        }
                        index++;
                        args[i] = (UInt16)value;
                    }
                    else if (args[i].GetType() == typeof(Int16))
                    {
                        UInt16 value = 0;
                        while (CanFestival.canRead16(m_address, (UInt16)(0x2000 + cmd), (byte)index, ref value) == 0)
                        {
                            if (!canIsNode(m_address)) throw new Exception();
                        }

                        index++;
                        args[i] = (Int16)value;
                    }
                    else if (args[i].GetType() == typeof(byte))
                    {
                        byte value = 0;
                        while (CanFestival.canRead8(m_address,(UInt16)(0x2000 + cmd),(byte)index,ref value) == 0)
                        {
                            if (!canIsNode(m_address)) throw new Exception();
                        }

                        index++;
                        args[i] = (byte)value;
                    }
                }

                return true;
            }
        }

        private bool Write_CRC(byte address, int cmd, params Object[] args)
        {
            if (!canIsNode(m_address))
            {
                int len = 0;
                foreach (Object obj in args)
                {
                    if (obj.GetType() == typeof(UInt32))
                        len += 4;
                    else if (obj.GetType() == typeof(Int32))
                        len += 4;
                    else if (obj.GetType() == typeof(UInt16))
                        len += 2;
                    else if (obj.GetType() == typeof(Int16))
                        len += 2;
                    else if (obj.GetType() == typeof(byte))
                        len++;
                }

                byte[] data = new byte[len + 4];
                uint index = 0;
                data[index++] = address;
                data[index++] = (byte)cmd;
                foreach (Object obj in args)
                {
                    if (obj.GetType() == typeof(UInt32))
                    {
                        data[index++] = (byte)((UInt32)obj >> 24);
                        data[index++] = (byte)((UInt32)obj >> 16);
                        data[index++] = (byte)((UInt32)obj >> 8);
                        data[index++] = (byte)((UInt32)obj);
                    }
                    else if (obj.GetType() == typeof(Int32))
                    {
                        data[index++] = (byte)((Int32)obj >> 24);
                        data[index++] = (byte)((Int32)obj >> 16);
                        data[index++] = (byte)((Int32)obj >> 8);
                        data[index++] = (byte)((Int32)obj);
                    }
                    else if (obj.GetType() == typeof(UInt16))
                    {
                        data[index++] = (byte)((UInt16)obj >> 8);
                        data[index++] = (byte)((UInt16)obj);
                    }
                    else if (obj.GetType() == typeof(Int16))
                    {
                        data[index++] = (byte)((Int16)obj >> 8);
                        data[index++] = (byte)((Int16)obj);
                    }
                    else if (obj.GetType() == typeof(byte))
                        data[index++] = (byte)obj;
                    else
                    {
                        return false;
                    }
                }

                crc_clear();
                for (int i = 0; i < len + 2; i++)
                {
                    crc_update(data[i]);
                }
                UInt16 crc = crc_get();
                data[len + 2] = (byte)(crc >> 8);
                data[len + 3] = (byte)crc;

                lock (getlock())
                {
                    Write(data, 0, len + 4);
                    try
                    {
                        ReadByte();
                    }
                    catch (TimeoutException)
                    {
                        Trace.WriteLine("Write_CRC Timeout:" + data[1].ToString());
                        return false;
                    }
                }
                return true;
            }
            else
            {
                int index = 0;
                if (args.Count() > 1)
                    index++;
                if (args.Count() == 0)
                {
                    while (CanFestival.canWrite8(m_address, (UInt16)(0x2000 + cmd), 0, 0) == 0)
                    {
                        if (!canIsNode(m_address)) throw new Exception();
                    }
                }
                else
                {
                    for (int i = 0; i < args.Count(); i++)
                    {
                        if (!canIsNode(m_address)) throw new Exception();
                        if (args[i].GetType() == typeof(UInt32))
                        {
                            while (CanFestival.canWrite32(m_address, (UInt16)(0x2000 + cmd), (byte)index, (UInt32)args[i]) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }
                            index++;
                        }
                        else if (args[i].GetType() == typeof(Int32))
                        {
                            while (CanFestival.canWrite32(m_address, (UInt16)(0x2000 + cmd), (byte)index, (UInt32)(Int32)args[i]) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }
                            index++;
                        }
                        else if (args[i].GetType() == typeof(UInt16))
                        {
                            while (CanFestival.canWrite16(m_address, (UInt16)(0x2000 + cmd), (byte)index, (UInt16)args[i]) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }
                            index++;
                        }
                        else if (args[i].GetType() == typeof(Int16))
                        {
                            while (CanFestival.canWrite16(m_address, (UInt16)(0x2000 + cmd), (byte)index, (UInt16)(Int16)args[i]) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }

                            index++;
                        }
                        else if (args[i].GetType() == typeof(byte))
                        {
                            while (CanFestival.canWrite8(m_address, (UInt16)(0x2000 + cmd), (byte)index, (byte)args[i]) == 0)
                            {
                                if (!canIsNode(m_address)) throw new Exception();
                            }

                            index++;
                        }
                    }
                }

                return true;
            }
        }

        public bool GetVersion(byte address, ref string version)
        {
            if (!canIsNode(address))
            {
                lock (getlock())
                {
                    DumpGarbage();

                    crc_clear();
                    crc_update(address);
                    crc_update(21);
                    Write(new byte[] { address, Commands.GETVERSION }, 0, 2);
                    try
                    {
                        byte data;
                        do
                        {
                            data = (byte)ReadByte();
                            crc_update(data);
                            if (data != 0)
                                version += Convert.ToChar(data);
                        } while (data != 0);
                        UInt16 ccrc = (UInt16)(ReadByte() << 8);
                        ccrc |= (UInt16)ReadByte();
                        if (crc_get() == ccrc)
                        {
                            Debug.WriteLine("GetVersion Valid: " + version);
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        version = "";
                    }
                }
                return false;
            }
            else
            {
                //CANOpen GetVersion
                StringBuilder data = new StringBuilder(64);
                Exception ex = new Exception();
                if (CanFestival.canReadString(address, 0x2000 + Commands.GETVERSION, 1, data, 64) == 1)
                {
                    version = data.ToString();
                    return true;
                }
                return false;
            }
        }

		public bool ResetEStop()
		{
			return Write_CRC(m_address, Commands.RESETSTOP);
		}

		public bool SetEStopLock(byte state)
		{
			return Write_CRC(m_address, Commands.SETESTOPLOCK, state);
		}

		public bool GetEStopLock(ref byte state)
		{
            ArrayList args = new ArrayList();
            args.Add(state);
            if (ReadCmd(m_address, Commands.GETESTOPLOCK, ref args))
            {
                state = (byte)args[0];
                return true;
            }
            return false;
		}

		public bool SetScriptAuto(UInt32 time)
		{
			return Write_CRC(m_address, Commands.SETSCRIPTAUTORUN, time);
		}

		public bool GetScriptAuto(ref UInt32 time)
		{
            ArrayList args = new ArrayList();
            args.Add(time);
            if (ReadCmd(m_address, Commands.GETSCRIPTAUTORUN, ref args))
            {
                time = (UInt32)args[0];
                return true;
            }
			return false;
		}

		public bool StartScript()
		{
			return Write_CRC(m_address, Commands.STARTSCRIPT);
		}

		public bool StopScript()
		{
			return Write_CRC(m_address, Commands.STOPSCRIPT);
		}

        public bool ST_M1Forward(byte pwr)
        {
			return Write_CRC(m_address, Commands.M1FORWARD, pwr);
        }

        public bool ST_M2Forward(byte pwr)
        {
			return Write_CRC(m_address, Commands.M2FORWARD, pwr);
        }

        public bool ST_M1Backward(byte pwr)
        {
			return Write_CRC(m_address, Commands.M1BACKWARD, pwr);
        }

        public bool ST_M2Backward(byte pwr)
        {
			return Write_CRC(m_address, Commands.M2BACKWARD, pwr);
        }

        public bool ST_M1Drive(byte pwr)
        {
			return Write_CRC(m_address, Commands.M17BIT, pwr);
        }

        public bool ST_M2Drive(byte pwr)
        {
			return Write_CRC(m_address, Commands.M27BIT, pwr);
        }

        public bool ST_MixedForward(byte pwr)
        {
			return Write_CRC(m_address, Commands.MIXEDFORWARD, pwr);
        }

        public bool ST_MixedBackward(byte pwr)
        {
			return Write_CRC(m_address, Commands.MIXEDBACKWARD, pwr);
        }

        public bool ST_MixedLeft(byte pwr)
        {
			return Write_CRC(m_address, Commands.MIXEDLEFT, pwr);
        }

        public bool ST_MixedRight(byte pwr)
        {
			return Write_CRC(m_address, Commands.MIXEDRIGHT, pwr);
        }

        public bool ST_MixedDrive(byte pwr)
        {
			return Write_CRC(m_address, Commands.MIXEDFB, pwr);
        }

        public bool ST_MixedTurn(byte pwr)
        {
			return Write_CRC(m_address, Commands.MIXEDLR, pwr);
        }

        public bool ST_SetMinMainVoltage(byte set)
        {
			return Write_CRC(m_address, Commands.SETMINMB, set);
        }

        public bool ST_SetMaxMainVoltage(byte set)
        {
			return Write_CRC(m_address, Commands.SETMAXMB, set);
        }

        public bool GetM1Encoder(ref Int32 enc,ref byte status)
        {
            ArrayList args = new ArrayList();
            args.Add(enc);
            args.Add(status);
            if (ReadCmd(m_address, Commands.GETM1ENC, ref args))
            {
                enc = (Int32)args[0];
                status = (byte)args[1];
                return true;
            }
            return false;
        }

        public bool GetM2Encoder(ref Int32 enc, ref byte status)
        {
            ArrayList args = new ArrayList();
            args.Add(enc);
            args.Add(status);
            if (ReadCmd(m_address, Commands.GETM2ENC, ref args))
            {
                enc = (Int32)args[0];
                status = (byte)args[1];
                return true;
            }
            return false;
        }

        public bool GetM1Speed(ref Int32 speed,ref byte status)
        {
            ArrayList args = new ArrayList();
            args.Add(speed);
            args.Add(status);
            if (ReadCmd(m_address, Commands.GETM1SPEED, ref args))
            {
                speed = (Int32)args[0];
                status = (byte)args[1];
                return true;
            }
            return false;
        }

        public bool GetM2Speed(ref Int32 speed, ref byte status)
        {
            ArrayList args = new ArrayList();
            args.Add(speed);
            args.Add(status);
            if (ReadCmd(m_address, Commands.GETM2SPEED, ref args))
            {
                speed = (Int32)args[0];
                status = (byte)args[1];
                return true;
            }
            return false;
        }

		public bool GetEncoders(ref Int32 M1cnt, ref Int32 M2cnt)
		{
            ArrayList args = new ArrayList();
            args.Add(M1cnt);
            args.Add(M2cnt);
            if (ReadCmd(m_address, Commands.GETENCODERS, ref args))
            {
                M1cnt = (Int32)args[0];
                M2cnt = (Int32)args[1];
                return true;
            }
            return false;
		}

		public bool GetISpeeds(ref Int32 M1speed, ref Int32 M2speed)
		{
            ArrayList args = new ArrayList();
            args.Add(M1speed);
            args.Add(M2speed);
            if (ReadCmd(m_address, Commands.GETISPEEDS, ref args))
            {
                M1speed = (Int32)args[0];
                M2speed = (Int32)args[1];
                return true;
            }
            return false;
		}

		public bool ResetEncoders()
        {
			return Write_CRC(m_address, Commands.RESETENC);
        }

		public bool SetEncoder1(UInt32 pos)
		{
			return Write_CRC(m_address, Commands.SETM1ENCCOUNT, pos);
		}

		public bool SetEncoder2(UInt32 pos)
		{
			return Write_CRC(m_address, Commands.SETM2ENCCOUNT, pos);
		}

		public bool GetMainVoltage(ref double voltage)
        {
            ArrayList args = new ArrayList();
            args.Add((UInt16)voltage);
            if (ReadCmd(m_address, Commands.GETMBATT, ref args))
            {
                voltage = Convert.ToDouble(args[0])/10.0;
                return true;
            }
            return false;
        }

        public bool GetLogicVoltage(ref double voltage)
        {
            ArrayList args = new ArrayList();
            args.Add((UInt16)voltage);
            if (ReadCmd(m_address, Commands.GETLBATT, ref args))
            {
                voltage = Convert.ToDouble(args[0]) / 10.0;
                return true;
            }
            return false;
        }

        public bool ST_SetMaxLogicVoltage(byte set)
        {
			return Write_CRC(m_address, Commands.SETMAXLB, set);
        }

        public bool ST_SetMinLogicVolage(byte set)
        {
			return Write_CRC(m_address, Commands.SETMINLB, set);
        }

        public bool SetM1VelocityConstants(double P, double I, double D, UInt32 qpps)
        {
            UInt32 p, i, d;
            p = (UInt32)(P * 65536.0);
            i = (UInt32)(I * 65536.0);
            d = (UInt32)(D * 65536.0);

			return Write_CRC(m_address, Commands.SETM1PID, (UInt32)d,
                                          (UInt32)p,
                                          (UInt32)i,
                                          qpps);
        }

        public bool SetM2VelocityConstants(double P, double I, double D, UInt32 qpps)
        {
            UInt32 p, i, d;
            p = (UInt32)(P * 65536.0);
            i = (UInt32)(I * 65536.0);
            d = (UInt32)(D * 65536.0);

            return Write_CRC(m_address, Commands.SETM2PID, (UInt32)d,
                                          (UInt32)p,
                                          (UInt32)i,
                                          qpps);
        }

        public bool GetM1ISpeed(ref Int32 speed, ref byte status)
        {
            ArrayList args = new ArrayList();
            args.Add(speed);
            args.Add(status);
            if (ReadCmd(m_address, Commands.GETM1ISPEED, ref args))
            {
                speed = (Int32)args[0];
                status = (byte)args[1];
                return true;
            }
            return false;
        }

        public bool GetM2ISpeed(ref Int32 speed, ref byte status)
        {
            ArrayList args = new ArrayList();
            args.Add(speed);
            args.Add(status);
            if (ReadCmd(m_address, Commands.GETM2ISPEED, ref args))
            {
                speed = (Int32)args[0];
                status = (byte)args[1];
                return true;
            }
            return false;
        }

        public bool M1Duty(Int16 duty)
        {
			return Write_CRC(m_address, Commands.M1DUTY, duty);
        }

        public bool M2Duty(Int16 duty)
        {
			return Write_CRC(m_address, Commands.M2DUTY, duty);
        }

        public bool MixedDuty(Int16 duty1, Int16 duty2)
        {
			return Write_CRC(m_address, Commands.MIXEDDUTY, duty1, duty2);
        }

        public bool M1Speed(Int32 speed)
        {
			return Write_CRC(m_address, Commands.M1SPEED, speed);
        }

        public bool M2Speed(Int32 speed)
        {
			return Write_CRC(m_address, Commands.M2SPEED, speed);
        }

        public bool MixedSpeed(Int32 speed1, Int32 speed2)
        {
			return Write_CRC(m_address, Commands.MIXEDSPEED, speed1, speed2);
        }

        public bool M1SpeedAccel(UInt32 accel, Int32 speed)
        {
			return Write_CRC(m_address, Commands.M1SPEEDACCEL, accel, speed);
        }

        public bool M2SpeedAccel(UInt32 accel, Int32 speed)
        {
			return Write_CRC(m_address, Commands.M2SPEEDACCEL, accel, speed);
        }

        public bool MixedSpeedAccel(UInt32 accel, Int32 speed1, Int32 speed2)
        {
			return Write_CRC(m_address, Commands.MIXEDSPEEDACCEL, accel, speed1, speed2);
        }

        public bool M1SpeedDistance(Int32 speed, UInt32 distance, byte buffer)
        {
			return Write_CRC(m_address, Commands.M1SPEEDDIST, speed, distance, buffer);
        }

        public bool M2SpeedDistance(Int32 speed, UInt32 distance, byte buffer)
        {
			return Write_CRC(m_address, Commands.M2SPEEDDIST, speed, distance, buffer);
        }

        public bool MixedSpeedDistance(Int32 speed1, UInt32 distance1, Int32 speed2, UInt32 distance2, byte buffer)
        {
			return Write_CRC(m_address, Commands.MIXEDSPEEDDIST, speed1, distance1, speed2, distance2, buffer);
        }

        public bool M1SpeedAccelDistance(Int32 accel, UInt32 speed, UInt32 distance, byte buffer)
        {
			return Write_CRC(m_address, Commands.M1SPEEDACCELDIST, accel, speed, distance, buffer);
        }

        public bool M2SpeedAccelDistance(Int32 accel, UInt32 speed, UInt32 distance, byte buffer)
        {
			return Write_CRC(m_address, Commands.M2SPEEDACCELDIST, accel, speed, distance, buffer);
        }

        public bool MixedSpeedAccelDistance(UInt32 accel, Int32 speed1, UInt32 distance1, Int32 speed2, UInt32 distance2, byte buffer)
        {
			return Write_CRC(m_address, Commands.MIXEDSPEEDACCELDIST, accel, speed1, distance1, speed2, distance2, buffer);
        }

        public bool GetBuffers(ref byte buffer1, ref byte buffer2)
        {
            ArrayList args = new ArrayList();
            args.Add(buffer1);
            args.Add(buffer2);
            if (ReadCmd(m_address, Commands.GETBUFFERS, ref args))
            {
                buffer1 = (byte)args[0];
                buffer2 = (byte)args[1];
                return true;
            }
            return false;
        }

		public bool GetPWMs(ref Int16 PWM1, ref Int16 PWM2)
		{
            ArrayList args = new ArrayList();
            args.Add(PWM1);
            args.Add(PWM2);
            if (ReadCmd(m_address, Commands.GETPWMS, ref args))
            {
                PWM1 = (Int16)args[0];
                PWM2 = (Int16)args[1];
                return true;
            }
            return false;
		}

		public bool GetCurrents(ref Int16 current1, ref Int16 current2)
        {
            ArrayList args = new ArrayList();
            args.Add(current1);
            args.Add(current2);
            if (ReadCmd(m_address, Commands.GETCURRENTS, ref args))
            {
                current1 = (Int16)args[0];
                current2 = (Int16)args[1];
                return true;
            }
            return false;
        }

        public bool MixedSpeedAccel2(UInt32 accel1, Int32 speed1, UInt32 accel2, Int32 speed2)
        {
			return Write_CRC(m_address, Commands.MIXEDSPEED2ACCEL, accel1, speed1, accel2, speed2);
        }

        public bool MixedSpeedAccelDistance2(UInt32 accel1, Int32 speed1, UInt32 distance1, UInt32 accel2, Int32 speed2, UInt32 distance2, byte buffer)
        {
			return Write_CRC(m_address, Commands.MIXEDSPEED2ACCELDIST, accel1, speed1, distance1, accel2, distance2, buffer);
        }

        public bool M1DutyAccel(Int16 duty, UInt32 accel)
        {
			return Write_CRC(m_address, Commands.M1DUTYACCEL, duty, accel);
        }

        public bool M2DutyAccel(Int16 duty, UInt32 accel)
        {
			return Write_CRC(m_address, Commands.M2DUTYACCEL, duty, accel);
        }

        public bool MixedDutyAccel(Int16 duty1, UInt32 accel1, Int16 duty2, UInt32 accel2)
        {
			return Write_CRC(m_address, Commands.MIXEDDUTYACCEL, duty1, accel1, duty2, accel2);
        }

        public bool GetM1VelocityConstants(ref double p, ref double i, ref double d, ref UInt32 qpps)
        {
            ArrayList args = new ArrayList();
            args.Add((UInt32)p);
            args.Add((UInt32)i);
            args.Add((UInt32)d);
            args.Add(qpps);
            if (ReadCmd(m_address, Commands.READM1PID, ref args))
            {
                p = Convert.ToDouble(args[0]) / 65536;
                i = Convert.ToDouble(args[1]) / 65536;
                d = Convert.ToDouble(args[2]) / 65536;
                qpps = (UInt32)args[3];
                return true;
            }
            return false;
        }

        public bool GetM2VelocityConstants(ref double p, ref double i, ref double d, ref UInt32 qpps)
        {
            ArrayList args = new ArrayList();
            args.Add((UInt32)p);
            args.Add((UInt32)i);
            args.Add((UInt32)d);
            args.Add(qpps);
            if (ReadCmd(m_address, Commands.READM2PID, ref args))
            {
                p = Convert.ToDouble(args[0]) / 65536;
                i = Convert.ToDouble(args[1]) / 65536;
                d = Convert.ToDouble(args[2]) / 65536;
                qpps = (UInt32)args[3];
                return true;
            }
            return false;
        }

        public bool SetMainVoltageLimits(double Min, double Max)
        {
            UInt16 min = (UInt16)(Min * 10.0);
            UInt16 max = (UInt16)(Max * 10.0);
			return Write_CRC(m_address, Commands.SETMAINVOLTAGES, min, max);
        }

        public bool SetLogicVoltageLimits(double Min, double Max)
        {
            UInt16 min = (UInt16)(Min * 10.0);
            UInt16 max = (UInt16)(Max * 10.0);
			return Write_CRC(m_address, Commands.SETLOGICVOLTAGES, min, max);
        }

        public bool GetMainVoltageLimits(ref double min, ref double max)
        {
            ArrayList args = new ArrayList();
            args.Add((UInt16)min);
            args.Add((UInt16)max);
            if (ReadCmd(m_address, Commands.GETMINMAXMAINVOLTAGES, ref args))
            {
                min = Convert.ToDouble(args[0]) / 10.0;
                max = Convert.ToDouble(args[1]) / 10.0;
                return true;
            }
            return false;
        }

        public bool GetLogicVoltageLimits(ref double min, ref double max)
        {
            ArrayList args = new ArrayList();
            args.Add((UInt16)min);
            args.Add((UInt16)max);
            if (ReadCmd(m_address, Commands.GETMINMAXLOGICVOLTAGES, ref args))
            {
                min = Convert.ToDouble(args[0]) / 10.0;
                max = Convert.ToDouble(args[1]) / 10.0;
                return true;
            }
            return false;
        }

        public bool SetM1PositionConstants(double P, double I, double D, UInt32 imax, UInt32 deadzone, Int32 minlimit, Int32 maxlimit)
        {
            UInt32 p, i, d;
            p = (UInt32)(P * 1024.0);
            i = (UInt32)(I * 1024.0);
            d = (UInt32)(D * 1024.0);

			return Write_CRC(m_address, Commands.SETM1POSPID, (UInt32)d, (UInt32)p, (UInt32)i, imax, deadzone, minlimit, maxlimit);
        }

        public bool SetM2PositionConstants(double P, double I, double D, UInt32 imax, UInt32 deadzone, Int32 minlimit, Int32 maxlimit)
        {
            UInt32 p, i, d;
            p = (UInt32)(P * 1024.0);
            i = (UInt32)(I * 1024.0);
            d = (UInt32)(D * 1024.0);

            return Write_CRC(m_address, Commands.SETM2POSPID, (UInt32)d, (UInt32)p, (UInt32)i, imax, deadzone, minlimit, maxlimit);
        }

        public bool GetM1PositionConstants(ref double p, ref double i, ref double d, ref UInt32 imax, ref UInt32 deadzone, ref Int32 minlimit, ref Int32 maxlimit)
        {
            ArrayList args = new ArrayList();
            args.Add((UInt32)p);
            args.Add((UInt32)i);
            args.Add((UInt32)d);
            args.Add(imax);
            args.Add(deadzone);
            args.Add(minlimit);
            args.Add(maxlimit);
            if (ReadCmd(m_address, Commands.READM1POSPID, ref args))
            {
                p = Convert.ToDouble(args[0]) / 1024;
                i = Convert.ToDouble(args[1]) / 1024;
                d = Convert.ToDouble(args[2]) / 1024;
                imax = (UInt32)args[3];
                deadzone = (UInt32)args[4];
                minlimit = (Int32)args[5];
                maxlimit = (Int32)args[6];
                return true;
            }
            return false;
        }

        public bool GetM2PositionConstants(ref double p, ref double i, ref double d, ref UInt32 imax, ref UInt32 deadzone, ref Int32 minlimit, ref Int32 maxlimit)
        {
            ArrayList args = new ArrayList();
            args.Add((UInt32)p);
            args.Add((UInt32)i);
            args.Add((UInt32)d);
            args.Add(imax);
            args.Add(deadzone);
            args.Add(minlimit);
            args.Add(maxlimit);
            if (ReadCmd(m_address, Commands.READM2POSPID, ref args))
            {
                p = Convert.ToDouble(args[0]) / 1024;
                i = Convert.ToDouble(args[1]) / 1024;
                d = Convert.ToDouble(args[2]) / 1024;
                imax = (UInt32)args[3];
                deadzone = (UInt32)args[4];
                minlimit = (Int32)args[5];
                maxlimit = (Int32)args[6];
                return true;
            }
            return false;
        }

        public bool M1SpeedAccelDeccelPosition(UInt32 accel, UInt32 speed, UInt32 deccel, Int32 position, byte buffer)
        {
			return Write_CRC(m_address, Commands.M1SPEEDACCELDECCELPOS, accel, speed, deccel, position, buffer);
        }

        public bool M2SpeedAccelDeccelPosition(UInt32 accel, UInt32 speed, UInt32 deccel, Int32 position, byte buffer)
        {
            return Write_CRC(m_address, Commands.M2SPEEDACCELDECCELPOS, accel, speed, deccel, position, buffer);
        }

        public bool MixedSpeedAccelDeccelPosition(UInt32 accel1, UInt32 speed1, UInt32 deccel1, Int32 position1, UInt32 accel2, UInt32 speed2, UInt32 deccel2, Int32 position2, byte buffer)
        {
            return Write_CRC(m_address, Commands.MIXEDSPEEDACCELDECCELPOS, accel1, speed1, deccel1, position1, accel2, speed2, deccel2, position2, buffer);
        }

        public bool SetM1DefaultAccel(UInt32 accel, UInt32 deccel)
        {
            return Write_CRC(m_address, Commands.SETM1DEFAULTACCEL, accel, deccel);
        }

        public bool SetM2DefaultAccel(UInt32 accel, UInt32 deccel)
        {
            return Write_CRC(m_address, Commands.SETM2DEFAULTACCEL, accel, deccel);
        }

        public bool GetDefaultAccel(ref UInt32 accel1, ref UInt32 deccel1, ref UInt32 accel2, ref UInt32 deccel2)
        {
            ArrayList args = new ArrayList();
            args.Add(accel1);
            args.Add(deccel1);
            args.Add(accel2);
            args.Add(deccel2);
            if (ReadCmd(m_address, Commands.GETDEFAULTACCEL, ref args))
            {
                accel1 = (UInt32)args[0];
                deccel1 = (UInt32)args[1];
                accel2 = (UInt32)args[2];
                deccel2 = (UInt32)args[3];
                return true;
            }
            return false;
        }

        public bool SetPinModes(byte s3mode, byte s4mode, byte s5mode)
		{
            return Write_CRC(m_address, Commands.SETPINFUNCTIONS, s3mode, s4mode, s5mode);
		}

        public bool GetPinModes(ref byte s3mode, ref byte s4mode, ref byte s5mode)
		{
            ArrayList args = new ArrayList();
            args.Add(s3mode);
            args.Add(s4mode);
            args.Add(s5mode);
            if (ReadCmd(m_address, Commands.GETPINFUNCTIONS, ref args))
            {
                s3mode = (byte)args[0];
                s4mode = (byte)args[1];
                s5mode = (byte)args[2];
                return true;
            }
            return false;
		}

        public bool SetCtrlsMode(byte ctrl1, byte ctrl2)
        {
            return Write_CRC(m_address, Commands.SETCTRLSMODE, ctrl1, ctrl2);
        }

        public bool GetCtrlsMode(ref byte ctrl1, ref byte ctrl2)
        {
            ArrayList args = new ArrayList();
            args.Add(ctrl1);
            args.Add(ctrl2);
            if (ReadCmd(m_address, Commands.GETCTRLSMODE, ref args))
            {
                ctrl1 = (byte)args[0];
                ctrl2 = (byte)args[1];
                if (ctrl1 > 3)
                    ctrl1 = 0;
                if (ctrl2 > 3)
                    ctrl2 = 0;
                return true;
            }
            return false;
        }

        public bool SetCtrl1(UInt16 ctrl1)
        {
            return Write_CRC(m_address, Commands.SETCTRL1, ctrl1);
        }

        public bool SetCtrl2(UInt16 ctrl2)
        {
            return Write_CRC(m_address, Commands.SETCTRL2, ctrl2);
        }

        public bool GetCtrls(ref UInt16 ctrl1, ref UInt16 ctrl2)
        {
            ArrayList args = new ArrayList();
            args.Add(ctrl1);
            args.Add(ctrl2);
            if (ReadCmd(m_address, Commands.GETCTRLS, ref args))
            {
                ctrl1 = (UInt16)args[0];
                ctrl2 = (UInt16)args[1];
                return true;
            }
            return false;
        }

        public bool SetAuto1(UInt16 autoduty,UInt32 autotimeout)
        {
            return Write_CRC(m_address, Commands.SETAUTO1, autoduty,autotimeout);
        }

        public bool SetAuto2(UInt16 autoduty, UInt32 autotimeout)
        {
            return Write_CRC(m_address, Commands.SETAUTO2, autoduty, autotimeout);
        }

        public bool GetAutos(ref UInt16 autoduty1, ref UInt32 autotimeout1, ref UInt16 autoduty2, ref UInt32 autotimeout2)
        {
            ArrayList args = new ArrayList();
            args.Add(autoduty1);
            args.Add(autotimeout1);
            args.Add(autoduty2);
            args.Add(autotimeout2);
            if (ReadCmd(m_address, Commands.GETAUTOS, ref args))
            {
                autoduty1 = (UInt16)args[0];
                autotimeout1 = (UInt32)args[1];
                autoduty2 = (UInt16)args[2];
                autotimeout2 = (UInt32)args[3];
                return true;
            }
            return false;
        }

        public bool SetDeadBand(byte min, byte max)
		{
			return Write_CRC(m_address, Commands.SETDEADBAND, min, max);
		}

		public bool GetDeadBand(ref byte min, ref byte max)
		{
            ArrayList args = new ArrayList();
            args.Add(min);
            args.Add(max);
            if (ReadCmd(m_address, Commands.GETDEADBAND, ref args))
            {
                min = (byte)args[0];
                max = (byte)args[1];
                if (min > 250)
                    min = 250;
                if (max > 250)
                    max = 250;
                return true;
            }
            return false;
		}

		public bool Defaults()
		{
			return Write_CRC(m_address, Commands.RESTOREDEFAULTS);
		}

		public bool GetTemperature(ref double temperature)
        {
            ArrayList args = new ArrayList();
            args.Add((UInt16)temperature);
            if (ReadCmd(m_address, Commands.GETTEMP, ref args))
            {
                temperature = Convert.ToDouble(args[0])/10.0;
                return true;
            }
            return false;
        }

		public bool GetTemperature2(ref double temperature2)
		{
            ArrayList args = new ArrayList();
            args.Add((UInt16)temperature2);
            if (ReadCmd(m_address, Commands.GETTEMP2, ref args))
            {
                temperature2 = Convert.ToDouble(args[0]) / 10.0;
                return true;
            }
            return false;
		}

		public bool GetStatus(ref UInt16 status)
        {
            ArrayList args = new ArrayList();
            args.Add(status);
            if (ReadCmd(m_address, Commands.GETERROR, ref args))
            {
                status = (UInt16)args[0];
                return true;
            }
            return false;
        }

        public bool GetEncoderModes(ref byte m1mode, ref byte m2mode)
        {
            ArrayList args = new ArrayList();
            args.Add(m1mode);
            args.Add(m2mode);
            if (ReadCmd(m_address, Commands.GETENCODERMODE, ref args))
            {
                m1mode = (byte)args[0];
                m2mode = (byte)args[1];
                return true;
            }
            return false;
        }

        public bool SetEncoder1Mode(byte mode)
        {
			return Write_CRC(m_address, Commands.SETM1ENCODERMODE, mode);
        }

        public bool SetEncoder2Mode(byte mode)
        {
			return Write_CRC(m_address, Commands.SETM2ENCODERMODE, mode);
        }

		public bool WriteNVM()
        {
            return Write_CRC(m_address, Commands.WRITENVM, 0xE22EAB7A);
        }

		public bool ReadNVM()
		{
			return Write_CRC(m_address, Commands.READNVM);
		}

		public bool SetConfig(UInt16 config)
        {
            return Write_CRC(m_address, Commands.SETCONFIG, config);
        }

        public bool GetConfig(ref UInt16 config)
        {
            ArrayList args = new ArrayList();
            args.Add(config);
            if (ReadCmd(m_address, Commands.GETCONFIG, ref args))
            {
                config = (UInt16)args[0];
                return true;
            }
            return false;
        }

		public bool SetM1LR(double L, double R)
		{
			UInt32 l,r;
			l = (UInt32)(L * 0x1000000);
			r = (UInt32)(R * 0x1000000);

			return Write_CRC(m_address, Commands.SETM1LR, l, r);
		}

		public bool SetM2LR(double L, double R)
		{
			UInt32 l, r;
			l = (UInt32)(L * 0x1000000);
			r = (UInt32)(R * 0x1000000);

            return Write_CRC(m_address, Commands.SETM2LR, l, r);
        }

		public bool GetM1LR(ref double l, ref double r)
		{
            ArrayList args = new ArrayList();
            args.Add((UInt32)l);
            args.Add((UInt32)r);
            if (ReadCmd(m_address, Commands.GETM1LR, ref args))
            {
                l = Convert.ToDouble(args[0]) / 0x1000000;
                r = Convert.ToDouble(args[1]) / 0x1000000;
                return true;
            }
            return false;
		}

		public bool GetM2LR(ref double l, ref double r)
		{
            ArrayList args = new ArrayList();
            args.Add((UInt32)l);
            args.Add((UInt32)r);
            if (ReadCmd(m_address, Commands.GETM2LR, ref args))
            {
                l = Convert.ToDouble(args[0]) / 0x1000000;
                r = Convert.ToDouble(args[1]) / 0x1000000;
                return true;
            }
            return false;
		}

		public bool CalibrateLR()
		{
			return Write_CRC(m_address, Commands.CALIBRATELR);
		}

		public bool SetM1Current(double fmin, double fmax)
		{
			UInt32 min,max;
			min = (UInt32)(fmin * 100);
			max = (UInt32)(fmax * 100);

			return Write_CRC(m_address, Commands.SETM1MAXCURRENT, max, min);
		}

		public bool SetM2Current(double fmin, double fmax)
		{
			UInt32 min, max;
			min = (UInt32)(fmin * 100);
			max = (UInt32)(fmax * 100);

            return Write_CRC(m_address, Commands.SETM2MAXCURRENT, max, min);
        }

		public bool GetM1Current(ref double min, ref double max)
		{
            ArrayList args = new ArrayList();
            args.Add((Int32)min);
            args.Add((Int32)max);
            if (ReadCmd(m_address, Commands.GETM1MAXCURRENT, ref args))
            {
                max = Convert.ToDouble(args[0]) / 100;
                min = Convert.ToDouble(args[1]) / 100;
                return true;
            }
            return false;
		}

		public bool GetM2Current(ref double min, ref double max)
		{
            ArrayList args = new ArrayList();
            args.Add((Int32)min);
            args.Add((Int32)max);
            if (ReadCmd(m_address, Commands.GETM2MAXCURRENT, ref args))
            {
                max = Convert.ToDouble(args[0]) / 100;
                min = Convert.ToDouble(args[1]) / 100;
                return true;
            }
            return false;
		}

		public bool SetDOUT(byte i, byte action)
		{
			return Write_CRC(m_address, Commands.SETDOUT, i, action);
		}

		public bool GetDOUT(ref byte len, ref byte[] actions)
		{
            ArrayList args = new ArrayList();
            args.Add(actions);
            return ReadLenCmd(ref len, m_address, Commands.GETDOUTS, ref args);
        }

		public bool SetPriorityLevels(byte lvl1,byte lvl2,byte lvl3)
		{
			return Write_CRC(m_address, Commands.SETPRIORITY, lvl1, lvl2, lvl3);
		}

		public bool GetPriorityLevels(ref byte lvl1, ref byte lvl2, ref byte lvl3)
		{
            ArrayList args = new ArrayList();
            args.Add(lvl1);
            args.Add(lvl2);
            args.Add(lvl3);
            if (ReadCmd(m_address, Commands.GETPRIORITY, ref args))
            {
                lvl1 = (byte)args[0];
                lvl2 = (byte)args[1];
                lvl3 = (byte)args[2];
                if (lvl1 > 3)
                    lvl1 = 0;
                if (lvl2 > 3)
                    lvl2 = 0;
                if (lvl3 > 3)
                    lvl3 = 0;
                return true;
            }
            return false;
		}

		public bool SetMixed(byte address, byte mixed)
		{
    	    return Write_CRC(m_address, Commands.SETADDRESSMIXED, address, mixed);
		}

		public bool GetMixed(ref byte address, ref byte mixed)
		{
            ArrayList args = new ArrayList();
            args.Add(address);
            args.Add(mixed);
            if (ReadCmd(m_address, Commands.GETADDRESSMIXED, ref args))
            {
                address = (byte)args[0];
                mixed = (byte)args[1];
                return true;
            }
            return false;
		}

        public bool SetSignal(byte i, byte type, byte mode, byte target, UInt16 minaction, UInt16 maxaction, byte lowpass,
							  UInt32 timeout, Int32 loadhome, Int32 min, Int32 max, Int32 center,
							  UInt32 deadband, Int32 powerexp, Int32 minout, Int32 maxout, UInt32 powermin)
		{
			return Write_CRC(m_address, Commands.SETSIGNAL, i, type, mode, target, minaction, maxaction, lowpass,
										  timeout, loadhome, min, max, center, deadband, powerexp, minout, maxout, powermin);
		}

		public bool GetSignals(ref byte len,
							   ref byte[] types, ref byte[] modes, ref byte[] targets, ref UInt16[] minactions, ref UInt16[] maxactions, ref byte[] lowpass,
							   ref UInt32[] timeouts, ref Int32[] loadhomes, ref Int32[] mins, ref Int32[] maxs, ref Int32[] centers,
							   ref UInt32[] deadbands, ref Int32[] powerexps, ref Int32[] minouts, ref Int32[] maxouts, ref UInt32[] powermins)
		{
            ArrayList args = new ArrayList();
            args.Add(types);
            args.Add(modes);
            args.Add(targets);
            args.Add(minactions);
            args.Add(maxactions);
            args.Add(lowpass);
            args.Add(timeouts);
            args.Add(loadhomes);
            args.Add(mins);
            args.Add(maxs);
            args.Add(centers);
            args.Add(deadbands);
            args.Add(powerexps);
            args.Add(minouts);
            args.Add(maxouts);
            args.Add(powermins);
            return ReadLenCmd(ref len, m_address, Commands.GETSIGNALS, ref args);
        }

		public bool SetStream(byte i, byte type, UInt32 rate, UInt32 timeout)
		{
			return Write_CRC(m_address, Commands.SETSTREAM, i, type, rate, timeout);
		}

		public bool GetStreams(ref byte len, ref byte[] types, ref UInt32[] rates, ref UInt32[] timeouts)
		{
            ArrayList args = new ArrayList();
            args.Add(types);
            args.Add(rates);
            args.Add(timeouts);
            return ReadLenCmd(ref len, m_address, Commands.GETSTREAMS, ref args);
        }

		public bool GetSignalValues(ref byte len,
							        ref Int32[] commands,
                                    ref Int32[] positions,
                                    ref Int32[] percents,
                                    ref Int32[] speeds,
                                    ref Int32[] speedss)
		{
            ArrayList args = new ArrayList();
            args.Add(commands);
            args.Add(positions);
            args.Add(percents);
            args.Add(speeds);
            args.Add(speedss);
            return ReadLenCmd(ref len, m_address, Commands.GETSIGNALSDATA, ref args);
		}

		public bool SetMode(byte mode)
		{
			return Write_CRC(m_address, Commands.SETPWMMODE, mode);
		}

		public bool GetMode(ref byte mode)
		{
            ArrayList args = new ArrayList();
            args.Add(mode);
            if (ReadCmd(m_address, Commands.GETPWMMODE, ref args))
            {
                mode = (byte)(Convert.ToInt32(args[0]) & 0x81);
                return true;
            }
            return false;
		}

        public bool SetNodeID(byte nodeId)
        {
            return Write_CRC(m_address, Commands.SETNODEID, nodeId);
        }

        public bool GetNodeID(ref byte node)
        {
            ArrayList args = new ArrayList();
            args.Add(node);
            if (ReadCmd(m_address, Commands.GETNODEID, ref args))
            {
                node = (byte)args[0];
                return true;
            }
            return false;
        }

        public static void canDisable()
        {
            try{
                CanFestival.canDisconnect();
            }
            catch (Exception Ex)
            {
                Log(Ex.ToString());
            }
        }

        public static bool canIsConnected()
        {
            try
            {
                if (CanFestival.m_canopencomport != "")
                    return true;
            }
            catch (Exception Ex)
            {
                Log(Ex.ToString());
            }
            return false;
        }

        public static bool canIsNode(byte node_id)
        {
            try
            {
                return CanFestival.canGetNodeName(node_id) != "";
            }
            catch (Exception Ex)
            {
                Log(Ex.ToString());
            }
            return false;
        }

        public static bool canEnable(string comport, bool ismcp)
        {
            try
            {
                return CanFestival.canEnable(comport, ismcp);
            }
            catch (Exception Ex)
            {
                Log(Ex.ToString());
            }
            return false;
        }

    }
}
