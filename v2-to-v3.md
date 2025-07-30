# V2 to V3
Here's a list of important changes from V2 to V3 to help you migrate

All code examples are in C#

## Namespace change

In V2, you used to import:
```c#
using FullscreenDetection
```

In V3, you now import:
```c#
using Narod.FullscreenDetection
```

NOTE: This was supposed to already be changed, but due to an error, it wasn't.

## New function names

All function names now use camel case, and have slightly tweaked function names.

V2:
```c#
GetHasDetected();
GetProgramDetected();
GetProcessIDDetected();
DetectFullscreenApplication();
```

V3:
```c#
getHasDetected();
getProgramDetected();
getProcessIDDetected();
getProgramDetails();
detectFullscreenApplication();
```

## New getter

In V2, to get outputs, you'd have to run several getters:
```c#
string appName = fullscreenDetector.GetProgramDetected();
bool detection = fullscreenDetector.GetHasDetected();
int pID = fullscreenDetector.GetProcessIDDetected();
```

In V3, you can now, in addition to those getters, use one to fetch a single struct:
```c#
appDetails checkedApp = fullscreenDetector.getProgramDetails();
string appName = checkedApp.windowTitle;
bool detection = checkedApp.detected;
int processID = checkedApp.processID;
```

## New fetch getter functionality

In V2, all getters would require you to run a detection first, e.g.
```c#
fullscreenDetector.DetectFullscreenApplication();
string appName = fullscreenDetector.GetProgramDetected();
```

In V3, all getters by default will run a new scan. This can be turned off as well.
```c#
string appName = fullscreenDetector.getProgramDetected(); // will run a detection first
string appName = fullscreenDetector.getProgramDetected(false); // will NOT run a detection first
```

## Amended exception types

In V2, getters would throw NullReferenceException. In V3, getters will instead throw InvalidOperationException.