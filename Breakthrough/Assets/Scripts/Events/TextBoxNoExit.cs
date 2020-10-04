using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxNoExit : TextBoxBase
{
    public override void addExtra()
    {
        btnObj.SetActive(false);
    }
}
