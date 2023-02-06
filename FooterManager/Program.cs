using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using FooterManager;

namespace FooterManager {
    internal class Program {
        private static void Main(string[] args)
        {
            // This program scans looks in the directory where it is 
            Console.WriteLine("Start!");
            new FooterManager().CombineAllFolders();

            Console.WriteLine("Geslaagd!");
        }
    }
}
