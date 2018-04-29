using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BashSoft
{
    public class SoftUniStudent: IStudent
    {

        private string username;
        private Dictionary<string, ICourse> courses;
        private Dictionary<string, double> marksByCourseName;


        public SoftUniStudent(string userName)
        {
            this.Username = userName;
            this.courses = new Dictionary<string, ICourse>();
            this.marksByCourseName = new Dictionary<string, double>();
        }

        public string Username
        {
            get
            {
                return this.username;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.username = value;
            }
        }

        public IReadOnlyDictionary<string, ICourse> EnrolledCourses
        {
            get
            {
                return courses;
            }
        }

        public IReadOnlyDictionary<string, double> MarksByCourseName
        {
            get
            {
                return marksByCourseName;
            }
        }
        public void EnrollInCourse(ICourse course)
        {
            if (this.courses.ContainsKey(course.Name))
            {
                throw new DuplicateEntryInStructureException(this.Username, course.Name);
            }

            this.courses.Add(course.Name, course);
        }

        public void SetMarkOnCourse (string courseName, params int[] scores)
        {
            if (!this.courses.ContainsKey(courseName))
            {
                throw new ArgumentException (ExceptionMessages.NotEnrolledInCourse);
                
            }

            if (scores.Length > SoftUniCourse.NumberOfTasksOnExam)
            {
                throw new ArgumentException(ExceptionMessages.InvalidNumberOfScores);
                return;
            }

            this.marksByCourseName.Add(courseName, CalculateMark(scores));
        }

        public double CalculateMark (int[] scores)
        {
            double percentageOfSolvedExam = scores.Sum() /
                (double)(SoftUniCourse.NumberOfTasksOnExam * SoftUniCourse.MaxScoreOnExamTask);
            double mark = percentageOfSolvedExam * 4 + 2;
            return mark;
        }

        public int CompareTo(IStudent other) => this.Username.CompareTo(other.Username);

        public override string ToString()
        {
            return this.Username;
        }
    }
}
