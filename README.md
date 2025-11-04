# Unity Developer Test Task - Leaderboard Popup

## Decription

This project is an implementation of a simple app that shows a leaderboard loaded from JSON file, parses and shows players data. Also downloaded avatars from server and caches locally.

## Packages and plugins in use

- **Zenject** - framework for implementation of **DI**, **Event Bus pattern** (via Signals/Commands) and **Factory Pattern** (Zenject Factories + Unity Object Pool);
- **DOTween** - package for simple and fast animations;
- **Addressables** - package for assets organization and managment.

## Modular system
The project is dived in several **Modules**. Each **Module** is a folder with Assembly Definition file used to organize the codebase and promote modularity and reusability. Also improves compilation performance by ensuring Unity recompiles only the assemblies that have changed, rather than the entire project.

<img width="789" height="320" alt="image" src="https://github.com/user-attachments/assets/c350ac01-fc0a-4314-a757-f03607517ce1" />

# Modules
## Module.Core
Serves as the kernel of the application, defining the fundamental architectural patterns. It includes base installers (Launchers), an implementation of the MVC pattern with its core classes, base implementations of the Factory pattern, and the Event Bus system with Command and Signal abstractions. Additionally, it provides utility functions and UI helpers used across other modules.
### CommandSignal
Provides interfaces `ISignal`, `ICommand`, and `ICommandWithParameter` used for handling events within the Event Bus system.
### Factory
Provides the base class `PooledFactoryBase<T>` for implementing concrete factories with create and release APIs. Utilizes Zenject factories together with Unity’s ObjectPool for efficient object pooling.
### Launcher
Provides the base class `LauncherInstaller`, an extension of Zenject’s MonoInstaller, designed to streamline setup by separating the installation of Components, Signals, Factories, and Services.
### MVC
Defines the foundational classes for implementing the MVC pattern. The core class is `ComponentControllerBase<TModel, TView>`. To create an MVC component, a class should inherit from ComponentControllerBase and optionally define a Model (derived from `ModelBase`) and a View (derived from `ViewBase`).
### UI
Provides helper components for UI. 
### Utils
Provides assembly helpers and component exntentions.
## Module.Common
Serves as the 3rdParty assets and packages container and provides Constant variables (`GlobalConstants1 static` class).
## Module.PopupService
Serves as the codebase for popups initialization from Addressables system. Contains original PopupService with several modifications (explained later in the README).
### Addressables
Defines interfaces and base classes for instantiating GameObjects from Addressables system and automatic injection to Zenject DI container.
### Launcher
Provides a Launcher for installing all components, services and signals related to this **Module**.
### PopupInitialization
Contains original `IPopupInitialization` interface for initializing popups
### Services
Contains modified version of original `PopupManagerService` implementation of `IPopupManagerService`. Provides API for opening and closing popups from Addressables system with automatic injection.
## Module.Bootloader
The entry point of the application. Initializes core systems — currently includes a simple loading sequence with a 1-second delay, as no external SDKs or subsystems are required at startup.
### CommandSignal
Defines commands and signals used in this **Module**.
### Controllers
Defines MVC components used in this **Module**. `LoadingScreenController` - MVC component for showing the first screen in the app's entry point.
### Launcher
Provides a Launcher for installing all components, services and signals related to this **Module**.
### Services
Contains `BootloaderService` that manages the app's entry point, initializes all systems and loads main scene.
## Module.App
The main application module that contains all leaderboard-related codebase, prefabs and sprites.
### CommandSignal
Defines commands and signals used in this **Module**.
### Controllers
Defines MVC components used in this **Module**. 
- `LeaderboardController` - MVC component for setting up and showing the leaderboard content;
- -`LeaderboardRecordController` - MVC component for showing one single record of leaderboard, contains logic for displaying player data (player name, rank, score and avatar image).
### Data
Contains leaderboard related data structures
### Factory
Contains an inherited from `PooledFactoryBase` class `LeaderboardRecordPooledFactory` for spawning records in leaderboard.
### Launcher
Provides a Launcher for installing all components, services and signals related to this **Module**.
### Services
- `IAvatarCacheService` and it's `AvatarCacheService` implementation for caching avatars. When an avatar is requested, the service first checks the in-memory cache, then attempts to load it from disk (Application.persistentDataPath), and finally downloads it from the web if not found locally.
- `LeaderboardDataService` is a service responsible for parsing JSON data.
### Utils
Contains a static `Utils` class for managing player rank (or `type` as defined in JSON structure) related data, such as retrieving font size and color based on rank.

# PopupManagerService modification
The original implementation of Popup service was responsible for two things: instantiating the popup from Addressables and initializing it via `IPopupInitialize`. The method `OpenPopup` breaks SRP principle of SOLID since the initialization might be done by some other services, for example, the service that first needs to Inject the Addressable GO into the DI container, handle it's own initialization via `Zenject.IInitializable`. So it was decided to decouple the logic of this service by letting the PopupManagerService to first load the Addressable GO
```C#
var popup = await _loader.LoadAsync(name);
```
and then inject the popup into DI so Zenject's initialization is handled first
```C#
await _injector.Initialize(popup, param);
```
The `ZenjectPopupInitializer` implementation of `IAddressableInjection` handles the injection and only then `IPopupInitialize`'s implementation gets called
```C#
_container.InjectGameObject(popup);

            var initComponents = popup.GetComponents<IPopupInitialization>();
            foreach (var component in initComponents)
                await component.Init(param);
```



