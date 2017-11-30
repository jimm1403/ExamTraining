using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketConsoleClient
{
	class RemoteFacade
	{
		TcpClient client;
		NetworkStream netStream;
		StreamReader reader;
		StreamWriter writer;
		int port = 11001;
		string serverName = "localHost";

		public RemoteFacade()
		{
			client = new TcpClient();
			ConnectToServer();
			netStream = client.GetStream();
			reader = new StreamReader(netStream);
			writer = new StreamWriter(netStream);

			Thread receiveThread = new Thread(Receive);
			receiveThread.Start();

		}

		private void ConnectToServer()
		{
			client.Connect(serverName, port);
		}

		private void Close()
		{
			client.GetStream().Close();
		}

		private void Dispose()
		{
			client.Close();
		}

		public void Receive()
		{
			string receiveMessage;
			while (true)
			{
				receiveMessage = reader.ReadLine();
				Console.WriteLine(receiveMessage);
			}
		}

		public void Send(string message)
		{
			writer.WriteLine(message);
			writer.Flush();
		}




	}
}
