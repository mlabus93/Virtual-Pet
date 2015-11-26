using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PersonalScripts
{
    public class OutfitChange : MonoBehaviour
    {

        private SkinnedMeshRenderer _skinMesh;
        // containers for all materials on pet
        private Material[] _mats;
        private List<Material> nwMats = new List<Material>();
        // original ordering of materials
        private Material[] _materialOrig;
        public bool _blink;
        public bool _changeFit;
        private int currentOutfitIndex = 0;
        private int currentEyeSelected; // 0 = a, 1 = b
        private const int EYE_RENDER_INDEX = 7, BODY_RENDER_INDEX = 0, EYES = 0, BODY = 1;
        bool _eOpened; // true if eyes are opened
        public bool enableBlinking; // true if blinking
        private float _timeToBeClosed, _timeToBeOpened; // blink rates
        public int _currentHatIndex;
        public GameObject[] _hats;

        public int GetCurrentOutfitIndex()
        {
            return currentOutfitIndex;
        }
        public int GetCurrentEyeSelected()
        {
            return currentEyeSelected % 2;
        }
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

        // Total Hats in game
        enum Hats
        {
            NoHat = 0,
            JailHat,
            SortingHat, // Harry Potter reference
            numHats
        }

        // Use this for initialization
        void Awake()
        {
            _skinMesh = gameObject.GetComponent<SkinnedMeshRenderer>();
            _mats = _skinMesh.materials;
            _materialOrig = _skinMesh.materials; // NOTE: must never be modified
            int work = _mats.GetLength(0);
            _blink = false;
            _changeFit = false;

            enableBlinking = true;
            _timeToBeOpened = 2f;
            _timeToBeClosed = .3f;
            // NOTE: this logic causes all hats in the scene to change
            //_hats = GameObject.FindGameObjectsWithTag("HAT");
            string FindHats = "animal_spine_joint/animal_spine2_joint/animal_head_joint/";
            string HatA = "animal_pr_hat_a";
            string HatB = "animal_pr_hat_b";

            _hats = new GameObject[(int)Hats.numHats];
            _hats[(int)Hats.NoHat] = new GameObject();
            _hats[(int)Hats.JailHat] = transform.parent.transform.Find(FindHats + HatA).gameObject;
            _hats[(int)Hats.SortingHat] = transform.parent.transform.Find(FindHats + HatB).gameObject;


            DisableAllHats();
        }

        void DisableAllHats()
        {
            // same as no hat
            _currentHatIndex = 0;
            for (int i = 0; i < _hats.Length; i++)
            {
                _hats[i].SetActive(false);
            }
        }

        public void ChangeHats()
        {
            ChangeHats(0);
        }
        public void ChangeHats(int index, bool loop = true)
        {
            // NOTE: In order for this logic to work players must initially have both
            // hats activated, once the list is acquired we can then loop through the
            // array of hats objects

            // save current index and clear hats
            int tempHatIndex = _currentHatIndex;
            int newIndex = (tempHatIndex + 1) % (int)Hats.numHats; // there are 3 head types
            DisableAllHats();
            //if (newIndex == (int)Hats.NoHat)
            //{
            //    _currentHatIndex = newIndex;
            //    return;
            //}

            if (loop)
            {
                Debug.Log(newIndex);
                _hats[newIndex].SetActive(true);
                _currentHatIndex = newIndex;
            }
            else
            {
                _hats[index].SetActive(true);
                _currentHatIndex = index;
            }
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
                }
                else
                {
                    _mats[EYE_RENDER_INDEX] = _materialOrig[(int)Faces.EyesBOpen];
                }
                _skinMesh.materials = _mats;
            }
            else
            {
                // 0 == a
                if (currentEyeSelected == 0)
                {
                    _mats[EYE_RENDER_INDEX] = _materialOrig[(int)Faces.EyesAClosed];
                }
                else
                {
                    _mats[EYE_RENDER_INDEX] = _materialOrig[(int)Faces.EyesBClosed];
                }

                _skinMesh.materials = _mats;
            }

        }

        int FindCurrentIndex(int matType)
        {
            Material currentlyBeingRendered = null;
            if (matType == BODY)
            {
                currentlyBeingRendered = this._mats[BODY_RENDER_INDEX];
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
                        currentEyeSelected = k;
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
        void Update()
        {
            // this changes eyes between a and b 
            // TODO: Remove we dont need these its 
            //just to show you how to use my methods
            if (_blink)
            {
                ChangeEyes();
                _blink = false;
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
        private List<Material> Swap(List<Material> lst, int indexA, int indexB)
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

}
