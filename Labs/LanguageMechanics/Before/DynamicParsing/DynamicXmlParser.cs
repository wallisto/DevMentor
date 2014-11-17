using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DynamicParsing
{
    class DynamicXmlParser : DynamicObject
    {
        XElement element;
        public DynamicXmlParser(string file)
        {
            element = XElement.Load(file);
        }

        private DynamicXmlParser(XElement el)
        {
            element = el;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            XElement sub = element.Element(binder.Name);
            if (sub == null)
            {
                result = null;
                return false;
            }
            else
            {
                result = new DynamicXmlParser(sub);
                return true;
            }           
        }

        public override string ToString()
        {
            return element.Value;
        }

        public string this[string attr]
        {
            get { return element.Attribute(attr).Value; }
        }
    }
}
