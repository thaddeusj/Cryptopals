using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace cryptopals
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (args[0] == "1")
            {
                if (args[1] == "1") Set1.Challenge1();
                if (args[1] == "2") Set1.Challenge2();
                if (args[1] == "3") Set1.Challenge3();
                if (args[1] == "4") Set1.Challenge4();
            }




            Console.ReadLine();

        }
    }
}
