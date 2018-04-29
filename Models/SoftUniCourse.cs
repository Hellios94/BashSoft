using BashSoft.Contracts;
using BashSoft.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BashSoft.Models
{
    public class SoftUniCourse : ICourse
    {

        private string name;
        private Dictionary<string, IStudent> students;

        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        public SoftUniCourse(string courseName)

        {
            this.Name = courseName;
            this.students = new Dictionary<string, IStudent>();
        }


        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName
        {
            get
            {
                return students;
            }
        }


        public void EnrollStudent(IStudent student)
        {
            if (this.students.ContainsKey(student.Username))
            {
                throw new DuplicateEntryInStructureException(student.Username, this.Name);
            }

            this.students.Add(student.Username, student);
        }

        public int CompareTo(ICourse other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
