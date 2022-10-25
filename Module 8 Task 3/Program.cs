﻿namespace Module_8_Task_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до папки:");
            string path = Console.ReadLine();
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists)
            {
                int files;
                Console.WriteLine("Исходный размер: " + DirectoryExtension.CountSize(di) + " байт");
                Console.WriteLine("Удалено на " + deleteFolder(path, out files) + " байт");
                Console.WriteLine("Удалено " + files + " файлов");
                Console.WriteLine("Размер после очистки: " + DirectoryExtension.CountSize(di) + " байт");
            }
            else
            {
                Console.WriteLine("Папки не существует");
            }

        }
        private static long deleteFolder(string folder, out int fileCount)
        {
            long size = 0;
            fileCount = 0;
            try
            {
                DirectoryInfo di = new DirectoryInfo(folder);
                DirectoryInfo[] diA = di.GetDirectories();
                FileInfo[] fi = di.GetFiles();
                foreach (FileInfo f in fi)
                {
                    if (DateTime.Now - f.LastAccessTime > TimeSpan.FromMinutes(30) && f.Exists)
                    {
                        fileCount++;
                        size += f.Length;//сколько удалено
                        f.Delete();
                    }
                }
                foreach (DirectoryInfo df in diA)
                {
                    if (df.Exists)
                    {
                        int files;
                        size += deleteFolder(df.FullName,out files);//сколько удалено
                        fileCount += files;
                        if (df.GetDirectories().Length == 0 && df.GetFiles().Length == 0) df.Delete();
                    }
                    else
                    {
                        Console.WriteLine("Папки не существует");
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
            return size;
        }
    }
}