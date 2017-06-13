using OfficeOpenXml;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectCore.Algotithm
{
    //Temp class -> Create Repository and Services
    public class Config
    {
        private const string XlsxContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public static List<Professor> Professors { get; set; }
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

                    Professor professor = Professors.FirstOrDefault(p => p.Id == Int32.Parse(worksheet.Cells[row, 2].Value.ToString()));
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
                int ColCount = worksheet.Dimension.Columns;

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
                int ColCount = worksheet.Dimension.Columns;

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
            Professors = new List<Professor>();

            FileInfo fileInfo = new FileInfo("ProjectData/ProfessorData.xlsx");
            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets["Professor"];
                int rowCount = worksheet.Dimension.Rows;
                int ColCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    Professors.Add(new Professor()
                    {
                        Id = Int32.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        Name = worksheet.Cells[row, 2].Value.ToString(),
                        CourseClasses = new List<CourseClass>()
                    });
                }
            }
        }

        public static void CreateSchedule(Schedule bestChromosome)
        {
            using (var package = createExcelPackage(bestChromosome))
            {
                package.SaveAs(new FileInfo("ProjectData/OutputExcel.xlsx"));
            }
        }

        private static ExcelPackage createExcelPackage(Schedule bestChromosome)
        {
            var package = new ExcelPackage();
            package.Workbook.Properties.Title = "Plan";
            package.Workbook.Properties.Author = "Wojszm";
            package.Workbook.Properties.Subject = "Plan";
            package.Workbook.Properties.Keywords = "Plan";


            var worksheet = package.Workbook.Worksheets.Add("Schedule");


            foreach (var item in Rooms.Select((Value, Index) => new { Index, Value }))
            {
                var value = item.Value;
                var index = item.Index;

                for (int dayOfWeek = 1; dayOfWeek <= Schedule.NUMBER_OF_DAYS; dayOfWeek++)
                {
                    worksheet.Cells[(index * (Schedule.NUMBER_OF_HOURS_IN_DAY + 3) + 1), 1].Value = item.Value.Name;
                    worksheet.Cells[(index * (Schedule.NUMBER_OF_HOURS_IN_DAY + 3) + 2), 1].Value = "Godzina/Dzieñ";
                    worksheet.Cells[(index * (Schedule.NUMBER_OF_HOURS_IN_DAY + 3) + 2), dayOfWeek + 1].Value = dayOfWeek;

                    for (int hour = 0; hour < Schedule.NUMBER_OF_HOURS_IN_DAY; hour++)
                    {
                        worksheet.Cells[(index * (Schedule.NUMBER_OF_HOURS_IN_DAY + 3) + 3) + hour, 1].Value = hour + 7;


                    }
                }

                foreach (var _class in bestChromosome.Classes)
                {
                    int numberOfRooms = Rooms.Count;
                    int daySize = Schedule.NUMBER_OF_HOURS_IN_DAY * numberOfRooms;

                    int position = _class.Value;
                    int day = position / daySize;
                    int time = position % daySize;
                    int roomId = time / Schedule.NUMBER_OF_HOURS_IN_DAY;
                    if (roomId == 0)
                        roomId = numberOfRooms;

                    time = time % Schedule.NUMBER_OF_HOURS_IN_DAY;

                    int duration = _class.Key.Duration;

                    if(roomId == value.Id)
                    {
                        for (int i = duration - 1; i >= 0; i--)
                        {
                            worksheet.Cells[(index * (Schedule.NUMBER_OF_HOURS_IN_DAY + 3) + 3) + time + i, day +2].Value = _class.Key.Course.Name + " / " + string.Join(",", _class.Key.StudentsGroups.Select(o => o.Name)) ;
                        }
                    }
                }

            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            return package;
        }
    }


}