using System;
using SplashKitSDK;

namespace RobotDodge
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Game Window", 800, 600);
            RobotDodge game = new RobotDodge(gameWindow);

            while (!gameWindow.CloseRequested && !game.Quit && game.Lives != 0)//Adding another condition the game stay while user still have live
            {
                SplashKit.ProcessEvents();

                game.HandleInput();
                game.Update();
                game.Draw();
            }
            gameWindow.Close();
        }
    }
}
