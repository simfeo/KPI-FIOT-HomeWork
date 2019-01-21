using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string test1 = ConfigSettings.ReadSetting("Test1");
            ConfigSettings.WriteSetting("Test1", "This is my new value");
            ConfigSettings.RemoveSetting("Test1");
            Console.WriteLine(test1);
        }
    }
}
