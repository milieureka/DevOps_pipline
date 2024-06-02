# RobotDodge Game

## Language:
C#, SplashKitSDK, .NET Application.

## Overview
The Robot Dodge game will have a player, represented by a bitmap on the screen, that can move around the screen and dodge incoming robots. Additionally, the player can shoot bullets that destroy the robots. The idea is to survive as long as you can. A final of this game running is shown below.

*Clicking the thumbnail will take you to YouTube. To return, simply click the browser's back button.*
[![Watch the video](https://img.youtube.com/vi/8gNv2OVNh60/hqdefault.jpg)](https://www.youtube.com/watch?v=8gNv2OVNh60)

**UML Diagram** 

![Robot Dodge UML Diagram](images/RobotDodge_UML%20Class%20diagram.png "Robot Dodge UML Class Diagram")

## Object-Oriented Programming (OOP) Concepts Details

**Main Entry: Program Class**

The Program class acts as the entry point of the application. It contains the Main() method, which initializes the game environment and starts the main game loop.

This class directly instantiates the RobotDoge class, thereby kickstarting the game management process.

**Core Game Management: RobotDoge Class**

The RobotDoge class serves as the central game controller. It is responsible for initializing game components like the Player and Robots, managing game states, processing user inputs, updating game logic, detecting collisions, and rendering the game objects on the screen.

Integration:

- Composition: Manages the lifecycle of Player and Robots, ensuring they are correctly initialized, updated, and rendered each frame.
- Method Calls: Regularly invokes methods from Player and Robot classes such as Update(), Draw(), and handles collisions.
  
**Player Interactions: Player Class**

Represents the user's character in the game. Responsible for handling player inputs, moving according to those inputs, shooting bullets, and displaying health status.

Properties: Includes bitmaps for visual representation and a list of Bullet objects to manage 
ammunition.

Integration:

- Event Handling: Processes keyboard and mouse inputs to control player movements and shooting.
- Bullet Management: Creates and manages Bullet instances, updating their positions and rendering them.
- Collisions: Checks for collisions with robots using its CollidedWith() method.

**Dynamic Obstacles: Robot (Abstract) and Subclasses**

The abstract Robot class provides a template for enemy characteristics and behaviors, which are specialized in subclasses like RoboA and RoboB.

Integration:

- Inheritance: RoboA and RoboB inherit from Robot, each implementing specific movement patterns, attack strategies, and rendering methods.
- Polymorphism: Utilizes overridden methods to exhibit different behaviors, enhancing gameplay variety.

**Projectile Mechanics: Bullet Class**

Handles the dynamics of bullets fired by the player, including movement, rendering, and collision detection.

Properties: Stores state information such as position, velocity, and direction.

Lifecycle Management: Managed by the Player class, which creates, updates, and deletes bullets based on game logic.

