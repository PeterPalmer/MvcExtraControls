MvcExtraControls - Helpfull extensions to System.Web.Mvc.HtmlHelper.
--------------------------------------------------------------------

ListEditorFor method creates an editor to add, remove and change elements in a list on the view model with full binding.

[NuGet package](https://www.nuget.org/packages/MvcExtraControls.ListEditor)

* * *
### Example usage

###### Model:
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

###### Controller:
```csharp
public ActionResult Index()
{
  var model = new ViewModel()
  {
    StringList = new List<string> { "One", "Two", "Three" }
  };

  return View(model);
}
```

###### Result:
![Result](https://raw.githubusercontent.com/PeterPalmer/MvcExtraControls/master/Example.png)
