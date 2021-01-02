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

    Public Function DetectFullscreenApplication() As List(Of Object) ' this is a base function which can be used to pull ANY fullscreen window, including web browsers.
        hWnd = GetForegroundWindow() ' assumed to be the fullscreen program, is actually just the current window in focus
        desktopHandle = GetDesktopWindow() ' gets the desktop window, as to check that it isn't the desktop which is in focus
        shellHandle = GetShellWindow() ' gets the shell window, as to check that it isn't the shell which is in focus

        If Not hWnd = Nothing And Not hWnd.Equals(IntPtr.Zero) Then
            If Not (hWnd.Equals(desktopHandle) Or hWnd.Equals(shellHandle)) Then
                GetWindowRect(hWnd, appBounds)
                screenBounds = Screen.FromHandle(hWnd).Bounds
                If ((appBounds.Bottom - appBounds.Top) = screenBounds.Height And (appBounds.Right - appBounds.Left) = screenBounds.Width) Then
                    runningFullScreen = True
                    GetWindowText(GetForegroundWindow, windowText, windowText.Capacity)
                End If
            End If
        End If
        Dim ReturnList As New List(Of Object)
        ReturnList.Add(runningFullScreen)
        If runningFullScreen = True Then
            ReturnList.Add(windowText.ToString)
            Dim processId As UInteger
            GetWindowThreadProcessId(hWnd, processId)
            ReturnList.Add(processId)
        End If
        runningFullScreen = False ' prevents requirement for reinit
        Return ReturnList
    End Function

    Public Function DetectGameFullscreen() As List(Of Object)
        Dim response As New List(Of Object)
        response = DetectFullscreenApplication()

        If response(0) = True Then
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

End Class
