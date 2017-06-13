using ProjectCore.Algotithm;
using System;

namespace ProjectCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Schedule prototype = new Schedule(2, 2, 90, 10);

            var instance = new Algorithm(100, 20, 5, prototype);

            instance.Start();
        }
    }
}