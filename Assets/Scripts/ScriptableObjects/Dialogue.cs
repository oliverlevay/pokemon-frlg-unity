﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Pokémon/Dialogue")]
public class Dialogue : ScriptableObject {
    [TextArea]
    public string regularDialogue;
    [TextArea]
    public string regularMaleDialogue;
    [TextArea]
    public string regularFemaleDialogue;
    [TextArea]
    public string trainerHasPokemonDialogue;
    public Gender gender;
}