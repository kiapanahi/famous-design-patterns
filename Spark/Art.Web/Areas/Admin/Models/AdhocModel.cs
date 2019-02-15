using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Art.Web.Areas.Admin.Models
{
    public class AdhocModel
    {
        public Dictionary<string, List<AdhocColumn>> Schema { get; set; }
        public string Sql { get; set; }
        public string Exception { get; set; }

        public List<Builtin> Builtins { get; set; }
        public string CurrentBuiltin { get; set; }


        public List<List<string>> Results { get; set; }
        public AdhocModel()
        {
            Results = new List<List<string>>();
            Builtins = new List<Builtin>();
        }
    }

    public class AdhocColumn
    {
        public string Name { get; set; }
        public string DataType { get; set; }
    }
    public class Builtin
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Sql { get; set; }
    }
}