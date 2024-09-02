using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestingService.Models
{
    public static class DataWork
    {
        private static string errorMessage = "Неопознанная ошибка";
        private static string successfulMessage = "Успешно";

        public static List<Tests> GetTestsList() => DatabaseConnection.connection.Tests.ToList();

        public static List<Questions> GetQuestionsList(Tests test) => DatabaseConnection.connection.Questions.Where(x=> x.TestId == test.TestId).ToList();
        public static List<Answers> GetAnswersList(Questions question)
        {
            List<QuestionAnswers> questionAnswers = DatabaseConnection.connection.QuestionAnswers.Where(x=> x.QuestionId == question.QuestionId).ToList();
            List<Answers> answers = new List<Answers>();
            foreach (QuestionAnswers element in questionAnswers)
            {
                answers.Add(DatabaseConnection.connection.Answers.FirstOrDefault(x=> x.AnswerId == element.AnswerId));
            }

            return answers;
        }

        public static string AddQuestion(Questions question, List<Answers> answers)
        {
            try
            {
                DatabaseConnection.connection.Questions.Add(question);
                DatabaseConnection.connection.SaveChanges();

                foreach (Answers answer in answers)
                {
                    DatabaseConnection.connection.Answers.Add(answer);
                    DatabaseConnection.connection.QuestionAnswers.Add(new QuestionAnswers { QuestionId = question.QuestionId, AnswerId = answer.AnswerId });
                }

                DatabaseConnection.connection.SaveChanges();

                return successfulMessage;
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
                return errorMessage;
            }
        }
        
        public static string AddTest(Tests test)
        {
            if (DatabaseConnection.connection.Tests.Any(x => x.TestTitle == test.TestTitle)) return "Тест с таким названием уже существует";
            try
            {
                DatabaseConnection.connection.Tests.Add(test);
                DatabaseConnection.connection.SaveChanges();

                return successfulMessage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return errorMessage;
            }
        }
    }
}
