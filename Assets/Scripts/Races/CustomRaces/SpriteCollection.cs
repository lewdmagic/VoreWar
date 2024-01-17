using UnityEngine;

public interface ISpriteCollection
{
     Sprite GetSprite(string name);
}

public class SpriteCollection : ISpriteCollection
{

    
    

    public Sprite GetSprite(string name)
    {
        return null;
    }

    public void Add(string name, Sprite sprite)
    {
        
    }
    
}