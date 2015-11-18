using UnityEngine;
using System.Collections;

public class CharacterSelection : MonoBehaviour {

    int numOfOutfits = 4;
    int numOfHats = 3;
    GameObject[] characters;
    GameObject currentCharacter;
    IAnimalCharacter iAnimal;
    int characterIndex;
    int outfitIndex;
    int hatIndex;

    // Use this for initialization
    void Start()
    {
        characterIndex = 0;
        outfitIndex = 0;
        hatIndex = 0;
        characters = GameObject.FindGameObjectsWithTag("Player");
        currentCharacter = characters[characterIndex];
        SetCharactersInactive();
        iAnimal = currentCharacter.GetComponent<IAnimalCharacter>();
    }

    private void SetCharactersInactive()
    {
        foreach (GameObject character in characters)
        {
            character.gameObject.SetActive(false);
        }
        characters[0].SetActive(true);
    }

    public void NextCharacter()
    {
        ResetOutfit();
        ResetRotation();
        GetNextCharacter();
        iAnimal = currentCharacter.GetComponent<IAnimalCharacter>();
        outfitIndex = 0;
        hatIndex = 0;
    }

    public void PrevCharacter()
    {
        ResetOutfit();
        ResetRotation();
        GetPrevCharacter();
        iAnimal = currentCharacter.GetComponent<IAnimalCharacter>();
        outfitIndex = 0;
        hatIndex = 0;
    }

    public void NextOutfit()
    {
        outfitIndex++;
        if (outfitIndex >= numOfOutfits)
        {
            outfitIndex -= numOfOutfits;
        }
        iAnimal.ChangeIntoSpecificFit(outfitIndex);
    }

    public void PrevOutfit()
    {
        outfitIndex--;
        if (outfitIndex < 0)
        {
            outfitIndex += numOfOutfits;
        }
        iAnimal.ChangeIntoSpecificFit(outfitIndex);
    }

    public void NextHat()
    {
        hatIndex++;
        if (hatIndex >= numOfHats)
        {
            hatIndex -= numOfHats;
        }
        iAnimal.ChangeHats();
    }

    public void PrevHat()
    {
        hatIndex--;
        if (hatIndex < 0)
        {
            hatIndex += numOfHats;
        }
        iAnimal.ChangeHats();
    }

    private void ResetOutfit()
    {
        iAnimal.ChangeIntoSpecificFit(0);
    }

    private void ResetRotation()
    {
        currentCharacter.transform.rotation = Quaternion.Euler(0, 140, 0);
    }

    private void GetNextCharacter()
    {
        GameObject prevCharacter = characters[characterIndex];
        characterIndex++;
        if (characterIndex >= characters.Length)
        {
            characterIndex -= characters.Length;
        }
        currentCharacter = characters[characterIndex];
        prevCharacter.gameObject.SetActive(false);
        currentCharacter.gameObject.SetActive(true);
    }

    private void GetPrevCharacter()
    {
        GameObject prevCharacter = characters[characterIndex];
        characterIndex--;
        if (characterIndex < 0)
        {
            characterIndex += characters.Length;
        }
        currentCharacter = characters[characterIndex];
        prevCharacter.gameObject.SetActive(false);
        currentCharacter.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
