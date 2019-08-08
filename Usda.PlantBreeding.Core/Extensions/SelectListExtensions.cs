using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Usda.PlantBreeding.Core.Extensions
{
    public static class SelectListExtensions
    {
        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> entities, Func<T, bool> Disabled, Func<T, object> Value, Func<T, object> Text)
        {
            return entities.Select(t => new SelectListItem
                                    {
                                        Value = Value(t).ToString(),
                                        Text = Text(t).ToString(),
                                        Disabled = Disabled(t)
                                    });
        }

        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> entities, Func<T, object> Value, Func<T, object> Text)
        {
            return entities.Select(t => new SelectListItem
            {
                Value = Value(t).ToString(),
                Text = Text(t).ToString()
            });
        }

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
