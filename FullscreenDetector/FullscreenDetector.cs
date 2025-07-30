    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    namespace Narod
    {
        namespace FullscreenDetection
        {
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        /// <summary>
        /// Represents the details of an application, including its window title, process ID, and detection status.
        /// </summary>
        public struct appDetails
        {
            public string? windowTitle;
            public int? processID;
            public bool detected;
        }

        public class FullscreenDetector
        {
            private bool? fullscreenDetected = null;
            private string? programDetected = null;
            private int? processID = null;

            /// <summary>
            /// Returns if a fullscreen application has been detected. By default, it will rescan the current foreground application.
            /// </summary>
            /// <param name="shouldRescan">Configures whether this function should rescan</param>
            /// <returns></returns>
            /// <exception cref="InvalidOperationException">Occurs if rescans are disabled and no previous scan results exist</exception>
            public bool getHasDetected(bool shouldRescan = true)
            {
                if (shouldRescan == true)
                {
                    detectFullScreenApplication();
                } else if (fullscreenDetected == null)
                {
                    throw new InvalidOperationException("Fullscreen detection has not been performed yet. Either run a detection first, or allow rescans: isFullscreenDetected(true).");
                }
                return (bool)fullscreenDetected;
            }

            /// <summary>
            /// Returns the program name of the detected fullscreen application. By default, it will rescan the current foreground application.
            /// </summary>
            /// <param name="shouldRescan">Configures whether this function should rescan</param>
            /// <returns></returns>
            /// <exception cref="InvalidOperationException">Occurs if rescans are disabled and no previous scan results exist, or if no fullscreen app was detected on last scan</exception>
            public string getProgramDetected(bool shouldRescan = true)
            {
                if (shouldRescan == true)
                {
                    detectFullScreenApplication();
                } else if (programDetected == null && fullscreenDetected == false)
                {
                    throw new InvalidOperationException("Full screen detection has been performed, but no full screen application was detected. Either check correctly if one has been detected, or use getProgramDetails()");
                } else if (programDetected == null)
                {
                    throw new InvalidOperationException("Fullscreen detection has not been performed yet. Either run a detection first, or allow rescans: getProgramDetected(true).");
                }
                return programDetected;
            }

            /// <summary>
            /// Returns the process ID of the detected fullscreen application. By default, it will rescan the current foreground application.
            /// </summary>
            /// <param name="shouldRescan">Configures whether this function should rescan</param>
            /// <returns></returns>
            /// <exception cref="InvalidOperationException">Occurs if rescans are disabled and no previous scan results exist, or if no fullscreen app was detected on last scan</exception>
            public int getProcessIDDetected(bool shouldRescan = true)
            {
                if (shouldRescan == true)
                {
                    detectFullScreenApplication();
                } else if (processID == null && fullscreenDetected == false)
                {
                    throw new InvalidOperationException("Full screen detection has been performed, but no full screen application was detected. Either check correctly if one has been detected, or use getProgramDetails()");
                } else if (processID == null)
                {
                    throw new InvalidOperationException("Fullscreen detection has not been performed yet. Either run a detection first, or allow rescans: getProcessIDDetected(true).");
                }
                return (int)processID;
            }

            /// <summary>
            /// Returns the details of the detected fullscreen application, including its window title, process ID, and detection status. By default, it will rescan the current foreground application.
            /// </summary>
            /// <param name="shouldRescan">Configures whether this function should rescan</param>
            /// <returns></returns>
            /// <exception cref="InvalidOperationException">Occurs if rescans are disabled and no previous scan results exist</exception>
            public appDetails getProgramDetails(bool shouldRescan = true)
            {
                if (shouldRescan == true)
                {
                    detectFullScreenApplication();
                } else if (fullscreenDetected == null)
                {
                    throw new InvalidOperationException("Fullscreen detection has not been performed yet. Either run a detection first, or allow rescans: getProgramDetails(true).");
                }
                return new appDetails
                {
                    detected = (bool)fullscreenDetected,
                    processID = processID,
                    windowTitle = programDetected
                };
            }

            [DllImport("user32.dll")]
            private static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll")]
            private static extern IntPtr GetDesktopWindow();

            [DllImport("user32.dll")]
            private static extern IntPtr GetShellWindow();

            [DllImport("user32.dll")]
            private static extern int GetWindowRect(IntPtr hWnd, out RECT rc);

            [DllImport("user32.dll")]
            public static extern int GetWindowText(IntPtr hWnd, System.Text.StringBuilder lpString, int cch);

            [DllImport("user32.dll")]
            public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processID);


            private System.Text.StringBuilder windowText = new System.Text.StringBuilder(256);
            private RECT appBounds = new();
            private Rectangle screenBounds = new();
            private IntPtr hWnd = new();

            private IntPtr desktopHandle = new();
            private IntPtr shellHandle = new();

            /// <summary>
            /// Checks for any fullscreen application currently running in the foreground.
            /// </summary>
            public void detectFullScreenApplication()
            {
                hWnd = GetForegroundWindow();
                desktopHandle = GetDesktopWindow();
                shellHandle = GetShellWindow();

                if (!(hWnd == null) && !(hWnd.Equals(IntPtr.Zero)) && (!(hWnd.Equals(desktopHandle)) || !(hWnd.Equals(shellHandle))))
                {
                    GetWindowRect(hWnd, out appBounds);
                    screenBounds = Screen.FromHandle(hWnd).Bounds;
                    if ((appBounds.Bottom - appBounds.Top) == screenBounds.Height && (appBounds.Right - appBounds.Left) == screenBounds.Width)
                    {
                        fullscreenDetected = true;
                        GetWindowText(hWnd, windowText, windowText.Capacity);
                        uint detectedProcessID = 0;
                        GetWindowThreadProcessId(hWnd, out detectedProcessID);
                        processID = (int)detectedProcessID;
                        programDetected = windowText.ToString();
                    }
                    else
                    {
                        windowText = new System.Text.StringBuilder(256);
                        programDetected = null;
                        processID = null;
                        fullscreenDetected = false;
                    }
                }
            }
        }
    }
}