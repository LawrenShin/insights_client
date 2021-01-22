using DigitalInsights.DB.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalInsights.DB.Silver
{
	public class PropertyMetadata
	{
		public PropertyMetadata()
		{

		}

		public int Id { get; set; }
		public string EntityName { get; set; }
		public string PropertyName { get; set; }
		public string FrontendName { get; set; }
		public string Description { get; set; }
		public FieldType FieldType { get; set; }
		public bool AllowsNull { get; set; }
		public bool IsEditable { get; set; }
		public string DropDownDictionary { get; set; }
		public string ChildrenEntityName { get; set; }
		public string RangeLow { get; set; }
		public string RangeHigh { get; set; }
	}
}
