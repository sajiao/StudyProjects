using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
   public class WebSocke
    {
        public static List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
        public static void WebSocketService()
        {
            FleckLog.Level = LogLevel.Debug;

            var server = new WebSocketServer("ws://0.0.0.0:8082");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    allSockets.ForEach(s => s.Send("Echo: " + message));

                };
            });


            var inputMsg = Console.ReadLine();
            while (inputMsg != "exit")
            {
                SendSocketMsg(inputMsg);
                inputMsg = Console.ReadLine();
            }
        }

        public static void SendSocketMsg(string msg)
        {
            foreach (var socket in allSockets)
            {
                socket.Send(msg);
            }
        }

    }
}
