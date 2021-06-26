using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{

    [SerializeField] private List<Sprite> indicatorSprites;

    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowLevel(int level)
    {
        Sprite spriteToShow = indicatorSprites[level];

        _spriteRenderer.sprite = spriteToShow;
    }
}
