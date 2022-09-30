# Narod's Game Detector
A .NET library written in VB.NET to detect full screen applications and games.

## Summary
Searches for on-screen fullscreen windows on the currently focussed screen.

## Usage (V1)
V1 has been replaced by V2. If you're still using 1.2.0 or older, please [take a look here for instructions.](https://github.com/NarodGaming/gamedetector/blob/main/README-v1.md)

## Usage (V2)
Code examples in VB.NET
- Download the latest release from the releases tab, or find in the NuGet package manager ([Narod.FullscreenDetector](https://www.nuget.org/packages/Narod.FullscreenDetector))
    - (Only required if downloaded from releases) Add a reference of the library in your project. (Project -> Add Reference... -> Browse -> Browse...)
- Import in to your program
```vb.net
Imports Narod.FullscreenDetection
```
- Create instance of FullscreenDetecter
```vb.net
Dim FullscreenDetectClient As New FullscreenDetector
```
- Call with the DetectFullscreenApplication function.
```vb.net
FullscreenDetectClient.DetectFullscreenApplication()
```
- Get output
```vb.net
Dim hasDetectedFullscreenApplication as Boolean = FullscreenDetectClient.GetHasDetected()
Dim programNameDetected As String = FullscreenDetectClient.GetProgramDetected() // throws nullreferenceexception if GetHasDetected is False
Dim processIDDetected As UInteger = FullscreenDetectClient.GetProcessIDDetected() // throws nullreferenceexception if GetHasDetected is False
```

## Further Help
I'll release a video at some point detailing how to use V2. My current video is for V1 which is significantly different, so those instructions won't work.

## Helping
If you find any bugs, please report it as an issue.

To-do:
- DirectX checks (if possible)
- Fix issues
