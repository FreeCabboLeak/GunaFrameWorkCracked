using GunaPatcher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunaPatcher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = $"[{DateTime.Now}] GunaPatcher by https://github.com/CabboShiba";
            if(!File.Exists(Environment.CurrentDirectory + @"\ReadBeforeUse.txt"))
            {
                File.WriteAllText(Environment.CurrentDirectory + @"\ReadBeforeUse.txt", "Run this program in the same folder as \"packages\" folder. You can find it in your project directories.\nOnce you have found it, you can run the program");
            }
            string TempFile = null;
            int count = 0;
            Log("Scanning for \"Guna.UI2.WinForms.2.0.4.4\" Directory...", "INFO", ConsoleColor.Yellow);
            string dir = Environment.CurrentDirectory + @"\packages\Guna.UI2.WinForms.2.0.4.4\lib";
            if (!Directory.Exists(dir))
            {
                Log($"Could not find {dir}. Please be sure that this program is running in the same folder as packages folder.\nPress enter to leave...", "ERROR", ConsoleColor.Red);
                Console.ReadLine();
                Process.GetCurrentProcess().Kill();
            }

            foreach (var item in Directory.GetDirectories(dir))
            {
                TempFile = item + @"\Guna.UI2.dll";
                if (File.Exists(TempFile))
                {
                    Log($"Found Guna.UI2.dll in {item} | Patching...", "INFO", ConsoleColor.Cyan);
                    try
                    {
                        File.Delete(TempFile);
                        Log("Succesfully deleted original Guna.UI2.dll | Replacing...", "INFO", ConsoleColor.Yellow);
                    }
                    catch(Exception ex)
                    {
                        Log($"Could not delete: {TempFile}\n{ex.Message}", "ERROR", ConsoleColor.Red);
                    }
                }
                try
                {
                    File.WriteAllBytes(TempFile, (byte[])Properties.Resources.ResourceManager.GetObject("Guna_UI2"));
                    count++;
                    Log("Succesfully copied cracked .DLL in: " + TempFile, "SUCCESS", ConsoleColor.Green);
                }
                catch (Exception ex)
                {
                    Log($"Could not overwrite cracked .DLL: {ex.Message}", "ERROR", ConsoleColor.Red);
                }
            }
            Log($"Succesfully patched {count} Guna.UI2.dll File.", "SUCCESS", ConsoleColor.Green);
            Log("Finished. Press enter to leave...", "FINISH", ConsoleColor.Yellow);
            Console.ReadLine();
            Process.GetCurrentProcess().Kill();
        }



        public static void Log(string Data, string Type, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")} - {Type}] {Data}");
            Console.ResetColor();
        }
    }
}
