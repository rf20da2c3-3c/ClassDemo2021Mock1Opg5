using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using ModelLib.model;


namespace ClassDemo2021Mock1Opg5
{
    public class Opg5Worker
    {
        private const int PORT = 2121;

        private static List<FootballPlayer> players = new List<FootballPlayer>()
        {
            new FootballPlayer(1, "Peter", 340000, 6),
            new FootballPlayer(2, "Henrik", 9340000, 9),
            new FootballPlayer(3, "Vibeke", 3340000, 2),
            new FootballPlayer(4, "Mohammed", 2340000, 3)
        };

        public Opg5Worker()
        {
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(
                    () =>
                    {
                        TcpClient tmpSocket = socket;
                        DoClient(tmpSocket);
                    }
                );
            }

        }

        private void DoClient(TcpClient socket)
        {
            using (StreamReader sr = new StreamReader(socket.GetStream()))
            using (StreamWriter sw = new StreamWriter(socket.GetStream()))
            {
                sw.AutoFlush = true;

                String kommando = sr.ReadLine();
                String data = sr.ReadLine();

                switch (kommando)
                {
                    case "HentAlle":
                        String jsonAlle = JsonSerializer.Serialize(players);
                        sw.WriteLine(jsonAlle);
                        break;

                    case "Hent":
                        int id = Convert.ToInt32(data);
                        String jsonEn = JsonSerializer.Serialize(players.Find(p=>p.Id == id));
                        sw.WriteLine(jsonEn);
                        break;

                    case "Gem":
                        FootballPlayer player = JsonSerializer.Deserialize<FootballPlayer>(data);
                        players.Add(player);
                        break;

                    default:
                        sw.WriteLine("Kommandoer: HentAlle, Hent <id>, Gem <json>");
                        break;
                }


            }
            socket?.Close();

        }
    }
}