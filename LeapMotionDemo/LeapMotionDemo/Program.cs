using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Leap;

namespace LeapMotionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
    
                // Create a sample listener and controller
                SampleListener listener = new SampleListener();
                Controller controller = new Controller();

                // Have the sample listener receive events from the controller
                controller.AddListener(listener);

                // Keep this process running until Enter is pressed
                
                Console.WriteLine("Press Enter to quit...");
                Console.ReadLine();

                // Remove the sample listener when done
                controller.RemoveListener(listener);
                controller.Dispose();


        }

        public static void Write(double value)
        {
            int leftOffSet = (Console.WindowWidth / 2) -10;
            int topOffSet = (Console.WindowHeight / 2);
            Console.SetCursorPosition(leftOffSet, topOffSet);

            var position = (int) (value*10);

            Console.WriteLine("|" +Indent(10 + position) + "#" + Indent(10 - position) + "|");
        }

        public static string Indent(int count)
        {
            return "".PadLeft(count);
        }
    }


    internal class SampleListener : Listener
    {
        public override void OnFrame(Controller arg)
        {
            // Get the most recent frame and report some basic information
            Frame frame = arg.Frame();
            Console.Clear();
            var pos = (frame.Hands.Leftmost.PalmPosition.x/200);

            if (pos > 1.0) 
                pos = 1.0f;
            if (pos < -1.0)
                pos = -1.0f;


            Program.Write(pos);
        }
    }
}

