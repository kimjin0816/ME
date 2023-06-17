using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum Ingredients
{
    beef,
    chicken,
    fish,
    green_onion,
    egg,
    onion,
    carrot
}

public class Recipe : MonoBehaviour
{
    [SerializeField] private GameObject gameDirector;
    private const int ingredientAmt = 7;
    private readonly int[] recipe1 = { 20, 0, 0, 0, 0, 10, 10 };
    private readonly int[] recipe2 = { 0, 10, 0, 10, 10, 10, 0 };
    private readonly int[] recipe3 = { 0, 0, 10, 10, 0, 10, 0 };
    private static readonly int[] randomRecipe = new int[ingredientAmt];

    public static int RecipeIndex { get; private set; }

    public bool IsRecipeComplete(int[] randomRecipe) // check if recipe is complete, return true if complete, return false if not
    {
        foreach (int i in randomRecipe)
        {
            if (i > 0)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsRecipeWrong(int[] randomRecipe) // check if recipe is wrong, return true if wrong, return false if not
    {
        foreach (int i in randomRecipe)
        {
            if (i < 0)
            {
                return true;
            }
        }
        return false;
    }

    private void Awake() // initialize randomRecipe
    {
        Init();
        gameDirector = GameObject.Find("GameDirector");
        RecipeIndex = CreateRandomRecipe();
    }

    private void Update()
    {
        if (IsRecipeComplete(randomRecipe) || IsRecipeWrong(randomRecipe)) // if recipe is complete or wrong, create new recipe
        {
            Init();
            RecipeIndex = CreateRandomRecipe();
            gameDirector.GetComponent<GameDirector>().UpdateRecipeUI();
            if (IsRecipeComplete(randomRecipe))
            {
                //RecipeIndex = CreateRandomRecipe();
                //Need to add score, speedup, etc
            }
            else if (IsRecipeWrong(randomRecipe))
            {
               //GameDirector.hp = 0;
            }
        }
    }

    private void Init()
    {
        Array.Clear(randomRecipe, 0, randomRecipe.Length); // initialize randomRecipe
    }

    private int CreateRandomRecipe() // create random recipe and return index of recipe
    {
        int randomIndex = Random.Range(0, 3);

        switch (randomIndex)
        {
            case 0:
                Array.Copy(recipe1, randomRecipe, ingredientAmt); // copy recipe1 to randomRecipe
                break;
            case 1:
                Array.Copy(recipe2, randomRecipe, ingredientAmt); // copy recipe2 to randomRecipe
                break;
            case 2:
                Array.Copy(recipe3, randomRecipe, ingredientAmt); // copy recipe3 to randomRecipe
                break;
        }

        return randomIndex;
    }

    public static Dictionary<int, int> ShowLeftoverRecipe() // return leftover recipe
    {
        Dictionary<int, int> temp = new Dictionary<int, int>();
        for (int i = 0; i < randomRecipe.Length; i++) // copy randomRecipe to temp
        {
            if (randomRecipe[i] > 0) // if randomRecipe[i] is not 0, add to temp
            {
                temp.Add(i, randomRecipe[i]);
            }
        }
        return temp;
    }

    public static void DecreaseIngredient(string name) // decrease ingredient
    {
        if (Enum.TryParse<Ingredients>(name, out var ingredient)) // if name is valid, decrease ingredient
        {
            randomRecipe[(int)ingredient]--;
        }
        else
        {
            throw new ArgumentException("Invalid ingredient name"); // if name is invalid, throw exception
        }
    }
}
