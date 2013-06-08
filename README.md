UniStat
=========
Unity 4+ C# Framework


What started out as a collection of classes and third party scripts copied from one project to another, eventually turned into a useful little framework that I thought should be shared with the wider Unity community.

I find this collection of scripts essential for all my new games and saves a lot of time, I hope that others find it useful :)

Please contact me if you wish to help make this better! No, really, because it's pretty terrible. I will gladly give collaborator to anyone with good ideas and a passion for Unity/C#.

PLEASE NOTE: UniStat is built to work with NGUI pretty closely, but if you don't use NGUI, you can delete the "DataBinding" folder as well as LoadingScene and still use the rest of the features.

Features
=====
* Generic data model: Keep all your game data in one place, whatever it may be (List, String, Dictionary or anything), persist it between scenes and save the game state perfectly to disk every time.
* NGUI Bindings: Bind a UILabel to your data model and it will automatically update when the model does.
* Scene-Mode-View or "SMV" design pattern (c)2013 Me
* Library of stuff I need often, ie. ColorHSV, Perlin & Simplex Noise Generators, Random name generators etc.
* Tested on PC, Mac, Linux and iOS. Cannot promise Android until I test it ;)

Quick Start Tutorial
=====
Create a new Unity project, and import NGUI and UniStat into the root of your assets. Setup your preferred folder structure, an example of what I generally use is:

![Folder Structure Example](http://i.imgur.com/MH4jUoY.png)

At the heart of every game, is of course the Player. Create a Player.cs file in your "Objects" folder.

    using UnityEngine;
    using System.Collections;
    
    public class Player {
      public string Name;
    }

Then there is the Game itself, create a Game.cs under Objects. This will be the core of your game.

    using UnityEngine;
    using System.Collections;
    using UniStat;
    
    public static class Game
    {
      public static Player CurrentPlayer
      {
        get {
          return GameData.Get<Player>("CurrentPlayer");
        }
        set {
          GameData.Put("CurrentPlayer",value);
        }
      }
      
      public static void StartNew()
      {
        Player player = new Player();
        player.Name = "Player 1";
        
        CurrentPlayer = player;
      }
    }
    
Now we'll create a Scene controller, in your Scripts/Scenes folder create a MainScene.cs file.

    using UnityEngine;
    using System.Collections;
    using UniStat;
    
    public class MainScene : Scene
    {
      protected override void Setup()
      {
        DefaultMode = new DefaultMode();
        
        if(Game.CurrentPlayer == null)
          Game.StartNew()
      }
      
      protected override void UpdateScene()
      {
        //Per-frame operations that are global to the scene can go here
      }
    }

And finally a default mode.

    using UnityEngine;
    using System.Collections;
    using UniStat;
    
    public class DefaultMode : Mode
    {
      public override void Activated(){
        Debug.Log("Default mode has been activated!");
        //Trigger some animations or enable an NGUI panel.
      }
      
      public override void Update(){
        //Per-frame modal operations go here
      }
      
      public override void OnGUI(){
        //If you really need to use OnGUI (don't), you can do it per-mode here, and it will magically disappear when the mode switches.   
      }
      
      public override void Cleanup(){
        //Called when this mode is deactivated. Hide the panel or trigger animations.
        Debug.Log("Default Mode was active for " + ElapsedTime.ToString("0.0") + " seconds";
      }
    }

Create an Empty GameObject

![Empty GameObject](http://i.imgur.com/q8rArUB.png)

Rename the object to "Scene", click "Add New Component" and select your Scene class.

![Scene](http://i.imgur.com/xSoQN9G.png)

Now run your scene in the Unity editor, you should see a console log saying the scene is activated, Hooray!
