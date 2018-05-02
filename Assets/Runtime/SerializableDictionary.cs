using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	protected Dictionary<TKey,TValue> dictionary = new Dictionary<TKey, TValue>();

	[SerializeField]
	[HideInInspector]
	private TKey[] serializedKeys = new TKey[0];

	[SerializeField]
	[HideInInspector]
	private TValue[] serializedValues = new TValue[0];

	void ISerializationCallbackReceiver.OnAfterDeserialize ()
	{
		if ( serializedKeys != null )
		{
			dictionary = new Dictionary<TKey, TValue>( serializedKeys.Length );

			for ( int i = 0; i < serializedKeys.Length; i++ )
			{
				dictionary.Add( serializedKeys[i], serializedValues[i] );
			}
		}
	}

	void ISerializationCallbackReceiver.OnBeforeSerialize ()
	{
		serializedKeys = dictionary.Keys.ToArray();
		serializedValues = dictionary.Values.ToArray();
	}

	public virtual ICollection<TKey> Keys
	{
		get
		{
			return ((IDictionary<TKey, TValue>) dictionary).Keys;
		}
	}

	public virtual ICollection<TValue> Values
	{
		get
		{
			return ((IDictionary<TKey, TValue>) dictionary).Values;
		}
	}

	public virtual int Count
	{
		get
		{
			return ((IDictionary<TKey, TValue>) dictionary).Count;
		}
	}

	public virtual bool IsReadOnly
	{
		get
		{
			return ((IDictionary<TKey, TValue>) dictionary).IsReadOnly;
		}
	}

	public virtual TValue this[TKey key]
	{
		get
		{
			return ((IDictionary<TKey, TValue>) dictionary)[key];
		}

		set
		{
			((IDictionary<TKey, TValue>) dictionary)[key] = value;
		}
	}

	public virtual void Add ( TKey key, TValue value )
	{
		((IDictionary<TKey, TValue>) dictionary).Add( key, value );
	}

	public virtual bool ContainsKey ( TKey key )
	{
		return ((IDictionary<TKey, TValue>) dictionary).ContainsKey( key );
	}

	public virtual bool Remove ( TKey key )
	{
		return ((IDictionary<TKey, TValue>) dictionary).Remove( key );
	}

	public virtual bool TryGetValue ( TKey key, out TValue value )
	{
		return ((IDictionary<TKey, TValue>) dictionary).TryGetValue( key, out value );
	}

	public virtual void Add ( KeyValuePair<TKey, TValue> item )
	{
		((IDictionary<TKey, TValue>) dictionary).Add( item );
	}

	public virtual void Clear ()
	{
		((IDictionary<TKey, TValue>) dictionary).Clear();
	}

	public virtual bool Contains ( KeyValuePair<TKey, TValue> item )
	{
		return ((IDictionary<TKey, TValue>) dictionary).Contains( item );
	}

	public virtual void CopyTo ( KeyValuePair<TKey, TValue>[] array, int arrayIndex )
	{
		((IDictionary<TKey, TValue>) dictionary).CopyTo( array, arrayIndex );
	}

	public virtual bool Remove ( KeyValuePair<TKey, TValue> item )
	{
		return ((IDictionary<TKey, TValue>) dictionary).Remove( item );
	}

	public virtual IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator ()
	{
		return ((IDictionary<TKey, TValue>) dictionary).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator ()
	{
		return ((IDictionary<TKey, TValue>) dictionary).GetEnumerator();
	}
}
