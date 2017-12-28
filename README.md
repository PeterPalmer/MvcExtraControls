MvcExtraControls - Helpfull extensions to System.Web.Mvc.HtmlHelper.
--------------------------------------------------------------------

ListEditorFor method creates an editor to add, remove and change elements in a list on the view model.

[nuget.org/packages/PeterPalmer.MvcExtraControls](https://www.nuget.org/packages/PeterPalmer.MvcExtraControls)

Example usage
-------------

###### View model:
```csharp
    [Bind(Include = "StringList")]
    public class ViewModel
    {
        public List<string> StringList { get; set; }
    }
```

###### View:
```Html
<html>
  <head>
    <script src="/Scripts/ListEditor.js"></script>
    <link href="~/Content/ListEditor.css" rel="stylesheet" />
  </head>
  <body>
    @Html.ListEditorFor(m => m.StringList)
  </body>
</html>
```
