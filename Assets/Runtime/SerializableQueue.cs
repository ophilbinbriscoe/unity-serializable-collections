using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class SerializableQueue<T> : Queue<T>, ISerializationCallbackReceiver
{
	[SerializeField]
	[HideInInspector]
	//[Tooltip( "FIFO (first-in first-out). 0 = front." )]
	protected T[] elements;

	void ISerializationCallbackReceiver.OnAfterDeserialize ()
	{
#if UNITY_EDITOR
		if ( elements != null )
		{
			for ( int i = 0; i < elements.Length; i++ )
			{
				Enqueue( elements[i] );
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

	public SerializableQueue () : base() { }

	public SerializableQueue ( int count ) : base( count ) { }
}
