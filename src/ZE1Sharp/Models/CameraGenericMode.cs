namespace ZE1Sharp.Models
{
    public enum CameraGenericMode
    {
        Playback,
        Still,
        Movie
    }

    public enum CameraSpecificMode
    {
        Playback,
        PlaybackPlaying,
        PlaybackPaused,

        Still,
        StillTimelapseCapturing,
        StillTimelapseIdle,
        StillIdle,
        StillBurst,

        Movie,
        MovieRecording,

        Unknown

    }
}