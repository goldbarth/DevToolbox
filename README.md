
# 🧰 DevToolbox

> Ein Blazor-Server Portfolio- und Lernprojekt mit Fokus auf **saubere Architektur**, **store-getriebenes State-Management** und **kontrollierte JavaScript-Interop** – demonstriert an realistischen Features (z. B. YouTube Playlist Manager).

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat-square&logo=dotnet)
![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4?style=flat-square&logo=blazor)
![C#](https://img.shields.io/badge/C%23-14.0-239120?style=flat-square&logo=csharp)
![SQLite](https://img.shields.io/badge/SQLite-003B57?style=flat-square&logo=sqlite)

---

## 📋 Über das Projekt

DevToolbox ist ein **Portfolio-Showcase-Projekt**. Der Schwerpunkt liegt nicht darauf, möglichst viele Tools zu sammeln, sondern darauf, **komplexere UI-Features strukturiert, testbar und erweiterbar** umzusetzen – inklusive Persistenz, asynchroner Datenflüsse und JS-Interop.

**Ziel:** Vertiefung meiner Kenntnisse in Blazor Server, EF Core, modernem C# und Architektur-Entscheidungen rund um State-Management.

👉 **Architecture documentation**

- 🇬🇧 [Architecture (English)](DevToolbox/docs/en/ARCHITECTURE_EN.md)
- 🇩🇪 [Architektur (Deutsch)](DevToolbox/docs/de/ARCHITECTURE_DE.md)


## 🔄 Recently Worked On

<!-- START_RECENTLY_WORKED_ON -->
| Feature | Commit Date | Commit |
|---------|-------------|--------|
| feat: refactor YouTube player feature to strict store-driven state management | 2026-02-06 20:19:38 CET | [4da5354](https://github.com/goldbarth/DevToolbox/commit/4da53541ba51775c0d04a728bc6d1bab8679dd6c) |
| feat: add OnPlayerStateChanged method for handling YouTube player state changes | 2026-02-06 15:02:21 CET | [5511eee](https://github.com/goldbarth/DevToolbox/commit/5511eee15c2db1e7974ff611da61e2050caff35b) |
| feat: add PlayerState model for YouTubePlayer feature | 2026-02-06 15:01:53 CET | [31af266](https://github.com/goldbarth/DevToolbox/commit/31af2668cd3ee8f2aa35fdc92293986700fe9fae) |
| feat: implement YouTube Player with playlist and video management, add interop for sortable lists and playback controls | 2026-02-04 17:52:33 CET | [d31b79d](https://github.com/goldbarth/DevToolbox/commit/d31b79df00b28e1a6e04b567ce96384eeba90a09) |
| feat: add drawers for creating playlists and adding videos with responsive UI structures | 2026-02-04 17:52:14 CET | [5767720](https://github.com/goldbarth/DevToolbox/commit/5767720f8ee5db6889d5c726484a45b21523427a) |
<!-- END_RECENTLY_WORKED_ON -->

---

## ✨ Features

### 🎵 YouTube Playlist Manager (Architektur-Demonstrator)

Dieses Feature dient bewusst als **Komplexitätstreiber** für die Architektur. Es kombiniert UI-State, Persistenz, asynchrone Workflows und externe JS-APIs.

- **Playlist-Verwaltung**: Erstellen und organisieren eigener YouTube-Playlists
- **Integrierter Player**: YouTube IFrame API via kontrollierter JS-Interop
- **Drag & Drop**: SortableJS + bewusstes Lifecycle-Handling in Blazor
- **Persistenz**: Lokale Speicherung in SQLite (EF Core)
- **Explizites State-Management**: Store-getriebener Datenfluss über Actions / Reducer / Effects

### 🔜 Weitere geplante Tools
*Das Projekt wächst organisch. Neue Features entstehen nur, wenn sie architektonisch interessant sind und etwas „echte“ Komplexität mitbringen.*

## 🛠️ Tech Stack

| Technologie | Version | Verwendung |
|-------------|---------|------------|
| **.NET** | 10.0 | Backend & Frontend Framework |
| **C#** | 14.0 | Programmiersprache |
| **Blazor Server** | - | Interactive Web UI |
| **Entity Framework Core** | 10.0.2 | ORM für Datenbankzugriff |
| **SQLite** | 10.0.2 | Lokale Datenbank |
| **MudBlazor** | 8.15.0 | UI-Komponenten-Bibliothek |
| **ASP.NET Core MVC** | - | Routing & Navigation |

### Architektur-Highlights
- **Feature-basierte Organisation**: Jedes Tool als eigenständiges Feature
- **Store-getriebenes State-Management**: Zentraler Store als Single Source of Truth
- **Unidirektionaler Datenfluss**: Zustandsänderungen ausschließlich über explizite Actions
- **Side-Effect Isolation**: DB-Zugriffe und JS-Interop in Effects gekapselt
- **Controlled JS Interop**: YouTube IFrame API + SortableJS ohne „hidden state“

## 🚀 Installation & Setup

### Voraussetzungen
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) oder höher

## 📁 Projektstruktur
```
DevToolbox/
├── Components/             # Wiederverwendbare Blazor-Komponenten
│   ├── Layout/             # Layout-Komponenten (NavMenu, MainLayout)
│   ├── Pages/              # Seiten (Home, Error, NotFound)
│   └── App.razor           # Root-Komponente
├── Data/                   # Datenzugriffsschicht
│   ├── ApplicationDbContext.cs    # EF Core DbContext
│   └── EntityMapping/      # Fluent API Konfigurationen
└── Features/               # Feature-Module
│   └── YouTubePlayer/      # YouTube Player Feature
│       ├── Components/     # Feature-spezifische Komponenten
│       ├── Models/         # Domain Models
│       ├── State/          # State slices + actions
│       ├── Store/          # store + reducer + effects
│       └── YouTubePlayer.razor
├── Migrations/             # EF Core Migrationen
├── wwwroot/                # Statische Assets (CSS, JS, Bilder)
│   ├── css/
│   └── js/
├── Program.cs              # Anwendungs-Einstiegspunkt
└── appsettings.json        # Konfiguration
```

## Design-Prinzipien
- Feature-Slices: Jedes Feature ist in sich geschlossen
- Separation of Concerns: UI, State, Side-Effects und Persistenz sind getrennt
- Single Source of Truth: Feature-State wird zentral im Store verwaltet
- Explizite State-Transitions: Änderungen passieren ausschließlich über Actions
- Dependency Injection: Lose Kopplung über DI

## 🎯 Verwendung
### YouTube Playlist Manager

1. Neue Playlist erstellen
- Name und Beschreibung hier eingeben

2. Videos hinzufügen
- Wähle eine Playlist aus
- Hinzufügen eines Videos per YouTube-URL

3. Videos abspielen & organisieren
- Videos werden direkt im Player abgespielt
- Ziehe Videos per Drag & Drop, um die Reihenfolge zu ändern

## 🤝 Mitwirken

Da dies ein persönliches Portfolio-Projekt ist, nehme ich derzeit keine Pull Requests an. Feedback und Anregungen sind jedoch immer willkommen!

## 📜 Lizenz
Dieses Projekt ist unter der MIT-Lizenz lizenziert.

## 👤 Kontakt
### 📧 Kontakt
- [![Portfolio](https://img.shields.io/badge/GitHub%20Pages-121013?logo=github&logoColor=white)](https://goldbarth.github.io/Portfolio/#/)
- [![LinkedIn](https://custom-icon-badges.demolab.com/badge/LinkedIn-0A66C2?logo=linkedin-white&logoColor=fff)](https://www.linkedin.com/in/felix-wahl-6763791b9/)
- [<kbd>E-Mail</kbd>](mailto:felix.wahl@live.de)
---

*Entwickelt mit ❤️ und .NET*

<!--
## 🎨 Zusätzliche Empfehlungen

Für ein noch professionelleres Portfolio-Projekt würde ich empfehlen:

1. **Screenshots hinzufügen**: Erstelle einen `/docs` oder `/screenshots` Ordner
2. **GitHub Actions**: CI/CD Pipeline für automatische Builds
3. **Contributing Guidelines**: `CONTRIBUTING.md` für Entwickler-Standards
4. **Code of Conduct**: `CODE_OF_CONDUCT.md`
5. **Changelog**: `CHANGELOG.md` für Versionshistorie
6. **Demo-Link**: Hoste das Projekt auf Azure/AWS und füge den Link hinzu
-->

