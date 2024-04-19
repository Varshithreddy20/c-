using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace constructors
{
    class Program
    {
            static void Main()
            {
                Question question1 = new Question();
                Question question2 = new Question("What is the capital of France?");
                Question question3 = new Question("What is the capital of Italy?", "Rome", "Paris", "Berlin", "Madrid", 'A');
                Question question4 = new Question() { questionText = "What is 2 + 2?", optionA = "1", optionB = "2", optionC = "4", optionD = "5", };

                Console.WriteLine("Question 1 options valid: " + question1.AreOptionsValid());
                Console.WriteLine("Question 2 options valid: " + question2.AreOptionsValid());
                Console.WriteLine("Question 3 options valid: " + question3.AreOptionsValid());
                Console.WriteLine("Question 4 options valid: " + question4.AreOptionsValid());
            }
        }
    
}
