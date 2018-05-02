using System;
using System.Collections.Generic;
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


        public void SendCommand(String command)
        {
            data = System.Text.Encoding.ASCII.GetBytes(command);
            stream.Write(data, 0, data.Length);
            System.Diagnostics.Debug.WriteLine("Command sent: " + command);
        }


        public void ListenForMessages()
        {

            workerThread = new Thread(Receive);
            workerThread.Name = "My Worker Thread";
            workerThread.Start();

        }


        private void Receive()
        {
            while (!stopReceiving)
            {
                incomingMessage = null;
                if (stream.CanRead && stream.DataAvailable)
                {
                    try
                    {
                        i = stream.Read(data, 0, data.Length);
                        incomingMessage = System.Text.Encoding.ASCII.GetString(data, 0, i);
                        System.Diagnostics.Debug.WriteLine(incomingMessage);

                        if (incomingMessage == "quit")
                        {
                            // TODO: Somekind of message that the server closed the connection
                        }
                        else if (ReceiveMessage != null)
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
