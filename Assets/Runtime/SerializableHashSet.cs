using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver
{
	[SerializeField]
	[HideInInspector]
	private T[] serializedItems = new T[0];

	void ISerializationCallbackReceiver.OnAfterDeserialize ()
	{
		if ( serializedItems != null )
		{
			for ( int i = 0; i < serializedItems.Length; i++ )
			{
				Add( serializedItems[i] );
			}
		}
	}

	void ISerializationCallbackReceiver.OnBeforeSerialize ()
	{
		serializedItems = this.ToArray();
	}

	public SerializableHashSet () : base() { }

	public SerializableHashSet ( IEnumerable<T> collection ) : base( collection ) { }
}
