# Architecture

This repository contains a Blazor feature ("YouTube Player") implemented with a strict, store-driven architecture.
The goal is: predictable state transitions, a single source of truth, and clean separation between UI and side effects.

## Principles

### Single Source of Truth
All feature state lives in `YouTubePlayerState` inside the `YouTubePlayerStore`.
The UI never mutates feature data directly.

### Unidirectional Data Flow
User/interop events are translated into **actions**.
Actions are processed by the store in two steps:

1. **Reduce**: pure state transition (no I/O)
2. **Effects**: side effects (DB, JS interop, async calls), optionally dispatching more actions

### UI Is Dispatch-Only
Razor components:
- render based on store state
- dispatch actions on user intent
- hold only local UI state (e.g. drawer open/close)

### Side Effects Live in the Store
Persistence (playlist/video CRUD), JS interop (YouTube iframe), and other asynchronous work is handled in the store effects.
Drawers and pages do not write to the database.

---

## Folder Overview (Feature)

Typical layout:

- `Features/YouTubePlayer/Store/`
  - `YouTubePlayerStore.cs`  
  Owns state, dispatch, reduce, and effects.
- `Features/YouTubePlayer/State/`
  - `YouTubePlayerState.cs` (root state)
  - sub-states (e.g. `PlaylistsState`, `QueueState`, `PlayerState`)
  - `YtAction.cs` (actions)
- `Features/YouTubePlayer/Models/`
  - domain models (e.g. `Playlist`, `VideoItem`)
- `wwwroot/js/`
  - `youtube-player-interop.js` (YouTube IFrame API interop)
  - `sortable-interop.js` (SortableJS integration)

---

## State Model

### Root State: `YouTubePlayerState`
Contains:
- `Playlists`: loading/empty/loaded (list of playlists)
- `Queue`: selected playlist + ordered list of videos + current index
- `Player`: player status (empty/loading/buffering/playing/paused)

The UI derives all view decisions from these values (e.g. show controls only if a video is selected).

### Queue
- `SelectedPlaylistId`: `Guid?`
- `Videos`: ordered list (sorted by `Position`)
- `CurrentIndex`: `int?` (current selection)

### Player
Represents playback lifecycle and is updated only via actions dispatched from JS interop (e.g. `PlayerStateChanged`, `VideoEnded`).

---

## Actions

Actions are the only input into the store.
Two categories:

### Commands (Intent)
Triggered by UI:
- `Initialize`
- `SelectPlaylist(playlistId)`
- `SelectVideo(index, autoplay)`
- `CreatePlaylist(...)`
- `AddVideo(...)`
- `SortChanged(oldIndex, newIndex)`

### Results (Loaded / Derived)
Triggered by effects:
- `PlaylistsLoaded(playlists)`
- `PlaylistLoaded(playlist)`
- optional error reporting (e.g. `OperationFailed(message)`)

Rule of thumb:
- **Commands** express what the user wants.
- **Results** represent completed I/O.

---

## Store Pipeline

### Dispatch
`Dispatch(action)` enqueues an action and processes it serially (important to avoid race conditions).

### Reduce (Pure)
The reducer returns a new immutable `YouTubePlayerState`.
No DB calls, no JS calls, no timing dependencies.

### Effects (Async)
Effects are triggered after reducing.
Effects may:
- call services (DB, HTTP)
- call JS interop
- dispatch additional actions (results or follow-up commands)

---

## UI Components

### Page (`YouTubePlayer.razor`)
Responsibilities:
- initialize the store after JS interop is ready
- render playlists, queue, and controls from store state
- dispatch actions on clicks
- forward JS callbacks into store actions (`PlayerStateChanged`, `VideoEnded`, etc.)

Allowed local UI state:
- drawer open flags
- SortableJS initialization flags (lifecycle concerns)

### Drawers
Drawers are “input components”:
- collect user input (e.g. playlist name, video URL)
- emit requests to the page/store
- do not persist data

---

## JS Interop

### YouTube Player Interop
`youtube-player-interop.js` owns:
- loading YouTube IFrame API
- creating/loading the player
- listening to state changes and forwarding events to .NET

The JS layer calls into .NET via `[JSInvokable]` methods.
The .NET side translates these callbacks into actions:
- `PlayerStateChanged(ytState, videoId)`
- `VideoEnded`

### SortableJS
SortableJS mutates the DOM. Blazor does not automatically know about such mutations.
Therefore:
- Sortable must be initialized/destroyed in a controlled way
- reorder results are reported to .NET and stored as state (via `SortChanged`)
- persistence of ordering is done in store effects

---

## Typical Flows

### App Start
1. UI initializes JS interop (DotNetObjectReference)
2. UI dispatches `Initialize`
3. Effect loads playlists -> `PlaylistsLoaded`
4. Optionally select first playlist -> `SelectPlaylist(...)`
5. Effect loads playlist -> `PlaylistLoaded(...)`
6. Optionally select first video -> `SelectVideo(0, false)`
7. Effect calls JS `loadVideo(...)`

### Select Playlist
1. UI dispatches `SelectPlaylist(id)`
2. Effect loads playlist -> `PlaylistLoaded(playlist)`
3. UI renders queue

### Select Video
1. UI dispatches `SelectVideo(index, autoplay)`
2. Reducer updates `Queue.CurrentIndex` and `Player` (e.g. Loading)
3. Effect calls JS `loadVideo(videoId, autoplay)`

### Sort Videos
1. JS reports reorder -> UI dispatches `SortChanged(oldIndex, newIndex)`
2. Reducer reorders `Queue.Videos`
3. Effect persists updated positions to DB

---

## Error Handling
Errors should not throw from UI event handlers.
Instead, the store can dispatch `OperationFailed(message)` and the UI can render an error banner/snackbar.

---

## Testing Notes
- Reducer can be unit-tested with input state + action -> output state.
- Effects can be tested by mocking services and verifying dispatched follow-up actions.
