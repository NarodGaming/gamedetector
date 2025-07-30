# Narod's Game Detector
A .NET library written in C# to detect full screen applications and games.

## Summary
Checks if the currently focussed window is a fullscreen application.

## Versioning
All major versions (1.x.x, 2.x.x, 3.x.x) do not have directly compatible code, so upgrading between these will require modifications to your code.

V3 supports both .NET 8.0+ and .NET 4.7.2+, so you should target this for new projects. Existing projects should target V3 as soon as reasonable.

Previous major versions have not received updates after the next major version, although I'm not necessarily against this if required for a good reason.

## Usage (V1)
V1 has long been replaced by V2 & V3. You should really upgrade to a newer version, [however you can find the old instructions here.](https://github.com/NarodGaming/gamedetector/blob/2.0.1/README-v1.md)

## Usage (V2)
V2 has been replaced by V3. If you're still using 2.0.1 or 2.0.0, please [take a look here for instructions.](https://github.com/NarodGaming/gamedetector/blob/main/README-v2.md)

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
