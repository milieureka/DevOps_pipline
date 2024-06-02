using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace RobotDodge
{
    public class RobotDodge
    {
        private Player _player;
        private Window _gameWindow;
        private List<Robot> _robots = new List<Robot>();

        public int Lives //Lives property
        {
            get { return _player.Lives; }
        }

        public bool Quit
        {
            get
            {
                return _player.Quit;
            }
        }

        public RobotDodge(Window gameWindow)
        {
            _gameWindow = gameWindow;
            _player = new Player(gameWindow);
        }

        public void HandleInput()
        {
            _player.HandleInput();
            _player.StayOnWindow(_gameWindow);
        }
        public void Draw()
        {
            _gameWindow.Clear(Color.White);

            foreach (Robot robot in _robots)
            {
                robot.Draw();
            }

            _player.Draw();
            _gameWindow.Refresh();
        }

        public Robot RandomRobot()
        {
            int randomChoice = SplashKit.Rnd(3); 

            switch (randomChoice)
            {
                case 0:
                    return new Boxy(_gameWindow, _player);
                case 1:
                    return new Roundy(_gameWindow, _player);
                case 2:
                    return new Swingy(_gameWindow, _player);
                default:
                    return new Boxy(_gameWindow, _player); // Default case
            }
        }

        public void Update()
        {
            foreach (Robot robot in _robots)
            {
                robot.Update();
            }

            _player.UpdateBullets();

            if (SplashKit.Rnd() < 0.002)//Change the speed of robot for recorsding purpose
            {
                _robots.Add(RandomRobot());
            }

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            List<Robot> _removedRobots = new List<Robot>();
            List<Bullet> _removedBullets = new List<Bullet>();

            foreach (Robot robot in _robots)
            {
                if (_player.CollideWith(robot) || robot.IsOffscreen(_gameWindow))
                {
                    _removedRobots.Add(robot);
                    _player.Lives--; //Adding a logic for decreasing live when player collides with a robot
                }

                foreach (Bullet bullet in _player.Bullets) //Circles Intersection detect and remove if Bullet and Robot collide
                {
                    if (SplashKit.CirclesIntersect(bullet.CollisionCircle, robot.CollisionCircle))
                    {
                        _removedRobots.Add(robot);
                        _removedBullets.Add(bullet);
                    }
                }
            }

            foreach (Robot robot in _removedRobots)
            {
                _robots.Remove(robot);
            }

            foreach (Bullet bullet in _removedBullets)
            {
                _player.Bullets.Remove(bullet);
            }
        }
    }
}
