using System.Collections.Generic;
using OdinSerializer;
using UnityEngine;

namespace Utility
{
    // Unfinished protopyte

    public interface IFastDictKey
    {
        string StringId { get; }
        int IntId { get; }
    }

    public class CustomSerializedScriptableObject<K, V> : ISerializationCallbackReceiver where K : IFastDictKey
    {
        [OdinSerialize]
        private Dictionary<string, V> _dictionary;

        private V[] _array;


        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            _dictionary = new Dictionary<string, V>();
        }
    }
}