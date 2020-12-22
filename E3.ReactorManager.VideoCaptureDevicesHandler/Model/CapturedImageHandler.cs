using E3.ReactorManager.Interfaces.DataAbstractionLayer;
using E3.ReactorManager.Interfaces.DataAbstractionLayer.Data;
using E3.ReactorManager.Interfaces.Framework.Logging;
using E3.ReactorManager.ParametersProvider.Model;
using EventAggregator.Core;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Unity;

namespace E3.ReactorManager.VideoCaptureDevicesHandler.Model
{
    public class CapturedImageHandler
    {
        IEventAggregator eventAggregator;
        IParametersProvider parametersProvider;
        IDatabaseWriter databaseWriter;
        ILogger logger;

        public CapturedImageHandler(IUnityContainer containerProvider, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            eventAggregator.GetEvent<SaveCapturedImageToDatabase>().Subscribe((capturedImageArgs) => Task.Factory.StartNew(new Action<object>(SaveCapturedImage), capturedImageArgs));
            parametersProvider = containerProvider.Resolve<IParametersProvider>();
            databaseWriter = containerProvider.Resolve<IDatabaseWriter>();
            logger = containerProvider.Resolve<ILogger>();
        }

        private void SaveCapturedImage(object capturedImageArgsObject)
        {
            CapturedImageArgs capturedImageArgs = capturedImageArgsObject as CapturedImageArgs;
            byte[] imgBytes = GetImageInBytes(capturedImageArgs.ImageBitmap);

            Dictionary<string, string> fieldDeviceParameters = parametersProvider.GetFieldDeviceParametersWithTheirValues(capturedImageArgs.DeviceId);
            var binFormatter = new BinaryFormatter();
            var memStream = new MemoryStream();
            binFormatter.Serialize(memStream, fieldDeviceParameters);

            IList<DbParameterInfo> parameters = new List<DbParameterInfo>
            {
                new DbParameterInfo("@FieldDeviceIdentifier", capturedImageArgs.DeviceId, DbType.String),
                new DbParameterInfo("@ImageData", imgBytes, DbType.Binary),
                new DbParameterInfo("@ParametersArray", memStream.ToArray(), DbType.Binary),
            };
            databaseWriter.ExecuteWriteCommand("LogReactorImageWithFieldDeviceParameters", CommandType.StoredProcedure, parameters.ToArray());
        }

        private byte[] GetImageInBytes(Bitmap src)
        {
            byte[] imgBytes = null;

            if (src != null)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    src.Save(memoryStream, ImageFormat.Png);
                    imgBytes = new byte[memoryStream.Length];
                    imgBytes = memoryStream.ToArray();
                }
            }

            return imgBytes;
        }

    }
}
