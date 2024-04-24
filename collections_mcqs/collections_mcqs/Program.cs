using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace collections_mcqs
{
    interface ITestPaper
        {
            string SubjectName { get; set; }
            string TestPaperName { get; set; }
            List<IQuestion> Questions { get; set; }
        }

        interface IQuestion
        {
            string QuestionText { get; set; }
            List<IOption> Options { get; set; }
            char CorrectAnswerLetter { get; set; }
            char OptionSelectedByStudent { get; set; }
            int MarksSecured { get; set; }
        }

        interface IOption
        {
            char OptionLetter { get; set; }
            string OptionText { get; set; }
        }

        interface IStudent
        {
            int RollNo { get; set; }
            string StudentName { get; set; }
            List<ITestPaper> TestPapers { get; set; }
        }

        class TestPaper : ITestPaper
        {
            public string SubjectName { get; set; }
            public string TestPaperName { get; set; }
            public List<IQuestion> Questions { get; set; }
        }

        class Question : IQuestion
        {
            public string QuestionText { get; set; }
            public List<IOption> Options { get; set; }
            public char CorrectAnswerLetter { get; set; }
            public char OptionSelectedByStudent { get; set; }
            public int MarksSecured { get; set; }
        }

        class Option : IOption
        {
            public char OptionLetter { get; set; }
            public string OptionText { get; set; }
        }

        class Student : IStudent
        {
            public int RollNo { get; set; }
            public string StudentName { get; set; }
            public List<ITestPaper> TestPapers { get; set; }
        }

        class Program
        {
            static void Main(string[] args)
            {
            // Example usage
            var testPaper1 = new TestPaper
            {
                SubjectName = "History",
                TestPaperName = "Test 1",
                Questions = new List<IQuestion>

                    
            {
                new Question
                {
                    QuestionText = "Who was the first president of the United States?",
                    CorrectAnswerLetter = 'B',
                    Options = new List<IOption>
                    {
                        new Option { OptionLetter = 'A', OptionText = "George Washington" },
                        new Option { OptionLetter = 'B', OptionText = "George Washington" },
                        new Option { OptionLetter = 'C', OptionText = "John Adams" },
                        new Option { OptionLetter = 'D', OptionText = "Thomas Jefferson" }
                    }
                },
                // Add more questions here
            }
                };

                var student1 = new Student
                {
                    RollNo = 1,
                    StudentName = "John Doe",
                    TestPapers = new List<ITestPaper> { testPaper1 }
                };

                // Allow student to attempt the test paper, record their answers and calculate marks
                AttemptTestPaper(student1, testPaper1);

                // Display results
                DisplayTestResults(student1, testPaper1);
            }

            static void AttemptTestPaper(IStudent student, ITestPaper testPaper)
            {
                foreach (var question in testPaper.Questions)
                {
                    Console.WriteLine(question.QuestionText);
                    foreach (var option in question.Options)
                    {
                        Console.WriteLine($"{option.OptionLetter}) {option.OptionText}");
                    }
                    Console.Write("Your choice: ");
                    char choice = char.ToUpper(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                    question.OptionSelectedByStudent = choice;
                    if (choice == question.CorrectAnswerLetter)
                    {
                        question.MarksSecured = 1;
                    }
                    else
                    {
                        question.MarksSecured = 0;
                    }
                }
            }

            static void DisplayTestResults(IStudent student, ITestPaper testPaper)
            {
                Console.WriteLine($"Test Results for {student.StudentName}: {testPaper.TestPaperName}");
                foreach (var question in testPaper.Questions)
                {
                    Console.WriteLine($"Question: {question.QuestionText}");
                    Console.WriteLine($"Correct Answer: {question.CorrectAnswerLetter}");
                    Console.WriteLine($"Your Answer: {question.OptionSelectedByStudent}");
                    Console.WriteLine($"Marks Secured: {question.MarksSecured}");
                    Console.WriteLine();
                }
            }
        }



        }
    

