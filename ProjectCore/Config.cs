using OfficeOpenXml;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GeneticAlgorithmSchedule.Models;

namespace ProjectCore
{
    //Temp class -> Create Repository and Services
    public class Config
    {
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public static List<Teacher> Professors { get; set; }
        public static List<StudentsGroup> StudentsGroups { get; set; }
        public static List<Course> Courses { get; set; }
        public static List<CourseClass> CourseClasses { get; set; }
        public static List<Room> Rooms { get; set; }

        public static void Init()
        {

            GetRooms();
            GetCourses();
            GetProfessors();
            GetStudentsGroups();
            GetCourseClasses();
        }

        private static void GetCourseClasses()
        {
            CourseClasses = new List<CourseClass>();

            FileInfo fileInfo = new FileInfo("ProjectData/ClassData.xlsx");
            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets["Class"];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {

                    Teacher professor = Professors.FirstOrDefault(p => p.Id == Int32.Parse(worksheet.Cells[row, 2].Value.ToString()));
                    Course course = Courses.FirstOrDefault(c => c.Id == Int32.Parse(worksheet.Cells[row, 3].Value.ToString()));
                    bool requiresLab = worksheet.Cells[row, 4].Value.ToString() == "1" ? true : false;
                    int duration = Int32.Parse(worksheet.Cells[row, 5].Value.ToString());
                    var studentsGroup = StudentsGroups.Where(s => s.Id == Int32.Parse(worksheet.Cells[row, 6].Value.ToString())).ToList();

                    CourseClasses.Add(new CourseClass(professor, course, studentsGroup, requiresLab, duration));

                }
            }
        }

        private static void GetCourses()
        {
            Courses = new List<Course>();

            FileInfo fileInfo = new FileInfo("ProjectData/CourseData.xlsx");
            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets["Course"];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    Courses.Add(new Course()
                    {
                        Id = Int32.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        Name = worksheet.Cells[row, 2].Value.ToString(),
                    });
                }
            }
        }

        private static void GetRooms()
        {
            Rooms = new List<Room>();

            FileInfo fileInfo = new FileInfo("ProjectData/RoomData.xlsx");
            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets["Room"];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    Rooms.Add(new Room()
                    {
                        Id = Int32.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        Name = worksheet.Cells[row, 2].Value.ToString(),
                        Lab = worksheet.Cells[row, 3].Value.ToString() == "1" ? true : false,
                        NumberOfSeats = Int32.Parse(worksheet.Cells[row, 4].Value.ToString())
                    });
                }
            }
        }

        private static void GetStudentsGroups()
        {
            StudentsGroups = new List<StudentsGroup>();

            FileInfo fileInfo = new FileInfo("ProjectData/GroupData.xlsx");
            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets["Group"];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    StudentsGroups.Add(new StudentsGroup()
                    {
                        Id = Int32.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        Name = worksheet.Cells[row, 2].Value.ToString(),
                        NumberOfStudents = Int32.Parse(worksheet.Cells[row, 3].Value.ToString()),
                        CourseClasses = new List<CourseClass>()
                    });
                }
            }
        }

        private static void GetProfessors()
        {
            Professors = new List<Teacher>();

            FileInfo fileInfo = new FileInfo("ProjectData/TeacherData.xlsx");
            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets["Professor"];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    Professors.Add(new Teacher()
                    {
                        Id = Int32.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        Name = worksheet.Cells[row, 2].Value.ToString(),
                        CourseClasses = new List<CourseClass>(),
                        Available = new List<int>()
                    });
                }

                var professorAvailable = package.Workbook.Worksheets["Available"];
                int professorAvailableRowCount = professorAvailable.Dimension.Rows;
                int professorAvailableColCount = professorAvailable.Dimension.Columns;

                for (int row = 2; row <= professorAvailableRowCount; row++)
                {
                    int professorId = Int32.Parse(professorAvailable.Cells[row, 1].Value.ToString());
                    int availableIndex = Int32.Parse(professorAvailable.Cells[row, 2].Value.ToString());

                    Professors.FirstOrDefault(p => p.Id == professorId).Available.Add(availableIndex);
                }

            }
        }

        public static void AddResult(List<float> best, List<float> forst, List<float> a)
        {
            CreateResults(best, forst, a);
            
        }

        private static ExcelPackage CreateResults(List<float> best, List<float> forst, List<float> a)
        {
            FileInfo fileInfo = new FileInfo("ProjectData/ResultExcel.xlsx");
            using (var package = new ExcelPackage(fileInfo))
            {

                for (int i = 0; i < a.Count; i++)
                {

                    var worksheet = package.Workbook.Worksheets["Time5"];

                    worksheet.Cells[i + 1, 1].Value = best[i];
                    worksheet.Cells[i + 1, 2].Value = forst[i];
                    worksheet.Cells[i + 1, 3].Value = a[i];

                }
                package.Save();

                return package;
            }
        }


        public static void CreateSchedule(Schedule bestChromosome, School school)
        {
            using (var package = CreateExcelPackage(bestChromosome, school))
            {
                package.SaveAs(new FileInfo("ProjectData/OutputExcel.xlsx"));
            }
        }

        private static ExcelPackage CreateExcelPackage(Schedule bestChromosome, School school)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Plan";
            package.Workbook.Properties.Author = "Wojszm";
            package.Workbook.Properties.Subject = "Plan";
            package.Workbook.Properties.Keywords = "Plan";


            var worksheet = package.Workbook.Worksheets.Add("Schedule");


            foreach (var item in Rooms.Select((value, index) => new { Index = index, Value = value }))
            {
                var value = item.Value;
                var index = item.Index;

                for (int dayOfWeek = 1; dayOfWeek <= school.NumberOfWorkDays; dayOfWeek++)
                {
                    worksheet.Cells[(index * (school.NumberOfHoursInDay + 3) + 1), 1].Value = item.Value.Name;
                    worksheet.Cells[(index * (school.NumberOfHoursInDay + 3) + 2), 1].Value = "Godzina/Dzieñ";
                    worksheet.Cells[(index * (school.NumberOfHoursInDay + 3) + 2), dayOfWeek + 1].Value = dayOfWeek;

                    for (int hour = 0; hour < school.NumberOfHoursInDay; hour++)
                    {
                        worksheet.Cells[(index * (school.NumberOfHoursInDay + 3) + 3) + hour, 1].Value = hour + 7;


                    }
                }

                foreach (var _class in bestChromosome.Classes)
                {
                    int numberOfRooms = Rooms.Count;
                    int daySize = school.NumberOfHoursInDay * numberOfRooms;

                    int position = _class.Value;
                    int day = position / daySize;
                    int time = position % daySize;
                    int roomPosition = time / school.NumberOfHoursInDay;

                    time = time % school.NumberOfHoursInDay;

                    int duration = _class.Key.Duration;

                    if (roomPosition == index)
                    {
                        for (int i = duration - 1; i >= 0; i--)
                        {
                            worksheet.Cells[(index * (school.NumberOfHoursInDay + 3) + 3) + time + i, day + 2].Value = _class.Key.Course.Name + " / " + string.Join(",", _class.Key.StudentsGroups.Select(o => o.Name)) + " " + _class.Key.Teacher.Id + " " + _class.Key.Teacher.Name;
                        }
                    }
                }

            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package;
        }
    }


}