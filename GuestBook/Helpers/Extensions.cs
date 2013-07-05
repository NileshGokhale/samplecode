using System.Web.Mvc;
using System.Linq.Expressions;
using System;
public static class Extensions
{
    public static string DisplayHelloFor<T, TTextProperty>(this HtmlHelper<T> html, Expression<Func<T, TTextProperty>> textExpression)
    {
        var text = ModelMetadata.FromLambdaExpression(textExpression, html.ViewData);
        return text.NullDisplayText;
    }
}