' Created by NarodGaming (c) 2020-2021
' Please leave this notice at the top of this file, you can add your name as an editor beneath if you would like.
' Additional authors/editors: 

Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Public Structure RECT
    Public Left As Integer
    Public Top As Integer
    Public Right As Integer
    Public Bottom As Integer
End Structure

Public Class FullscreenDetector
    Private HasDetected As Boolean = Nothing
    Private ProgramDetected As String = Nothing
    Private ProcessIDDetected As UInteger = Nothing

    Public Function GetHasDetected() As Boolean
        Return HasDetected
    End Function

    Public Function GetProgramDetected() As String
        Return ProgramDetected
    End Function

    Public Function GetProcessIDDetected() As UInteger
        Return ProcessIDDetected
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetForegroundWindow() As IntPtr

    End Function
    <DllImport("user32.dll")>
    Private Shared Function GetDesktopWindow() As IntPtr

    End Function
    <DllImport("user32.dll")>
    Private Shared Function GetShellWindow() As IntPtr

    End Function
    <DllImport("user32.dll")>
    Private Shared Function GetWindowRect(ByVal hwnd As IntPtr, <Out> ByRef rc As RECT) As Integer

    End Function
    <DllImport("user32.dll")>
    Private Shared Function GetWindowText(ByVal hwnd As IntPtr, ByVal lpString As System.Text.StringBuilder, ByVal cch As Integer) As Integer

    End Function
    <DllImport("user32.dll")>
    Private Shared Function GetWindowThreadProcessId(ByVal hWnd As IntPtr, <Out> ByRef processId As UInteger) As UInteger

    End Function

    Private runningFullScreen As Boolean = False
    Private windowText As New System.Text.StringBuilder(256)
    Private appBounds As New RECT
    Private screenBounds As New Rectangle
    Private hWnd As New IntPtr

    Private desktopHandle As New IntPtr
    Private shellHandle As New IntPtr

    ''' <summary>
    ''' Returns any fullscreen application detected. A fullscreen application is determined as one that fills the entire screen (if multi-monitor, screen user is currently using).
    ''' 
    ''' If no application detected, only the Boolean will be present in list.
    ''' </summary>
    Public Sub DetectFullscreenApplication() ' this is a base function which can be used to pull ANY fullscreen window, including web browsers.
        hWnd = GetForegroundWindow() ' assumed to be the fullscreen program, is actually just the current window in focus
        desktopHandle = GetDesktopWindow() ' gets the desktop window, as to check that it isn't the desktop which is in focus
        shellHandle = GetShellWindow() ' gets the shell window, as to check that it isn't the shell (usually explorer.exe) which is in focus

        If Not hWnd = Nothing And Not hWnd.Equals(IntPtr.Zero) Then ' checks to make sure there actually is a currently focussed window, and that the focussed window is valid (not hidden, etc)
            If Not (hWnd.Equals(desktopHandle) Or hWnd.Equals(shellHandle)) Then ' checks to see if the application doesn't match the desktop window (would likely happen if no window focussed), and that it doesn't match the shell application (usually explorer.exe)
                GetWindowRect(hWnd, appBounds) ' gets the window size of the application it is going to check
                screenBounds = Screen.FromHandle(hWnd).Bounds ' gets the current monitor size (resolution)
                If ((appBounds.Bottom - appBounds.Top) = screenBounds.Height And (appBounds.Right - appBounds.Left) = screenBounds.Width) Then ' aka if window is fullscreen
                    runningFullScreen = True ' set boolean to True
                    GetWindowText(GetForegroundWindow, windowText, windowText.Capacity) ' get window text of application
                End If
            End If
        End If
        HasDetected = runningFullScreen
        If runningFullScreen = True Then ' only run if fullscreen application was detected
            ProgramDetected = windowText.ToString
            Dim processId As UInteger ' create variable to store process ID of application in
            GetWindowThreadProcessId(hWnd, processId) ' get process ID of that specific application/window
            ProcessIDDetected = processId
        Else
            ProgramDetected = Nothing
            ProcessIDDetected = Nothing
        End If
        runningFullScreen = False ' prevents requirement for reinit
    End Sub

End Class
