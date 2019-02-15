using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace DoFactory.GangOfFour.Memento.NETOptimized
{
    /// <summary>
    /// MainApp startup class for .NET optimized 
    /// Memento Design Pattern.
    /// </summary>
    class MainApp
    {
        /// <summary>
        /// Entry point into console application.
        /// </summary>
        static void Main()
        {
            // Init sales prospect through object initialization
            var s = new SalesProspect
                      {
                          Name = "Joel van Halen",
                          Phone = "(412) 256-0990",
                          Budget = 25000.0
                      };

            // Store internal state
            var m = new ProspectMemory();
            m.Memento = s.SaveMemento();

            // Change originator
            s.Name = "Leo Welch";
            s.Phone = "(310) 209-7111";
            s.Budget = 1000000.0;

            // Restore saved state
            s.RestoreMemento(m.Memento);

            // Wait for user
            Console.ReadKey();
        }
    }

    /// <summary>
    /// The 'Originator' class
    /// </summary>
    [Serializable]
    class SalesProspect
    {
        string name;
        string phone;
        double budget;

        // Gets or sets name
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                Console.WriteLine("Name:   " + name);
            }
        }

        // Gets or sets phone
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                Console.WriteLine("Phone:  " + phone);
            }
        }

        // Gets or sets budget
        public double Budget
        {
            get { return budget; }
            set
            {
                budget = value;
                Console.WriteLine("Budget: " + budget);
            }
        }

        // Stores (serializes) memento
        public Memento SaveMemento()
        {
            Console.WriteLine("\nSaving state --\n");

            var memento = new Memento();
            return memento.Serialize(this);
        }

        // Restores (deserializes) memento
        public void RestoreMemento(Memento memento)
        {
            Console.WriteLine("\nRestoring state --\n");

            var s = (SalesProspect)memento.Deserialize();
            this.Name = s.Name;
            this.Phone = s.Phone;
            this.Budget = s.Budget;
        }
    }

    /// <summary>
    /// The 'Memento' class
    /// </summary>
    class Memento
    {
        MemoryStream stream = new MemoryStream();
        SoapFormatter formatter = new SoapFormatter();

        public Memento Serialize(object o)
        {
            formatter.Serialize(stream, o);
            return this;
        }

        public object Deserialize()
        {
            stream.Seek(0, SeekOrigin.Begin);
            object o = formatter.Deserialize(stream);
            stream.Close();

            return o;
        }
    }

    /// <summary>
    /// The 'Caretaker' class
    /// </summary>
    class ProspectMemory
    {
        public Memento Memento { get; set; }
    }
}
