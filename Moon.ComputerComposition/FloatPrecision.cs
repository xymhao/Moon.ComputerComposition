using System;

namespace Moon.ComputerComposition2
{
    public class FloatPrecision
    {
        public static void main()
        {
            float a = 20000000.0f;
            float b = 1.0f;
            float c = a + b;
            Console.WriteLine("c is " + c);
            float d = c - a;
            Console.WriteLine("d is " + d);
        }

        /// <summary>
        /// sum is 1.6777216E7
        /// </summary>
        public static void Sum()
        {
            float sum = 0.0f;
            for (int i = 0; i < 20000000; i++)
            {
                float x = 1.0f;
                sum += x;
            }

            Console.WriteLine("sum is " + sum);
        }

        //Kahan Summation 算法
        public static void KahanSummation()
        {
            float sum = 0.0f;
            float c = 0.0f;
            for (int i = 0; i < 20000000; i++)
            {
                float x = 1.0f;
                float y = x - c;
                float t = sum + y;
                c = (t - sum) - y;
                if (c<0)
                {
                    Console.WriteLine(c);
                }
                sum = t;
            }

            Console.WriteLine("sum is " + sum);
        }
    }
}