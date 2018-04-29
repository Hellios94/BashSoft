using BashSoft;
using BashSoft.Contracts;
using BashSoft.DataStructures;
using BashSoft.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BashSoft
{
    public class StudentRepository : IDatabase
    {
        public bool isDataInitialized;
        private Dictionary<string, ICourse> courses;
        private Dictionary<string, IStudent> students;
        private IDataSorter sorter;
        private IDataFilter filter;


        public StudentRepository (IDataFilter filter, IDataSorter sorter)
        {
            this.filter = filter;
            this.sorter = sorter;
        }

        public  void LoadData(string fileName)
        {

            if (this.isDataInitialized)
            {

                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataIsAlredyInitializedException);
                return;
            }

            this.students = new Dictionary<string, IStudent>();
            this.courses = new Dictionary<string, ICourse>();
            this.ReadData(fileName);

        }

        public void UnloadData()
        {
            if (!this.isDataInitialized)
            {
                throw new ArgumentException (ExceptionMessages.DataNotInititializedExceptionMessage);
            }

            this.students = null;
            this.courses = null;
            this.isDataInitialized = false;
        }

        private void ReadData(string fileName)
        {
            string path = SessionData.currentsPath + "\\" + fileName;
            OutputWriter.WriteMessageOnNewLine("Reading Data..");

            if (File.Exists(path))
            {
                string pattern = @"([A-Z][a-zA-Z#\+]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                Regex rgx = new Regex(pattern);
                string[] allInputLines = File.ReadAllLines(path);

                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[line]) && rgx.IsMatch(allInputLines[line]))
                    {
                        Match currentMatch = rgx.Match(allInputLines[line]);
                        string courseName = currentMatch.Groups[1].Value;
                        string userName = currentMatch.Groups[2].Value;
                        string scoresStr = currentMatch.Groups[3].Value;
                        try
                        {
                            int[] scores = scoresStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(int.Parse).ToArray();
                            if (scores.Any(x => x > 100 || x < 0))
                            {
                                throw new ArgumentException (ExceptionMessages.InvalidScore);
                            }
                            
                            if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!this.students.ContainsKey(userName))
                            {
                                this.students.Add(userName, new SoftUniStudent(userName));
                            }

                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new SoftUniCourse(courseName));
                            }

                            ICourse course = this.courses[courseName];
                            IStudent student = this.students[userName];

                            student.EnrollInCourse(course);
                            student.SetMarkOnCourse(courseName, scores);

                            course.EnrollStudent(student);
                        }
                        catch(FormatException fex)
                        {
                            OutputWriter.DisplayException(fex.Message + $"at line : {line}");
                        }
                    }
                }

                
                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
        }

        private  bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (this.courses.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    throw new ArgumentException(ExceptionMessages.InexistingCourseInDataBase);

                }
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.DataNotInititializedExceptionMessage);
            }
            
        }

        private  bool IsQueryForStudentPossible(string courseName, string studentUserName)
        {
            if (IsQueryForCoursePossible(courseName) && this.courses[courseName].StudentsByName.ContainsKey(studentUserName))
            {
                return true;
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InexistingStudentInDataBase);

            }

            return false;
        }


        public void GetStudentMarkInCourse(string courseName, string username)
        {
            if (IsQueryForStudentPossible(courseName, username))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double>(username, this.courses[courseName].StudentsByName[username].MarksByCourseName[courseName]));
            }
        }

        public  void GetStudentsByCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var studentMarksEntry in this.courses[courseName].StudentsByName)
                {
                    this.GetStudentMarkInCourse(courseName, studentMarksEntry.Key);
                }
            }
        }

        public  void FilterAndTake (string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks = this.courses[courseName].StudentsByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

                this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
            }
        }

        public  void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }

                Dictionary<string, double> marks =
                    this.courses[courseName].StudentsByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

                this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
            }
        }

        public ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp)
        {
            SimpleSortedList<ICourse> sortedCourses = new SimpleSortedList<ICourse>(cmp);
            sortedCourses.AddAll(this.courses.Values);

            return sortedCourses;
        }

        public ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> cmp)
        {
            SimpleSortedList<IStudent> sortedStudents = new SimpleSortedList<IStudent>(cmp);
            sortedStudents.AddAll(this.students.Values);

            return sortedStudents;
        }
    }
}
