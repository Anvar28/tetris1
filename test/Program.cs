﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static (int minf, int max) getFigureMinMax()
        {
            int maxJ = 1, minJ = 2;

            return (minf: minJ, max: maxJ);
        }

        static void Main(string[] args)
        {

            //int[,] a = new int[4, 4];
            //int[,] b = new int[4, 4];

            //a[1, 1] = 7;

            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        Console.Write(a[i, j]);
            //        b[j, i] = a[i, 3 - j];
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine("=========");
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        Console.Write(b[i, j]);
            //    }
            //    Console.WriteLine();
            //}

            //Random rnd = new Random();
            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine(rnd.Next(0, 10));
            //}

            var a = getFigureMinMax();

            Console.WriteLine(a.minf);

            Console.ReadLine();
        }
    }
}
