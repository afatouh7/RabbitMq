// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("welcome to ticketing services");


var factory = new ConnectionFactory()
{
   
                HostName = "localhost",
                UserName = "user",
                Password = "password",
                VirtualHost="/"
 };
var con = factory.CreateConnection();
using var channel = con.CreateModel();
channel.QueueDeclare("booking", durable: true, exclusive: true);


var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
 {
     var body = args.Body.ToArray();
     var message = Encoding.UTF8.GetString(body);
     Console.WriteLine($"New ticket{message}");
 };
channel.BasicConsume("booking", true, consumer);
Console.ReadKey();