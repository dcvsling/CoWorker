using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorker.Models.Abstractions
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Delegate | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = true)]
    public abstract class ActionAttribute : Attribute
    {
        public ActionAttribute(string method, string path)
        {

        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface,AllowMultiple =false,Inherited =false)]
    public class RESTAttribute : Attribute
    {
        public RESTAttribute(string name)
        {

        }

    }
}
