using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FooterManager {
    public class FooterManager {
        public void CombineAllFolders()
        {
            
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("Folders in " + currentDirectory + " zoeken om samen te voegen.");
            //currentDirectory = Console.ReadLine();

            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Your input: {0}", a);

            foreach (string folder in Directory.GetDirectories(currentDirectory))
            {
                CombineFolder(folder);
            }
        }

        private void CombineFolder(string folder)
        {
            Console.WriteLine(folder + " samenvoegen");
            Console.WriteLine("");

            // create file
            string newPdf = folder + " - samengevoegd.pdf";
            using FileStream fileStream = new FileStream(newPdf, FileMode.Create);
            using Document doc = new Document();
            using PdfWriter writer = PdfWriter.GetInstance(doc, fileStream);

            // loop through all documents
            int combinedPdfFiles = 0;
            foreach (string file in Directory.GetFiles(folder, "*.pdf"))
            {
                combinedPdfFiles++;

                if (!doc.IsOpen()) doc.Open(); 
                PdfReader reader = new PdfReader(file);
                PdfContentByte cb = writer.DirectContent;
                int numberOfPages = reader.NumberOfPages;


                // Loop through all the pages
                for (int i = 1; i <= numberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);

                    doc.SetPageSize(new Rectangle(page.Width, page.Height+20));
                    doc.NewPage();

                    Rectangle pageSize = reader.GetPageSizeWithRotation(i);
                    cb.AddTemplate(page, 0, 20);
                    cb.SetRGBColorFill(100, 100, 100);
                    cb.BeginText();
                    cb.SetFontAndSize(BaseFont.CreateFont(), 11);
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, System.IO.Path.GetFileNameWithoutExtension(file), pageSize.GetRight(0) / 2, pageSize.GetBottom(5), 0);
                    cb.EndText();
                }
            }

            // remove empty file
            if (combinedPdfFiles == 0)
            {
                fileStream.Close();
                File.Delete(newPdf);
                Console.WriteLine("Geen pdf bestanden gevonden in " + folder + ", er is dus ook geen samengevoegde pdf gemaakt.");
                return;
            }

            doc.Close();
        }
    }
}
