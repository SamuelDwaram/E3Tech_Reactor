using PylonC.NET;
using PylonC.NETSupportLibrary;
using System;

namespace E3Tech.Pylon.Wpf.SDK.Helpers
{

    public class PylonNodeValueHandler
    {
        private string name = "ValueName"; /* The name of the node. */
        private ImageProvider m_imageProvider = null; /* The image provider providing the node handle and status events. */
        private NODE_HANDLE m_hNode = new NODE_HANDLE(); /* The handle of the node. */
        private NODE_CALLBACK_HANDLE m_hCallbackHandle = new NODE_CALLBACK_HANDLE(); /* The handle of the node callback. */
        private NodeCallbackHandler m_nodeCallbackHandler = new NodeCallbackHandler(); /* The callback handler. */

        public PylonNodeValueHandler()
        {
            m_nodeCallbackHandler.CallbackEvent += new NodeCallbackHandler.NodeCallback(NodeCallbackEventHandler);
            Reset();
        }

        /* Used for connecting an image provider providing the node handle and status events. */
        public ImageProvider MyImageProvider
        {
            set
            {
                m_imageProvider = value;
                /* Image provider has been connected. */
                if (m_imageProvider != null)
                {
                    /* Register for the events of the image provider needed for proper operation. */
                    m_imageProvider.DeviceOpenedEvent += new ImageProvider.DeviceOpenedEventHandler(DeviceOpenedEventHandler);
                    m_imageProvider.DeviceClosingEvent += new ImageProvider.DeviceClosingEventHandler(DeviceClosingEventHandler);
                    /* Update the control values. */
                    UpdateValues();
                }
                else /* Image provider has been disconnected. */
                {
                    Reset();
                }
            }
        }

        public string NodeName
        {
            get => name;
            set { name = value; }
        }

        /* A device has been opened. Update the control. */
        private void DeviceOpenedEventHandler(object sender , EventArgs e)
        {
            try
            {
                /* Get the node. */
                m_hNode = m_imageProvider.GetNodeFromDevice(name);

                /* Features, like 'Gain', are named according to the GenICam Standard Feature Naming Convention (SFNC).
                   The SFNC defines a common set of features, their behavior, and the related parameter names.
                   This ensures the interoperability of cameras from different camera vendors.

                   Some cameras, e.g. cameras compliant to the USB3 Vision standard, use a later SFNC version than
                   previous Basler GigE and Firewire cameras. Accordingly, the behavior of these cameras and
                   some parameters names will be different.
                 */
                if (!m_hNode.IsValid /* No node has been found using the provided name. */
                    && (name == "GainRaw" || name == "ExposureTimeRaw")) /* This means probably that the camera is compliant to a later SFNC version. */
                {
                    /* Check to see if a compatible node exists. The SFNC 2.0, implemented by Basler USB Cameras for instance, defines Gain
                       and ExposureTime as features of type Float.*/
                    if (name == "GainRaw")
                    {
                        m_hNode = m_imageProvider.GetNodeFromDevice("Gain");
                    }
                    else if (name == "ExposureTimeRaw")
                    {
                        m_hNode = m_imageProvider.GetNodeFromDevice("ExposureTime");
                    }
                    /* Update the displayed name. */
                    string displayName = GenApi.NodeGetDisplayName(m_hNode) + ":";
                    /* The underlying integer representation of Gain and ExposureTime can be accessed using
                       the so called alias node. The alias is another representation of the original parameter.

                       Since this slider control can only be used with Integer nodes we have to use
                       the alias node here to display and modify Gain and ExposureTime.
                    */
                    m_hNode = GenApi.NodeGetAlias(m_hNode);
                    /* Register for changes. */
                    m_hCallbackHandle = GenApi.NodeRegisterCallback(m_hNode, m_nodeCallbackHandler);
                }
                else
                {
                    /* Update the displayed name. */
                    string displayName = GenApi.NodeGetDisplayName(m_hNode) + ":";
                    /* Register for changes. */
                    m_hCallbackHandle = GenApi.NodeRegisterCallback(m_hNode, m_nodeCallbackHandler);
                }
                /* Update the control values. */
                UpdateValues();
            }
            catch
            {
                /* If errors occurred disable the control. */
                Reset();
            }
        }

        /* The device has been closed. Update the control. */
        private void DeviceClosingEventHandler(object sender, EventArgs e)
        {
            Reset();
        }

        /* The node state has changed. Update the control. */
        private void NodeCallbackEventHandler(NODE_HANDLE handle)
        {
            if (handle.Equals(m_hNode))
            {
                /* Update the control values. */
                UpdateValues();
            }
        }

        /* Deactivate the control and deregister the callback. */
        private void Reset()
        {
            if (m_hNode.IsValid && m_hCallbackHandle.IsValid)
            {
                try
                {
                    GenApi.NodeDeregisterCallback(m_hNode, m_hCallbackHandle);
                }
                catch
                {
                    /* Ignore. The control is about to be disabled. */
                }
            }
            m_hNode.SetInvalid();
            m_hCallbackHandle.SetInvalid();
        }

        /* Get the current values from the node and display them. */
        private void UpdateValues()
        {
            try
            {

                if (m_hNode.IsValid)
                {
                    if (GenApi.NodeGetType(m_hNode) == EGenApiNodeType.IntegerNode)  /* Check if proper node type. */
                    {
                        /* Get the values. */
                        bool writable = GenApi.NodeIsWritable(m_hNode);
                        int min = checked((int)GenApi.IntegerGetMin(m_hNode));
                        int max = checked((int)GenApi.IntegerGetMax(m_hNode));
                        int val = checked((int)GenApi.IntegerGetValue(m_hNode));
                        int inc = checked((int)GenApi.IntegerGetInc(m_hNode));

                        /* Update the slider. */
                        //slider.Minimum = min;
                        //slider.Maximum = max;
                        //slider.Value = val;
                        //slider.SmallChange = inc;
                        //slider.TickFrequency = (max - min + 5) / 10;

                        /* Update the values. */
                        //labelMin.Text = "" + min;
                        //labelMax.Text = "" + max;
                        //labelCurrentValue.Text = "" + val;

                        /* Update accessibility. */
                        //slider.Enabled = writable;
                        //labelMin.Enabled = writable;
                        //labelMax.Enabled = writable;
                        //labelName.Enabled = writable;
                        //labelCurrentValue.Enabled = writable;

                        return;
                    }
                }
            }
            catch
            {
                /* If errors occurred disable the control. */
            }
            Reset();
        }

        public void SetNodeValue(int value)
        {
            if (m_hNode.IsValid)
            {
                try
                {
                    if (GenApi.NodeIsWritable(m_hNode))
                    {                        
                        GenApi.IntegerSetValue(m_hNode, value);
                    }
                }
                catch
                {
                    /* Ignore any errors here. */
                }
            }
        }
    }
}
