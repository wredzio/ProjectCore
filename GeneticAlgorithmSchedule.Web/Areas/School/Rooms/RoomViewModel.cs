using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Areas.School.Rooms
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool Lab { get; set; }

        [Range(0,500)]
        public int NumberOfSeats { get; set; }
    }
}
