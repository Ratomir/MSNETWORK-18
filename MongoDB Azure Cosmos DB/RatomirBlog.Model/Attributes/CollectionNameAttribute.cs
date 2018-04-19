using System;
using System.Collections.Generic;
using System.Text;

namespace RatomirBlog.Model.Attributes
{
    public class CollectionNameAttribute : Attribute
    {
        public string Name { get; set; }

        public CollectionNameAttribute(string name)
        {
            Name = name;
        }
    }
}
