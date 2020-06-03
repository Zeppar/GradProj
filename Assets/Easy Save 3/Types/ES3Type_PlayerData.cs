using System;
using UnityEngine;

namespace ES3Types
{
	[ES3PropertiesAttribute("pos", "goodInfos", "playerHp", "energy")]
	public class ES3Type_PlayerData : ES3ObjectType
	{
		public static ES3Type Instance = null;

		public ES3Type_PlayerData() : base(typeof(PlayerData)){ Instance = this; }

		protected override void WriteObject(object obj, ES3Writer writer)
		{
			var instance = (PlayerData)obj;
			
			writer.WriteProperty("pos", instance.pos, ES3Type_Vector2.Instance);
			writer.WriteProperty("goodInfos", instance.goodInfos);
			writer.WriteProperty("playerHp", instance.playerHp, ES3Type_float.Instance);
			writer.WriteProperty("energy", instance.energy, ES3Type_float.Instance);
		}

		protected override void ReadObject<T>(ES3Reader reader, object obj)
		{
			var instance = (PlayerData)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "pos":
						instance.pos = reader.Read<UnityEngine.Vector2>(ES3Type_Vector2.Instance);
						break;
					case "goodInfos":
						instance.goodInfos = reader.Read<System.Collections.Generic.List<GoodInfo>>();
						break;
					case "playerHp":
						instance.playerHp = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					case "energy":
						instance.energy = reader.Read<System.Single>(ES3Type_float.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}

		protected override object ReadObject<T>(ES3Reader reader)
		{
			var instance = new PlayerData();
			ReadObject<T>(reader, instance);
			return instance;
		}
	}

	public class ES3Type_PlayerDataArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3Type_PlayerDataArray() : base(typeof(PlayerData[]), ES3Type_PlayerData.Instance)
		{
			Instance = this;
		}
	}
}