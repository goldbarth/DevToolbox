using DevToolbox.Features.YouTubePlayer.Models;

namespace DevToolbox.Features.YouTubePlayer.State;

public record QueueState(
    Guid? SelectedPlaylistId,
    IReadOnlyList<VideoItem> Videos,
    int? CurrentIndex)
{
    public bool HasSelection => SelectedPlaylistId is not null;
    public bool HasVideo  => CurrentIndex is not null;
}