using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxOneObj : TextBoxBase
{
    public GameObject extra;
    public override void addExtra()
    {
        extra.SetActive(true);
    }

    public override void removeExtra()
    {
        extra.SetActive(false);
    }
}
