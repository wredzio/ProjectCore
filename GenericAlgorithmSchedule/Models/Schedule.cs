using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithmSchedule.Models
{
    public static class Extensions
    {
        public static void RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dic,
                                                      TKey fromKey, TKey toKey)
        {
            TValue value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

        public static List<T> RepeatedDefault<T>(int count)
        {
            return Repeated(default(T), count);
        }

        public static List<T> Repeated<T>(T value, int count)
        {
            List<T> ret = new List<T>(count);
            ret.AddRange(Enumerable.Repeat(value, count));
            return ret;
        }
    }

    public class Schedule
    {
        private Random _rand;

        public const int NUMBER_OF_SCORE = 5;
        public float Fitness { get; set; }
        public List<bool> Criteria { get; set; }
        public List<CourseClass>[] Slots { get; set; }
        public Dictionary<CourseClass, int> Classes { get; set; }
        public School School { get; set; }

        public Schedule(School school, bool createWithEmptySlots, Random rand)
        {
            this.School = school;
            this._rand = rand;
            this.Fitness = 0;

            Classes = new Dictionary<CourseClass, int>();

            Slots = new List<CourseClass>[School.NumberOfWorkDays * School.NumberOfHoursInDay * School.Rooms.Count()];
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i] = new List<CourseClass>();
            }

            Criteria = Extensions.RepeatedDefault<bool>(School.CourseClasses.Count() * 5);

            if(!createWithEmptySlots)
            {
                MakeNew();
            }
            else
            {

            }

        }

        private Schedule(Schedule schedule)
        {
            this._rand = schedule._rand;
            this.Slots = schedule.Slots;
            this.Classes = schedule.Classes;
            this.Criteria = schedule.Criteria;
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

        public void CalculateFitness()
        {
            int score = 0;

            int numberOfRooms = School.Rooms.Count();
            int daySize = School.NumberOfHoursInDay * numberOfRooms;

            int criteria = 0;


            foreach (var _class in Classes)
            {
                int position = _class.Value;
                int day = position / daySize;
                int time = position % daySize;
                int roomId = time / School.NumberOfHoursInDay;
                if (roomId == 0)
                    roomId = numberOfRooms;

                time = time % School.NumberOfHoursInDay;

                int duration = _class.Key.Duration;

                bool isRoomOverlap = false;

                for (int i = duration - 1; i >= 0; i--)
                {
                    if (Slots[position + i].Count > 1)
                    {
                        isRoomOverlap = true;
                        break;
                    }
                }

                if (!isRoomOverlap)
                    score++;

                Criteria[criteria] = !isRoomOverlap;

                CourseClass courseClass = _class.Key;
                Room room = School.Rooms.FirstOrDefault(o => o.Id == roomId);

                Criteria[criteria + 1] = room.NumberOfSeats >= courseClass.NumberOfSeats;

                if (Criteria[criteria + 1])
                    score++;

                Criteria[criteria + 2] = !courseClass.RequiresLab || (courseClass.RequiresLab && room.Lab);

                if (Criteria[criteria + 2])
                    score++;

                bool professorOverlap = false, studentsGroupOverlap = false;

                for (int i = numberOfRooms, t = day * daySize + time; i > 0; i--, t += School.NumberOfHoursInDay)
                {
                    for (int j = duration - 1; j >= 0; j--)
                    {
                        var courseClasses = Slots[t + j];

                        foreach (var _courseClass in courseClasses)
                        {
                            if (!courseClass.Equals(_courseClass))
                            {
                                if (!professorOverlap && courseClass.ProfessorOverlaps(_courseClass))
                                {
                                    professorOverlap = true;
                                }

                                if (!studentsGroupOverlap && courseClass.GroupsOverlap(_courseClass))
                                {
                                    studentsGroupOverlap = true;
                                }

                                if (professorOverlap && studentsGroupOverlap)
                                {

                                }
                            }
                        }
                    }
                }

                if (!professorOverlap)
                    score++;

                Criteria[criteria + 3] = !professorOverlap;

                if (!studentsGroupOverlap)
                    score++;

                Criteria[criteria + 4] = !studentsGroupOverlap;

                criteria = criteria + 5;
            }

            Fitness = (float)score / (School.CourseClasses.Count() * NUMBER_OF_SCORE);
        }
    }
}
