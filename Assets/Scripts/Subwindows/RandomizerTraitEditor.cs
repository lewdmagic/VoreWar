﻿using OdinSerializer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RandomizerTraitEditor : MonoBehaviour
{
    public Transform Folder;
    public GameObject RandomizerTraitPrefab;
    internal List<RandomizerTrait> RandomizerTags;
    public Button AddBtn;
    internal Button AddBtnInstance;

    internal void Open()
    {
        gameObject.SetActive(true);

        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }

        Setup();
    }

    private void Setup()
    {
        RandomizerTags = new List<RandomizerTrait>();
        foreach (var entry in State.RandomizeLists)
        {
            var randomizerTrait = CreateRandomizerTrait(entry);
            RandomizerTags.Add(randomizerTrait);
        }

        CreateAddButton();
    }

    private void CreateAddButton()
    {
        if (AddBtnInstance != null)
        {
            AddBtnInstance.onClick.RemoveAllListeners();
            Destroy(AddBtnInstance.gameObject);
        }

        AddBtnInstance = Instantiate(AddBtn, Folder);
        var btn = AddBtnInstance.GetComponent<Button>();
        var btnTxt = btn.GetComponentInChildren<Text>();
        btnTxt.text = "Add";
        btn.onClick.AddListener(() =>
        {
            var created = CreateRandomizerTrait();
            RandomizerTags.Add(created);
            CreateAddButton();
        });
    }

    private RandomizerTrait CreateRandomizerTrait(RandomizeList savedCustom = null)
    {
        if (savedCustom != null)
        {
            var obj = Instantiate(RandomizerTraitPrefab, Folder);
            var rt = obj.GetComponent<RandomizerTrait>();
            rt.Name.text = savedCustom.Name;
            rt.Chance.text = (savedCustom.Chance * 100).ToString();
            rt.ID = savedCustom.ID;
            var ranTraits = new Dictionary<TraitType, bool>();
            foreach (TraitType r in State.RandomizeLists.ConvertAll(r => (TraitType)r.ID))
            {
                if (savedCustom.RandomTraits.Contains(r))
                    ranTraits[r] = true;
                else
                    ranTraits[r] = false;
            }

            foreach (TraitType trait in (TraitType[])Enum.GetValues(typeof(TraitType)))
            {
                if (savedCustom.RandomTraits.Contains(trait))
                    ranTraits[trait] = true;
                else
                    ranTraits[trait] = false;
            }

            rt.TraitDictionary = ranTraits;
            rt.CloneBtn.onClick.AddListener(() =>
            {
                var clone = CreateRandomizerTrait(savedCustom);
                clone.ID = FindNewId();
                clone.Name.text = "new" + clone.Name.text;
                RandomizerTags.Add(clone);
                CreateAddButton();
            });
            rt.RemoveBtn.onClick.AddListener(() =>
            {
                Remove(rt);
                Destroy(rt.gameObject);
            });
            return rt;
        }
        else
        {
            var newItemTemplate = Instantiate(RandomizerTraitPrefab, Folder);
            var rt = newItemTemplate.GetComponent<RandomizerTrait>();
            rt.Name.text = "";
            rt.Chance.text = "100";
            var last = RandomizerTags.LastOrDefault();
            rt.ID = last == null ? 1001 : FindNewId();
            var ranTraits = new Dictionary<TraitType, bool>();
            foreach (TraitType r in State.RandomizeLists.ConvertAll(r => (TraitType)r.ID))
            {
                ranTraits[r] = false;
            }

            foreach (TraitType trait in (TraitType[])Enum.GetValues(typeof(TraitType)))
            {
                ranTraits[trait] = false;
            }

            rt.TraitDictionary = ranTraits;
            rt.CloneBtn.onClick.AddListener(() =>
            {
                var clone = CreateRandomizerTrait(savedCustom);
                clone.ID = FindNewId();
                clone.Name.text = "new" + clone.Name.text;
                RandomizerTags.Add(clone);
                CreateAddButton();
            });
            rt.RemoveBtn.onClick.AddListener(() =>
            {
                Remove(rt);
                Destroy(rt.gameObject);
            });
            return rt;
        }
    }

    private int FindNewId()
    {
        bool taken = true;
        int index = 0;
        while (taken)
        {
            index++;
            taken = RandomizerTags.Any(rt => rt.ID == 1000 + index);
        }

        return 1000 + index;
    }

    private void Remove(RandomizerTrait rt)
    {
        foreach (Race race in RaceFuncs.RaceEnumerable())
        {
            RaceSettingsItem item = State.RaceSettings.Get(race);
            item.RaceTraits.Remove((TraitType)rt.ID);
        }

        RandomizerTags.Remove(rt);
    }

    public void Persist()
    {
        List<RandomizeList> randomizeLists = new List<RandomizeList>();
        bool valid = true;
        RandomizerTags.ForEach(tag =>
        {
            if (!Validate(tag))
            {
                State.GameManager.CreateMessageBox("Saving failed: Trait with name \"" + tag.Name.text + "\" is incomplete or invalid.");
                valid = false;
            }

            RandomizeList newCustom = new RandomizeList();
            newCustom.ID = tag.ID;
            newCustom.Name = tag.Name.text;
            newCustom.Chance = int.Parse(tag.Chance.text) / 100f;
            newCustom.RandomTraits = new List<TraitType>();
            foreach (var trait in tag.TraitDictionary)
            {
                if (trait.Value) newCustom.RandomTraits.Add(trait.Key);
            }

            randomizeLists.Add(newCustom);
        });
        if (valid)
        {
            State.RandomizeLists = new List<RandomizeList>();
            State.RandomizeLists.AddRange(randomizeLists);
            string[] printable = randomizeLists.ConvertAll(item => item.ToString()).ToArray();
            File.WriteAllLines($"{State.StorageDirectory}customTraits.txt", printable);
        }

        Close();
    }

    public bool Validate(RandomizerTrait randomizerTrait)
    {
        int res;
        if (randomizerTrait.Name.text.Length < 1) return false;
        if (randomizerTrait.Chance.text.Length < 1 || !int.TryParse(randomizerTrait.Chance.text, out res) || res < 0) return false;
        if (randomizerTrait.TraitDictionary.Where(i => i.Value).Count() < 1) return false;
        if (RandomizerTags.Where(rt => rt.Name.text == randomizerTrait.Name.text).Count() > 1) return false;
        return true;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        int children = Folder.childCount;
        for (int i = children - 1; i >= 0; i--)
        {
            Destroy(Folder.GetChild(i).gameObject);
        }
    }
}