using System;

namespace Cryptographic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input a string");
            string input = Console.ReadLine();
            input = input.Replace(" ", "").ToUpper();                     

            char[] otpArray = new char[input.Length];
            char[] inputText = input.ToCharArray();
            char[] cipherText = new char[input.Length];

            Random random = new Random();
            for(int x = 0; x < otpArray.Length; x++) 
            {
                int otp = random.Next(0, 26) + 65;
                otpArray[x] = (char)otp;
                int index = (otpArray[x] - 65 + inputText[x] - 65) % 26 + 65;
                cipherText[x] = (char)index;
            }
            string otpString = new string(otpArray);
            string cipherString = new string(cipherText);

            Console.WriteLine($"Input: {input}\nOTP: {otpString}\nCipher text: {cipherString}");
        }
    }
}
