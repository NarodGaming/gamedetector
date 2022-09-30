# V1 to V2
Here's a list of the important changes from V1 to V2 to help you migrate.

All code examples are in VB.NET

## Namespace change

In V1, you used to import:
```vb.net
Imports FullScreenDetection
```

In V2, you now import:
```vb.net
Imports Narod.FullscreenDetection
```

## Object name change

In V1, you used to create an object instance like:
```vb.net
Dim FullscreenDetectClient as New FullscreenDetecter
```

In V2, you now create an object instance like:
```vb.net
Dim FullscreenDetectClient As New FullscreenDetector
```

## Change from 2D list to clean getters

In V1, you got an output from the function directly like:
```vb.net
Dim detectionappresponse As List(Of Object) = FullscreenDetectClient.DetectFullscreenApplication()
```

In V2, you now get an output like:
```vb.net
FullscreenDetectClient.DetectFullscreenApplication()

Dim hasDetectedFullscreenApplication as Boolean = FullscreenDetectClient.GetHasDetected()
Dim programNameDetected As String = FullscreenDetectClient.GetProgramDetected() // throws nullreferenceexception if GetHasDetected is False
Dim processIDDetected As UInteger = FullscreenDetectClient.GetProcessIDDetected() // throws nullreferenceexception if GetHasDetected is False
```

## Removal of functions

In V1, you could use any of the following functions:
```vb.net
Dim detectionappresponse As List(Of Object) = FullscreenDetectClient.DetectFullscreenApplication()
Dim detectiongameresponse As List(Of Object) = FullscreenDetectClient.DetectGameFullscreen()
Dim customappresponse As List(Of Object) = FullscreenDetectClient.DetectCustomFullscreen("(partial/complete)-window-title-of-application-here")
```

In V2, you can now only use the following function:
```vb.net
FullScreenDetectClient.DetectFullscreenApplication()
```

You should instead check the program title yourself within your own program to see if it's what you are looking for. This way you don't even need to store it locally, should you wish:
```vb.net
If (FullScreenDetectClient.GetProgramDetected() = "Google Chrome") Then
    Console.WriteLine("Google Chrome detected!")
End If
```