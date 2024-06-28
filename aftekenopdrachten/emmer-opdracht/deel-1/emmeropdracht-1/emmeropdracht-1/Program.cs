using emmeropdracht_1.Models;
using System;
using emmeropdracht_1.Exceptions;
using emmeropdracht_1.Events;

namespace emmeropdracht_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Bucket bucketLessThanTen = new Bucket(5);
            }
            catch (FalseCapacityException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                RainBarrel barrel = new RainBarrel(70);
            }
            catch (FalseCapacityException e)
            {
                Console.WriteLine(e.Message);
            }

            // Test the Bucket class
            Bucket bucket = new Bucket();
            Console.WriteLine("Bucket content: " + bucket.Content);

            bucket.Fill(8);
            Console.WriteLine("Bucket content: " + bucket.Content);

            bucket.Empty();
            Console.WriteLine("Bucket content: " + bucket.Content);


            // Test the RainBarrel class
            RainBarrel rainBarrel = new RainBarrel(100);
            try
            {
                rainBarrel.Fill(120);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            
            // Test the OilBarrel class
            OilBarrel oilBarrel = new OilBarrel();
            try
            {
                oilBarrel.Fill(200);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            // Test filling a bucket from another bucket
            Bucket secondBucket = new Bucket(15);
            secondBucket.Fill(10);
            Console.WriteLine("Bucket content: " + bucket.Content);
            bucket.Fill(secondBucket);
            Console.WriteLine("Bucket content: " + bucket.Content);

            secondBucket.Fill(10);
            try
            {
                bucket.Fill(secondBucket);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            bucket = new Bucket();
            Console.WriteLine("Bucket content: " + bucket.Content);
            // Test the events
            bucket.Full += OnFull;
            bucket.Overflowing += OnOverflowing;

            try
            {
                bucket.Fill(12);
                bucket.Empty();
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                bucket.Fill(18);
                Console.WriteLine("Bucket content: " + bucket.Content);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("");
            var otherBucket = new Bucket();
            otherBucket.Full += OnFull;
            otherBucket.Overflowing += OnOverflowing;
            otherBucket.Fill(10);
            try
            {
                otherBucket.Fill(bucket);
                Console.WriteLine("Bucket content: " + otherBucket.Content);
                Console.WriteLine("Fillbucket content: " + bucket.Content);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void OnFull(object sender, EventArgs e)
        {
            Console.WriteLine("Bucket is full");
        }

        static void OnOverflowing(object sender, OverflowEventArgs e)
        {
            Console.WriteLine("Overflowing foreseen: " + e.OverflowAmount);
            int overflow = e.OverflowAmount;
            Console.WriteLine("Do you want to cancel the overflow? (y/n)");
            string input = Console.ReadLine();
            e.Cancel = input == "y";
            if (e.Cancel)
            {
                Console.WriteLine("Overflow cancelled");
            } else
            {
                Console.WriteLine("How much do you want to overflow?");
                e.OverflowAmount = int.Parse(Console.ReadLine());
                if(e.OverflowAmount > overflow)
                {
                    e.OverflowAmount = overflow;
                }
                Console.WriteLine("Overflowing with " + e.OverflowAmount);
            }
        }
    }
}