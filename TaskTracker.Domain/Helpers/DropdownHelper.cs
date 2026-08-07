using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TaskTracker.Data;

namespace TaskTracker.Domain.Helpers
{
    public static class DropdownHelper
    {

        public static SelectList GetMilestones(
            IEnumerable<Milestone> milestones,
            int? selected = null)
        {
            var items = milestones
                .Select(m => new
                {
                    Id = m.Id,

                    DisplayName =
                        m.Name +
                        " (" +
                        m.Project.Name +
                        ")"
                })
                .ToList();


            return new SelectList(
                items,
                "Id",
                "DisplayName",
                selected);
        }

    }
}