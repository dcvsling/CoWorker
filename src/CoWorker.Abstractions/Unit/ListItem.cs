using System;

namespace CoWorker.Abstractions.Values
{

    public struct ListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public bool IsSelected { get; set; }
    }
}
