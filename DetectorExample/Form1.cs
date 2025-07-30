using Narod.FullscreenDetection;

namespace DetectorExample
{
    public partial class Form1 : Form
    {
        private FullscreenDetector fullscreenDetector = new FullscreenDetector();
        public Form1()
        {
            InitializeComponent();
        }

        private void searchAppsBtn_Click(object sender, EventArgs e)
        {
            appDetails checkedApp = fullscreenDetector.getProgramDetails();
            appsListbox.Items.Add($"Detected: {checkedApp.detected} - Title: {checkedApp.windowTitle} - pID: {checkedApp.processID}");
        }

        private void autoScanTimer_Tick(object sender, EventArgs e)
        {
            appDetails checkedApp = fullscreenDetector.getProgramDetails();
            appsListbox.Items.Add($"(AUTO) Detected: {checkedApp.detected} - Title: {checkedApp.windowTitle} - pID: {checkedApp.processID}");
        }
    }
}
