using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithmSchedule.Web.Emails.PostBoxs
{
    static public class PostBoxService
    {
        public static void AddPostBoxService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IPostBox, PostBox>();
        }
    }
}
