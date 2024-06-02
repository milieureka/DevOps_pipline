using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace RobotDodge
{
    public class Player
    {

        private Bitmap _playerBitmap;
        private Bitmap _heartBitmap; //Create _heart field
        private SplashKitSDK.Timer _gameTimer = new SplashKitSDK.Timer("Game Timer");  // Initialize the timer
        private double _angle;//Initialize angle
        private List<Bullet> _bullets = new List<Bullet>(); //Initilize bullet lists

        // Constants for readability and maintainability
        private const int SPEED = 5;
        private const int GAP = 10;
        private const int INITIAL_LIVES = 5;

        public double X { get; private set; }
        public double Y { get; private set; }
        public bool Quit { get; private set; } = false;
        public int Lives { get; set; } // add a Lives property to the Player class

        public double Angle // Angle property
        {
            get { return _angle; }
            set { _angle = value; }
        }

        public int Width
        {
            get
            {
                return _playerBitmap.Width;
            }
        }
        public int Height
        {
            get
            {
                return _playerBitmap.Height;
            }
        }

        public List<Bullet> Bullets
        {
            get { return _bullets; }
        }

        public Player(Window gameWindow)
        {
            _playerBitmap = new Bitmap("Player", "Player.png");
            _heartBitmap = new Bitmap("heart", "heart.png");// Load heart access

            X = (gameWindow.Width - Width) / 2;
            Y = (gameWindow.Height - Height) / 2;

            Lives = INITIAL_LIVES; // Initialize with 5 lives

            _gameTimer.Start();//Start the timer
            SplashKit.Delay(2000);
        }

        public void Draw()
        {
            _playerBitmap.Draw(X, Y);

            //Drawing hearts
            int heartX = 10;
            int heartY = 10;
            int heartSpacing = 20;
            for (int i = 0; i < Lives; i++)
            {
                _heartBitmap.Draw(heartX + i * heartSpacing, heartY); ;
            }

            // Draw the elapsed time on the screen
            int elapsedMilliseconds = (int)_gameTimer.Ticks;
            int elapsedSeconds = elapsedMilliseconds / 1000;

            SplashKit.DrawText($"{elapsedSeconds} seconds have passed", Color.Black, 500, 20);
            SplashKit.DrawText($"which is {elapsedSeconds} score", Color.Red, 500, 40);

            // Draw bullets
            DrawBullets();
        }

        public void HandleInput()
        {
            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                Y -= SPEED;
            }
            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                Y += SPEED;
            }
            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                X += SPEED;
            }
            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                X -= SPEED;
            }
            if (SplashKit.KeyDown(KeyCode.EscapeKey))
            {
                Quit = true;
            }
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            double mouseX = SplashKit.MouseX();
            double mouseY = SplashKit.MouseY();

            // Calculate angle from player to mouse cursor
            double angleToMouse = Math.Atan2(mouseY - Y, mouseX - X) * (180.0 / Math.PI);

            Matrix2D anchorMatrix = SplashKit.TranslationMatrix(SplashKit.PointAt(_playerBitmap.Width / 2, _playerBitmap.Height / 2));

            // Move centre point of picture to origin
            Matrix2D result = SplashKit.MatrixMultiply(SplashKit.IdentityMatrix(), SplashKit.MatrixInverse(anchorMatrix));
            // Rotate around origin
            result = SplashKit.MatrixMultiply(result, SplashKit.RotationMatrix(_angle));
            // Move it back...
            result = SplashKit.MatrixMultiply(result, anchorMatrix);

            // Now move to location on screen...
            result = SplashKit.MatrixMultiply(result, SplashKit.TranslationMatrix(X, Y));

            // Result can now transform a point to the ship's location
            // Get right/centre
            Vector2D vector = new Vector2D
            {
                X = _playerBitmap.Width,
                Y = _playerBitmap.Height / 2
            };
            // Transform it...
            vector = SplashKit.MatrixMultiply(result, vector);

            // Create a new bullet at the player's center, aimed towards the mouse
            Bullet newBullet = new Bullet(vector.X, vector.Y, angleToMouse);

            // Add the new bullet to the bullet list
            _bullets.Add(newBullet);
        }

        public void UpdateBullets()
        {
            foreach (Bullet bullet in _bullets)
            {
                bullet.Update();
            }
        }

        public void DrawBullets()
        {
            foreach (Bullet bullet in _bullets)
            {
                bullet.Draw();
            }
        }

        public void StayOnWindow(Window limit)
        {
            if (X < GAP)
            {
                X = GAP;
            }
            if (X + _playerBitmap.Width > limit.Width - GAP)
            {
                X = limit.Width - GAP - _playerBitmap.Width;
            }
            if (Y < GAP)
            {
                Y = GAP;
            }
            if (Y + _playerBitmap.Height > limit.Height - GAP)
            {
                Y = limit.Height - GAP - _playerBitmap.Height;
            }
        }

        public bool CollideWith(Robot other)
        {
            return _playerBitmap.CircleCollision(X, Y, other.CollisionCircle);
        }
    }
}
