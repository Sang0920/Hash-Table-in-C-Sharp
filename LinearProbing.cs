/*
 See https://aka.ms/new-console-template for more information
 Using .NET framework 6.0
*/

//NHOM 6
//Bang Bam Dia chi Mo Linear Probing trong C# (kieu du lieu so nguyen cho bang bam)




using System;

//define constant(s)
static class Constants
{
    public const double DEFAULT_LOAD_FACTOR = 0.5;  //50%
    public const int NULL_KEY_VALUE = -1;   //if the Key == null then Value = -1
}

namespace LinearProbing
{
    class LinearProbing
    {
        public struct Node
        {
            public int key, value;
        }

        public struct HashMap
        {
            public Node[] arr;
            public int size,    //size of hash table
                           n;  //number of elements (not null)
        }

        public static int HashFunc(int key, int M)
        {
            //Key cannot be divided by 0
            if (M == 0)
            {
                Console.WriteLine("Loi! Size cua bang bam phai lon hon 0.");
                System.Environment.Exit(0);
            }

            //hash function must return the hash value >= 0
            return (key % M) >= 0 ? (key % M) : -(key % M);
        }

        public static void InitHashTable(ref HashMap ht, int capacity)
        {
            ht.size = capacity;
            ht.n = 0;
            ht.arr = new Node[ht.size];
            for (int i = 0; i < ht.size; i++)
            {
                ht.arr[i].value = Constants.NULL_KEY_VALUE;
            }
        }

        public static void Traverse(HashMap ht)
        {
            Console.WriteLine("Table size: " + ht.size);
            Console.WriteLine("Index\tKey\tValue (null = -1, even = 0, odd = 1)");
            for (int i = 0; i < ht.size; i++)
            {
                if (ht.arr[i].value != Constants.NULL_KEY_VALUE)
                {
                    Console.WriteLine("[" + i + "]:\t" + ht.arr[i].key + "\t" + ht.arr[i].value);
                }
                else if (ht.arr[i].value == Constants.NULL_KEY_VALUE)
                {
                    Console.WriteLine("[" + i + "]:\t" + "null" + "\t" + ht.arr[i].value);
                }

            }
        }

