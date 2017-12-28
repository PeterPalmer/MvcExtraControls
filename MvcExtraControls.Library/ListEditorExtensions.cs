using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MvcExtraControls.Library
{
    public static class ListEditorExtensions
    {
        /// <summary>
        /// Returns HTML editor for each element in the list that is represented
        /// by the expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <returns>An HTML editor for each element in the list that is represented by the expression.</returns>
        public static MvcHtmlString ListEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return ListEditorFor(htmlHelper, expression, null, "Remove", "Add item");
        }

        /// <summary>
        /// Returns HTML editor for each element in the list that is represented
        /// by the expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <returns>An HTML editor for each element in the list that is represented by the expression.</returns>
        public static MvcHtmlString ListEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return ListEditorFor(htmlHelper, expression, htmlAttributes, "Remove", "Add item");
        }

        /// <summary>
        /// Returns HTML editor for each element in the list that is represented
        /// by the expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="removeText">Text displayed in the remove-button.</param>
        /// <param name="addText">Text displayed in the add-button.</param>
        /// <returns>An HTML editor for each element in the list that is represented by the expression.</returns>
        public static MvcHtmlString ListEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string removeText, string addText)
        {
            return ListEditorFor(htmlHelper, expression, null, removeText, addText);
        }

        /// <summary>
        /// Returns HTML editor for each element in the list that is represented
        /// by the expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <param name="removeText">Text displayed in the remove-button.</param>
        /// <param name="addText">Text displayed in the add-button.</param>
        /// <returns>An HTML editor for each element in the list that is represented by the expression.</returns>
        public static MvcHtmlString ListEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes, string removeText, string addText)
        {
            return ListEditorFor(htmlHelper, expression, htmlAttributes, removeText, addText, String.Empty, String.Empty);
        }

        /// <summary>
        /// Returns HTML editor for each element in the list that is represented
        /// by the expression.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the value.</typeparam>
        /// <param name="htmlHelper">The HTML helper instance that this method extends.</param>
        /// <param name="expression">An expression that identifies the object that contains the properties to display.</param>
        /// <param name="htmlAttributes">An object that contains the HTML attributes to set for the element.</param>
        /// <param name="removeText">Text displayed in the remove-button.</param>
        /// <param name="addText">Text displayed in the add-button.</param>
        /// <param name="removeClass">CSS-class for the remove-button.</param>
        /// <param name="addClass">CSS-class for the add-button.</param>
        /// <returns>An HTML editor for each element in the list that is represented by the expression.</returns>
        public static MvcHtmlString ListEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes, string removeText, string addText, string removeClass, string addClass)
        {
            var expressionText = ExpressionHelper.GetExpressionText(expression);
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var stringList = metadata.Model as IList<string>;

            if(stringList == null)
            {
                if(metadata.Model == null)
                {
                    return MvcHtmlString.Empty;
                }

                return MvcHtmlString.Create($"<strong>{expressionText} must be a list of strings.</strong>");
            }

            var editorId = GetEditorId(expressionText);
            var builder = new StringBuilder($"<ul id='{editorId}' name='{expressionText}' class='ListEditor'>\r\n");
            var elementsCount = stringList.Count();

            for (int i = 0; i < elementsCount; i++)
            {
                var htmlTextbox = htmlHelper.TextBox($"{expressionText}[{i}]", stringList[i], htmlAttributes);
                var removeSpan = $"<span class='{removeClass}' onclick='{editorId}.Remove(this)'>{removeText}</span>";
                builder.AppendLine($"<li>{htmlTextbox}{removeSpan}</li>");
            }

            builder.AppendLine("</ul>");

            if (string.IsNullOrEmpty(addClass))
            {
                addClass = "addItem";
            }
            else
            {
                addClass = String.Concat(addClass.Trim(), " addItem");
            }

            builder.AppendLine($"<div class='{addClass}' onclick='{editorId}.AddItem()'>{addText}</div>");

            var inputCssClass = GetCssClass(htmlAttributes);
            builder.AppendLine($"<script>{editorId} = new ListEditor('{editorId}', {elementsCount}, '{inputCssClass}', '{removeText}', '{removeClass}');</script>");

            return new MvcHtmlString(builder.ToString());
        }

        private static string GetEditorId(string expression)
        {
            return expression.Replace(".", "_").Replace("[", "_").Replace("]", "_");
        }

        private static string GetCssClass(object htmlAttributes)
        {
            var propInfo = htmlAttributes?.GetType()?.GetProperty("class");
            if(propInfo == null)
            {
                return String.Empty;
            }

            return propInfo.GetValue(htmlAttributes, null).ToString();
        }
    }
}
