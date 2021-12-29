//Day la chuong trinh minh hoa cho bai tap lon (assignment) cua Nhom 6 - Bang bam bang phuong phap noi ket truc tiep

// See https://aka.ms/new-console-template for more information
//Using .NET framework 6.0

using System;

static class Constants
{
    public const int M = 5;    //Max size is a prime number
}

namespace HashMap
{

    class SeparateChaining
    {
        public struct Student
        {
            public string Name;
            public double GPA;
            public int ID;
        }
        public class Node
        {
            public Student student;  //This is value
            public int ID; //This is key
            public Node next;
        }

        public static Node CreateNode(Student x)
        {
            Node p = new Node();
            p.ID = x.ID;    //Key = Student.ID
            p.student = x;  //Value = Student
            p.next = null;
            return p;
        }

        public static void InitHashTable(Node[] heads)
        {
            for (int i = 0; i < Constants.M; i++)
            {
                heads[i] = null;
            }
        }

        public static int HashFunc(int value)
        {
            return (value % Constants.M) >= 0 ? (value % Constants.M) : -(value % Constants.M);     //Return unsigned integer number
        }

        public static bool InsertNode(Node[] heads, Node node)
        {
            int h = HashFunc(node.ID);
            Node r = heads[h];
            Node tmp = heads[h];

            while(tmp != null)
            {
                if (tmp.ID == node.ID)
                {
                    Console.WriteLine("Khoa " + tmp.ID + " bi trung.");
                    return false;
                }
                tmp = tmp.next;
            }

            Node prev = null;
            while (r != null && r.student.GPA < node.student.GPA)
            {
                prev = r;
                r = r.next;
            }

            Node p = CreateNode(node.student);
            //Add head
            if (prev == null)
            {
                heads[h] = p;
                p.next = r;
            }
            //Add tail
            else if (r == null)
            {
                prev.next = p;
            }
            //Add previous of r
            else
            {
                p.next = r;
                prev.next = p;
            }
            return true;
        }

        //print a student
        public static void PrintStudent(Student st)
        {
            Console.Write(st.Name + ", " + st.GPA);
        }

        public static void Traverse(Node[] heads)
        {
            Console.WriteLine("Hash Table");
            for (int i = 0; i < Constants.M; i++)
            {
                if (heads[i] != null)
                {
                    Console.Write("Bucket[" + i + "]: ");
                    Node p = heads[i];
                    while (p != null)
                    {
                        Console.Write("{ID: " + p.ID + ", ");
                        PrintStudent(p.student);
                        Console.Write("} -> ");
                        p = p.next;
                    }
                    Console.Write("\n");
                }
                else
                {
                    Console.WriteLine("Bucket[" + i + "]: null");
                }
            }
        }


        public static Node Search(Node[] heads, int x)
        {
            int h = HashFunc(x);

            Node p = heads[h];

            while (p != null && p.ID != x)
            {
                p = p.next;
            }
            return p;
        }

        public static void Delete(ref Node[] heads, int key)
        {
            int h = HashFunc(key);
            Node head = heads[h];

            Node prev = null;
            Node tmp = head;

            //if node present at head
            if (tmp != null && tmp.ID == key)
            {
                heads[h] = tmp.next;    //Change head
                return;
            }
            //Else, search for the key to be delete,
            else
            {
                while (tmp != null && tmp.ID != key)
                {
                    prev = tmp;
                    tmp = tmp.next;
                }

                //if key was not present in linked list
                if (tmp == null)
                {
                    Console.WriteLine("Khong co khoa nao co gia tri " + key);
                    return;
                }

                //Unlinke node from linked list
                prev.next = tmp.next;
            }
        }

        public static int Menu(string[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Console.WriteLine(items[i]);
            }
            Console.Write("\tChon chuc nang: ");

            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }

        public static void Main()
        {
            Console.Title = "Bang bam bang phuong phap ket noi truc tiep.";

            Node[] heads = new Node[Constants.M];

            InitHashTable(heads);


            Student[] students = new Student[Constants.M];

            students[0].ID = 7894;
            students[0].Name = "Nguyen Van A";
            students[0].GPA = 8.4;

            students[1].ID = 7412;
            students[1].Name = "Tran Van B";
            students[1].GPA = 7.5;

            students[2].ID = 1587;
            students[2].Name = "Nguyen Ngoc Minh C";
            students[2].GPA = 9.3;

            students[3].ID = 9612;
            students[3].Name = "Huynh Van D";
            students[3].GPA = 8.5;

            for (int i = 0; i < students.Length; i++)
            {
                if (students[i].ID != 0)    //if(students[i].ID != null)
                {
                    Node p = CreateNode(students[i]);
                    InsertNode(heads, p);
                }
            }
            Traverse(heads);

            string[] Items = { "\n\tOptions:\n0. Thoat.",
                "1. Them mot sinh vien.",
                "2. Xoa mot sinh vien.",
                "3. Tim kiem thong tin mot sinh vien."};
            int choice;

            do
            {
                choice = Menu(Items);
                switch (choice)
                {
                    case 0: { break; }
                    case 1:
                        {
                            Student st;
                            Console.WriteLine("Nhap vao thong tin sinh vien can them:");
                            Console.Write("\tMa so sinh vien: ");
                            st.ID = Convert.ToInt32(Console.ReadLine());
                            Console.Write("\tTen sinh vien: ");
                            st.Name = Console.ReadLine();
                            Console.Write("\tDiem trung binh: ");
                            st.GPA = Convert.ToDouble(Console.ReadLine());
                            Node p = CreateNode(st);

                            if (InsertNode(heads, p))
                            {
                                Console.WriteLine("Bang bam sau khi them:");
                                Traverse(heads);
                            }
                            break;
                        }
                    case 2:
                        {
                            Console.Write("\tMa so sinh vien can xoa: ");
                            int ID = Convert.ToInt32(Console.ReadLine());
                            Delete(ref heads, ID);
                            Console.WriteLine("Bang bam sau khi xoa:");
                            Traverse(heads);
                            break;
                        }
                    case 3:
                        {
                            Console.Write("\tNhap vao ma so sinh vien can tim kiem: ");
                            int ID = Convert.ToInt32(Console.ReadLine());
                            if (Search(heads, ID) == null)
                            {
                                Console.WriteLine("Khong co sinh vien nao co ma so " + ID);
                            }
                            else
                            {
                                Console.WriteLine("Thong tin sinh vien co ma so " + ID);
                                PrintStudent(Search(heads, ID).student);
                                Console.Write("\n");
                            }
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Lua chon khong hop le.");
                            break;
                        };
                }
            } while (choice != 0);

            Console.WriteLine("Press any key to close this Window...");
            Console.ReadKey();
            return;
        }
    }
}
