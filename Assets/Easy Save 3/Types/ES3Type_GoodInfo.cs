using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("skillInfo", "count")]
	public class ES3Type_GoodInfo : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_GoodInfo() : base(typeof(GoodInfo)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (GoodInfo)obj;
			
			writer.WriteProperty("skillInfo", instance.skillInfo);
			writer.WriteProperty("count", instance.count, ES3Type_int.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (GoodInfo)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "skillInfo":
						instance.skillInfo = reader.Read<SkillInfo>();
						break;
					case "count":
						instance.count = reader.Read<System.Int32>(ES3Type_int.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new GoodInfo();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_GoodInfoArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_GoodInfoArray() : base(typeof(GoodInfo[]), ES3Type_GoodInfo.Instance)
		{
			Instance = this;
		}
	}
}