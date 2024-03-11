using System.IO;

namespace B64;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("1 encode source.zip, 2 decode base64.txt");
        ConsoleKeyInfo key =Console.ReadKey();
        if (key.KeyChar == '1')
        {
            Byte[] bytes = File.ReadAllBytes("source.zip");
            String file = Convert.ToBase64String(bytes);
            File.WriteAllText("base64.txt", file);
        }
        if (key.KeyChar == '2')
        {
            string file= File.ReadAllText("base64.txt");
            Byte[] bytes = Convert.FromBase64String(file);
            File.WriteAllBytes("source.zip", bytes);
        }
    }
}
