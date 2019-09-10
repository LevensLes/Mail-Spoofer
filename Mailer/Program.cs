using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mailer
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static void Main(string[] args)
        {

            Console.Title = "Made by levensles";


            Console.WriteLine("Choose method ( spam / manual )");
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "spam":
                    {

                        Console.Clear();
                        spam();
                        break;
                    }
                case "manual":
                    {
                        Console.Clear();
                        manual();
                        break;
                    }
                default:
                    {

                        return;
                    }
            }


            manual();
            Console.Read();
        }

        public static void manual()
        {
            string frommail = "";
            string tomail = "";
            string subject = "";
            string message = "";
            Console.Write("Sending to: ");
            tomail = Console.ReadLine();

            Console.Write("Sending from: ");
            frommail = Console.ReadLine();

            Console.Write("subject: ");
            subject = Console.ReadLine();

            Console.Write("message: ");
            message = Console.ReadLine();


         
            string fromname = "";


            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["fromName"] = fromname;
                values["fromemail"] = frommail;
                values["to"] = tomail;
                values["subject"] = subject;
                values["message"] = message;

                var response = client.UploadValues("link to mail php", values);

                var responseString = Encoding.Default.GetString(response);
                Console.WriteLine(responseString);
                Console.Read();
            }

            Console.Read();
        }


        public static void spam()
        {
            Console.Write("Email addy to spam: ");
            string tomail = "";
            tomail = Console.ReadLine();

            Console.Write("Amount of mails to send: ");
            string maxmailstr = Console.ReadLine();

            int maxmails = 0;
            
            try
            {
                maxmails = Int32.Parse(maxmailstr);
            }
            catch { }

            int curmails = 1;
            List<string> keys = new List<string>();
            while (true)
            {
              
                string frommail = randomstring() + "@gmail.com";
                       
                string subject = randomstring();
                string message = randomstring();
               
                         
                string fromname = GenerateName(6);

                using (var client = new WebClient())
                {
                    var values = new NameValueCollection();
                    values["fromName"] = fromname;
                    values["fromemail"] = frommail;
                    values["to"] = tomail;
                    values["subject"] = subject;
                    values["message"] = message;

                    var response = client.UploadValues("link to mail php", values);

                    var responseString = Encoding.Default.GetString(response);
                
                    if (responseString.Contains("From:"))
                    {
                        Console.WriteLine("Email send succesfully from: " + frommail + " [" + curmails + "/" + maxmails + "]");
                    }

                    if (curmails == maxmails)
                    {
                        Console.WriteLine("Done");
                        Console.Read();
                        break;
                    }
                }
                curmails++;
            }

        }
        public static string randomstring(){
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz".ToCharArray();
            byte[] data = new byte[16];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(16);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
         
        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; 
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;


        }

    }
}
