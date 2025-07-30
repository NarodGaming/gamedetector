# Narod's Game Detector
A .NET library written in C# to detect full screen applications and games.

## Summary
Searches for on-screen fullscreen windows on the currently focussed screen.

## Usage (V2)
V2 has been replaced by V3. If you're still using 2.0.1 or older, please [take a look here for instructions.](https://github.com/NarodGaming/gamedetector/blob/main/README-v2.md)

## Usage (V3)
Code examples in C#
- Download the latest release from the releases tab, or find in the NuGet package manager ([Narod.FullscreenDetector](https://www.nuget.org/packages/Narod.FullscreenDetector))
    - (Only required if downloaded from releases) Add a reference of the library in your project. (Project -> Add Reference... -> Browse -> Browse...)
- Import in to your program
```c#
using Narod.FullscreenDetection;
```
- Create instance of FullscreenDetector
```c#
FullscreenDetector fullscreenDetector = new FullscreenDetector();
```
- Call with the DetectFullscreenApplication function.
```c#
fullscreenDetector.detectFullscreenApplication()
```
- Get output
```c#
appDetails checkedApp = fullscreenDetector.getProgramDetails();
string appName = checkedApp.windowTitle;
bool detection = checkedApp.detected;
int processID = checkedApp.processID;
```

## Further Help
There's a *very* simple test program included in the solution (called DetectorExample).

## Helping
If you find any bugs, please report it as an issue.

To-do:
- DirectX checks (if possible)
- Fix issues
