using System;
using SplashKitSDK;

namespace RobotDodge
{
    public abstract class Robot
    {
        public Color MainColor { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Vector2D Velocity { get; set; }

        public int Width
        {
            get
            {
                return 50;
            }
        }
        public int Height
        {
            get
            {
                return 50;
            }
        }

        public Circle CollisionCircle
        {
            get
            {
                double centerX = X + (Width / 2);
                double centerY = Y + (Height / 2);

                return SplashKit.CircleAt(centerX, centerY, 20);
            }
        }

        public Robot(Window gameWindow, Player player)
        {
            // Lets test with starting at top left... for now 
            X = 0;
            Y = 0;

            if (SplashKit.Rnd() < 0.5)
            {
                X = SplashKit.Rnd(gameWindow.Width);

                if (SplashKit.Rnd() < 0.5)
                    Y = -Height;
                else
                    Y = gameWindow.Height;
            }
            else
            {
                Y = SplashKit.Rnd(gameWindow.Height);

                if (SplashKit.Rnd() < 0.5)
                    X = -Width;
                else
                    X = gameWindow.Width;
            }

            const int SPEED = 2;//Change the speed for recording purpose
            // Get a Point for the Robot 
            Point2D fromPt = new Point2D()
            {
                X = X,
                Y = Y
            };
            // Get a Point for the Player 
            Point2D toPt = new Point2D()
            {
                X = player.X,
                Y = player.Y
            };
            // Calculate the direction to head. 
            Vector2D dir;
            dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
            // Set the speed and assign to the Velocity 
            Velocity = SplashKit.VectorMultiply(dir, SPEED);

            MainColor = Color.RandomRGB(200);
        }

        public abstract void Draw();

        public void Update()
        {
            X += Velocity.X;
            Y += Velocity.Y;
        }

        public bool IsOffscreen(Window screen)
        {
            return X < -Width || X > screen.Width || Y < -Height || Y > screen.Height;
        }
    }

    public class Boxy : Robot
    {
        public Boxy(Window gameWindow, Player player) : base(gameWindow, player)
        {

        }
        public override void Draw()
        {
            double leftX;
            double rightX;
            double eyeX;
            double mouthY;

            leftX = X + 12;
            rightX = X + 27;
            eyeX = Y + 10;
            mouthY = Y + 30;

            SplashKit.FillRectangle(Color.Gray, X, Y, Width, Height);
            SplashKit.FillRectangle(MainColor, leftX, eyeX, 10, 10);
            SplashKit.FillRectangle(MainColor, rightX, eyeX, 10, 10);
            SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
            SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
        }
    }

    public class Roundy : Robot
    {
        public Roundy(Window gameWindow, Player player) : base(gameWindow, player)
        {

        }

        public override void Draw()
        {
            double leftX, midX, rightX;
            double midY, eyeY, mouthY;
            leftX = X + 17;
            midX = X + 25;
            rightX = X + 33;
            midY = Y + 25;
            eyeY = Y + 20;
            mouthY = Y + 35;
            SplashKit.FillCircle(Color.White, midX, midY, 25);
            SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
            SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
            SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
            SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
            SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
        }
    }

    public class Swingy : Robot
    {
        public Swingy(Window gameWindow, Player player) : base(gameWindow, player)
        {

        }

        public override void Draw()
        {
            double centerX, centerY;
            double eyeX, mouthY;

            centerX = X + (Width / 2);
            centerY = Y + (Height / 2);
            eyeX = centerX - 5;
            mouthY = centerY + 10;

            SplashKit.FillTriangle(Color.Gray, centerX, Y, X, centerY, X + Width, centerY);
            SplashKit.FillTriangle(Color.Gray, centerX, Y + Height, X, centerY, X + Width, centerY);

            SplashKit.FillCircle(MainColor, eyeX, centerY - 5, 10);
            SplashKit.FillCircle(MainColor, eyeX + 10, centerY - 5, 10);

            // Draw the mouth as a rectangle (you can modify this)
            SplashKit.FillCircle(MainColor, eyeX, mouthY, 5);
        }
    }
}
