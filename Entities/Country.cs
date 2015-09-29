using System;
using System.Collections.Generic;
using System.Text;
using Raven.Imports.Newtonsoft.Json;
using Raven.Imports.Newtonsoft.Json.Linq;

namespace Birko.SuperFaktura.Entities
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
