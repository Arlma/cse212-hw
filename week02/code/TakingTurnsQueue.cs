using System;

public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add a new person to the queue with a name and number of turns.
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining (0 or less = infinite)</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person in the queue and return them.
    /// If the person has remaining turns or infinite turns, they are re-enqueued.
    /// Throws InvalidOperationException if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        if (_people.IsEmpty())
        {
            throw new InvalidOperationException("No one in the queue.");
        }

        Person person = _people.Dequeue();

        if (person.Turns <= 0)
        {
            // Infinite turns → always re-enqueue
            _people.Enqueue(person);
        }
        else
        {
            // Finite turns → use one turn
            person.Turns--;
            if (person.Turns > 0)
            {
                _people.Enqueue(person);
            }
        }

        return person;
    }
}
