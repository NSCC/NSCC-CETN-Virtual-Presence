using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPClientLib
{
    public class RPClient
    {

        TcpClient client = null;
        NetworkStream stream = null;
        Byte[] data = new Byte[256];
        Thread workerThread;
        String incomingMessage;
        int i;
        bool stopReceiving = false;

        public event ReceiveMessageEventHandler ReceiveMessage;

        public RPClient() { }


        /// <summary>
        /// Makes a connection to the robot.
        /// </summary>
        /// <param name="ipAddress">The robot's IP Address</param>
        /// <param name="port">The port the robot is listening on.</param>
        /// <returns></returns>
        public bool Connect(String ipAddress, Int32 port)
        {            
            try
            {
                client = new TcpClient(ipAddress, port);                
                stream = client.GetStream();
                System.Diagnostics.Debug.WriteLine("Connected");
            }
            catch (SocketException ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to connect: " + ex.Message);
                return false;
            }           
           
            return true;
        }


        /// <summary>
        /// Closes the connection to the robot.
        /// </summary>
        public void Disconnect()
        {            
            if (workerThread != null)
            {
                stopReceiving = true;
                workerThread.Join();
            }

            if (stream != null)
            {
                stream.Close();
            }

            if (client != null)
            {
                client.Close();
            }
        }
       

        /// <summary>
        /// Sends a message to the robot.
        /// </summary>
        /// <param name="command"></param>
        public void SendCommand(String command)
        {
            if (stream.CanWrite)
            {
                data = System.Text.Encoding.ASCII.GetBytes(command);
                try
                {
                    stream.Write(data, 0, data.Length);

                    // TODO: Delete this line for testing only
                    System.Diagnostics.Debug.WriteLine("Command sent: " + command);

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Command could not be sent: " + ex.Message);
                }
               
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Command could not be sent: Cannot write to network stream");
            }      
        }


        /// <summary>
        /// Starts the thread to listen for incoming messages from the robot.
        /// </summary>
        public void ListenForMessages()
        {
            workerThread = new Thread(Receive);
            workerThread.Name = "My Worker Thread";
            workerThread.Start();
        }


        /// <summary>
        /// Recieves an incoming message from the robot.
        /// </summary>
        private void Receive()
        {
            while (!stopReceiving)
            {
                incomingMessage = null;
                if (stream.CanRead && stream.DataAvailable)
                {
                    try
                    {
                        MemoryStream memStream = new MemoryStream();
                        
                        do
                        {
                            i = stream.Read(data, 0, data.Length);
                            memStream.Write(data, 0, i);
                        }
                        while (stream.DataAvailable);                        

                        incomingMessage = System.Text.Encoding.ASCII.GetString(memStream.ToArray());

                        //TODO: Delete this line for testing only.
                        System.Diagnostics.Debug.WriteLine(incomingMessage);
                        
                        if (ReceiveMessage != null)
                        {
                         
                            ReceiveMessage(this, new ReceivedMessageEventArgs(incomingMessage));
                        }
                    }
                    catch (ObjectDisposedException e)
                    {
                        //TODO: Handle the error.
                    }                    
                }
            }
        }

    }
}
