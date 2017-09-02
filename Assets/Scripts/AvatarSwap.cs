using UnityEngine;
using System.Collections;

public class AvatarSwap : MonoBehaviour {

    public int index = 0;

    public GameObject[] avatars;
    //public enum SelectedAvatar : int {UnityCharacter, Boy, Girl, IronMan, Superman}
    //public SelectedAvatar avatar = SelectedAvatar.UnityCharacter;

    void Start () {
	    if(avatars.Length == 0)
        {
            Debug.LogError("Avatars array is empty!");
        }

        ChooseAvatar();
	}

    void ChooseAvatar()
    {
        foreach(GameObject ob in avatars)
        {
            ob.SetActive(false);
            //ob.transform.position = new Vector3(100, 100, 100);
        }

        avatars[index].transform.position = Vector3.zero;
        avatars[index].SetActive(true);
    }

    public void ChangeAvatar()
    {
        if (index != avatars.Length - 1)
            index++;
        else
            index = 0;

        ChooseAvatar();
    }
}
