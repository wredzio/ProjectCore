using GeneticAlgorithmSchedule.Infrastructure.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithmSchedule.Models
{
    public class Schedule : IChromosome
    {
        public static bool WithSoft { get; set; }

        private readonly Random _rand;
        public const int NumberOfScore = 6;
        public const int NumberOfSoftScore = 2;

        public List<CourseClass>[] Slots { get; set; }
        public Dictionary<CourseClass, int> Classes { get; set; }
        public School School { get; set; }
        public float Fitness { get; set; }
        public float FitnessSoft { get; set; }
        protected string Identity { get; set; }

        public Schedule(School school, bool createWithEmptySlots, Random rand)
        {
            this.School = school;
            this._rand = rand;
            this.Fitness = 0;

            if (!WithSoft)
                this.FitnessSoft = 1;

            Classes = new Dictionary<CourseClass, int>();

            Slots = new List<CourseClass>[School.NumberOfWorkDays * School.NumberOfHoursInDay * School.Rooms.Count()];
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i] = new List<CourseClass>();
            }

            if (!createWithEmptySlots)
            {
                MakeNew();
            }
        }

        private Schedule(Schedule schedule)
        {
            this._rand = schedule._rand;
            this.Slots = schedule.Slots;
            this.Classes = schedule.Classes;
            this.Fitness = schedule.Fitness;
            this.School = schedule.School;
        }

        public Schedule Copy()
        {
            return new Schedule(this);
        }

        public void MakeNew()
        {
            List<CourseClass> courseClasses = School.CourseClasses.ToList();

            foreach (var courseClass in courseClasses)
            {
                int numberOfRooms = School.Rooms.Count();
                int duration = courseClass.Duration;
                int day = _rand.Next() % School.NumberOfWorkDays;
                int room = _rand.Next() % numberOfRooms;
                int time = _rand.Next() % (School.NumberOfHoursInDay + 1 - duration);
                int position = day * numberOfRooms * School.NumberOfHoursInDay + room * School.NumberOfHoursInDay + time;

                for (int i = duration - 1; i >= 0; i--)
                {
                    this.Slots[position + i].Add(courseClass);
                }

                this.Classes.Add(courseClass, position);
            }
            this.CalculateFitness();
        }

        public virtual void CalculateFitness()
        {
            int score = 0;
            int softScore = 0;

            int numberOfRooms = School.Rooms.Count();
            int daySize = School.NumberOfHoursInDay * numberOfRooms;

            foreach (var _class in Classes)
            {
                int position = _class.Value;

                int day = position / daySize;
                int time = position % daySize;
                int roomPosition = time / School.NumberOfHoursInDay;

                time = time % School.NumberOfHoursInDay;

                int duration = _class.Key.Duration;

                bool isRoomOverlap = this.IsRoomOverlap(position, duration);
                if (!isRoomOverlap)
                    score++;

                CourseClass courseClass = _class.Key;
                Room room = School.Rooms.ElementAt(roomPosition);

                if (room != null && room.NumberOfSeats >= courseClass.NumberOfSeats)
                    score++;

                if (room != null && (!courseClass.RequiresLab || (courseClass.RequiresLab && room.Lab)))
                    score++;

                bool teacherOverlap = false, studentsGroupOverlap = false;

                for (int i = numberOfRooms, t = day * daySize + time; i > 0; i--, t += School.NumberOfHoursInDay)
                {
                    for (int j = duration - 1; j >= 0; j--)
                    {
                        var courseClasses = Slots[t + j];

                        foreach (var _courseClass in courseClasses)
                        {
                            if (!courseClass.Equals(_courseClass))
                            {
                                if (!teacherOverlap && courseClass.TeacherOverlaps(_courseClass))
                                {
                                    teacherOverlap = true;
                                }

                                if (!studentsGroupOverlap && courseClass.GroupsOverlap(_courseClass))
                                {
                                    studentsGroupOverlap = true;
                                }
                            }
                        }
                    }
                }

                if (!teacherOverlap)
                    score++;

                if (!studentsGroupOverlap)
                    score++;

                bool isTeacherAvilable = true;
                for (int i = 0; i < duration; i++)
                {
                    if (courseClass.Teacher.Availables.All(o => o.Id != time + day * School.NumberOfHoursInDay + i))
                    {
                        isTeacherAvilable = false;
                        break;
                    }
                }

                if (isTeacherAvilable)
                    score++;

                if (WithSoft)
                {
                    if (time < School.NumberOfHoursInDay / 2)
                    {
                        softScore += 2;
                    }
                }
            }


            SetIdentity();

            Fitness = (float)score / (School.CourseClasses.Count() * NumberOfScore);
            FitnessSoft = (float)softScore / (School.CourseClasses.Count() * NumberOfSoftScore);
        }

        protected void SetIdentity()
        {
            StringBuilder identityBuilder = new StringBuilder();

            foreach (var Class in Classes)
            {
                identityBuilder.Append(Class.Value + Class.Key.Course.Id);
            }

            Identity = identityBuilder.ToString();
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Schedule schedule = (Schedule)obj;

            return schedule.Identity == this.Identity;
        }

        public override int GetHashCode() => base.GetHashCode();

        protected bool IsRoomOverlap(int position, int duration)
        {
            bool isRoomOverlap = false;

            for (int i = duration - 1; i >= 0; i--)
            {
                if (Slots[position + i].Count > 1)
                {
                    isRoomOverlap = true;
                    break;
                }
            }

            return isRoomOverlap;
        }
    }
}
