using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour
{
    [SerializeField]
    Image playerAvatar; // The player's on-screen avatar
    Sprite avatar; // The avatar for this object
    [SerializeField]
    Sprite checkedSprite; // The sprite for when an avatar is checked (a tick mark)
    [SerializeField]
    Sprite uncheckedSprite; // The sprite for when an avatar is unchecker (a cross)
    [SerializeField]
    Image checkbox; // The checkbox for this avatar
    [SerializeField]
    Image[] checkboxes; // The checkbox for other avatars

    // Runs before first frame update
    void Start()
    {
        avatar = GetComponent<Image>().sprite;

        // If this avatar is selected switch to the checked sprite, otherwise switch to the unchecked sprite
        if(playerAvatar.sprite == avatar)
        {
            checkbox.sprite = checkedSprite;
        }
        else
        {
            checkbox.sprite = uncheckedSprite;
        }
    }

    public void SelectAvatar()
    {
        playerAvatar.sprite = avatar;

        // Loop through each checkbox and set their sprite to unchecked
        foreach(Image image in checkboxes)
        {
            image.sprite = uncheckedSprite;
        }

        // Set this avatar's checkbox to marked
        checkbox.sprite = checkedSprite;
    }
}
