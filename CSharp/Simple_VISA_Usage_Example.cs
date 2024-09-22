using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NationalInstruments.Visa;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rmSession = new ResourceManager(); 

            IEnumerable<string> devices = rmSession.Find("(ASRL|GPIB|TCPIP|USB)?*");
            foreach (string device in devices)
            {
                Console.WriteLine(device);
            }

            MessageBasedSession mbSession = (MessageBasedSession)rmSession.Open("TCPIP0::127.0.0.1::inst0::INSTR");

            mbSession.RawIO.Write("*RST\n");

            mbSession.RawIO.Write("*IDN?\n");
            string temp2 = mbSession.RawIO.ReadString();
            Console.WriteLine("{0}", temp2);

            // Use the query function
            Console.Write("{0}", Query(ref mbSession, "*IDN?"));


            mbSession.Dispose();
        }

        static string Query(ref MessageBasedSession sess, string command)
        {
            sess.RawIO.Write(command + "\n");
            return sess.RawIO.ReadString();
        }
    }
}
