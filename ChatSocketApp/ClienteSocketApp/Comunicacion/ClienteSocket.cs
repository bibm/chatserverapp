using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClienteSocketApp.Comunicacion
{
    public class ClienteSocket
    {
        private int puerto;
        private string ipServidor;
        private Socket comServidor;
        private StreamReader reader;
        private StreamWriter writer;

        public ClienteSocket(int puerto, string ipServidor)
        {
            this.puerto = puerto;
            this.ipServidor = ipServidor;
        }

        public bool Conectar()
        {

            try
            {
                this.comServidor = new Socket(AddressFamily.InterNetwork
                    , SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ipServidor), puerto);
                this.comServidor.Connect(endpoint);
                Stream stream = new NetworkStream(this.comServidor);
                this.writer = new StreamWriter(stream);
                this.reader = new StreamReader(stream);
                return true;

            }
            catch (IOException ex)
            {
                return false;
            }

        }
        public string Leer()
        {
            try
            {
                return this.reader.ReadLine().Trim();

            }
            catch (IOException ex)
            {
                return null;
            }
        }
        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                return true;
            }catch(IOException ex)
            {
                return false;
            }
        }
        public void Desconectar()
        {
            this.comServidor.Close();
        }



    }
}
