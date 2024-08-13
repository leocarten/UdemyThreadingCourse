using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyThreadingCourse.AOC_Practice.ContinuationSkills
{
    class AOC
    {

        private string Path;
        public AOC(string path)
        {
            this.Path = path;
        }

        public string[] GetInput()
        {
            string[] lines = File.ReadAllLines(this.Path);

            List<string> array = new List<string>(); //dynamically allocate 

            foreach (string line in lines)
            {
                array.Add(line);
            }
            return array.ToArray();
        }

        private bool containsVowel(string input)
        {
            int count = 0;
            foreach(var letter in input)
            {
                if (letter == 'a' || letter == 'e' || letter == 'i' || letter == 'o' || letter == 'u')
                {
                    count++;
                    if (count == 3)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool containsDuplicate(string input)
        {
            for (int i = 0; i < input.Length - 1; i++)
            {
                if (input[i] == input[i + 1])
                {
                    return true;
                }

            }
            if (input[input.Length - 1] == input[input.Length - 2])
            {
                return true;
            }
            return false;
        }

        public bool isValid(string input)
        {
            if (containsVowel(input) && ( !input.Contains("ab") && !input.Contains("cd") && !input.Contains("pq") && !input.Contains("xy")) && containsDuplicate(input))
            {
                return true;
            }
            return false;
        }

    }

    class AOC_Driver
    {
        static void Main_(string[] args)
        {

            var aoc = new AOC("C:/Users/leoth/source/repos/UdemyThreadingCourse/UdemyThreadingCourse/AOC_Practice/ContinuationSkills/input.txt");
            int count = 0;

            // read input and put it into an array 
            var task1 = Task.Factory.StartNew(() => aoc.GetInput());

            //Console.WriteLine(aoc.GetInput());
            var task2 = task1.ContinueWith(t =>
            {
                var result = task1.Result;
                foreach(var str in result)
                {
                    if (aoc.isValid(str))
                    {
                        count++;
                    }
                }
            });

            task2.Wait();

            Console.WriteLine(count);


            // read each item in array, checking logic rules
            // if all logic rules apply, increment counter

        }
    }

}
