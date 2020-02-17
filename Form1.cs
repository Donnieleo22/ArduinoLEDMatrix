using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; // For the Stopwatch

namespace Arduino_LED_Matrix    
{
    public partial class Form1 : Form
    {
        public Stopwatch watch { get; set; } // For the timing later in the program
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            watch = Stopwatch.StartNew(); // Starts the stopwatch at the program start
            port.Open(); // Opens the serial port for the arduino
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e) // For every mouse movement, do this method
        {
            int x = 0;
            int y = e.Y / 80; /* The y value can be left alone since the arduino code uses an array and if say 60 is divided by 80
                                 the value equals 0 due to the way ints are divided and the beginning of an array starts with 0 */


            /* The form is 640x640 and the LED Matrix is 8x8 so there is an 80x80 pixel area for every LED in the matrix.
               This series of 'if' statements determines where the LED should be. Look in the arduino for reference to why
               which number corresponds with each LED */

            if (e.X / 80 == 0)
            {
                x = 128;
            }
            else if (e.X / 80 == 1)
            {
                x = 64;
            }
            else if (e.X / 80 == 2)
            {
                x = 32;
            }
            else if (e.X / 80 == 3)
            {
                x = 16;
            }
            else if (e.X / 80 == 4)
            {
                x = 8;
            }
            else if (e.X / 80 == 5)
            {
                x = 4;
            }
            else if (e.X / 80 == 6)
            {
                x = 2;
            }
            else if (e.X / 80 == 7)
            {
                x = 1;
            }

            writeToPort(new Point(x, y)); // Executes the writeToPort method
        }

        public void writeToPort(Point coordinates)
        {
            if (watch.ElapsedMilliseconds > 15) // I dont really know why this is needed but watch the Michael Reeves vid for an explanation
            {
                watch = Stopwatch.StartNew();
                port.Write(String.Format("X{0}Y{1}", coordinates.X, coordinates.Y));
            }
        }

    }
}
