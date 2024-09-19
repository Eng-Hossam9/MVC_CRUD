namespace Company.Hossam.PL.Helper
{
    public static class Document
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\",FolderName);

            var FileName=$"{Guid.NewGuid()}_{file.FileName}";

            var FilePath= Path.Combine(@$"{ FolderPath}\", FileName);

           using var Filestream = new FileStream(FilePath, FileMode.Create);
            
            file.CopyTo(Filestream);

            return FileName;

        }

        public static void DeleteFile(string FileName, string FolderName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\",@$"{FolderName}\",FileName);
            if (File.Exists(FilePath))
            { 
                File.Delete(FilePath);
            }

        }
    }
}

