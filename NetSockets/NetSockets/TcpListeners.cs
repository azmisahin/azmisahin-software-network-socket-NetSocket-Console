using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using System.Net;
using System.Net.Sockets;


namespace NetSockets
{
    public class TcpListeners
    {
        public TcpListeners()
        {
            Initalize(9999);
        }

        public TcpListeners(int Port)
        {
            Initalize(Port);
        }

        public void Initalize(int Port)
        {
            TcpListener server=null;
            
            try
            {
                #region Tanımlar
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localAddr, Port);

                server.Start();

                Byte[] bytes = new Byte[256];

                String data = null;
                #endregion

                #region Data Bekleniyor
                while (true)
                {
                    Console.Write("Bağlantı bekleniyor... ");

                    TcpClient client = server.AcceptTcpClient();

                    #region Bağlantı Alınıyor
                    Console.WriteLine("Bağlandı!");

                    data = null;

                    NetworkStream stream = client.GetStream();

                    int i;
                    #endregion


                    #region Ağ Akımı Belleğe Aktarılıyor
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Alındı: {0}", data);
                        data = data.ToUpper();
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        //Veri Gönderiliyor
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Gönderildi: {0}", data);
                    }
                    #endregion



                    client.Close();
                }
                #endregion

            }
            catch(SocketException e)
            {
                Console.WriteLine("Hata : {0}", e);
            }
            finally
            {
                server.Stop();
            }
            
            Console.WriteLine("\nDevam Etmek için bir tuşa basınız...");
            Console.Read();
        }
    }
}