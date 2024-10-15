using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_O_NEW
{   //genertic class to set up methods and the console
    class Program
    {
        static void Main(string[] args)
        {
            //setting up the 'game screen' or console X and Y lengths
            Console.WindowWidth = 40;
            Console.WindowHeight = 35;
            //setting up the loading X and Y limits in console
            Console.BufferWidth = Console.WindowWidth;
            Console.BufferHeight = Console.WindowHeight;
            //Hiding the cursor in console
            Console.CursorVisible = false;

            //creating a game class to call the methods above
            Game myGame = new Game();
            myGame.RunGame();          

            Console.ReadKey(true);
        }
    }
    //game class to hold all the methods used in the game
    class Game
    {
        //main methods to call
        bool GameOver = false;
        int LastFrameSpacePressed = 0;
        int FrameCount = 0;
        static int FlappyPositionY;
        int LastPressedSpacebar = 0;
        // PIPE variable myPipe, PipePosX, PipeGap Start, PipeGap End
        static char myPipe = ' ';
        static int PipePositionX = -1;
        static char PipeGap = ' ';
        static int PipeGapSize = 0;

        static int Score = 0;

        //what happens when you run the game (or use the run game method)
        public void RunGame()
        {
            Console.WriteLine(" < FLAPPY Game Running");
            //placing flappy in the Y middle of the console
            FlappyPositionY = Console.WindowHeight / 2;
            //main gameplay loop! (or while game is still beign played
            while (GameOver == false) {

                HandleInput();
                {   //move flappy up or down depending on if space was pressed within last 2 frames
                    if (FrameCount - LastFrameSpacePressed < 2)
                    {
                        FlappyPositionY = FlappyPositionY - 1;
                    }
                    else
                    {
                        FlappyPositionY = FlappyPositionY + 1;
                    }
                    //write FLAPPYO to the console and update every frame
                    Console.SetCursorPosition(8, FlappyPositionY);
                    if (FrameCount >= 1)
                    {
                        Console.Write('o');
                    }
                    //else (BOUNCY)
                    //{
                    //    Console.Write('O');
                    //}
                }
                // Console.WriteLine("");

                HandleInput();
                System.Threading.Thread.Sleep(1000);
                FrameCount = FrameCount + 1;
                //clears the console while the game is running AT THE END
                Console.Clear();

                //Pipes();
                MovePipes();
            }
            while (GameOver == true);
            {
                //collisions                               
                if (FlappyPositionY == PipePositionX)
                {
                    FrameCount = 0;
                    Console.Write("GAME OVER");
                    Console.ReadKey();
                }
            }
        }
        private void HandleInput()
        {
            //Not stopping for key pressed
            if (Console.KeyAvailable)
            {
                //process the key press                            
                ConsoleKeyInfo KeyInfo = Console.ReadKey(true);

                LastFrameSpacePressed = FrameCount;
            }
        }
        static private void Pipes()
        {
            //for console height
            // 0 -> 40
            for (int i = 0; i < Console.BufferHeight; i++)
            {
                // i is 1
                Console.SetCursorPosition(PipePositionX, i); //(40-1 = 39, 1)

                Console.BackgroundColor = ConsoleColor.Green;
                Console.Write(myPipe);

                Console.BackgroundColor = ConsoleColor.Black;            
            }
        }
        private void MovePipes()
        {
            Pipes();                     
                //put posx on cursor position
                PipePositionX = Console.BufferWidth - 15; 
                //Console.SetCursorPosition(PipePositionX, i);
                //need to increment Y every frame, can't set to a certain point or it will keep re-setting there
                
                PipePositionX--;

                if (PipePositionX < 0 || PipePositionX > Console.BufferWidth)
                {
                    //PipePositionX = (Console.BufferWidth - 1);
                    Score++;
                }
            //loop through Y, take away two, set random Y position & loop, set position for pipe gap & draw pipe around it?
        }
    }
}
/* 
      Outline
      How to handle input + move the bird?
      How will we draw the game to the console?
 
    MAIN FUNCTIONS FLAPPY O
       int Score;
        int FlappyPositionY;
        const int FlappyPositionX;
        - variable Pipe [
	     -   PipePosX
	     -   PipeGap Start
	     -   PipeGap End
        ]
        int FrameCount;
 
        void Main {
	        while (true) {
		        HandleInput();
		        MoveFlappyGravity();
		        MovePipes();
		        CheckCollision();
		        DrawGame();
		        // Wait for next frame
		        Thread.Sleep(100);
		        Frame Count goes up.
	        }
        }
 
        HandleInput {
           Check if we have key available
             if Key is Spacebar
	        Set last frame space bar pressed = 0
        }
 
        MoveFlappyGravity {
           If Last Frame Space Bar Pressed < 2:
               FLappyPositionY goes up
           Else:
	        FlappyPositionY goes down
        }
 
        DrawGame {
	        Write empty space to current flappy position
	        Write O to current flappy position
        }
         
 
      TIME SYSTEM
      Convert real time second to () in-game frames
  
      WRITE TO CONSOLE
      Write O to console every in-game frame
      Write [] pipes to console every in-game frams
      Update bird based on in-game frames and gravity (and input)
      Update pipes based on in-game frames
  
      COLLISIONS
      Get Bird position for collisions, if bird is in contact, game ends
      Pipes spawn randomly with constant gap
  
      PLAYER INPUT
      Re-write screen every in-game second (clear screen, or write "")
      Jump function registered to "get space bar down"
      When you press space it writes O
      Gravity adds momentum to O
      Gravity registers and increments every few frames (5fps)
  
      Getting the game to update in real time
      Making the O move with gravity on the Y (and X)
      Making the pipes move on the X axis
      Keep score based on pipes passed
      Game over screen (press any button to retry)
 */
