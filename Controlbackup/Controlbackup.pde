
import processing.net.*;
import java.awt.Robot;
import java.awt.event.KeyEvent;
import java.awt.AWTException;
import beads.*;
import java.util.Arrays;

Server s;
Client c;
String buffer;
int input;
int lastInput;
int keyInput[] = {
  KeyEvent.VK_W, KeyEvent.VK_A, KeyEvent.VK_S, KeyEvent.VK_D, KeyEvent.VK_Q, 
  KeyEvent.VK_E, KeyEvent.VK_I, KeyEvent.VK_J, KeyEvent.VK_K, KeyEvent.VK_L, 
  KeyEvent.VK_T, KeyEvent.VK_Y, KeyEvent.VK_G, KeyEvent.VK_H, KeyEvent.VK_ESCAPE, 
  KeyEvent.VK_V, KeyEvent.VK_SPACE, KeyEvent.VK_F, KeyEvent.VK_N, KeyEvent.VK_M, 
  KeyEvent.VK_U, KeyEvent.VK_R, KeyEvent.VK_S, KeyEvent.VK_SPACE, KeyEvent.VK_PERIOD, 
  KeyEvent.VK_C, KeyEvent.VK_O, KeyEvent.VK_M
};
Robot robot;
AudioContext ac;
void setup() 
{
  size(450, 255);
  background(204); // Slow it down a little
  s = new Server(this, 12345); // Start a simple server on a port
  ac = new AudioContext();
  //launch("notepad");
  launch("C:/Documents and Settings/Administrator/Desktop/Shortcut to brainv2.tcl.lnk");
  
  try { 
    robot = new Robot();
  } catch (AWTException e) {
    e.printStackTrace();
    exit();
  }
}

void draw() 
{
  // Receive data from client
  c = s.available();
  if (c != null) {
    buffer = c.readString();
    input = int(buffer); 
    println(input);
    
    if (input == 16){
      String audioFileName = "D:/Sounds/ohno.wav";
      SamplePlayer player = new SamplePlayer(ac, SampleManager.sample(audioFileName));
      Gain g = new Gain(ac, 2, 1);
      g.addInput(player);
      ac.out.addInput(g);
      ac.start();
    }
    
    if (input == 17){
      String audioFileName = "D:/Sounds/OhYeah.mp3";
      SamplePlayer player = new SamplePlayer(ac, SampleManager.sample(audioFileName));
      Gain g = new Gain(ac, 2, 0.2);
      g.addInput(player);
      ac.out.addInput(g);
      ac.start();
    }
    
    if (input == 18){
      String audioFileName = "D:/Sounds/Starwars imp march.wav";
      SamplePlayer player = new SamplePlayer(ac, SampleManager.sample(audioFileName));
      Gain g = new Gain(ac, 2, 0.6);
      g.addInput(player);
      ac.out.addInput(g);
      ac.start();
    }
    
    if (input == 19){
      String audioFileName = "D:/Sounds/R2d2 Scream.mp3";
      SamplePlayer player = new SamplePlayer(ac, SampleManager.sample(audioFileName));
      Gain g = new Gain(ac, 2, 0.4);
      g.addInput(player);
      ac.out.addInput(g);
      ac.start();
    }
    
    if (input == 20){
      String audioFileName = "D:/Sounds/DejaVu.mp3";
      SamplePlayer player = new SamplePlayer(ac, SampleManager.sample(audioFileName));
      Gain g = new Gain(ac, 2, 0.8);
      g.addInput(player);
      ac.out.addInput(g);
      ac.start();
    }
    
    if (input == 21){
      String audioFileName = "D:/Sounds/dalek.mp3";
      SamplePlayer player = new SamplePlayer(ac, SampleManager.sample(audioFileName));
      Gain g = new Gain(ac, 2, 0.6);
      g.addInput(player);
      ac.out.addInput(g);
      ac.start();
    }
    
    if (input == 22){
      ac.stop();
      ac.reset();
    }
    
    if (input > 0 && input < 28){
    input = input - 1;
    lastInput = input;
    robot.keyPress(keyInput[input]);
    }
    
    if (input == 29){
      String audioFileName = "D:/Sounds/DejaVu.mp3";
      SamplePlayer player = new SamplePlayer(ac, SampleManager.sample(audioFileName));
      Gain g = new Gain(ac, 2, 0.8);
      g.addInput(player);
      ac.out.addInput(g);
      ac.start();
    }
    
    if (input != lastInput)
    robot.keyRelease(keyInput[lastInput]);
  }
}