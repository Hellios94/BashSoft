using BashSoft.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BashSoft
{
    public class RepositoryFilter : IDataFilter
    {
        public void FilterAndTake(Dictionary<string, double> studentsWithMarks, string wantedFilter, int StudentToTake)
        {
            if (wantedFilter == "excellent")
            {
                this.FilterAndTake(studentsWithMarks, x => x >= 5, StudentToTake);
            }
            else if (wantedFilter == "average")
            {
                this.FilterAndTake(studentsWithMarks, x => x < 5 && x >= 3.5, StudentToTake);
            }
            else if (wantedFilter == "poor")
            {
                this.FilterAndTake(studentsWithMarks, x => x <= 3.5, StudentToTake);
            }

            else
            {
                throw new ArgumentException (ExceptionMessages.InvalidStudentFilter);
            }
        }

        private void FilterAndTake(Dictionary<string, double> studentsWithMarks,
            Predicate<double> givenFilter, int StudentToTake)
        {

            int counterForPrinted = 0;
            foreach (var studentMark in studentsWithMarks)
            {
                if (counterForPrinted == StudentToTake)
                {
                    break;
                }

               

                if (givenFilter(studentMark.Value))
                {
                    OutputWriter.PrintStudent(new KeyValuePair<string, double>(studentMark.Key, studentMark.Value));
                    counterForPrinted++;
                }
            }
        }


    //    private static bool ExcellentFilter(double mark)
    //  {
    //        return mark >= 5.0;
    //    }

    //    private static bool AverageFilter(double mark)
    //    {
    //        return mark < 5.0 && mark >= 3.5;
    //    }

    //   private static bool PoorFilter(double mark)
    //    {
    //        return mark < 3.5;
    //    }

        private static double Average(List<int> scoresOnTasks)
        {
            int totalScores = 0;
            foreach (var score in scoresOnTasks)
            {
                totalScores += score;
            }

            var percentageOfAll = totalScores / (scoresOnTasks.Count * 100);
            var mark = percentageOfAll * 4 + 2;
            return mark;
        }
    }
}
