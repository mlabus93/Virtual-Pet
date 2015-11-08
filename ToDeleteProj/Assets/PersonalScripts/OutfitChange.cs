﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OutfitChange : MonoBehaviour {

    private SkinnedMeshRenderer _skinMesh;
    // containers for all materials on pet
    private Material[] _mats;
    private List<Material> nwMats = new List<Material>();
    // original ordering of materials
    private Material[] _materialOrig;
    public bool _blink;
    public bool _changeFit;
    private int currentOutfitIndex;
    private int currentEyeSelected; // 0 = a, 1 = b
    private const int EYE_RENDER_INDEX = 7, BODY_RENDER_INDEX = 0, EYES = 0, BODY = 1;
    bool _eOpened; // true if eyes are opened
    public bool enableBlinking; // true if blinking
    private float _timeToBeClosed, _timeToBeOpened; // blink rates

    // IMPORTANT: these enumeration pertain to the index in
    // Materials array in Skinned Mesh Renderer, Must remain consistent
    // on each character object

    // Total outfits in game
    enum Outfits
    {
        Commando = 0,
        PJs,
        Suit,
        JailBird
    };

    // Total faces in game
    enum Faces
    {
        EyesAClosed = 4,
        EyesAOpen,
        EyesBClosed,
        EyesBOpen
    };

	// Use this for initialization
	void Start () 
    {
        _skinMesh = gameObject.GetComponent<SkinnedMeshRenderer>();
        _mats = _skinMesh.materials;
        _materialOrig = _skinMesh.materials; // NOTE: must never be modified
        int work = _mats.GetLength(0);
        Debug.Log(work);
        _blink = false;
        _changeFit = false;

        enableBlinking = true;
        _timeToBeOpened = 2f;
        _timeToBeClosed = .3f;
	}

    void FixedUpdate()
    {
        if (enableBlinking)
        {
            if (_eOpened)
            {
                if (_timeToBeOpened > 0)
                {
                    _timeToBeOpened -= Time.deltaTime;
                }
                else
                {
                    // reset eye open time and close eyes
                    _eOpened = !_eOpened;
                    _timeToBeOpened = 4f;
                    Blink();
                }
            }
            else
            {
                if (_timeToBeClosed > 0)
                {
                    _timeToBeClosed -= Time.deltaTime;
                }
                else
                {
                    // reset eye close time and open eyes
                    _eOpened = !_eOpened;
                    _timeToBeClosed = .3f;
                    Blink();
                }
            }
            Debug.Log("open time: " + _timeToBeOpened + " closed time " + _timeToBeClosed);
        }
        
    }
    void Blink()
    {
        FindCurrentIndex(EYES);
        if (_eOpened)
        {
            // 0 == a
            if (currentEyeSelected == 0)
            {
                _mats[EYE_RENDER_INDEX] = _materialOrig[(int)Faces.EyesAOpen];
                Debug.Log("currently selected 0 after");
            }
            else
            {
                _mats[EYE_RENDER_INDEX] = _materialOrig[(int)Faces.EyesBOpen];
                Debug.Log("currently selected 1 after");
            }
            _skinMesh.materials = _mats;
        }
        else
        {
            // 0 == a
            if (currentEyeSelected == 0)
            {
                _mats[EYE_RENDER_INDEX] = _materialOrig[(int)Faces.EyesAClosed];
                Debug.Log("currently selected 0");
            }
            else
            {
                _mats[EYE_RENDER_INDEX] = _materialOrig[(int)Faces.EyesBClosed];
                Debug.Log("currently selected 1");
            }

            _skinMesh.materials = _mats;
        }

    }
	
    int FindCurrentIndex(int matType)
    {
        Material currentlyBeingRendered;
        if (matType == BODY)
        {
            currentlyBeingRendered = _mats[BODY_RENDER_INDEX];
            for (int k = 0; k < _mats.Length; k++)
            {
                if (currentlyBeingRendered == _materialOrig[k])
                {
                    currentOutfitIndex = k;
                    return k;
                }
            }
            Debug.LogError("Body material not found in referrence array");
            return -1;
        }
        if (matType == EYES)
        {
            currentlyBeingRendered = _mats[EYE_RENDER_INDEX];
            for (int k = 0; k < _mats.Length; k++)
            {
                if (currentlyBeingRendered == _materialOrig[k])
                {
                    currentOutfitIndex = k;
                    return k;
                }
            }
            Debug.LogError("Eye material not found in referrence array");
            return -1;
        }
        Debug.LogError("Material not found in referrence array");
        return -1;
    }

	// Update is called once per frame
	void Update () 
    {
        // this changes eyes between a and b 
        // TODO: Remove we dont need these its 
        //just to show you how to use my methods
	    if (_blink)
        {
            ChangeEyes();
            _blink = false;
        }

        if (_changeFit)
        {
            ChangeOutfit(1,true);
            _changeFit = false;
        }
	}

    public void ChangeOutfit(int outfitIndex = -1, bool loop = false)
    {
        // reset OutfitIndex to proper index in referrence array
        FindCurrentIndex(BODY);

        // loops through all outfits
        if (loop)
        {
            _mats[BODY_RENDER_INDEX] = _materialOrig[(currentOutfitIndex + 1) % 4];
            _skinMesh.materials = _mats;
        }
        // selects specific outfit
        else
        {
            _mats[BODY_RENDER_INDEX] = _materialOrig[outfitIndex];
            _skinMesh.materials = _mats;
        }
    }

    // since there are only 2 eyes available for each animal i've decided to make this
    // function swap between the 2 eyes instead of picking an index
    public void ChangeEyes()
    {
        // sets eyes to correct index
        FindCurrentIndex(EYES);
        currentEyeSelected = (currentEyeSelected + 1) % 2;
        
        // 0 == a
        if (currentEyeSelected == 0)
        {
            _mats[EYE_RENDER_INDEX] = _materialOrig[(int)Faces.EyesAOpen];
        }
        else
        {
            _mats[EYE_RENDER_INDEX] = _materialOrig[(int)Faces.EyesBOpen];
        }
        _skinMesh.materials = _mats;
    }

    // method to swap elements in list
    private List<Material> Swap (List<Material> lst, int indexA, int indexB)
    {
        var temp = lst[indexA];
        lst[indexA] = lst[indexB];
        lst[indexB] = temp;
        return lst;
    }

    // method to swap elements in list
    private Material[] Swap(Material[] lst, int indexA, int indexB)
    {
        var temp = lst[indexA];
        lst[indexA] = lst[indexB];
        lst[indexB] = temp;
        return lst;
    }
}
