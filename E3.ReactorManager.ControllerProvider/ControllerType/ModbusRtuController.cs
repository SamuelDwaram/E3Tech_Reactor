using E3.ReactorManager.ControllerProvider.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace E3.ReactorManager.ControllerProvider.ControllerType
{
    public class ModbusRtuController : IController
    {
        public ModbusRtuController(IUnityContainer containerProvider)
        {
            
        }

        public void Connect()
        {
            
        }

        public void Disconnect()
        {
            
        }

        public bool IsConnected()
        {
            throw new NotImplementedException();
        }

        public bool Write(string writeMemoryInfo, object writeData)
        {
            string[] writeMemoryInfoArray = writeMemoryInfo.Split('|');

            int startAddress = int.Parse(writeMemoryInfoArray[0].Substring(1));
            int registersCountToBeWritten = int.Parse(writeMemoryInfoArray[1]);

            //Prepare Write Data
            IList<object> writeDataAvailable = writeData as IList<object>;
            short[] values = writeDataAvailable.Select(item => Convert.ToInt16(item)).ToArray();

            bool writeStatus = false;
            try
            {
                if (BaseModbusAdapter.Instance.Open(PortNumber, 9600, 8, Parity.None, StopBits.One))
                {
                    switch (writeMemoryInfoArray[0].Substring(0, 1))
                    {
                        case "4":   //Write Holding Registers
                            writeStatus = BaseModbusAdapter.Instance.SendFc16(Convert.ToByte(Address), (ushort)startAddress, (ushort)registersCountToBeWritten, values, ResponseTime);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show(ioEx.Message, "COM port Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UnauthorizedAccessException unauthorizedAccessEx)
            {
                //MessageBox.Show(unauthorizedAccessEx.Message, "COM port Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(unauthorizedAccessEx.Message);
            }
            finally
            {
                BaseModbusAdapter.Instance.Close();
            }

            return writeStatus;
        }

        public Dictionary<string, ushort> Read(string memoryHandle)
        {
            Dictionary<string, ushort> dataRead = new Dictionary<string, ushort>();

            string[] memoryAddressInfo = memoryHandle.Split('|');

            int startAddress = int.Parse(memoryAddressInfo[0].Substring(1));
            int registersCountToBeRead = int.Parse(memoryAddressInfo[1]);
            ushort[] values = new ushort[registersCountToBeRead];
            bool readingStatus = false;

            try
            {
                if (BaseModbusAdapter.Instance.Open(PortNumber, 9600, 8, Parity.None, StopBits.One))
                {
                    switch (memoryAddressInfo[0].Substring(0, 1))
                    {
                        case "4":   //Read Holding Registers
                            readingStatus = BaseModbusAdapter.Instance.SendFc3(Convert.ToByte(Address), (ushort)startAddress, (ushort)registersCountToBeRead, ref values, ResponseTime);
                            break;
                        case "3":   //Read Input Registers
                            readingStatus = BaseModbusAdapter.Instance.SendFc4(Convert.ToByte(Address), (ushort)startAddress, (ushort)registersCountToBeRead, ref values, ResponseTime);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show(ioEx.Message, "COM port Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(UnauthorizedAccessException unauthorizedAccessEx)
            {
                //MessageBox.Show(unauthorizedAccessEx.Message, "COM port Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Console.WriteLine(unauthorizedAccessEx.Message);
            }
            finally
            {
                BaseModbusAdapter.Instance.Close();
            }

            //Continue updating the memory address and value dictionary with the read values based on ReadingStatus
            for (int i = 0; i < values.Length; i++)
            {
                dataRead.Add((ushort.Parse(memoryAddressInfo[0]) + i).ToString(), readingStatus ? values[i] : default);
            }
            //Add read operation success = 1 or failure = 0 to the dictionary
            dataRead.Add("ReadOperationStatus", Convert.ToUInt16(readingStatus ? 1 : 0));

            return dataRead;
        }

        #region Controller Properties
        public string Label { get; set; }
        public string Identifier { get; set; }
        public string Address { get; set; }
        public string PortNumber { get; set; }
        public string ProviderName { get; set; } = "ModbusRtu";
        public int ResponseTime { get; set; }
        public IList<string> UsedMemoryAddresses { get; set; }
        #endregion

    }

    sealed class BaseModbusAdapter
    {
        private SerialPort sp = new SerialPort();
        public string modbusStatus;
        private static readonly BaseModbusAdapter modbusAdapterInstance = new BaseModbusAdapter();

        #region Constructor / Deconstructor
        static BaseModbusAdapter()
        {

        }

        private BaseModbusAdapter()
        {
        }

        public static BaseModbusAdapter Instance
        {
            get => modbusAdapterInstance;
        }

        ~BaseModbusAdapter()
        {
        }
        #endregion

        #region Open / Close Serial Ports
        public bool Open(string portName, int baudRate, int databits, Parity parity, StopBits stopBits)
        {
            //Ensure port isn't already opened:
            if (!sp.IsOpen)
            {
                //Assign desired settings to the serial port:
                sp.PortName = portName;
                sp.BaudRate = baudRate;
                sp.DataBits = databits;
                sp.Parity = parity;
                sp.StopBits = stopBits;
                //These timeouts are default and cannot be editted through the class at this point:
                sp.ReadTimeout = 1000;
                sp.WriteTimeout = 1000;
                sp.Open();
                modbusStatus = portName + " opened successfully";
                return true;
            }
            else
            {
                modbusStatus = portName + " already opened";
                return false;
            }
        }
        public bool Close()
        {
            //Ensure port is opened before attempting to close:
            if (sp.IsOpen)
            {
                try
                {
                    sp.Close();
                }
                catch (Exception err)
                {
                    modbusStatus = "Error closing " + sp.PortName + ": " + err.Message;
                    return false;
                }
                modbusStatus = sp.PortName + " closed successfully";
                return true;
            }
            else
            {
                modbusStatus = sp.PortName + " is not open";
                return false;
            }
        }
        #endregion

        #region CRC Computation
        private void GetCRC(byte[] message, ref byte[] CRC)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }
        #endregion

        #region Build Message
        private void BuildMessage(byte address, byte type, ushort start, ushort registers, ref byte[] message)
        {
            //Array to receive CRC bytes:
            byte[] CRC = new byte[2];

            message[0] = address;
            message[1] = type;
            message[2] = (byte)(start >> 8);
            message[3] = (byte)start;
            message[4] = (byte)(registers >> 8);
            message[5] = (byte)registers;

            GetCRC(message, ref CRC);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];
        }
        #endregion

        #region Check Response
        private bool CheckResponse(byte[] response)
        {
            //Perform a basic CRC check:
            byte[] CRC = new byte[2];
            GetCRC(response, ref CRC);
            if (CRC[0] == response[response.Length - 2] && CRC[1] == response[response.Length - 1])
                return true;
            else
                return false;
        }
        #endregion

        public ushort[] ModulateReponseIntoUshortArray(byte[] serialPortResponse, int arrayLength)
        {
            ushort[] values = new ushort[arrayLength];
            //Return requested register values:
            for (int i = 0; i < (serialPortResponse.Length - 5) / 2; i++)
            {
                values[i] = serialPortResponse[2 * i + 3];
                values[i] <<= 8;
                values[i] += serialPortResponse[2 * i + 4];
            }
            return values;
        }

        public void ClearIOBuffers()
        {
            //Clear in/out buffers:
            sp.DiscardOutBuffer();
            sp.DiscardInBuffer();
            sp.BaseStream.Flush();
        }

        #region Get Response
        private void GetResponse(ref byte[] response)
        {
            //There is a bug in .Net 2.0 DataReceived Event that prevents people from using this
            //event as an interrupt to handle data (it doesn't fire all of the time).  Therefore
            //we have to use the ReadByte command for a fixed length as it's been shown to be reliable.
            for (int i = 0; i < response.Length; i++)
            {
                response[i] = (byte)sp.ReadByte();
            }
        }
        #endregion

        #region Function 16 - Write Multiple Registers
        public bool SendFc16(byte address, ushort start, ushort registers, short[] values, int responseTime)
        {
            //Ensure port is open:
            if (sp.IsOpen)
            {
                ClearIOBuffers();
                //Message is 1 addr + 1 fcn + 2 start + 2 reg + 1 count + 2 * reg vals + 2 CRC
                byte[] message = new byte[9 + 2 * registers];
                //Function 16 response is fixed at 8 bytes
                byte[] response = new byte[8];

                //Add bytecount to message:
                message[6] = (byte)(registers * 2);
                //Put write values into message prior to sending:
                for (int i = 0; i < registers; i++)
                {
                    message[7 + 2 * i] = (byte)(values[i] >> 8);
                    message[8 + 2 * i] = (byte)(values[i]);
                }
                //Build outgoing message:
                BuildMessage(address, 16, start, registers, ref message);

                WriteMessageToSerialPort(message, 0, message.Length);
                bool responseStatus = GetResponseFromSerialPort(ref response, response.Length, responseTime);

                //Evaluate message:
                if (responseStatus && CheckResponse(response))
                {
                    modbusStatus = "Read successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }
        }
        #endregion

        #region Function 3 - Read Holding Registers
        public bool SendFc3(byte address, ushort start, ushort registers, ref ushort[] values, int responseTime)
        {
            //Ensure port is open:
            if (sp.IsOpen)
            {
                ClearIOBuffers();
                //Function 3 request is always 8 bytes:
                byte[] message = new byte[8];
                //Function 3 response buffer:
                byte[] response = new byte[5 + 2 * registers];
                //Build outgoing modbus message:
                BuildMessage(address, 3, start, registers, ref message);

                WriteMessageToSerialPort(message, 0, message.Length);
                bool responseStatus = GetResponseFromSerialPort(ref response, response.Length, responseTime);

                //Evaluate message:
                if (responseStatus && CheckResponse(response))
                {
                    values = ModulateReponseIntoUshortArray(response, values.Length);
                    modbusStatus = "Read successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }
        }
        #endregion

        #region Function 4 - Read Input Registers
        public bool SendFc4(byte address, ushort start, ushort registers, ref ushort[] values, int responseTime)
        {
            //Ensure port is open:
            if (sp.IsOpen)
            {
                ClearIOBuffers();
                //Function 4 request is always 8 bytes:
                byte[] message = new byte[8];
                //Function 4 response buffer:
                byte[] response = new byte[5 + 2 * registers];
                //Build outgoing modbus message:
                BuildMessage(address, 4, start, registers, ref message);

                WriteMessageToSerialPort(message, 0, message.Length);
                bool responseStatus = GetResponseFromSerialPort(ref response, response.Length, responseTime);

                //Evaluate message:
                if (responseStatus && CheckResponse(response))
                {
                    values = ModulateReponseIntoUshortArray(response, values.Length);
                    modbusStatus = "Read successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }
        }
        #endregion

        #region Write and Read from Serial Port
        public void WriteMessageToSerialPort(byte[] message, int offSet, int length)
        {
            sp.Write(message, offSet, length);
        }

        public bool GetResponseFromSerialPort(ref byte[] response, int responseLength, int responseTime)
        {
            byte[] serialPortResponse = new byte[responseLength];
            bool responseStatus = false;
            CancellationTokenSource cts = new CancellationTokenSource();
            try
            {
                Task getResponseTask = Task.Run(() => { GetResponse(ref serialPortResponse); }, cts.Token);
                responseStatus = getResponseTask.Wait(responseTime);
                if (responseStatus)
                {
                    response = serialPortResponse;
                }
                else
                {
                    cts.Cancel();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + " : " + DateTime.Now.ToString());
            }
            finally
            {
                cts.Dispose();
            }
            return responseStatus;
        }
        #endregion

    }
}
