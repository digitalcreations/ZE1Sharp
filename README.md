# ZE1Sharp

.NET PCL implementation of the [Z Camera E-1 API](https://github.com/digitalcreations/Z-Camera-Doc/blob/master/http.md).

## Usage

```
PM> Install-Package ZE1Sharp
```

The API requires that you are connected to the camera's WiFi. Since the camera has a static IP on that network, no configuration is necessary.

Example taken almost directly from our integration tests:

```cs
var controller = new CameraController();
var mode = await controller.GetModeAsync();
if (mode != CameraSpecificMode.StillIdle)
{
    await controller.SetModeAsync(CameraGenericMode.Still);
}

var picturesRemaining = await controller.Still.GetRemainingCountAsync();
var file = await controller.Still.CaptureSingleAsync();
var thumbnail = await controller.FileSystem.DownloadThumbnailAsync(file);
```

Note that newing up the `CameraController` does not attempt a connection to the camera. You'll need to try calling methods before that.

## Features

Control almost any feature exposed by the E-1 API. You can:

- read camera information
- set the camera mode
- set all the camera settings with the exception of the autofocus area
- take still pictures
- take timelapse series
- start and stop recording
- restart/shutdown the camera (but you won't be able to turn it on again)

Missing at the moment (PRs welcome):

- autofocus area (API returns strange values in 0.16 firmware)
- format SD card
- check if SD card is present
- magnify feature

## Reading/writing settings

The E-1 API specify that you must read settings before writing them. This is implemented with the following pattern in .NET:

```cs
var setting = await controller.Settings.PhotoSize.ReadAsync();
// photosize is a option setting, so its value has a few properties:
var currentValue = setting.Value.Value;
var newValue = setting.Value.Options.First();
```

Since the possible values for a setting can change, we can't really hard code constants that is guaranteed to work with future API versions. Therefore, for range settings, you need to find an appropriate value from the `Options` dynamically at runtime.

To save a setting, use the setting object you got back and `SaveAsync()`:

```cs
await setting.SaveAsync(newValue);
```

If you try to set a value that is not valid, the library should throw an exception before trying to send the command to the camera.

Range and string settings work similarly.

## Tested with

Tested to work well with 0.16 firmware, but should also work fine with 0.20.

## Developed by

This library is maintained by [Digital Creations AS](http://www.digitalcreations.no). We have no connection to Imaginevision, and this library is provided for use at your own risk.