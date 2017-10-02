using System;
using System.Collections.Generic;
using System.Text;

namespace CoWorker.EntityFramework.Conventions
{
    public class EFConventionsOptions
    {
		public EFConventionsOptions()
		{
			MaxLength = new Dictionary<string, int>();
			RemoveName = new List<string>();
		}

		public Dictionary<string,int> MaxLength { get; set; }
		public List<string> RemoveName { get; set; }
    }
}
