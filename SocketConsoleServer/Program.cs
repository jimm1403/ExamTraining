using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
			Program myProgram = new Program();
			myProgram.Run();
        }

		public void Run()
		{
			TcpListener listener = new TcpListener(IPAddress.Any, 11001);
			listener.Start();
			TcpClient newClient;
			ClientHandler clientHandler;

			while (true)
			{
				Console.WriteLine(" -- Server is running");
				newClient = listener.AcceptTcpClient();
				Console.WriteLine(" -- Client Connected");
				clientHandler = new ClientHandler(newClient);
			}
		}
    }
}
