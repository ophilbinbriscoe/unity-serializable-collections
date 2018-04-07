# Serializable Collections
Serializable versions of generic C# collections (Stack, Queue, Dictionary, HashSet, and more).

## Usage
Due to limitations in Unity's serialization system, you will need to create non-generic subclasses of these collections for every generic type argument you need to use them with. For example, if you wanted to use a `SerializableQueue` with `int` values, you'd create a C# file somewhere in your project that looked like this:

```C#
// This attribute tells Unity to try and serialize this class,
// if you don't include it, the collection won't work properly.
[System.Serializable]
public class IntegerQueue : SerializableQueue<int>
{
    // Default constructor
    public IntegerQueue () : base() { }

    // Another possible constructor
    // (you don't have to define these, but if you want to use a particular constructor, you'll need to include it here)
    public IntegerQueue ( int count ) : base( count) { }
}
```

Then you'd be ready to use the `IntegerQueue` collection in your scripts. For example:

```C#
using UnityEngine;

public class SomeBehaviour : MonoBehaviour
{
    // You'll be able to set some values in the inspector,
    // and everything will persist between Play/Stop.
    public IntegerQueue myQueue = new IntegerQueue( 10 );

    // You don't have to explicitly initialize a collection, if you'd prefer,
    // you can let Unity's serialization system initialize an empty one for you.
    public IntegerQueue anEmptyQueue;

    void Update ()
    {
        if ( myQueue.Count > 0 )
        {
             Debug.Log( myQueue.Dequeu() );
        }
    }
}
```
