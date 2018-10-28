using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using WebServer.Routing;

namespace WebServer
{
    public class Server
    {
        private const string LocalHostIpAddress = "127.0.0.1";

        private readonly int Port;

        private readonly TcpListener Listener;

        private readonly ServerRoutingTable ServerRoutingTable;

        private bool IsRunning;

        public int IpAddress { get; }


        public Server(int port, ServerRoutingTable serverRoutingTable)
        {
            this.Port = port;
            this.Listener = new TcpListener(IPAddress.Parse(LocalHostIpAddress), port);
            this.ServerRoutingTable = serverRoutingTable;
        }

        public void Run()
        {
            this.Listener.Start();
            this.IsRunning = true;

            Console.WriteLine($"Server started at http://{LocalHostIpAddress}:{Port}");

            var task = Task.Run(this.ListenLoop);
            task.Wait();
        }

        public async Task ListenLoop()
        {
            while (this.IsRunning)
            {
                var client = await this.Listener.AcceptSocketAsync();
                var connectionHandler = new ConnectionHandler(client, this.ServerRoutingTable);
                var responseTask = connectionHandler.ProcessRequestAsync();
                responseTask.Wait();
            }
        }
    }
}