using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPage : MonoBehaviour
{
    public List<GameObject> pages = new List<GameObject>();
    public int indexPage= 0; // current page

    public void ChangePage(int value)
    {
        if(indexPage < pages.Count)
        {
            Debug.Log("button Clicked");
            int newIndexPage = indexPage + (1 * value);
            if (newIndexPage < pages.Count && newIndexPage >= 0)
            {
                pages[indexPage].SetActive(false);
                pages[newIndexPage].SetActive(true);

                indexPage = newIndexPage;
            }
        }  
    }
}
