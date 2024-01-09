using UnityEngine;

public class ScreenSelector : MonoBehaviour
{
    private GameObject[] childObjects;
    private int currentIndex = 0;

    void Start()
    {
        // Get all child objects of the current GameObject
        childObjects = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }

        // Activate the first child object
        SetActiveChild(currentIndex);
    }


    public void MenuSelectForward()
    {
        // Move to the next child object
        currentIndex = (currentIndex + 1) % childObjects.Length;
        SetActiveChild(currentIndex);
    }

    public void MenuSelectBack()
    {
        // Move to the previous child object
        currentIndex = (currentIndex - 1 + childObjects.Length) % childObjects.Length;
        SetActiveChild(currentIndex);
    }

    void SetActiveChild(int index)
    {
        // Deactivate all child objects
        foreach (var child in childObjects)
        {
            child.SetActive(false);
        }

        // Activate the specified child object
        childObjects[index].SetActive(true);
    }
}
