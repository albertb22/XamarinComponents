﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Xamarin.iOS.Binding.Transformer
{
    public class ApiNamespace : ApiObject
    {
        protected internal override string NodeName => $"namespace[@name='{Name}']";

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "delegate", Order = 1)]
        public List<ApiDelegate> Delegates { get; set; }

        [XmlElement(ElementName = "class", Order = 2)]
        public List<ApiClass> Types { get; set; }

        public ApiNamespace()
        {
            Types = new List<ApiClass>();
            Delegates = new List<ApiDelegate>();
        }

        internal protected override void SetParent(ApiObject parent)
        {
            base.SetParentInternal(parent);

            foreach (var aObject in Delegates)
            {
                aObject.SetParent(this);
            }

            foreach (var aObject in Types)
            {
                aObject.SetParent(this);
            }
        }

        internal protected override void UpdatePathList(ref Dictionary<string, ApiObject> dict)
        {
            dict.Add(Path, this);

            foreach (var aNamespace in Delegates)
            {
                aNamespace.UpdatePathList(ref dict);
            }

            foreach (var aNamespace in Types)
            {
                aNamespace.UpdatePathList(ref dict);
            }
        }
    }
}