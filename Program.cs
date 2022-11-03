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

        Player player = new Player(ScreenHeight, ScreenWidth, MovementSpeed, RectangleSize);
        Fall fall = new Fall();

        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Greed");
        Raylib.SetTargetFPS(60);
        

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawText("Score: ", 12, 12, 20, Color.WHITE);
            
            player.input();
            fall.initialize();
        
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
        public void initialize()
        {
            int count = 0;
            if (count == 10)
            {

                count = 0;
            }
            else
            {
                count += 1;
            }
            //Rectangle[] =  
        }

    }
    abstract public class Falling_objects
    {
        public Vector2 Position { get; set; } = new Vector2(0, 0);
        public Vector2 Velocity { get; set; } = new Vector2(0, 0);
        public Falling_objects()
        {

        }


    }
    public class Rock: Falling_objects
    {
        // shape = O
    }
    public class Gem: Falling_objects
    {
        // shape = * 
    }
    public class Shooter
    {
        // spaceBar = shoot
        // shoot = |
    }  

    }
}