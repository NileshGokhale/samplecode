using System.Web.Mvc;
using System.Linq.Expressions;
using System;

namespace GuestBook.Helpers
{
    /// <summary>
    /// Extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Displays the hello for.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TTextProperty">The type of the text property.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="textExpression">The text expression.</param>
        /// <returns></returns>
        public static string DisplayHelloFor<T, TTextProperty>(this HtmlHelper<T> html, Expression<Func<T, TTextProperty>> textExpression)
        {
            var text = ModelMetadata.FromLambdaExpression(textExpression, html.ViewData);
            return text.NullDisplayText;
        }
    }
}