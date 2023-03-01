namespace E_commerce.Helper
{
    public class FileManager
    {
		public static string SaveFile(string rootPath, string folderName, IFormFile file)
		{
			string name = file.FileName;
			name = name.Length > 64 ? name.Substring(name.Length - 64, 64) : name;
			name = Guid.NewGuid().ToString() + name;
			string savePath = Path.Combine(rootPath, folderName, name);
			using (FileStream fileStream = new FileStream(savePath, FileMode.Create))
			{
				file.CopyTo(fileStream);
			}
			return name;
		}

		public static void DeleteFile(string rootPath, string folderName, string fileName)
		{
			string deletePath = Path.Combine(rootPath, folderName, fileName);
			if (System.IO.File.Exists(deletePath))
			{
				System.IO.File.Delete(deletePath);
			}
		}

	}
}
