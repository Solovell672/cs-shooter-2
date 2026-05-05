// LevelManager.cs

using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] environments;
    public int levelIndex;

    void Start()
    {
        SetupLevel(levelIndex);
    }

    void SetupLevel(int index)
    {
        if (index >= 0 && index < environments.Length)
        {
            Instantiate(environments[index], Vector3.zero, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Invalid level index!");
        }
    }
}