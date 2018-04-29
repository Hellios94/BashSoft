using BashSoft.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BashSoft
{
    public class RepositorySorter : IDataSorter
    {

        public void OrderAndTake(Dictionary<string, double> StudentsMark, string comparison,
            int studentsToTake)
        {

            comparison = comparison.ToLower();
            if (comparison == "ascending")
            {

                OrderAndTake(StudentsMark, studentsToTake, CompareInOrder);
                this.PrintStudents(StudentsMark.OrderBy(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else if (comparison == "descending")
            {
                OrderAndTake(StudentsMark, studentsToTake, CompareDescendingOrder);
                this.PrintStudents(StudentsMark.OrderByDescending(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }

            else
            {
                throw new ArgumentException (ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private void PrintStudents(Dictionary<string, double> studentsSorted)
        {
            foreach (KeyValuePair<string, double> keyValuePair in studentsSorted)
            {
                OutputWriter.PrintStudent(keyValuePair);
            }
        }

        private void OrderAndTake (Dictionary<string, double> wantedData, int studentsToTake,
            Func<KeyValuePair<string, List<int>>, KeyValuePair<string, List<int>>, int> comparisonFunc)
        {

        }


        private int CompareInOrder(KeyValuePair<string, List<int>> firstValue,
            KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = 0;
            foreach (int i in firstValue.Value)
            {
                totalOfFirstMarks += 1;
            }
            int totalOfSecondMarks = 0;
            foreach (int i in secondValue.Value)
            {
                totalOfSecondMarks += 1;
            }

            return totalOfSecondMarks.CompareTo(totalOfFirstMarks);
        }

        private int CompareDescendingOrder(KeyValuePair<string, List<int>> firstValue,
            KeyValuePair<string, List<int>> secondValue)
        {
            int totalOfFirstMarks = 0;
            foreach (int i in firstValue.Value)
            {
                totalOfFirstMarks += 1;
            }
            int totalOfSecondMarks = 0;
            foreach (int i in secondValue.Value)
            {
                totalOfSecondMarks += 1;
            }

            return totalOfFirstMarks.CompareTo(totalOfSecondMarks);
        }


        private Dictionary<string, double> GetSortedStudents (Dictionary<string,double> studentsWanted,
           int takeCount, Func<KeyValuePair<string, double>, KeyValuePair<string, double>, int> comparison)
        {
            int valuesTaken = 0;
            Dictionary<string, double> studentsSorted = new Dictionary<string, double>();
            KeyValuePair<string, double> nextInOrder = new KeyValuePair<string, double>();
            bool isSorted = false;

            while (valuesTaken < takeCount)
            {
                isSorted = true;

                foreach (var studentsWithScore in studentsWanted)
                {
                    if (!String.IsNullOrEmpty(nextInOrder.Key))
                    {
                        int comparisonResult = comparison(studentsWithScore, nextInOrder);
                        if (comparisonResult >= 0 && !studentsSorted.ContainsKey(studentsWithScore.Key))
                        {
                            nextInOrder = studentsWithScore;
                            isSorted = false;
                        }
                    }
                    else
                    {
                        if (studentsSorted.ContainsKey(studentsWithScore.Key))
                        {
                            nextInOrder = studentsWithScore;
                            isSorted = false;
                               
                        }
                    }
                }

                if (!isSorted)
                {
                    studentsSorted.Add(nextInOrder.Key, nextInOrder.Value);
                    valuesTaken++;
                    nextInOrder = new KeyValuePair<string, double>();
                }
            }

            return studentsSorted;
        }
    }
}
