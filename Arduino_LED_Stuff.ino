#include <LedControl.h> // Make sure you have this imported


// Put the leads into the corresponding pins
int DIN = 12;
int CS = 11;
int CLK = 10;

// x and y for the LEDs
int x;
int y;

// The string that the .NET form will send to the arduino
String serialData;

LedControl lc = LedControl(DIN, CLK, CS, 0);

void setup() {
 lc.shutdown(0,false);       //The MAX72XX is in power-saving mode on startup
 lc.setIntensity(0,15);      // Set the brightness to maximum value
 lc.clearDisplay(0);         // and clear the display
 
 Serial.begin(9600);
 Serial.setTimeout(10);
}

void loop() {
  // put your main code here, to run repeatedly:
  //yeet
}

void serialEvent(){ // This method runs every time the google form sends data
  // Puts the string into the serialData string
  serialData = Serial.readString();

  // Resets the LED matrix to all blank
  int c[8] = {0,0,0,0,0,0,0,0};

  // Just watch the Michael Reeves vid for thi
  x = parseDataX(serialData);
  y = parseDataY(serialData);

  // Assigns the code for the specific LED to turn on
  c[y] = x;

  // Prints out the LED onto the matrix
  printByte(c);
}

int parseDataX(String data)
{
  data.remove(data.indexOf("Y"));
  data.remove(data.indexOf("X"), 1);

  return data.toInt();
}

int parseDataY(String data)
{
  data.remove(0, data.indexOf("Y") + 1);

  return data.toInt();
}

void printByte(int num [])
{
  // Resets the int everytime this executes
  int i = 0;

  // Sets every LED for every row (All num values will equal to 0 except one)
  for(i=0;i<8;i++)
  {
    lc.setRow(0,i,num[i]);
  }
}
