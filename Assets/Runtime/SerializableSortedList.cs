using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableSortedList<TKey,TValue> : IDictionary<TKey,TValue>, ISerializationCallbackReceiver
{
	protected SortedList<TKey,TValue> list = new SortedList<TKey, TValue>();

	[SerializeField]
	[HideInInspector]
	private TKey[] serializedKeys = new TKey[0];

	[SerializeField]
	[HideInInspector]
	private TValue[] serializedValues = new TValue[0];

	void ISerializationCallbackReceiver.OnAfterDeserialize ()
	{
#if UNITY_EDITOR
		if ( serializedKeys != null )
		{
			list = new SortedList<TKey, TValue>( serializedKeys.Length );

			for ( int i = 0; i < serializedKeys.Length; i++ )
			{
				list.Add( serializedKeys[i], serializedValues[i] );
			}
		}
#endif
	}

	void ISerializationCallbackReceiver.OnBeforeSerialize ()
	{
#if UNITY_EDITOR
		serializedKeys = list.Keys.ToArray();
		serializedValues = list.Values.ToArray();
#endif
	}

	public SerializableSortedList ()
	{
		list = new SortedList<TKey, TValue>();
	}

	public SerializableSortedList ( int capacity )
	{
		list = new SortedList<TKey, TValue>( capacity );
	}

	public virtual ICollection<TKey> Keys
	{
		get
		{
			return ((IDictionary<TKey, TValue>) list).Keys;
		}
	}

	public virtual ICollection<TValue> Values
	{
		get
		{
			return ((IDictionary<TKey, TValue>) list).Values;
		}
	}

	public virtual int Count
	{
		get
		{
			return ((IDictionary<TKey, TValue>) list).Count;
		}
	}

	public virtual bool IsReadOnly
	{
		get
		{
			return ((IDictionary<TKey, TValue>) list).IsReadOnly;
		}
	}

	public virtual TValue this[TKey key]
	{
		get
		{
			return ((IDictionary<TKey, TValue>) list)[key];
		}

		set
		{
			((IDictionary<TKey, TValue>) list)[key] = value;
		}
	}

	public virtual void Add ( TKey key, TValue value )
	{
		((IDictionary<TKey, TValue>) list).Add( key, value );
	}

	public virtual bool ContainsKey ( TKey key )
	{
		return ((IDictionary<TKey, TValue>) list).ContainsKey( key );
	}

	public virtual bool Remove ( TKey key )
	{
		return ((IDictionary<TKey, TValue>) list).Remove( key );
	}

	public virtual bool TryGetValue ( TKey key, out TValue value )
	{
		return ((IDictionary<TKey, TValue>) list).TryGetValue( key, out value );
	}

	public virtual void Add ( KeyValuePair<TKey, TValue> item )
	{
		((IDictionary<TKey, TValue>) list).Add( item );
	}

	public virtual void Clear ()
	{
		((IDictionary<TKey, TValue>) list).Clear();
	}

	public virtual bool Contains ( KeyValuePair<TKey, TValue> item )
	{
		return ((IDictionary<TKey, TValue>) list).Contains( item );
	}

	public virtual void CopyTo ( KeyValuePair<TKey, TValue>[] array, int arrayIndex )
	{
		((IDictionary<TKey, TValue>) list).CopyTo( array, arrayIndex );
	}

	public virtual bool Remove ( KeyValuePair<TKey, TValue> item )
	{
		return ((IDictionary<TKey, TValue>) list).Remove( item );
	}

	public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator ()
	{
		return ((IDictionary<TKey, TValue>) list).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator ()
	{
		return ((IDictionary<TKey, TValue>) list).GetEnumerator();
	}
}
