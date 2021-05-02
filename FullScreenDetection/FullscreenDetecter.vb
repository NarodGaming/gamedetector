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

Public Class FullscreenDetecter

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
    ''' <returns>
    ''' List(Of Object) (Boolean: True if application detected - False if not, String: Window text of application if was detected, UInteger: Process ID of application if was detected)
    ''' </returns>
    Public Function DetectFullscreenApplication() As List(Of Object) ' this is a base function which can be used to pull ANY fullscreen window, including web browsers.
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
        Dim ReturnList As New List(Of Object) ' set up list for response
        ReturnList.Add(runningFullScreen) ' add boolean response
        If runningFullScreen = True Then ' only run if fullscreen application was detected
            ReturnList.Add(windowText.ToString) ' add application name to list
            Dim processId As UInteger ' create variable to store process ID of application in
            GetWindowThreadProcessId(hWnd, processId) ' get process ID of that specific application/window
            ReturnList.Add(processId) ' add process ID to list
        End If
        runningFullScreen = False ' prevents requirement for reinit
        Return ReturnList ' return response
    End Function

    ''' <summary>
    ''' Returns any fullscreen application detected, but filters out browsers (more to come in future versions). A fullscreen game is determined as one that fills the entire screen (if multi-monitor, screen user is currently using).
    ''' 
    ''' If no application detected, only the Boolean will be present in list.
    ''' </summary>
    ''' <returns>
    ''' List(Of Object) (Boolean: True if application detected - False if not, String: Window text of application if was detected, UInteger: Process ID of application if was detected)
    ''' </returns>
    Public Function DetectGameFullscreen() As List(Of Object)
        Dim response As New List(Of Object) ' create response "list" variable
        response = DetectFullscreenApplication() ' runs main function to check if fullscreen app

        If response(0) = True Then ' checks to see if main function returned true
            ' fullscreen detected
            Dim applicationame As String = response(1)
            If applicationame.Contains("Microsoft Edge") Or applicationame.Contains("Mozilla Firefox") Or applicationame.Contains("Google Chrome") Or applicationame.Contains("Microsoft? Edge") Then ' if it's a webbrowser, it isn't considered a game. The extra "Microsoft? Edge" is there due to a bug that can occur on the current version of Edge.
                Dim responselist As New List(Of Object) From {
                    False
                }
                Return responselist
            End If
            Return response ' all checks run, is considered a game to be passed forwards
        Else
            ' fullscreen not detected, this function will return a list with just 1 response as the false boolean
            Dim responselist As New List(Of Object) From {
                False
            }
            Return responselist
        End If
    End Function

    ''' <summary>
    ''' Returns if specified application name (from window title text) is currently in focus and maximised.
    ''' 
    ''' If the application was not detected, only the Boolean will be present in list.
    ''' </summary>
    ''' <returns>
    ''' List(Of Object) (Boolean: True if application detected - False if not, String: Window text of application if was detected, UInteger: Process ID of application if was detected)
    ''' </returns>
    Public Function DetectCustomFullscreen(appname As String) As List(Of Object)
        Dim response As New List(Of Object) ' create response "list" variable
        response = DetectFullscreenApplication() ' runs main function to check if fullscreen app

        If response(0) = True Then ' checks to see if main function returned true
            ' fullscreen detected
            Dim applicationame As String = response(1)
            If applicationame.Contains(appname) Then
                Dim responselist As New List(Of Object) From {
                    False
                }
                Return responselist
            End If
            Return response ' all checks run, is considered a game to be passed forwards
        Else
            ' fullscreen not detected, this function will return a list with just 1 response as the false boolean
            Dim responselist As New List(Of Object) From {
                False
            }
            Return responselist
        End If
    End Function

End Class
