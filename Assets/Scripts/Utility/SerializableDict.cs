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

    public class CustomSerializedScriptableObject<TK, TV> : ISerializationCallbackReceiver where TK : IFastDictKey
    {
        [OdinSerialize]
        private Dictionary<string, TV> _dictionary;

        private TV[] _array;


        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            _dictionary = new Dictionary<string, TV>();
        }
    }
}