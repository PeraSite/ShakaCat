﻿using System.Text;
using PixelCrushers;
using ShakaCat;
using Sirenix.Serialization;
using UnityEngine;
using SerializationUtility = Sirenix.Serialization.SerializationUtility;

public class OdinDataSerializer : DataSerializer {
	public ScriptableObjectCache Cache;
	public DataFormat Format;

	public override string Serialize(object data) {
		var context = new SerializationContext {
			StringReferenceResolver = new ScriptableObjectStringReferenceResolver {Cache = Cache},
		};
		return Encoding.UTF8.GetString(SerializationUtility.SerializeValue(data, Format, context));
	}

	public override T Deserialize<T>(string s, T data = default) {
		var context = new DeserializationContext {
			StringReferenceResolver = new ScriptableObjectStringReferenceResolver {Cache = Cache},
		};
		var bytes = Encoding.UTF8.GetBytes(s);
		return SerializationUtility.DeserializeValue<T>(bytes, Format, context);
	}
}

public class ScriptableObjectStringReferenceResolver : IExternalStringReferenceResolver {
	public ScriptableObjectCache Cache;
	public IExternalStringReferenceResolver NextResolver { get; set; }

	public bool CanReference(object value, out string id) {
		if (value is ScriptableObject so) {
			id = so.name;
			return true;
		}

		id = null;
		return false;
	}

	public bool TryResolveReference(string id, out object value) {
		value = Cache.Cache.Find(so => so.name == id);
		return value != null;
	}
}
