using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatSocketApp.Comunicacion
{
    public class ServerSocket
    {
        private int puerto;
        private Socket servidor;
        private Socket comCliente;
        private StreamWriter writer;
        private StreamReader reader;
        /// <summary>
        /// El servidor se levanta en el puerto especifico
        /// </summary>
        /// <param name="puerto"></param>


        public ServerSocket(int puerto)
        {
            this.puerto = puerto;
            
        }


        public bool Iniciar()
        {

            try
            {
            //Crear instancia de socket
            this.servidor = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            //Hacer bind para tomar control del puerto
            this.servidor.Bind(new IPEndPoint(IPAddress.Any, this.puerto));
            //Escuchar a una cantidad de clientes
            this.servidor.Listen(10);

            return true;

            }catch(Exception ex)
            {
                return false;
            }

        }

        public bool Escribir(string mensaje)
        {
            try
            {
                this.writer.WriteLine(mensaje);
                this.writer.Flush();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public string Leer()
        {
            try
            {
                return this.reader.ReadLine().Trim();
            }catch(Exception ex)
            {
                return null;
            }
        }

        public bool ObtenerCliente()
        {
            try
            {
                this.comCliente = this.servidor.Accept();
                Stream stream = new NetworkStream(this.comCliente);
                this.writer = new StreamWriter(stream);
                this.reader = new StreamReader(stream);

                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public void CerrarConexion()
        {
            this.comCliente.Close();
        }
    }
}
