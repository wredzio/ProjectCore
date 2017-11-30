using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticAlgorithmSchedule.Database.Models.Applications
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string role) : base(role) { }
    }
}
