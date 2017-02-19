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
                int chal = 0;

                string comm = Console.ReadLine();
                try
                {
                    chal = int.Parse(comm);
                }
                catch { }

                if (chal == 1) Set1.Challenge1();
                if (chal == 2) Set1.Challenge2();
                if (chal == 3) Set1.Challenge3();
                if (chal == 4) Set1.Challenge4();
                if (chal == 5) Set1.Challenge5();
                if (chal == 6) Set1.Challenge6();
                if (chal == 7) Set1.Challenge7();
                if (chal == 8) Set1.Challenge8();
            }


            if (args[0] == "2")
            {
                int chal = 0;

                string comm = Console.ReadLine();
                try
                {
                    chal = int.Parse(comm);
                }
                catch { }

                if (chal == 2) Set2.Challenge2();


            }

            Console.ReadLine();

        }
    }
}
