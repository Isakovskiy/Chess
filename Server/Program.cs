using Domain.Models;
using Domain;
using System.Net;
using System.Net.Sockets;
using Domain.Models.Figures;
using System.Text;
using System.Text.Json;
using System.Runtime.Serialization;

TcpListener server = null;
TcpListener secondPlayerListener = null;

Chess _chess;

try
{
    IPAddress localAddr = IPAddress.Parse("127.0.0.1");

    server = new TcpListener(localAddr, port);

    server.Start();

    //_chess = new Chess();

    Console.WriteLine("Ожидание подключений... ");

    TcpClient client1 = server.AcceptTcpClient();
    Console.WriteLine("Подключен 1 клиент");

    TcpClient client2 = server.AcceptTcpClient();
    Console.WriteLine("Подключен 2 клиент");

    Console.WriteLine("Выполнение запроса...");

    NetworkStream stream1 = client1.GetStream();
    NetworkStream stream2 = client2.GetStream();

    string response = "Ты первый игрок";
    var nc = new cls(1, "HELL");
    var x = JsonSerializer.Serialize<cls>(nc);
    byte[] data = Encoding.UTF8.GetBytes(x);

    stream1.Write(data, 0, data.Length);

    response = "Ты второй игрок";
    data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(new cls(2, "hello")));
    stream2.Write(data, 0, data.Length);
    stream1.Close();
    stream2.Close();

    client1.Close();
    client2.Close();

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
finally
{
    if(server != null)
    {
        server.Stop();
    }
    if (secondPlayerListener != null)
    {
        secondPlayerListener.Stop();
    }
}
[Serializable]
public class cls
{
    public int x { get; set; }
    public string s { get; set; }
    public cls(int x, string s)
    {
        this.x = x;
        this.s = s;
    }

}
class BoardPainter : IBoardPainter
{
    public void DrawAvaibleCells(List<Cell> avaibleCells)
    {
        throw new NotImplementedException();
    }

    public void DrawBoard(Cell[,] sells)
    {
        throw new NotImplementedException();
    }

    public void ResetAvaibleCells()
    {
        throw new NotImplementedException();
    }
}

class FigurePainter : IFiguresPainter
{
    public void CancelFigure(Figure figure)
    {
        throw new NotImplementedException();
    }

    public void ChooseFigure(Cell figureCell)
    {
        throw new NotImplementedException();
    }

    public TransformFigures DrawFigureReplaceSelectionAndGet()
    {
        throw new NotImplementedException();
    }

    public void MoveFigure(Cell from, Cell to)
    {
        throw new NotImplementedException();
    }
}


static partial class Program
{
    const int port = 8888;


}



