# About
This repository contains a heavily commented project that demonstrates a use of the [AlchemyBow.Core](https://github.com/kempnymaciej/alchemy-core) framework on the example of the classic snake game.

# Instalation
1. Clone the repository.
2. Run the project with Unity 2021.3.2f1 (or any 2020+)

# Manual
The purpose of creating this project was to present various aspects of the [AlchemyBow.Core] framework. Feel free to explore it.

If you're looking for an example of a specific feature, here's the list (All path are relative to the `Assets` folder.):
1. CoreProjectContext
    * ...\_Scripts\Core\Elements\SnakeGameProjectContext.cs
    * ...\Resources\Core\SnakeGameProjectContext.prefab
2. CoreController
    * ...\_Scripts\Core\SnakeGameCoreController.cs
    * ...\_Scripts\Core\Elements\MenuScene\MenuSceneCoreController.cs
    * ...\_Scripts\Core\Elements\GameScene\GameSceneCoreController.cs
3. ICoreLoadable
    * ...\_Scripts\Core\Elements\SnakeGameProjectContext.cs
    * ...\_Scripts\Core\Elements\MenuScene\MenuSceneCoreController.cs
    * ...\_Scripts\Core\Elements\GameScene\GameSceneCoreController.cs
4. ICoreLoadingCallbacksHandler
    * ...\_Scripts\ViewControllers\MainMenuViewController.cs
    * ...\_Scripts\ViewControllers\MenuHintViewController.cs
    * ...\_Scripts\ViewControllers\Installers\MenuSceneViewControllersInstaller.cs
5. Scene Changing
    * ...\_Scripts\Core\SnakeGameCoreController.cs
    * ...\_Scripts\Core\Elements\MenuScene\MenuSceneCoreController.cs
    * ...\_Scripts\Core\Elements\GameScene\GameSceneCoreController.cs
    * ...\_Scripts\Core\SceneTriggers\ ...
6. States
    * ...\_Scripts\Core\Elements\GameScene\GameSceneCoreController.cs
    * ...\_Scripts\Core\Elements\GameScene\ ...
    * ...\_Scripts\Core\StateConditions\ ...
7. Bind<T>(T value)
    * ...\_Scripts\ViewControllers\Installers\GameSceneViewControllersInstaller.cs
8. Bind(Type key, object value)
    * ...\_Scripts\Views\ViewsInstaller.cs
9. BindInaccessible(object instance);
    * ...\_Scripts\ViewControllers\Installers\MenuSceneViewControllersInstaller.cs
10. Dynamic Collection Binding
    * ...\_Scripts\ViewControllers\Installers\MenuSceneViewControllersInstaller.cs