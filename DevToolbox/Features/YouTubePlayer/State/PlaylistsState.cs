using DevToolbox.Features.YouTubePlayer.Models;

namespace DevToolbox.Features.YouTubePlayer.State;

public abstract record PlaylistsState
{
    public sealed record Loading : PlaylistsState;
    public sealed record Loaded(IReadOnlyList<Playlist> Items) : PlaylistsState;
    public sealed record Empty : PlaylistsState;
    public sealed record Error(string Message) : PlaylistsState;
}