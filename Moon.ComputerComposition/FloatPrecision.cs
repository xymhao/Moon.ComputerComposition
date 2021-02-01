using System;
using System.Globalization;

namespace Moon.ComputerComposition2
{
    public class FloatPrecision
    {
        public static void main()
        {
            var a = 20000000.0f;
            var b = 1.0f;
            float sum = a + b;
            Console.WriteLine("c is " + sum);
            float d = sum - a;
            Console.WriteLine("d is " + d);

            float t = sum - a - b;
            if (t < 0) Console.WriteLine(t);

            sum += b - t;
            Console.WriteLine(sum.ToString(CultureInfo.InvariantCulture));

            float y = sum - a - b;
            if (y > 0)
            {
                sum = sum - y;
                Console.WriteLine(sum.ToString(CultureInfo.InvariantCulture));
            }

            Console.WriteLine(double.MaxValue);
        }

        /// <summary>
        ///     sum is 1.6777216E7
        /// </summary>
        public static void Sum()
        {
            var sum = 0.0f;
            for (var i = 0; i < 20000000; i++)
            {
                var x = 1.0f;
                sum += x;
            }

            Console.WriteLine("sum is " + sum);
        }

        //Kahan Summation 算法
        public static void KahanSummation()
        {
            var sum = 0.0f;
            var c = 0.0f;
            for (var i = 0; i < 20000000; i++)
            {
                var x = 1.0f;
                float y = x - c;
                float t = sum + y;
                c = t - sum - y;
                if (c < 0) Console.WriteLine(c);
                sum = t;
            }

            Console.WriteLine("sum is " + sum);
        }
    }
}