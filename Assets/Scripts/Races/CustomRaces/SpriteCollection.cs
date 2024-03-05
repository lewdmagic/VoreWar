using System.Collections.Generic;
using UnityEngine;

public interface ISpriteCollection
{
    Sprite GetSprite(string name);

    string Description { get; }
}

public class SpriteCollection : ISpriteCollection
{
    public SpriteCollection(string description)
    {
        Description = description;
    }

    public string Description { get; private set; }

    private Dictionary<string, Sprite> _dictionary = new Dictionary<string, Sprite>();

    public Sprite GetSprite(string name)
    {
        return _dictionary.GetOrNull(name);
    }

    public void Set(string name, Sprite sprite)
    {
        _dictionary[name] = sprite;
    }
}