        public static bool isPrime(int n)
        {
            if (n <= 1) return false;
            for(int i=2; i<n; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        public static int getPrime(int n)
        {
            n++;
            while (!isPrime(n))
            {
                n++;
            }
            return n;   
        }

        public static bool Insert(ref HashMap ht, int key)
        {
            //Insert an element
            int h = HashFunc(key, ht.size);
            for (int i = 0; i < ht.size; i++)
            {
                int pos = (h + i) % ht.size;

                if (ht.arr[pos].key == key && ht.arr[pos].value != Constants.NULL_KEY_VALUE)
                {
                    Console.WriteLine("\nKey " + key + " bi trung.");
                    return false;
                }
                else if (ht.arr[pos].value == Constants.NULL_KEY_VALUE)
                {

                    /*Set value for each key:
                    * - default value: -1 (InitHashTable() function);
                    * - if key mod 2 = 0 then value = 1;
                    * - else value = 0.
                    */
                    int value = (key % 2 == 0) ? 0 : 1;

                    ht.arr[pos].key = key;
                    ht.arr[pos].value = value;
                    ht.n++;
                    return true;
                }
            }
            return false;
        }

        public static void Resize(ref HashMap ht, int newCapacity)
        {
            HashMap newHT = new HashMap();

            InitHashTable(ref newHT, newCapacity);
            for (int i = 0; i < ht.size; i++)
            {
                if (ht.arr[i].value != Constants.NULL_KEY_VALUE)
                {
                    int key = ht.arr[i].key;
                    Insert(ref newHT, key);
                }
            }

            ht = newHT;
        }

        public static void Search(HashMap ht, int key)
        {
            int hash = HashFunc(key, ht.size), pos;
            bool isFound = false;
            for (int i = 0; i < ht.size; i++)
            {
                pos = (hash + i) % ht.size;
                if (ht.arr[pos].value != Constants.NULL_KEY_VALUE && key == ht.arr[pos].key)
                {
                    Console.WriteLine("Tim thay key " + key + " tai vi tri thu " + pos);
                    isFound = true;
                    break;
                }
            }
            if (!isFound)
                Console.WriteLine("Khong tim thay phan tu " + key + " trong bang bam.");
        }

        public static bool Delete(ref HashMap ht, int key)
        {
            int pos, h = HashFunc(key, ht.size);
            for (int i = 0; i < ht.size; i++)
            {
                pos = (h + i) % ht.size;
                if (ht.arr[pos].key == key && ht.arr[pos].value != Constants.NULL_KEY_VALUE)
                {
                    ht.arr[pos].value = Constants.NULL_KEY_VALUE;
                    ht.arr[pos].key = 0;    //null
                    ht.n--;
                    Resize(ref ht, ht.size);
                    return true;
                }
            }
            Console.WriteLine("Key " + key + " khong ton tai trong bang bam.");
            return false;
        }

        public static void Swap(ref Node a, ref Node b)
        {
            Node t = a;
            a = b;
            b = t;
        }

        public static int Partition(Node[] arr, int low, int high)
        {
            Node pivot = arr[high];  // selecting last element as pivot
            int i = (low - 1);  // index of smaller element

            for (int j = low; j <= high - 1; j++)
            {
                // If the current element is smaller than or equal to pivot
                if (arr[j].key <= pivot.key)
                {
                    i++;    // increment index of smaller element
                    Swap(ref arr[i], ref arr[j]);
                }
            }
            Swap(ref arr[i + 1], ref arr[high]);
            return (i + 1);
        }
        /*  
            a[] is the array, p is starting index, that is 0, 
            and r is the last index of array.  
        */
        public static void QuickSort(ref Node[] arr, int p, int r)
        {
            if (p < r)
            {
                int q;
                q = Partition(arr, p, r);
                QuickSort(ref arr, p, q - 1);
                QuickSort(ref arr, q + 1, r);
            }
        }

        public static int Menu(string[] items)
        {
            for(int i=0; i< items.Length; i++)
            {
                Console.WriteLine(items[i]);    
            }
            Console.Write("Chon chuc nang: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }

        static void Main()
        {
            Console.Title ="Bang bam dia chi mo (Linear Probing)";

            HashMap ht = new HashMap();

            int M = 5;  //Size of hash table first time

            InitHashTable(ref ht, M);

            int[] arr = { 3, -3, 3, 445, 47, -63, 10, -8, 74, 0, 121, -1 };
            Console.WriteLine("Mang cua ban: ");
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + "\t");
            }

                //Insert array to hash table
                for (int i = 0; i < arr.Length; i++)
                {

                    //Resize when mumber of elements in hash map over or equal 50%
                    double loadFactor = (double)ht.n / ht.size;
                    if (loadFactor >= Constants.DEFAULT_LOAD_FACTOR)
                    {
                        Resize(ref ht, getPrime(ht.size * 2));
                    }

                    Insert(ref ht, arr[i]);
                }
            Console.WriteLine("\nBang bam cua ban: ");
            Traverse(ht);
            int choice;
            
            string[] Items = { "\n\tOptions\n0. Thoat.", "1. Them mot phan tu vao bang bam.", "2. Xoa mot phan tu khoi bang bam", "3. Tim kiem mot phan tu", "4. Sap xep lai bang bam" };
            do {
                 choice = Menu(Items);
                switch (choice) {
                    case 0: { break; }
                    case 1: {
                            Console.Write("\tNhap vao phan tu ban muon them: ");
                            int key = Convert.ToInt32(Console.ReadLine());

                            //Resize when mumber of elements in hash map over or equal 50%
                            double loadFactor = (double)ht.n / ht.size;
                            if (loadFactor >= Constants.DEFAULT_LOAD_FACTOR)
                            {
                                Resize(ref ht, getPrime(ht.size * 2));
                            }

                            if (Insert(ref ht, key) == true)
                            {
                                Console.WriteLine("Sau khi them mot khoa " + key + " vao bang bam:");
                                Traverse(ht);
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Write("Nhap vao phan tu ban muon xoa: ");
                            int key = Convert.ToInt32(Console.ReadLine());
                            if (Delete(ref ht, key))
                            {
                                Console.WriteLine("Sau khi xoa mot khoa " + key + " khoi bang bam:");
                                Traverse(ht);
                            }
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Nhap vao phan tu ban muon tim: ");
                            int key = Convert.ToInt32(Console.ReadLine());
                            Search(ht, key);
                            break;
                        }
                    case 4:
                        {
                            Resize(ref ht, ht.n);   //Resize to ht.n (number of elments in hash table)
                            Console.WriteLine("Bang bam sau khi resize: ");
                            Traverse(ht);

                            Console.WriteLine("\nSap xep tang dan:" + ht.n);
                            HashMap tmpHT = ht;     //Use temporary hash map 
                            QuickSort(ref tmpHT.arr, 0, tmpHT.size - 1);
                            Console.WriteLine("Bang bam sau khi sap xep tang dan (bang bam tam thoi):");
                            Traverse(tmpHT);
                            break;
                        }
                    default: {
                            Console.WriteLine("Lua chon khong hop le.");
                            break;
                        }
                }
            }while (choice!=0);

            Console.Write("\nNhan Enter de thoat...");
            Console.ReadKey();
        }
    }
}

