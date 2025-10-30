using System;
using System.Collections.Generic;
using System.Diagnostics;
public static class Arrays
{
    // Part 1: Arrays
    // Function: MultiplesOf
    // ------------------------------------------------------------
    // The function should create and return an array of multiples 
    // of a number. The starting number and the number of multiples 
    // are provided as inputs to the function.
    //
    // Example: MultiplesOf(3, 5) → {3, 6, 9, 12, 15}
    // ------------------------------------------------------------
    
    public static double[] MultiplesOf(double start, int count)
    {
        // PLAN / PROCESS STEPS:
        // Step 1: Create a new array to store the multiples. 
        //         The array size should equal the number of multiples (count).
        // Step 2: Loop through from 0 up to count - 1.
        // Step 3: For each index i, calculate start * (i + 1).
        // Step 4: Store this value in the array at index i.
        // Step 5: After the loop finishes, return the filled array.

        // IMPLEMENTATION:
        double[] multiples = new double[count]; // Step 1
        for (int i = 0; i < count; i++)          // Step 2
        {
            multiples[i] = start * (i + 1);      // Step 3 and 4
        }
        return multiples;                        // Step 5
    }

    // ------------------------------------------------------------
    // Part 2: Solving a Complicated Problem Using a List
    // Function: RotateListRight
    // ------------------------------------------------------------
    // This function receives a list of data and an amount to rotate
    // to the right.
    //
    // Example: 
    // RotateListRight({1,2,3,4,5,6,7,8,9}, 5)
    // → {5,6,7,8,9,1,2,3,4}
    //
    // RotateListRight({1,2,3,4,5,6,7,8,9}, 3)
    // → {7,8,9,1,2,3,4,5,6}
    // ------------------------------------------------------------

    public static List<int> RotateListRight(List<int> data, int amount)
    {
        // PLAN / PROCESS STEPS:
        // Step 1: Calculate the index position (splitIndex) where rotation will occur.
        //         splitIndex = data.Count - amount
        // Step 2: Use GetRange() to get the last 'amount' elements (these move to the front).
        // Step 3: Use GetRange() again to get the first part of the list (from start to splitIndex).
        // Step 4: Create a new list and combine both parts:
        //         - Add the last 'amount' elements first.
        //         - Then add the first part.
        // Step 5: Return the new rotated list.

        // IMPLEMENTATION:
        int splitIndex = data.Count - amount;             // Step 1
        List<int> tail = data.GetRange(splitIndex, amount); // Step 2
        List<int> head = data.GetRange(0, splitIndex);      // Step 3

        List<int> rotated = new List<int>();              // Step 4
        rotated.AddRange(tail);
        rotated.AddRange(head);

        return rotated;                                   // Step 5
    }
}