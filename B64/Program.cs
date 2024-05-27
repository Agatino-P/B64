using System.IO;
using System.Reflection;

namespace B64;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("1 encode, 2 decode");
        bool encode = Console.ReadKey().KeyChar == '1';


        if (encode)
        {
            EncodeDir();
        }
        if (!encode)
        {
            DecodeDir();
        }

    }

    private static void EncodeDir()
    {
        string originalDir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Original");
        string encodedDir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Encoded");

        if (!Path.Exists(encodedDir))
        {
            Directory.CreateDirectory(encodedDir);
        }

        DirectoryInfo dir = new DirectoryInfo(originalDir);

        FileInfo[] fileInfos = dir.GetFiles("*.*");
        foreach (var fileInfo in fileInfos)
        {

            string targetFilePath = Path.Combine(encodedDir, $"{fileInfo.Name}.b64");
            EncodeSingleFile(fileInfo.FullName, targetFilePath);
        }
    }

    private static void DecodeDir()
    {
        string originalDir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Original");
        string encodedDir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Encoded");

        if (!Path.Exists(originalDir))
        {
            Directory.CreateDirectory(originalDir);
        }

        DirectoryInfo dir = new DirectoryInfo(encodedDir);

        FileInfo[] fileInfos = dir.GetFiles("*.*");
        foreach (var fileInfo in fileInfos)
        {
            string targetFilePath = Path.Combine(originalDir, Path.GetFileNameWithoutExtension(fileInfo.Name));
            DecodeSingleFile(fileInfo.FullName, targetFilePath);
        }
    }


    private static void DecodeSingleFile(string b64File, string destFile)
    {
        string file = File.ReadAllText(b64File);
        Byte[] bytes = Convert.FromBase64String(file);
        File.WriteAllBytes(destFile, bytes);
    }

    private static void EncodeSingleFile(string sourceFile, string b64File)
    {
        Byte[] bytes = File.ReadAllBytes(sourceFile);
        String file = Convert.ToBase64String(bytes);
        File.WriteAllText(b64File, file);
    }
}
