using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcExtraControls.ExampleNet46.Models
{
    [Bind(Include = "StringList, NestedModel")]
    public class ViewModel
    {
        public List<string> StringList { get; set; }
        public ViewModel NestedModel { get; set; }
    }
}