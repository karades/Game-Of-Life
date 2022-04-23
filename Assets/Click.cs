using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private bool isWhite = true;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
    }

    public void setBlack()
    {
        spriteRenderer.color = Color.black;
        isWhite = false;
    }
    public void setWhite()
    {
        spriteRenderer.color = Color.white;
        isWhite = true;
    }
    public bool getIsWhite()
    {
        return isWhite;
    }
    public bool getIsBlack()
    {
        return !isWhite;
    }
    private void OnMouseDown()
    {

        if (isWhite)
        {
            spriteRenderer.color = Color.black;
            isWhite = false;
        }
        else
        {
            spriteRenderer.color = Color.white;
            isWhite = true;
        }
    }
}
