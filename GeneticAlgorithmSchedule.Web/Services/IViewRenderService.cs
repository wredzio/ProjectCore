using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderToString<T>(string viewName, T model);
    }
}
