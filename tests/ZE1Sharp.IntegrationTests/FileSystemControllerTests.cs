namespace ZE1Sharp.IntegrationTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Xunit;

    using ZE1Sharp.Models;

    public class StillControllerTests
    {
        [Fact]
        public async Task CaptureAsync()
        {
            var controller = new CameraController();
            var mode = await controller.GetModeAsync();
            if (mode != CameraSpecificMode.StillIdle)
            {
                await controller.SetModeAsync(CameraGenericMode.Still);
            }

            var countBeforeCapture = await controller.Still.GetRemainingCountAsync();
            var file = await controller.Still.CaptureSingleAsync();
            var countAfterCapture = await controller.Still.GetRemainingCountAsync();
            Assert.Equal(countBeforeCapture - 1, countAfterCapture);

            var stream = await controller.FileSystem.DownloadThumbnailAsync(file);
            Assert.Equal("image/jpeg", stream.MimeType);
        }
    }

    public class SettingsControllerTests
    {
        [Fact]
        public async Task ReadSSIDAsync()
        {
            var controller = new CameraController();
            var setting = await controller.Settings.SSID.ReadAsync();
            Assert.Equal("unknown", setting.Value.Value);
            Assert.True(setting.Value.ReadOnly);
        }

        [Fact]
        public async Task ReadAndSetPhotoSizeToWrongValueAsync()
        {
            var controller = new CameraController();
            var setting = await controller.Settings.PhotoSize.ReadAsync();
            var valueBefore = setting.Value.Value;
            await Assert.ThrowsAsync<ArgumentException>(async () => await setting.SetAsync("foo"));

            setting = await controller.Settings.PhotoSize.ReadAsync();
            Assert.Equal(valueBefore, setting.Value.Value);
        }

        [Fact]
        public async Task ReadAndSetPhotoSizeToAllowedValueAsync()
        {
            var controller = new CameraController();
            var setting = await controller.Settings.PhotoSize.ReadAsync();
            var valueBefore = setting.Value.Value;
            var chosenValue = setting.Value.Options.First(v => v != valueBefore);
            await setting.SetAsync(chosenValue);

            Assert.Equal(chosenValue, setting.Value.Value);
            setting = await controller.Settings.PhotoSize.ReadAsync();
            Assert.Equal(chosenValue, setting.Value.Value);
        }
    }

    public class CameraControllerTests
    {
        [Fact]
        public async Task GetInfoAsync()
        {
            var controller = new CameraController();
            var info = await controller.GetInfoAsync();
            Assert.Equal("E1", info.Model);
        }

        [Fact]
        public async Task SetDateTimeAsync()
        {
            var controller = new CameraController();
            await controller.SetDateTimeAsync(DateTime.Now.Subtract(TimeSpan.FromDays(3)));
        }

        // These two tests shuts down and reboots the camera. Makes it hard to run the rest of the tests.
        /*
        [Fact]
        public async Task ShutdownAsync()
        {
            var controller = new CameraController();
            await controller.ShutdownAsync();
        }

        [Fact]
        public async Task RebootAsync()
        {
            var controller = new CameraController();
            await controller.RebootAsync();
        }
        */
    }
}
