
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace GeneticAlgorithmSchedule.Models
{
    public class CourseClass
    {
        public Professor Professor { get; set; }
        public Course Course { get; set; }
        public List<StudentsGroup> StudentsGroups { get; set; }
        public int NumberOfSeats { get; set; }
        public bool RequiresLab { get; set; }
        public int Duration { get; set; }

        public CourseClass(Professor professtor, Course course, List<StudentsGroup> studentsGroups,
                            bool requiresLab, int duration)
        {
            Professor = professtor;
            Course = course;
            RequiresLab = requiresLab;
            Duration = duration;
            StudentsGroups = studentsGroups;

            Professor.CourseClasses.Add(this);

            foreach (StudentsGroup studentGroup in StudentsGroups)
            {
                studentGroup.CourseClasses.Add(this);
                NumberOfSeats += studentGroup.NumberOfStudents;
            }
        }

        public bool GroupsOverlap(CourseClass courseClass)
        {
            foreach (var group in courseClass.StudentsGroups)
            {
                if (StudentsGroups.Any(o => o.Equals(group)))
                    return true;
            }
            return false;
        }

        public bool ProfessorOverlaps(CourseClass courseClass)
        {
            return Professor.Id == courseClass.Professor.Id;
        }
    }
}
