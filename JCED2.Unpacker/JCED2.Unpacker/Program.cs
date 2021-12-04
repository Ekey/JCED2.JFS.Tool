using System;
using System.IO;

namespace JCED2.Unpacker
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Juiced 2 JFS Unpacker");
            Console.WriteLine("(c) 2021 Ekey (h4x0r) / v{0}\n", Utils.iGetApplicationVersion());
            Console.ResetColor();

            if (args.Length != 2)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[Usage]");
                Console.WriteLine("    JCED2.Unpacker <m_File> <m_Directory>\n");
                Console.WriteLine("    m_File - Source of JFS archive file");
                Console.WriteLine("    m_Directory - Destination directory\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("[Examples]");
                Console.WriteLine("    JCED2.Unpacker E:\\Games\\Juiced2\\scripts.jfs D:\\Unpacked");
                Console.ResetColor();
                return;
            }

            String m_JfsFile = args[0];
            String m_Output = Utils.iCheckArgumentsPath(args[1]);

            if (!File.Exists(m_JfsFile))
            {
                Utils.iSetError("[ERROR]: Input JFS file -> " + m_JfsFile + " <- does not exist");
                return;
            }

            if (!File.Exists(m_JfsFile + "hdr"))
            {
                Utils.iSetError("[ERROR]: Unable to find header file -> " + m_JfsFile + "hdr" + " <- does not exist");
                return;
            }

            JfsUnpack.iDoIt(m_JfsFile, m_Output);
        }
    }
}
