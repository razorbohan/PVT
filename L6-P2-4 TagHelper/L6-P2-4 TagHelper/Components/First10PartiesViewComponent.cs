using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Memorizer.Web.Components
{
    public class First10PartiesViewComponent: ViewComponent
    {
        public First10PartiesViewComponent()
        {
            
        }

        public string Invoke()
        {
            //var item = phones.OrderByDescending(x => x.Price).Take(1).FirstOrDefault();

            return $"Самый дорогой телефо";
        }
    }
}
