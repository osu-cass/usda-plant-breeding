using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Usda.PlantBreeding.Site.Models.Helpers
{
    public static class Extensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> source,
                                                                   Func<T, string> valuePredicate,
                                                                   Func<T, string> textPredicate,
                                                                   Func<T, bool> selectedPredicate) where T : class
        {
            foreach (T item in source)
            {
                yield return new SelectListItem()
                {
                    Text = textPredicate(item),
                    Value = valuePredicate(item),
                    Selected = selectedPredicate(item)
                };
            }
        }
    }
}