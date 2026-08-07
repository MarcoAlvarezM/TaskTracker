using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace TaskTracker.Domain.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<SelectListItem> GetSelectList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new SelectListItem
                {
                    Value = Convert.ToInt32(e).ToString(),
                    Text = e.ToString()
                });
        }
    }
}