import processing.net.*;

Server s; 
Client c;
char input;
char check;
int data;
PImage bg;

void setup() { 
  size(440, 440);
  
  bg = loadImage("background.jpg");
  background(bg);
  stroke(0);
  //frameRate(5); // Slow it down a little
  s = new Server(this, 8888);  // Start a simple server on a port
} 
void draw() { 
  //  print("client");
  //delay(1000);
  //c.write("1");
  //delay(500);
  //c.write("0");
  //delay(1000);
  if (keyPressed == true) {
    
    stroke(255);
    input = key;
    
    if(input != check){
      
      switch(input){
        case 'w':
          data = 1;
          break;
        case 'a':
          data = 2;
          break;
        case 's':
          data = 3;
          break;
        case 'd':
          data = 4;
          break;
        case 'q':
          data = 5;
          break;
        case 'e':
          data = 6;
          break;
        case 'i':
          data = 7;
          break;
        case 'j':
          data = 8;
          break;
        case 'k':
          data = 9;
          break;
        case 'l':
          data = 10;
          break;
        default:
          data = 0;
          break;
      }
      print(input+"-"+data);
      print(check);
      if (c != null){
        c.write(data);
      }
     else{
     }
    }
    check = key;
    delay(250);
  }
  // Receive data from client
  //c = s.available();
  //if (c != null) {
  //  input = c.readString(); 
  //  input = input.substring(0, input.indexOf("\n"));  // Only up to the newline
  //  data = int(split(input, ' '));  // Split values into an array
  //  // Draw line using received coords
  //  stroke(0);
  //  line(data[0], data[1], data[2], data[3]); 
  //}
}
void keyReleased(){
  data = 0;
  println(data);
  check = ' ';
  if(c != null){
    c.write(data);
  }
}