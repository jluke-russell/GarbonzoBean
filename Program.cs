using System;
using Raylib_cs;
using System.Numerics;
using System.Collections.Generic;

// Our code will include eight classes; player, Score, rock, gem, shooter, falling objects, Point, & Game. 

namespace cse210_student_csharp_Greed
{
    public class GreedGame
    {
        public static void Main()
        {
        int ScreenHeight = 480;
        int ScreenWidth = 800;
        int RectangleSize = 15;
        int MovementSpeed = 4;
        int count = 0;
        var Objects = new List<Falling_objects>();

        Player player = new Player(ScreenHeight, ScreenWidth, MovementSpeed, RectangleSize);
        Fall fall = new Fall(MovementSpeed, Objects, ScreenHeight, ScreenWidth, RectangleSize, count);

        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Greed");
        Raylib.SetTargetFPS(60);
        

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawText("Score: ", 12, 12, 20, Color.WHITE);
            
            player.input();
            fall.Step();
        
            Raylib.EndDrawing();


        }
        Raylib.CloseWindow();
    }

    // Move all movement and draw rectangle related objects into the movement. and the draw method to the class.
    public class Player
    {
        public Player(int ScreenHeight, int ScreenWidth, int MovementSpeed, int RectangleSize)
        {
           PlayerRectangle = new Rectangle(ScreenWidth - (RectangleSize * 2), ScreenHeight - (RectangleSize * 2), RectangleSize, RectangleSize);
           Speed = MovementSpeed;
        }
        Rectangle PlayerRectangle;
        int Speed;
        public void input()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) 
            {
                PlayerRectangle.x += Speed;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) 
            {
                PlayerRectangle.x -= Speed;
            }
            
            drawPlayer();
        }
        public void drawPlayer()
        {
            Raylib.DrawRectangleRec(PlayerRectangle, Color.WHITE); 
        }
    }
    public class score
    {

    }
    public class Fall
    {
        public Fall(int MovementSpeed, List<Falling_objects> Objects, int ScreenHeight, int ScreenWidth, int RectangleSize, int count)
        {
            Speed = MovementSpeed;
            objects = Objects; 
            Height = ScreenHeight;
            Width = ScreenWidth;
            Size = RectangleSize;
            Count = count;
        }
        int Speed;
        List<Falling_objects> objects;
        int Height;
        int Width;
        int Size;
        int Count;


        public void Step()
        {
            var Random = new Random();
            int whichType = Random.Next(2);
            int randomY = Random.Next(2, Speed-1);
            int randomX = Random.Next(0, Width - Size);
            var position = new Vector2(randomX, 0 - Size);
            var objectsToRemove = new List<Falling_objects>();

            if (Count == 20)
            {
                CreateObjects(whichType, randomY, position, Size);
                Count = 0;
            }
            else
            {
                Count += 1;
            }

            foreach (var obj in objects) 
            {
                obj.Draw();
            }

            foreach (var obj in objects) 
            {
                obj.Move();
            }
            foreach (var obj in objects)
            {
                if (obj.Position.Y > Height + 20)
                {
                    objectsToRemove.Add(obj);
                }
            }
            objects = objects.Except(objectsToRemove).ToList();
        }
        public void CreateObjects(int whichType, int randomY, Vector2 position, int Size)
        {
            switch (whichType) 
                {
                case 0:
                    var rock = new Rock(Color.RED, Size);
                    rock.Position = position;
                    rock.Velocity = new Vector2(0, randomY);
                    objects.Add(rock);
                    break;
                case 1:
                    var gem = new Gem(Color.BLUE, Size / 2);
                    gem.Position = position;
                    gem.Velocity = new Vector2(0, randomY);
                    objects.Add(gem);
                    break;
                }
        }
        

    }
    public class Falling_objects
    {
        
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public Vector2 Velocity { get; set; } = new Vector2(0, 0);

        virtual public void Draw() {
        // Base game objects do not have anything to draw
        }

        public void Move() 
        {
        Vector2 NewPosition = Position;
        NewPosition.Y += Velocity.Y;
        Position = NewPosition;
        }
    }
    public class ColoredObject: Falling_objects 
    {
        public Color Color { get; set; }

        public ColoredObject(Color color) { Color = color; }
    }
    public class Rock: ColoredObject
    {
        public int Size { get; set; }
        public Rock(Color color, int size): base(color) {
            Size = size;
        }
        override public void Draw() {
            Raylib.DrawRectangleLines((int)Position.X, (int)Position.Y, Size, Size+(Size/3), Color);
        }
    }
    public class Gem: ColoredObject
    {
        public int Radius { get; set; }
        public Gem(Color color, int radius): base(color) 
        {
            Radius = radius;
        }
        override public void Draw() 
        {
            Raylib.DrawCircle((int)Position.X, (int)Position.Y, Radius, Color);
        }
    }
    public class Shooter
    {
        // spaceBar = shoot
        // shoot = |
    }  

    }
}