using System;

namespace DoFactory.GangOfFour.Memento.Structural
{
    /// <summary>
    /// MainApp startup class for Structural 
    /// Memento Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            Originator o = new Originator();
            o.State = "On";

            // Store internal state
            Caretaker c = new Caretaker();
            c.Memento = o.CreateMemento();

            // Continue changing originator
            o.State = "Off";

            // Restore saved state
            o.SetMemento(c.Memento);

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Originator' class
    /// </summary>
    class Originator
    {
        string state;

        // Property
        public string State
        {
            get { return state; }
            set
            {
                state = value;
                Console.WriteLine("State = " + state);
            }
        }

        // Creates memento 
        public Memento CreateMemento()
        {
            return (new Memento(state));
        }

        // Restores original state
        public void SetMemento(Memento memento)
        {
            Console.WriteLine("Restoring state...");
            State = memento.State;
        }
    }

    /// <summary>
    /// The 'Memento' class
    /// </summary>
    class Memento
    {
        string state;

        // Constructor
        public Memento(string state)
        {
            this.state = state;
        }

        // Gets or sets state
        public string State
        {
            get { return state; }
        }
    }

    /// <summary>
    /// The 'Caretaker' class
    /// </summary>
    class Caretaker
    {
        Memento memento;

        // Gets or sets memento
        public Memento Memento
        {
            set { memento = value; }
            get { return memento; }
        }
    }
}
