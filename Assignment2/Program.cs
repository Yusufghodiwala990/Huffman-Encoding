
/*----------------------------------------------------------------------*/

/*This console application uses Huffman trees to encode and decode text using a 
   priority queue. Character and their frequencies are stored as nodes with left 
   and right references. In the priority queue, 2 nodes with the least total 
   frequency have higher priority and are removed first. Thus generating fewer 
    bits for higher frequency characters */

/*----------------------------------------------------------------------*/

//Assignment 2 by Yusuf Ghodiwala and Nizar Khalili 



using System;
using System.Collections.Generic;
using System.Text;


// using the PriorityQueue given in class
namespace PriorityQueue
{
    class Node : IComparable
    {
        public char Character { get; set; }
        public int Frequency { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        //Constructor
        //Storing character,frequency and references to left and right
        public Node(char character, int frequency, Node left, Node right)
        {
            this.Character = character;
            this.Frequency = frequency;
            this.Left = left;
            this.Right = right;
        }

        //Implementing CompareTo from IComparable
        public int CompareTo(Object obj)
        {
            if (obj != null)
            {
                Node q = (Node)obj;   // Explicit cast
                if (q != null)
                    return Frequency - q.Frequency; //If the current node's freq is greater than the passed node's freq
                else
                    return 1;
            }
            else
                return 1;
        }

        public interface IContainer<T>
        {
            void MakeEmpty();  // Reset an instance to empty
            bool Empty();      // Test if an instance is empty
            int Size();        // Return the number of items in an instance
        }

        public interface IPriorityQueue<T> : IContainer<T> where T : IComparable
        {
            void Add(T item);  // Add an item to a priority queue
            void Remove();     // Remove the item with the highest priority
            T Front();         // Return the item with the highest priority
        }

        // Priority Queue
        // Implementation:  Binary heap
        public class PriorityQueue<T> : IPriorityQueue<T> where T : IComparable
        {
            private int capacity;  // Maximum number of items in a priority queue
            private T[] A;
            private int count;     // Number of items in a priority queue

            // Constructor 1
            // Create a priority with maximum capacity of size
            // Time complexity:  O(1)
            public PriorityQueue(int size)
            {

                capacity = size;
                A = new T[size + 1];  // Indexing begins at 1
                count = 0;
            }

            // Constructor 2
            // Create a priority from an array of items
            // Time complexity:  O(n)
            public PriorityQueue(T[] inputArray)
            {
                int i;

                count = capacity = inputArray.Length;
                A = new T[capacity + 1];

                for (i = 0; i < capacity; i++)
                {
                    A[i + 1] = inputArray[i];
                }

                BuildHeap();
            }

            // PercolateUp
            // Percolate up an item from position i in a priority queue
            // Time complexity:  O(log n)
            private void PercolateUp(int i)
            {
                int child = i, parent;

                // As long as child is not the root (i.e. has a parent)
                while (child > 1)
                {
                    parent = child / 2;
                    if (A[child].CompareTo(A[parent]) < 0)
                    // If child has a higher priority than parent
                    {
                        // Swap parent and child
                        T item = A[child];
                        A[child] = A[parent];
                        A[parent] = item;
                        child = parent;  // Move up child index to parent index
                    }
                    else
                        // Item is in its proper position
                        return;
                }
            }

            // Add
            // Add an item into the priority queue
            // Time complexity:  O(log n)
            public void Add(T item)
            {
                if (count < capacity)
                {
                    A[++count] = item;  // Place item at the next available position
                    PercolateUp(count);
                }
            }

            // PercolateDown
            // Percolate down from position i in a priority queue
            // Time complexity:  O(log n)
            private void PercolateDown(int i)
            {
                int parent = i, child;

                // While parent has at least one child
                while (2 * parent <= count)
                {
                    // Select the child with the highest priority
                    child = 2 * parent;    // Left child index
                    if (child < count)  // Right child also exists
                        if (A[child + 1].CompareTo(A[child]) < 0)  // Right child has a higher priority than left child
                            child++;

                    // If child has a higher priority than parent
                    if (A[child].CompareTo(A[parent]) < 0)
                    {
                        // Swap parent and child
                        T item = A[child];
                        A[child] = A[parent];
                        A[parent] = item;
                        parent = child;  // Move down parent index to child index
                    }
                    else return; // Item is in its proper place
                }
            }

            // Remove
            // Remove an item from the priority queue
            // Time complexity:  O(log n)
            public void Remove()
            {
                if (!Empty())
                {
                    // Remove item with highest priority (root) and
                    // replace it with the last item
                    A[1] = A[count--];

                    // Percolate down the new root item
                    PercolateDown(1);
                }
            }

            // Front
            // Return the item with the highest priority
            // Time complexity:  O(1)
            public T Front()
            {
                if (!Empty())
                {
                    return A[1];  // Return the root item (highest priority)
                }
                else
                    return default(T);
            }

            // BuildHeap
            // Create a binary heap from a given list
            // Time complexity:  O(n)
            private void BuildHeap()
            {
                int i;

                // Percolate down from the last parent to the root (first parent)
                for (i = count / 2; i >= 1; i--)
                {
                    PercolateDown(i);
                }
            }

            // HeapSort
            // Sort and return inputArray
            // Time complexity: O(n log n)
            public void HeapSort(T[] inputArray)
            {
                int i;

                capacity = count = inputArray.Length;

                // Copy input array to A (indexed from 1)
                for (i = capacity - 1; i >= 0; i--)
                {
                    A[i + 1] = inputArray[i];
                }

                // Create a binary heap
                BuildHeap();

                // Remove the next item and place it into the input (output) array
                for (i = 0; i < capacity; i++)
                {
                    inputArray[i] = Front();
                    Remove();
                }
            }

            // MakeEmpty
            // Reset a priority queue to empty
            // Time complexity:  O(1)
            public void MakeEmpty()
            {
                count = 0;
            }

            // Empty
            // Return true if the priority is empty; false otherwise
            // Time complexity:  O(1)
            public bool Empty()
            {
                return count == 0;
            }

            // Size
            // Return the number of items in the priority queue
            // Time complexity:  O(1)
            public int Size()
            {
                return count;
            }
        }


        // Huffman class which implements a Huffman tree

        class Huffman
        {
            private Node HT;   //Huffman tree to create codes and decode text    // Reference to the root of the tree
            private Dictionary<char, string> D = new Dictionary<char, string>(); // Dictionary to encode text
            Dictionary<char, int> charDictionary = new Dictionary<char, int>();  // Dictionary to store characters and frequencies

            public Huffman(string S)
            {
                Dictionary<char, int> charWithFreq = new Dictionary<char, int>();
                charWithFreq = AnalyzeText(S); // Calling analyzetext which retruns a dictionary of characters and frequencies
                Build(charWithFreq); // Building huffman tree with the given dictionary

                // Formatting
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("* Huffman Tree visualization *");
                Console.WriteLine("(Binary tree is presented at a 90 degree angle)");
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                // Printing the huffman tree
                Print(HT, 0);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine();
                Console.WriteLine("*  End of visualization *");

                // Creating codes
                CreateCodes(HT, "");

                // Encoding
                string bits = Encode(S);

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Your currently encoded message is: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(bits);
                Console.WriteLine();
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Number of bits in encoded message: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(bits.Length);
                Console.WriteLine();

                // Decoding
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Decoding ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(bits);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" yields the message: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Decode(bits, HT));
                Console.ResetColor();
            }


            // Method : AnalyzeText
            // Parameters: Takes the string from the user
            // Return Type : Dictionary
            // Description: Given a certain string, a dictionary is created with characters and their frequencies, and returned.

            private Dictionary<char, int> AnalyzeText(string S) // Key is the character and the value is the frequency
            {
                foreach (char currentChar in S)
                {
                    if (charDictionary.ContainsKey(currentChar)) /* If a certain char(key) exists in the dictionary, 
                                                                    increase its frequency*/
                    {
                        charDictionary[currentChar]++; // [] signifies to alter a value(frequency) of the 
                        // Specified key,  key == character
                    }
                    else
                    {
                        charDictionary.Add(currentChar, 1);  //If it's a new character
                    }
                }
                return charDictionary;
            }


            // Method : Build
            // Parameters: Dictionary of characters and their frequency
            // Return Type : Void
            /* Description: Given a dictionary, we build a tree using the Priority Queue 
               where 2 nodes with the highest priority are removed. */

            private void Build(Dictionary<char, int> charDictionary)
            {
                // Implementing priority queue class and using it's methods to mainpulate the huffman tree
                PriorityQueue<Node> PQ;

                PQ = new PriorityQueue<Node>(charDictionary.Count); // Creating a priority queue of Nodes 
                                                           // Based on the size of the dictionary(char,frequency)

                foreach (KeyValuePair<char, int> entry in charDictionary) // Adding each character as a node
                {
                    Node q;
                    q = new Node(entry.Key, entry.Value, null, null);
                    PQ.Add(q);              // Creating leaf nodes 
                }

                /* To create root nodes, HT gets put back into the queue each time until size !=1*/
                while (PQ.Size() != 1)
                {
                    // Removing the two highest priority nodes
                    // Less frequent == higher priority
                    Node x = PQ.Front();
                    PQ.Remove();
                    Node y = PQ.Front();
                    PQ.Remove();

                    // Error checking if user enters nothing
                    if (x == null || y == null)
                        throw new ArgumentException("Input cannot be empty, exiting program.");

                    // Forming a root node by adding the removed nodes frequency
                    HT = new Node(default(char), x.Frequency + y.Frequency, x, y);
                    PQ.Add(HT);
                }
                HT = PQ.Front(); // First node in the queue will be the tree created with all the left and right references
            }




            // Method : CreateCodes
            // Parameters: Root Node(Tree) and an empty string of bits
            // Return Type : Void
            /* Description: Given a root(tree), traversal is done of the tree recursively whilst appending 
               bits and adding them to dictionary D when we reach a leaf node. Key => char, Value=> bits */

            private void CreateCodes(Node HT, string bits)
            {
                if (charDictionary.Count == 1) // If we only have one character(no distinct characters)
                    D.Add(HT.Character, "0");  // Append a 0(value) to a given character(key) into Dictionary D

                else if (HT.Left == null && HT.Right == null) //If we reach base node
                    D.Add(HT.Character, bits); // Add all the bits appended to the Dictionary along with the character

                else
                {
                    if (HT.Left != null)
                    {
                        CreateCodes(HT.Left, bits + "0"); // Traverse on the left of the tree recursively
                    }

                    if (HT.Right != null)
                    {
                        CreateCodes(HT.Right, bits + "1"); // Traverse on the right of the tree recursively
                    }
                }
            }



            // Method : Encode
            // Parameters: String the user entered
            // Return Type : String of Bits
            /* Description: Dictionary D has all the characters and the required bits to enocode the string passed.
               This method simply appends all the bits(value) given a certain char(key)*/

            private string Encode(string s)
            {
                string bits = "";

                foreach (char c in s) // Looping through each char in given string
                    bits += D[c];  // Appending bits(value) from the dictionary based on the current char in s

                return bits; // z => 10101
            }


            // Method : Decode
            // Parameters: String of bits(generated through Encode), Root of the tree
            // Return Type : Decoded Text(String)
            /* Description: Traversal of the tree is done iteratively based on the path 
               specified by bits generated "0"=> left, "1"=>right. */

            private string Decode(string S, Node HT)
            {
                Node temp = HT;    // Reference to the root
                string decoded = "";  // Variable to store the decoded text

                if (temp.Left == null && temp.Right == null) // If we only have one character in the tree
                {
                    foreach (char v in S) // Loop through all the bits and decode append the character
                    {
                        decoded += HT.Character;
                    }
                }
                else
                {
                    // Traversing the tree based on the path given by the string
                    foreach (char c in S) // Looping through the bits
                    {
                        if (c == '0')           // If it's 0, go left
                            temp = temp.Left;
                        else                     // Else go right
                            temp = temp.Right;

                        if (temp.Left == null && temp.Right == null) // If we reach the leaf node
                        {
                            decoded += temp.Character;        // Append the character to decoded
                            temp = HT; // Start at the root once again once a char has been found
                        }
                    }
                }
                return decoded; // 10101 => z
            }


            // Method : Print
            // Parameters: String of bits(generated through Encode), Root of the tree
            // Return Type : Decoded Text(String)
            /* Description: Print method to display the binary tree at a 90 degree angle. Done 
               recursively. ---The code for this method was taken from the lecture slides on Binary Trees--- */

            public void Print(Node HT, int indent)
            {

                if (HT != null)
                {
                    // Adding indentation whilst traversing the right sub tree of the huffman tree
                    Print(HT.Right, indent + 5);
                    // Print the item and its frequency
                    Console.WriteLine(new string(' ', indent) + HT.Character + HT.Frequency);
                    // Adding indentation whilst traversing the left sub tree of the huffman tree
                    Print(HT.Left, indent + 5);
                }
            }



            static void Main(string[] args)
            {
                try // Try block for exception handling
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Welcome to Huffman Tree");
                    Console.Write("Enter text to encode (or -1 to quit): ");
                    string s = Console.ReadLine();

                    while (s != "-1") // Loop to exit if -1 input is detected
                    {
                        Huffman g = new Huffman(s);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("******************************************");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        Console.Write("Enter next text to encode (or -1 to quit): ");
                        s = Console.ReadLine();

                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Thank you for your participation.");
                    Console.ReadLine();
                }
                catch (ArgumentException e) // Catching a no input exception
                {
                    Console.WriteLine(e.Message); // Displaying the error message
                    Console.ReadLine();
                }
            }
        }
    }
}
