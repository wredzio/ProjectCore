using System.Collections.Generic;
using System;
using System.Linq;

namespace ProjectCore.Algotithm
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
        private Random rand = new Random();

        public const int NUMBER_OF_DAYS = 5;
        public const int NUMBER_OF_HOURS_IN_DAY = 12;
        public int NumberOfCrossoverPoints { get; set; }
        public int MutationSize { get; set; }
        public int CrosoverProbability { get; set; }
        public int MutationProbability { get; set; }
        public float Fitness { get; set; }
        public List<bool> Criteria { get; set; }
        public List<CourseClass>[] Slots { get; set; }
        public Dictionary<CourseClass, int> Classes { get; set; }

        public Schedule(int numberOfCrossoverPoints, int mutationSize, int crosoverProbability, int mutationProbability)
        {
            Config.Init();

            this.NumberOfCrossoverPoints = numberOfCrossoverPoints;
            this.MutationSize = mutationSize;
            this.CrosoverProbability = crosoverProbability;
            this.MutationProbability = mutationProbability;
            this.Fitness = 0;

            Classes = new Dictionary<CourseClass, int>();

            Slots = new List<CourseClass>[NUMBER_OF_DAYS * NUMBER_OF_HOURS_IN_DAY * Config.Rooms.Count];
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i] = new List<CourseClass>();
            }

            Criteria = Extensions.RepeatedDefault<bool>(Config.CourseClasses.Count * 5);
        }

        private Schedule(Schedule schedule, bool setUpOnly)
        {

            Config.Init();

            if (!setUpOnly)
            {
                this.Slots = schedule.Slots;
                this.Classes = schedule.Classes;
                this.Criteria = schedule.Criteria;
                this.Fitness = schedule.Fitness;
            }
            else
            {
                Classes = new Dictionary<CourseClass, int>();

                Slots = new List<CourseClass>[NUMBER_OF_DAYS * NUMBER_OF_HOURS_IN_DAY * Config.Rooms.Count];
                for (int i = 0; i < Slots.Length; i++)
                {
                    Slots[i] = new List<CourseClass>();
                }

                Criteria = Extensions.RepeatedDefault<bool>(Config.CourseClasses.Count * 5);
            }

            this.NumberOfCrossoverPoints = schedule.NumberOfCrossoverPoints;
            this.MutationSize = schedule.MutationSize;
            this.CrosoverProbability = schedule.CrosoverProbability;
            this.MutationProbability = schedule.MutationProbability;
        }

        public Schedule Copy(bool setUpOnly)
        {
            return new Schedule(this, setUpOnly);
        }

        public Schedule MakeNewFromPrototype()
        {
            Schedule newChromosome = new Schedule(this, true);

            List<CourseClass> courseClasses = Config.CourseClasses;

            foreach (var courseClass in courseClasses)
            {
                int numberOfRooms = Config.Rooms.Count;
                int duration = courseClass.Duration;
                int day = rand.Next() % NUMBER_OF_DAYS;
                int room = rand.Next() % numberOfRooms;
                int time = rand.Next() % (NUMBER_OF_HOURS_IN_DAY + 1 - duration);
                int position = day * numberOfRooms * NUMBER_OF_HOURS_IN_DAY + room * NUMBER_OF_HOURS_IN_DAY + time;

                for (int i = duration - 1; i >= 0; i--)
                {
                    newChromosome.Slots[position + i].Add(courseClass);
                }

                newChromosome.Classes.Add(courseClass, position);
            }
            newChromosome.CalculateFitness();

            return newChromosome;
        }

        public Schedule Crossover(Schedule secondParent)
        {
            if (rand.Next() % 100 > CrosoverProbability)
                return new Schedule(this, false);

            Schedule newChromosome = new Schedule(this, true);

            int size = Classes.Count;

            List<bool> pointsToCrossover = Extensions.RepeatedDefault<bool>(size);

            for (int i = NumberOfCrossoverPoints; i > 0; i--)
            {
                while (true)
                {
                    int crossoverPoint = rand.Next() % size;
                    if (!pointsToCrossover[crossoverPoint])
                    {
                        pointsToCrossover[crossoverPoint] = true;
                        break;
                    }
                }
            }

            bool first = rand.Next() % 2 == 0;
            for (int i = 0; i < size; i++)
            {
                if (first)
                {
                    var firstParentCourseClass = Classes.ElementAt(i);

                    newChromosome.Classes.Add(firstParentCourseClass.Key, firstParentCourseClass.Value);

                    for (int j = firstParentCourseClass.Key.Duration - 1; j >= 0; j--)
                    {
                        newChromosome.Slots[firstParentCourseClass.Value + j].Add(firstParentCourseClass.Key);
                    }
                }
                else
                {
                    var secondParentCourseClass = secondParent.Classes.ElementAt(i);

                    newChromosome.Classes.Add(secondParentCourseClass.Key, secondParentCourseClass.Value);

                    for (int j = secondParentCourseClass.Key.Duration - 1; j >= 0; j--)
                    {
                        newChromosome.Slots[secondParentCourseClass.Value + j].Add(secondParentCourseClass.Key);
                    }
                }

                if (pointsToCrossover[i])
                {
                    first = !first;
                }
            }
            newChromosome.CalculateFitness();

            return newChromosome;
        }

        public void Mutation()
        {
            if (rand.Next() % 100 > MutationProbability)
            {
                return;
            }

            int numberOfClasses = Classes.Count;
            int numberOfSlots = Slots.Length;

            for (int i = MutationSize; i > 0; i--)
            {
                int randomClassIdToMove = rand.Next() % numberOfClasses;
                KeyValuePair<CourseClass, int> courseClassToMutation = Classes.ToList<KeyValuePair<CourseClass, int>>()[randomClassIdToMove];

                int numberOfRooms = Config.Rooms.Count;
                int duration = courseClassToMutation.Key.Duration;
                int day = rand.Next() % NUMBER_OF_DAYS;
                int room = rand.Next() % numberOfRooms;
                int time = rand.Next() % (NUMBER_OF_HOURS_IN_DAY + 1 - duration);
                int position = day * numberOfRooms * NUMBER_OF_HOURS_IN_DAY + room * NUMBER_OF_HOURS_IN_DAY + time;

                for (int j = duration - 1; j >= 0; j--)
                {
                    var courseClasses = Slots[courseClassToMutation.Value + j];

                    foreach (var courseClass in courseClasses)
                    {
                        if (courseClassToMutation.Value.Equals(courseClass))
                        {
                            courseClasses.Remove(courseClass);
                            break;
                        }
                    }

                    Slots[position + j].Add(courseClassToMutation.Key);
                }
                Classes[courseClassToMutation.Key] = position;
            }
            CalculateFitness();
        }

        public void CalculateFitness()
        {
            int score = 0;

            int numberOfRooms = Config.Rooms.Count;
            int daySize = NUMBER_OF_HOURS_IN_DAY * numberOfRooms;

            int criteria = 0;


            foreach (var _class in Classes)
            {
                int position = _class.Value;
                int day = position / daySize;
                int time = position % daySize;
                int roomId = time / NUMBER_OF_HOURS_IN_DAY;
                if (roomId == 0)
                    roomId = numberOfRooms;

                time = time % NUMBER_OF_HOURS_IN_DAY;

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
                Room room = Config.Rooms.FirstOrDefault(o => o.Id == roomId);

                Criteria[criteria + 1] = room.NumberOfSeats >= courseClass.NumberOfSeats;

                if (Criteria[criteria + 1])
                    score++;

                Criteria[criteria + 2] = !courseClass.RequiresLab || (courseClass.RequiresLab && room.Lab);

                if (Criteria[criteria + 2])
                    score++;

                bool professorOverlap = false, studentsGroupOverlap = false;

                for (int i = numberOfRooms, t = day * daySize + time; i > 0; i--, t += NUMBER_OF_HOURS_IN_DAY)
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

            Fitness = (float)score / (Config.CourseClasses.Count * NUMBER_OF_DAYS);
        }
    }
}