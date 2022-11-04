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
        int score = 0;
        var Objects = new List<Falling_objects>();
        Rectangle PlayerRectangle = new Rectangle(ScreenWidth - (RectangleSize * 2), ScreenHeight - (RectangleSize * 2), RectangleSize, RectangleSize);


        Player player = new Player(MovementSpeed, PlayerRectangle);
        Fall fall = new Fall(MovementSpeed, Objects, ScreenHeight, ScreenWidth, RectangleSize, count, score);

        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Greed");
        Raylib.SetTargetFPS(60);
        

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);

            fall.newScore();
            player.Input();
            player.drawPlayer();
            fall.Step(player.PlayerRectangle);
            
        
            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
        }
    }

    // Move all movement and draw rectangle related objects into the movement. and the draw method to the class.
    public class Player
    {
        public Player(int MovementSpeed, Rectangle Player)
        {
           PlayerRectangle = Player;
           Speed = MovementSpeed;
        }
        public Rectangle PlayerRectangle;
        int Speed;
        public void Input()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) 
            {
                PlayerRectangle.x += Speed;
            }

            if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) 
            {
                PlayerRectangle.x -= Speed;
            }
        }
        public void drawPlayer()
        {
            Raylib.DrawRectangleRec(PlayerRectangle, Color.WHITE); 
        }
        
    }
    public class Fall
    {
        public Fall(int MovementSpeed, List<Falling_objects> Objects, int ScreenHeight, int ScreenWidth, int RectangleSize, int count, int score)
        {
            Speed = MovementSpeed;
            objects = Objects; 
            Height = ScreenHeight;
            Width = ScreenWidth;
            Size = RectangleSize;
            Count = count;
            Score = score;
        }
        int Speed;
        List<Falling_objects> objects;
        int Height;
        int Width;
        int Size;
        int Count;
        int Score;



        public void Step(Rectangle Player)
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
            // Check for Collisions
            foreach (var obj in objects)
            {
                if(obj is Rock)
                {
                    
                    Rock rock = (Rock)obj;
                    if (Raylib.CheckCollisionRecs(rock.eachRectangle(), Player)) 
                    {
                        Console.WriteLine("Rock");
                        subScore();
                        objectsToRemove.Add(obj);
                    }   
                }
                else if (obj is Gem)
                {
                    
                    Gem gem = (Gem)obj;
                    if (Raylib.CheckCollisionCircleRec(gem.Position, gem.Radius, Player)) 
                    {
                        Console.WriteLine("Gem");
                        addScore();
                        objectsToRemove.Add(obj);
                    }  
                }

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
        public void addScore()
        {
            Score += 1;
        }
        public void subScore()
        {
            Score -= 1;
        }
        public void newScore()
        {
            int returnScore = Score;
            Raylib.DrawText("Score: " + returnScore , 12, 12, 20, Color.WHITE);
        }
        public void CreateObjects(int whichType, int randomY, Vector2 position, int Size)
        {
            switch (whichType) 
                {
                case 0:
                    var rock = new Rock(GenerateColor(), Size);
                    rock.Position = position;
                    rock.Velocity = new Vector2(0, randomY);
                    objects.Add(rock);
                    break;
                case 1:
                    var gem = new Gem(GenerateColor(), Size / 2);
                    gem.Position = position;
                    gem.Velocity = new Vector2(0, randomY);
                    objects.Add(gem);
                    break;
                }
        }
        public Color GenerateColor()
        {
            var Random = new Random();
            Color[] Colors = {Color.SKYBLUE, Color.BROWN, Color.BEIGE,Color.DARKPURPLE, Color.VIOLET, Color.PURPLE, Color.DARKBLUE, Color.BLUE, 
                        Color.BLACK, Color.DARKGREEN, Color.LIME, Color.GREEN, Color.MAROON, Color.RED, Color.PINK, Color.ORANGE, Color.GOLD, Color.YELLOW,
                        Color.DARKGRAY, Color.GRAY, Color.LIGHTGRAY, Color.BLANK, Color.MAGENTA, Color.RAYWHITE, Color.DARKBROWN, Color.WHITE}; 
            var randomColorNumber = Random.Next(0, Colors.Length);
            Color randomColor = Colors[randomColorNumber];
            return randomColor;
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
        public Rectangle eachRectangle()
        {
            Rectangle ownedRectangle = new Rectangle((int)Position.X, (int)Position.Y, Size, Size+(Size/3));
            return ownedRectangle;
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