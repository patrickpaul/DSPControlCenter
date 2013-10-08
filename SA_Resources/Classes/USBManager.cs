using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SA_Resources
{
    
    [StructLayout(LayoutKind.Sequential)]
    public struct DEV_BROADCAST_VOLUME
    {
        public int dbcv_size; // size of the struct
        public int dbcv_devicetype; // DBT_DEVTYP_VOLUME
        public int dbcv_reserved; // reserved; do not use
        public int dbcv_unitmask; // Bit 0=A, bit 1=B, and so on (bitmask)
        public short dbcv_flags; // DBTF_MEDIA=0x01, DBTF_NET=0x02 (bitmask)
    }


    public delegate void UsbStateChangedEventHandler(UsbStateChangedEventArgs e);


    public enum UsbStateChange
    {
        Added,
        Removing,
        Removed
    }


    public class UsbStateChangedEventArgs : EventArgs
    {
        public UsbStateChangedEventArgs(UsbStateChange state, DEV_BROADCAST_VOLUME volume)
        {
            this.State = state;
            this.Volume = volume;
        }

        public DEV_BROADCAST_VOLUME Volume
        {
            get;
            private set;
        }

        public UsbStateChange State
        {
            get;
            private set;
        }
    }

    public class UsbManager : IDisposable
    {

        #region DriverWindow

        private class DriverWindow : NativeWindow, IDisposable
        {

            private const int WM_DEVICECHANGE = 0x0219; // device state change
            private const int DBT_DEVICEARRIVAL = 0x8000; // detected a new device
            private const int DBT_DEVICEQUERYREMOVE = 0x8001; // preparing to remove
            private const int DBT_DEVICEREMOVECOMPLETE = 0x8004; // removed 
            private const int DBT_DEVTYP_PORT = 0x00000003; // port


            public DriverWindow()
            {
                // create a generic window with no class name
                base.CreateHandle(new CreateParams());
            }


            public void Dispose()
            {
                base.DestroyHandle();
                GC.SuppressFinalize(this);
            }


            public event UsbStateChangedEventHandler StateChanged;


            protected override void WndProc(ref Message message)
            {
                base.WndProc(ref message);

                if ((message.Msg == WM_DEVICECHANGE) && (message.LParam != IntPtr.Zero))
                {
                    DEV_BROADCAST_VOLUME volume = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(
                        message.LParam, typeof(DEV_BROADCAST_VOLUME));

                    // We only want port changes
                    if (volume.dbcv_devicetype == DBT_DEVTYP_PORT)
                    {
                        switch (message.WParam.ToInt32())
                        {
                            case DBT_DEVICEARRIVAL:
                                SignalDeviceChange(UsbStateChange.Added, volume);
                                break;

                            case DBT_DEVICEQUERYREMOVE:
                                // can intercept
                                break;

                            case DBT_DEVICEREMOVECOMPLETE:
                                SignalDeviceChange(UsbStateChange.Removed, volume);
                                break;
                        }
                    }
                }
            }


            private void SignalDeviceChange(UsbStateChange state, DEV_BROADCAST_VOLUME volume)
            {

                if (StateChanged != null)
                {
                    StateChanged(new UsbStateChangedEventArgs(state, volume));
                }
            }

        }

        #endregion WndProc Driver



        private DriverWindow window;

        private UsbStateChangedEventHandler handler;
        private bool isDisposed;


        //========================================================================================
        // Constructor
        //========================================================================================

        /// <summary>
        /// Initialize a new instance.
        /// </summary>

        public UsbManager()
        {
            this.window = null;
            this.handler = null;
            this.isDisposed = false;
        }


        #region Lifecycle

        /// <summary>
        /// Destructor.
        /// </summary>

        ~UsbManager()
        {
            Dispose();
        }


        /// <summary>
        /// Must shutdown the driver window.
        /// </summary>

        public void Dispose()
        {
            if (!isDisposed)
            {
                if (window != null)
                {
                    window.StateChanged -= new UsbStateChangedEventHandler(DoStateChanged);
                    window.Dispose();
                    window = null;
                }

                isDisposed = true;

                GC.SuppressFinalize(this);
            }
        }

        #endregion Lifecycle


        //========================================================================================
        // Events/Properties
        //========================================================================================

        /// <summary>
        /// Add or remove a handler to listen for USB disk drive state changes.
        /// </summary>

        public event UsbStateChangedEventHandler StateChanged
        {
            add
            {
                if (window == null)
                {
                    // create the driver window once a consumer registers for notifications
                    window = new DriverWindow();
                    window.StateChanged += new UsbStateChangedEventHandler(DoStateChanged);
                }

                handler = (UsbStateChangedEventHandler)Delegate.Combine(handler, value);
            }

            remove
            {
                handler = (UsbStateChangedEventHandler)Delegate.Remove(handler, value);

                if (handler == null)
                {
                    // destroy the driver window once the consumer stops listening
                    window.StateChanged -= new UsbStateChangedEventHandler(DoStateChanged);
                    window.Dispose();
                    window = null;
                }
            }
        }


        //========================================================================================
        // Methods
        //========================================================================================

        /// <summary>
        /// Internally handle state changes and notify listeners.
        /// </summary>
        /// <param name="e"></param>

        private void DoStateChanged(UsbStateChangedEventArgs e)
        {
            if (handler != null)
            {

                // we can only interrogate drives that are added...
                // cannot see something that is no longer there!

                if ((e.State == UsbStateChange.Added))
                {
                    // the following Begin/End invokes looks strange but are required
                    // to resolve a "DisconnectedContext was detected" exception which
                    // occurs when the current thread terminates before the WMI queries
                    // can complete.  I'm not exactly sure why that would happen...
                }

                handler(e);
            }
        }

    }
}
