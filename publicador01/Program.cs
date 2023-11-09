using RabbitMQ.Client;
using System.Text;

namespace publicador01
{
    internal class Program
    {
        static void Main()
        {
            //Servidor do Rabbitmq
            var servidor = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "usuario",
                Password = "senha123"
            };

            //Conexão com o servidor
            var conexao = servidor.CreateConnection();
            {
                //Canal
                using (var canal = conexao.CreateModel())
                {
                    //Criar fila ou escutar fila
                    canal.QueueDeclare(queue: "ola",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

                    //Mensagem
                    string mensagem = "Olá Mundo!";

                    //Convertendo a Mensagem em bytes
                    var corpoMensagem = Encoding.UTF8.GetBytes(mensagem);

                    canal.BasicPublish(exchange: "",
                                         routingKey: "ola",
                                         basicProperties: null,
                                         body: corpoMensagem);
                    Console.WriteLine(" [x] Enviou {0}", mensagem);
                }

                Console.WriteLine(" Pressione [enter] para sair.");
                Console.ReadLine();
            }
        }
    }
}