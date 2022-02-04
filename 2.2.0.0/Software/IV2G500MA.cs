//Mario A. Dominguez Guerrero 
//September - 2020

#region System Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
#endregion

#region Project Libraries
using Keyence.IV2.Sdk;
#endregion

namespace Software
{
    class IV2G500MA 
    {
        #region Variables

        #region Communicacion Parameters[#]
        /// 0 = Communication Status (Disconnected = 0, Connected = 1)
        /// 1 = Read / Write Coils, (Read = 0, Write = 1)
        /// 2 = Control (Disable = 0 / Enable = 1)
        private static bool[] parameters = new bool[4] { false, false, false, false };
        public bool[] Parameters
        {
            get
            {
                return parameters;
            }

            set
            {
                parameters = value;
            }
        }
        #endregion

        #region TCP/IP Client Settings
        private static string iP;
        public string IP
        {
            get
            {
                return iP;
            }

            set
            {
                iP = value;
            }
        }
        private static int port;
        public int Port
        {
            get
            {
                return port;
            }

            set
            {
                port = value;
            }
        }
        private static string startPoint;
        public string StartPoint
        {
            get
            {
                return startPoint;
            }

            set
            {
                startPoint = value;
            }
        }
        //Message
        public const int MAX_COMMAND_SIZE = 3000;
        #endregion

        #region Keyence SDK 
        private static VisionSensorStore store = new VisionSensorStore();
        private static IVisionSensor sensor;

        private byte _activeToolNo;
        private int permissionToSwitchProg = 100;
        private static readonly Size IMAGE_SIZE = new Size(640, 480);
        private static Bitmap image;
        public Bitmap Image
        {
            get
            {
                return image;
            }

            set
            {
                image = value;
            }
        }
        private static Bitmap image_Master;
        public Bitmap Image_Master
        {
            get
            {
                return image_Master;
            }

            set
            {
                image_Master = value;
            }
        }

        public struct SensorInfo
        {
            public string ModelName, Version, SerialNumber, DeviceName;
        }
        public struct SensorProgramSettings
        {
            public string ProgramName;
            public int ProgramNumber;
            public bool ExternalTrigger;
            public ushort TriggerCycle;
        }
        //Inspection Result
        private static string textResult;
        public string TextResult
        {
            get
            {
                return textResult;
            }

            set
            {
                textResult = value;
            }
        }
        //Program List
        public ComboBox ProgList = new ComboBox();
        #endregion

        #region Command Types
        /// 12 = Results 
        /// 02 = Current program 
        private const int Results_SIZE = 12;
        private const int CurrentProg_SIZE = 2;
        //Current Program
        private static string currentProg;
        public string CurrentProg
        {
            get
            {
                return currentProg;
            }

            set
            {
                currentProg = value;
            }
        }
        //Results after trigger
        private static string[] result = new string[Results_SIZE];
        public string[] Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }
        //Values for each Cmd type
        private static string values_Msg;
        public string Values_Msg
        {
            get
            {
                return values_Msg;
            }

            set
            {
                values_Msg = value;
            }
        }
        #endregion

        #endregion

        #region Callbacks
        //IV2-G500MA: Image (thread safe calls)
        public delegate void GetProgListCallback();
        //IV2-G500MA: Switch Program (thread safe calls)
        public delegate void SwitchProgCallback();
        #endregion

        #region Objects
        private static SensorInfo _sensorInfo;
        internal SensorInfo _SensorInfo
        {
            get
            {
                return _sensorInfo;
            }

            set
            {
                _sensorInfo = value;
            }
        }

        private static SensorProgramSettings _sensorProgSettings;
        internal SensorProgramSettings _SensorProgSettings
        {
            get
            {
                return _sensorProgSettings;
            }

            set
            {
                _sensorProgSettings = value;
            }
        }
        #endregion

        #region Controls

        #endregion

