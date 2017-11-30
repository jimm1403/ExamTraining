using System;

namespace SocketConsoleClient
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
			string folder;
			RemoteFacade remoteFacade = new RemoteFacade();
			folder = Console.ReadLine();
			remoteFacade.Send(folder);

		}
    }
}
