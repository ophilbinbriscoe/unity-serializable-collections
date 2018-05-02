using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class SerializableStack<T> : Stack<T>, ISerializationCallbackReceiver
{
	[SerializeField]
	[HideInInspector]
	//[Tooltip( "LIFO (last-in first-out). 0 = bottom." )]
	protected T[] elements;

	void ISerializationCallbackReceiver.OnAfterDeserialize ()
	{
#if UNITY_EDITOR
		if ( elements != null )
		{
			for ( int i = elements.Length - 1; i >= 0; i-- )
			{
				Push( elements[i] );
			}
		}
#endif
	}

	void ISerializationCallbackReceiver.OnBeforeSerialize ()
	{
#if UNITY_EDITOR
		elements = ToArray();
#endif
	}

	public SerializableStack () : base() { }

	public SerializableStack ( int count ) : base( count ) { }
}