        #region Functions
        //Connect to the IV2-G500MA (Server)
        private bool Connect()
        {
            try
            {
                #region Vision sensor    
                //Set the Start point
                VisionSensorStore.StartPoint = IPAddress.Parse(startPoint);
                //Get the connection to the Vision sensor
                sensor = store.Create(IPAddress.Parse(iP), (ushort)port);
                sensor.ImageAcquired += SensorImageAcquired;
                sensor.ProgramSettingsUpdated += SensorProgramSettingsUpdated;
                sensor.ResultUpdated += SensorResultUpdated;
                #endregion

                #region Get Sensor Info
                _sensorInfo.DeviceName = sensor.DeviceName;
                _sensorInfo.ModelName = sensor.ModelName;
                _sensorInfo.Version = sensor.SensorVersion;
                _sensorInfo.SerialNumber = sensor.SerialNo;
                #endregion

                #region Get Program Info
                _sensorProgSettings.ProgramNumber = sensor.ActiveProgram.ProgramNo;
                _sensorProgSettings.ProgramName = sensor.ActiveProgram.ProgramName;
                _sensorProgSettings.ExternalTrigger = sensor.ActiveProgram.ExternalTrigger;
                _sensorProgSettings.TriggerCycle = sensor.ActiveProgram.TriggerCycleMilliSec;
                GetProgramList();
                #endregion

                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)Scanner_Comm.CommStatus] = true;
                return true;
            }
            catch (Exception)
            {
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)Scanner_Comm.CommStatus] = false;
                //Message
                HMI.OForm.SystemMessages("Connection Error\n", "Error");

                return false;
            }
        }
        //Disconnect to the IV2-G500MA (Server)
        private bool Disconnect()
        {
            try
            {
                //Status of the Connection (Connected = 1, Disconnected = 0)
                parameters[(int)Scanner_Comm.CommStatus] = false;
                //Vision sensor SDK   
                sensor.ImageAcquired -= SensorImageAcquired;
                sensor.ProgramSettingsUpdated -= SensorProgramSettingsUpdated;
                sensor.ResultUpdated -= SensorResultUpdated;
                sensor.Dispose();

                return true;
            }
            catch (Exception)
            {
                //Message
                HMI.OForm.SystemMessages("Disconnection Error\n", "Error");
                return false;
            }
        }
        //Read data from the IV2-G500MA (Server)
        public void TickTack()
        {
            //Connection is Open
            if (parameters[(int)Scanner_Comm.CommStatus])
            { 
                try
                {
                    //Vision sensor SDK: Keep Alive   
                    sensor.TickTack();
                }
                catch (Exception ee)
                {
                    textResult = "NG";
                    FetchTickTack();
                    //HMI.OForm.SystemMessages("TicKTack Error:" + ee +"\n", "Error");
                }              
            }
        }

        private void FetchTickTack()
        {
            Disconnect();
            Thread.Sleep(200);
            Connect();
            Thread.Sleep(800);
            SwitchProg(_sensorProgSettings.ProgramNumber);
            Thread.Sleep(500);
            Trigger();
        }
        //Trigger
        public void Trigger()
        {
            try
            {
                sensor.Trigger();  
            }
            catch (Exception)
            {
                textResult = "NG";
            }
        }
        //Select program
        public void SwitchProg(int ProgramNumber)
        {
            try
            {
                sensor.SwitchProgramTo(sensor.Programs[ProgramNumber]);
                Thread.Sleep(50);
            }
            catch (Exception)
            {
                textResult = "NG";
            }
        }
        //View all programs availables
        public void GetProgramList()
        {
            //Clear the List
            ProgList.Items.Clear();

            try
            {
                for (int i = 0; i < sensor.Programs.Length; i++)
                {
                    ProgList.Items.Add(sensor.Programs[i].ProgramName);
                }
            }
            catch (Exception)
            {

                HMI.OForm.SystemMessages("Switch Program Error\n", "Error");
            }
        }
        
        #region Image Acquaried
        private void SensorImageAcquired(object sender, ImageAcquiredEventArgs e)
        {
            image = new Bitmap(IMAGE_SIZE.Width, IMAGE_SIZE.Height, PixelFormat.Format24bppRgb);
            BitmapData lockData = image.LockBits(new Rectangle(Point.Empty, IMAGE_SIZE), ImageLockMode.WriteOnly,
                                                 PixelFormat.Format24bppRgb);
            Marshal.Copy(e.LiveImage.ByteData, 0, lockData.Scan0, e.LiveImage.ByteData.Length);
            image.UnlockBits(lockData);
            DrawToolWindow(image);
        }
        private void DrawToolWindow(Bitmap image)
        {
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                for (byte i = 0; i <= 16; i++)
                {
                    if (i == _activeToolNo) continue;
                    DrawToolWindow(graphics, i);
                }
                DrawToolWindow(graphics, _activeToolNo);
            }
        }
        private void DrawToolWindow(Graphics graphics, byte toolNoi)
        {
            var okColor = toolNoi == _activeToolNo ? Color.Lime : Color.Green;
            var ngColor = toolNoi == _activeToolNo ? Color.Magenta : Color.Red;
            ToolSettingBase toolSetting = sensor.ActiveProgram[toolNoi];
            if (toolSetting.ToolType == "") return;
            sensor.DrawWindow(graphics, okColor, ngColor, toolNoi);
        }
        #endregion

        //Program Selected: Settings
        private void SensorProgramSettingsUpdated(object sender, EventArgs e)
        {
            _sensorProgSettings.ProgramNumber = sensor.ActiveProgram.ProgramNo;
            _sensorProgSettings.ProgramName = sensor.ActiveProgram.ProgramName;
            _sensorProgSettings.ExternalTrigger = sensor.ActiveProgram.ExternalTrigger;
            _sensorProgSettings.TriggerCycle = sensor.ActiveProgram.TriggerCycleMilliSec;

            image_Master = CreateMasterImage(sensor.ActiveProgram.MasterImage);
        }
        //Program Selected: Master Image
        private static Bitmap CreateMasterImage(Keyence.IV2.Sdk.Image masterImage)
        {
            var masterBitmap = new Bitmap(640, 480, PixelFormat.Format24bppRgb);
            var masterData = masterBitmap.LockBits(new Rectangle(Point.Empty, new Size(640, 480)),
                                                   ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
            try
            {
                Marshal.Copy(masterImage.ByteData, 0, masterData.Scan0, masterImage.ByteData.Length);
            }
            finally
            {
                masterBitmap.UnlockBits(masterData);
            }
            return masterBitmap;
        }
        private void SensorResultUpdated(object sender, ToolResultUpdatedEventArgs Result)
        {
            textResult = Result.TotalStatusResult ? "OK" : "NG";
        }
        

        #region Convertions
        //Converter Byte Array to Char Array
        private char[] ByteArrayToCharArray(byte[] Message, int Size)
        {
            //Define the Array variable to save the message on base of its length
            char[] resultado = new char[Size];

            //Read all byte array 
            for (int i = 0; i < Size; i++)
            {
                //Convert the byte to Char of the message and then added the value to the char array 
                resultado[i] += Convert.ToChar(Message[i]);
            }
            //Message converted
            return resultado;
        }
        //Converter Char Array to String
        private String CharArrayToString(char[] Message)
        {
            string resultado = "";

            for (int i = 0; i < Message.Length; i++)
            {
                //Adding characters to String
                resultado += Message[i].ToString();
            }
            return resultado;
        }
        #endregion

        #endregion

        #region Threads
        //Open the communication
        public bool Open_Communication()
        {
            if (!parameters[(int)Scanner_Comm.Control])
            {
                Connect();

                if (parameters[(int)Scanner_Comm.CommStatus])
                {
                    parameters[(int)Scanner_Comm.Control] = true;
                    //Active the communication process
                    Thread Tick = new Thread(new ThreadStart(() => Communication()));
                    Tick.Start();
                    return true;
                }
                else
                {
                    parameters[(int)Scanner_Comm.Control] = false;
                    return false;
                }
            }
            else
                return false;
        }
        //Communication Proccess
        private void Communication()
        {
            while (parameters[(int)Scanner_Comm.CommStatus])
            {
                //Reading the IV2 G500MA Read Messages
                Thread.Sleep(100);
                if (sensor != null)
                {
                    TickTack();
                }
                
            }
        }
        //Close the communication
        public bool Close_Communication()
        {
            if (parameters[(int)Scanner_Comm.Control])
            {
                Disconnect();
                parameters[(int)Scanner_Comm.Control] = false;
                return true;
            }
            else
            {
                parameters[(int)Scanner_Comm.Control] = false;
                return false;
            }
        }
        #endregion
    }
}
