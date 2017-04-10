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
 // stroke(0);
  //frameRate(5); // Slow it down a little
  c = new Client(this, "10.69.227.39", 12345);  // Change the ip to the rp7i's ip on the same network as this computer
} 
void draw() { 
  //  print("client");
  //delay(1000);
  //c.write("1");
  //delay(500);
  //c.write("0");
  //delay(1000);
  // take in a keypress and assign it to a corisponding intiger to send as data
  if (keyPressed == true) {
    
    stroke(255);
    input = key;
    
    if(input > 0){
      
      switch(input){
        case 'w':
          data = 1;
          break;
        case 'a':
          data = 4;
          break;
        case 's':
          data = 3;
          break;
        case 'd':
          data = 2;
          break;
        case 'q':
          data = 6;
          break;
        case 'e':
          data = 5;
          break;
        case 'i':
          data = 7;
          break;
        case 'j':
          data = 10;
          break;
        case 'k':
          data = 9;
          break;
        case 'l':
          data = 8;
          break;
        case 't':
          data = 11;
          break;
        case 'y':
          data = 12;
          break;
        case 'g':
          data = 13;
          break;
        case 'h':
          data = 14;
          break;
        case 'x':
          data = 15;
          break;
        case 'v':
          data = 16;
          break; 
        case 'b':
          data = 17;
          break;
        case 'c':
          data = 18;
          break;
        case 'n':
          data = 19;
          break;
        case 'm':
          data = 20;
          break;
        case 'z':
          data = 21;
          break;
        case 'r':
          data = 22;
          break;
        case 'f':
          data = 29;
          break;
        default:
          data = 0;
          break;
      }
      print(input+"-"+data);
      print(check);
      // send the data to the server
      if (c != null){
        println("SENDING");
        c.write(str(data));
      }
     else{
     }
    }
    check = key;
    delay(300);
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
// send a zeroto tell the program on the rp7i to release the key
void keyReleased(){
  data = 0;
  println(data);
  check = ' ';
  if(c != null){
    c.write(data);
  }
}