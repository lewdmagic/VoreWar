using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathNodeManager : MonoBehaviour
{
    public GameObject NodePrefab;
    private List<GameObject> _nodes;
    private List<SpriteRenderer> _spriteRenders;
    private List<Text> _texts;
    private int _nextNum = 0;

    private void Start()
    {
        _nodes = new List<GameObject>();
        _spriteRenders = new List<SpriteRenderer>();
        _texts = new List<Text>();
    }

    public void ClearNodes()
    {
        foreach (GameObject arrow in _nodes)
        {
            arrow.SetActive(false);
        }

        _nextNum = 0;
    }

    public void PlaceNode(Color color, Vec2I location, string text = "")
    {
        if (_nextNum >= _nodes.Count)
        {
            _nodes.Add(Instantiate(NodePrefab, transform));
            _spriteRenders.Add(_nodes[_nextNum].GetComponent<SpriteRenderer>());
            _texts.Add(_nodes[_nextNum].GetComponentInChildren<Text>());
        }

        _nodes[_nextNum].SetActive(true);
        _nodes[_nextNum].transform.position = new Vector2(location.X, location.Y);
        _spriteRenders[_nextNum].color = color;
        _texts[_nextNum].text = text;
        _texts[_nextNum].gameObject.SetActive(text != "");
        _nextNum++;
    }
}