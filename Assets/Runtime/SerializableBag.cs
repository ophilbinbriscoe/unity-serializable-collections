using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A bag capable of holding one or more of a given element.
/// </summary>
/// <typeparam name="T">Element type.</typeparam>
[Serializable]
public class SerializableBag<T> : IEnumerable, IEnumerable<KeyValuePair<T, int>>, ICollection<KeyValuePair<T, int>>, ISerializationCallbackReceiver
{
	private SortedList<T,int> list;

	[SerializeField]
	[HideInInspector]
	private int count;

	[SerializeField]
	[HideInInspector]
	private T[] serializedKeys = new T[0];

	[SerializeField]
	[HideInInspector]
	private int[] serializedValues = new int[0];

	void ISerializationCallbackReceiver.OnAfterDeserialize ()
	{
		if ( serializedKeys != null )
		{
			list = new SortedList<T, int>( serializedKeys.Length );

			for ( int i = 0; i < serializedKeys.Length; i++ )
			{
				list.Add( serializedKeys[i], serializedValues[i] );
			}
		}
	}

	void ISerializationCallbackReceiver.OnBeforeSerialize ()
	{
		serializedKeys = list.Keys.ToArray();
		serializedValues = list.Values.ToArray();
	}

	public SerializableBag ()
	{
		list = new SortedList<T, int>();
	}

	public SerializableBag ( int capacity )
	{
		list = new SortedList<T, int>( capacity );
	}

	/// <summary>
	/// The number of values (including duplicates) in this bag.
	/// </summary>
	public int Count
	{
		get
		{
			return count;
		}
	}

	/// <summary>
	/// The number of unique values in this bag.
	/// </summary>
	public int UniqueCount
	{
		get
		{
			return list.Count;
		}
	}

	/// <summary>
	/// Check if the passed-in inventory is a subset of the queried inventory.
	/// </summary>
	/// <param name="bag">Query.</param>
	/// <returns>True, if the passed-in inventory is a subset of the queried inventory.</returns>
	public bool Contains ( SerializableBag<T> bag )
	{
		foreach ( var pair in bag )
		{
			if ( this[pair.Key] < pair.Value )
			{
				return false;
			}
		}

		return true;
	}
		
	public override string ToString ()
	{
		string str = string.Empty;

		foreach ( var pair in list )
		{
			str += string.Format( "{0} {1}, ", pair.Key, pair.Value );
		}

		return string.Format( "({0})", str.TrimEnd( ' ', ',' ) );
	}

	/// <summary>
	/// The number of a given element in the bag.
	/// </summary>
	/// <param name="element">Element.</param>
	/// <returns>The number of the passed-in element in the bag..</returns>
	public int this[T element]
	{
		get
		{
			int number;

			if ( list.TryGetValue( element, out number ) )
			{
				return number;
			}

			return 0;
		}

		set
		{
			Set( element, value );
		}
	}

	/// <summary>
	/// Set the number of a given element in the bag.
	/// </summary>
	/// <param name="element">Element.</param>
	/// <param name="number">Number.</param>
	/// <param name="relative">Whether the passed-in number should be added to the existing number.</param>
	public void Set ( T element, int number, bool relative = false )
	{
		int previous;

		if ( list.TryGetValue( element, out previous ) )
		{
			if ( relative )
			{
				Set( element, previous + number, false );
			}
			else
			{
				if ( number < 0 )
				{
					throw new InvalidOperationException( "A bag cannot contain a negative number of a given element." );
				}

				if ( number == 0 )
				{
					list.Remove( element );

					count -= previous;
				}
				else
				{
					list[element] = number;

					count += number - previous;
				}
			}
		}
		else
		{
			if ( number > 0 )
			{
				list[element] = number;

				count += number;
			}
			else if ( number < 0 )
			{
				throw new InvalidOperationException( "A bag cannot contain a negative number of a given element." );
			}
		}
	}

	/// <summary>
	/// Add a number of a given element to the bag.
	/// </summary>
	/// <param name="element">Element.</param>
	/// <param name="number">Number associated with the element </param>
	public void Add ( T element, int number )
	{
		Set( element, number, true );
	}

	/// <summary>
	/// Add a number of a given element to the bag.
	/// </summary>
	/// <param name="elements">Type and number of elements to add.</param>
	public void Add ( KeyValuePair<T, int> elements )
	{
		Set( elements.Key, elements.Value, true );
	}

	/// <summary>
	/// Remove all of a given element from the bag.
	/// </summary>
	/// <param name="element">Element.</param>
	public void RemoveAll ( T element )
	{
		Set( element, 0 );
	}

	/// <summary>
	/// Remove a number of a given element to the bag.
	/// </summary>
	/// <param name="element">Element.</param>
	/// <param name="number">Number associated with the element </param>
	public void Remove ( T element, int number )
	{
		Set( element, -number, true );
	}

	/// <summary>
	/// Remove a number of a given element to the bag.
	/// </summary>
	/// <param name="elements">Type and number of elements to remove.</param>
	public void Remove ( KeyValuePair<T, int> elements )
	{
		Set( elements.Key, -elements.Value, true );
	}

	/// <summary>
	/// Whether or not this bag contains one or more of a given element.
	/// </summary>
	/// <param name="element">Element.</param>
	/// <returns>True, if the bag contains one or more of the passed-in element.</returns>
	public bool Contains ( T element )
	{
		return this[element] >= 1;
	}

	/// <summary>
	/// Whether or not this bag contains (at least) some number of a given element.
	/// </summary>
	/// <param name="number">Number.</param>
	/// <param name="element">Element.</param>
	/// <returns>True, if the number of the given element in this bag is greater than or equal to the passed-in number.</returns>
	public bool Contains ( int number, T element)
	{
		return this[element] >= number;
	}

	/// <summary>
	/// Whether or not this bag contains (exactly) some number of a given element.
	/// </summary>
	/// <param name="number">Number.</param>
	/// <param name="element">Element.</param>
	/// <returns>True, if the number of the given element in this bag is equal to the passed-in number.</returns>
	public bool ContainsExactly ( int number, T element )
	{
		return this[element] == number;
	}

	public int NumberOf ( T element )
	{
		return this[element];
	}

	public void Clear ()
	{
		list.Clear();

		count = 0;
	}

	/// <summary>
	/// The number of elements (not including duplicates) in the bag.
	/// </summary>
	int ICollection<KeyValuePair<T, int>>.Count
	{
		get
		{
			return list.Count;
		}
	}

	bool ICollection<KeyValuePair<T, int>>.IsReadOnly
	{
		get
		{
			return false;
		}
	}

	bool ICollection<KeyValuePair<T, int>>.Contains ( KeyValuePair<T, int> item )
	{
		return ((IDictionary<T, int>) list).Contains( item );
	}

	void ICollection<KeyValuePair<T, int>>.CopyTo ( KeyValuePair<T, int>[] array, int arrayIndex )
	{
		((IDictionary<T, int>) list).CopyTo( array, arrayIndex );
	}

	bool ICollection<KeyValuePair<T, int>>.Remove ( KeyValuePair<T, int> item )
	{
		return ((IDictionary<T, int>) list).Remove( item );
	}

	public IEnumerator<KeyValuePair<T, int>> GetEnumerator ()
	{
		return ((IDictionary<T, int>) list).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator ()
	{
		return ((IDictionary<T, int>) list).GetEnumerator();
	}
}
