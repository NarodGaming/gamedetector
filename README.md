# Narod's Game Detector
A .NET library written in VB.NET to detect full screen applications and games.

## Usage
Code examples in VB.NET
- Download the latest release from the releases tab.
- Add a reference of the library in your project. (Project -> Add Reference... -> Browse -> Browse...)
- Import in to your program
```vb.net
Imports FullScreenDetection
```
- Create instance of Fullscreendetecter
```vb.net
Dim FullscreenDetectClient as New FullscreenDetecter
```
- Call with either the DetectFullscreenApplication function, or DetectGameFullscreen.
```vb.net
Dim detectionappresponse As List(Of Object) = FullscreenDetectClient.DetectFullscreenApplication()
Dim detectiongameresponse As List(Of Object) = FullscreenDetectClient.DetectGameFullscreen()
```

Both functions will return the a list of objects:
* Position 0 is a boolean, which will be True if it successfully detected something, or False if not.
* Position 1 is a string, with the window text of the program detected (if detected, otherwise no item added)
* Position 2 is a UInteger, with the process PID of the program detected (if detected, otherwise no item added)

DetectFullscreenApplication will return any full screen application.
DetectGameFullscreen will filter out select programs that checks are in place for (e.g. web browsers).

## Helping
If you find any bugs, please report it as an issue.

To-do:
- DirectX checks (if possible)
- Fix issues