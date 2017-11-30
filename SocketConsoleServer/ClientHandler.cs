using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketConsoleServer
{
	class ClientHandler
	{
		Thread clientThread;
		TcpClient client;
		StreamReader reader;
		StreamWriter writer;
		NetworkStream netStream;

		public TcpClient Client { get { return client; } }

		public ClientHandler(TcpClient newClient)
		{
			client = newClient;

			netStream = client.GetStream();
			reader = new StreamReader(netStream);
			writer = new StreamWriter(netStream);

			StartClient();
		}
		public void HandleClient()
		{
			string folder;
			string answer;
			while (true)
			{
				folder = reader.ReadLine();

				Console.WriteLine(" -- " + folder);

				answer = FindDirectory(folder);

				SendToClient(answer);
			}
		}

		public void StartClient()
		{
			clientThread = new Thread(HandleClient);
			clientThread.Start();
		}

		public void SendToClient(string message)
		{
			writer.WriteLine(message);
			writer.Flush();
		}

		public string FindDirectory(string folder)
		{
			string directory = "";
			DirectoryInfo dir = new DirectoryInfo(folder);

			if (dir.Exists)
			{
				directory = dir.CreationTime.ToString() + Environment.NewLine;

				DirectoryInfo[] subDirs = dir.GetDirectories();

				foreach (var dirInfo in subDirs)
				{
					directory += " >> Name:<" + dirInfo.Name + "> Extension:<" + dirInfo.Extension + ">" + Environment.NewLine;
				}
			}
			else
			{
				return "Directory does not exist";
			}


			return directory;
		}
	}
}
