using System;
using System.Data;
using SplashKitSDK;

namespace RobotDodge
{
    public class Bullet
    {
        private Bitmap _bulletBitmap = new Bitmap("Bullet", "Bullet.png");
        private double _x, _y, _angle;
        private bool _active = false;

        public double X { get; private set; }
        public double Y { get; private set; }
        
        public Bullet(double x, double y, double angle)
        {
            _x = x - _bulletBitmap.Width / 2;
            _y = y - _bulletBitmap.Height / 2;
            _angle = angle;
            _active = true;
        }

        public void Update()
        {
            const int BULLET_SPEED = 8;
            Vector2D movement = new Vector2D();
            Matrix2D rotation = SplashKit.RotationMatrix(_angle);
            movement.X += BULLET_SPEED;
            movement = SplashKit.MatrixMultiply(rotation, movement);
            _x += movement.X;
            _y += movement.Y;

            if ((_x > SplashKit.ScreenWidth() || _x < 0) || _y > SplashKit.ScreenHeight() || _y < 0)
            { _active = false; }
        }

        public void Draw()
        {
            if (_active)
            {
                DrawingOptions options = SplashKit.OptionRotateBmp(_angle);
                _bulletBitmap.Draw(_x, _y, options);
            }
        }

        public Circle CollisionCircle
        {
            get
            {
                double centerX = _x + (_bulletBitmap.Width / 2);
                double centerY = _y + (_bulletBitmap.Height / 2);

                return SplashKit.CircleAt(centerX, centerY, 10);
            }
        }
    }
}
