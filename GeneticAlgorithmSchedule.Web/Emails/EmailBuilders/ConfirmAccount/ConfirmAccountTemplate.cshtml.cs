using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GeneticAlgorithmSchedule.Web.Emails.EmailBuilders.ConfirmAccount
{
    public class ConfirmAccountTemplateModel : PageModel
    {
        public string UserFullName { get; set; }

        public void OnGet()
        {
        }
    }
}