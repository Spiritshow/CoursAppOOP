using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace CoursAppOOP
{

    public partial class Form1 : Form
    {
        class CComputer : IComparable<CComputer>
        {
            private int ID { get; set; }
            private float V_cpu { get; set; }
            private int count_cores { get; set; }
            private float amount_RAM { get; set; }
            private float amount_HD { get; set; }
            private float amount_occupied_HD { get; set; }
            private float parametr { get; set; }
            public CComputer()
            {
                ID = 0;
                V_cpu = 0.0f;
                count_cores = 0;
                amount_RAM = 0.0f;
                amount_HD = 0.0f;
                amount_occupied_HD = 0.0f;
                parametr = float.MaxValue;
            }

            public CComputer(int _ID, float _V_cpu, int _count_cores, float _amount_RAM, float _amount_HD, float _amount_occupied_HD, float _paremetr)
            {
                ID = _ID;
                V_cpu = _V_cpu;
                count_cores = _count_cores;
                amount_RAM = _amount_RAM;
                amount_HD = _amount_HD;
                amount_occupied_HD = _amount_occupied_HD;
                parametr = _paremetr;
            }

            public int getID() { return ID; }
            public float getAmountCpu() { return V_cpu; }
            public int getCountCores() { return count_cores; }
            public float getAmountRAM() { return amount_RAM; }
            public float getAmountHD() { return amount_HD; }
            public float getAmountOccupiedHD() { return amount_occupied_HD; }

            public void setCcomputer(int _ID, int V, int cores, float am_RAM, float am_HD, float am_oc_HD)
            {
                ID= _ID;
                V_cpu = V;
                count_cores = cores;
                amount_RAM = am_RAM;
                amount_HD = am_HD; AllMemory += amount_HD;
                while (am_oc_HD > amount_HD)
                {
                    Console.WriteLine("Занятой пямяти не может быть больше самого обёма памяти.");
                    Console.WriteLine("Введите коректное значение занятого объёма памяти: ");
                    am_oc_HD = float.Parse(Console.ReadLine());
                }

                amount_occupied_HD = am_oc_HD;
            }

            public float getFreeAmountHD() { return amount_HD - amount_occupied_HD; }
            public bool CaBeCopied(float copy) { return copy < getFreeAmountHD(); }
            public void Info()
            {
                Console.WriteLine("Частота процессора: " + V_cpu);
                Console.WriteLine("Кол-во ядер: " + count_cores);
                Console.WriteLine("Объём оперативной памяти: " + amount_RAM);
                Console.WriteLine("Объём Жёсткого диска: " + amount_HD);
                Console.WriteLine("Занято места на Жёстком диске: " + amount_occupied_HD);
            }
            public void EditHD(float occupied)
            {
                amount_occupied_HD = occupied;
            }
            public void EditHD(float occupied, float amout)
            {
                amount_occupied_HD = occupied;
                AllMemory -= amount_HD;
                AllMemory += amout;
                amount_HD = amout;

            }

            //Ex G
            public static float AllMemory { set; get; }

            public static float getAllMemory() { return AllMemory; }

            public void SetParametrs(int i)
            {
                switch (i)
                {
                    case 0:
                        parametr = V_cpu;
                        break;
                    case 1:
                        parametr = count_cores;
                        break;
                    case 2:
                        parametr = amount_RAM;
                        break;
                    case 3:
                        parametr = amount_HD;
                        break;
                    case 4:
                        parametr = amount_occupied_HD;
                        break;
                    case 5:
                        parametr = getFreeAmountHD();
                        break;
                    case 6:
                        parametr = getID();
                        break;
                }

            }
            public float getParametr() { return parametr; }
            public static bool operator ==(CComputer a, CComputer b)
            {
                return a.getParametr() == b.getParametr();
            }
            public static bool operator !=(CComputer a, CComputer b)
            {
                return a.getParametr() == b.getParametr();
            }
            public static bool operator >(CComputer a, CComputer b)
            {
                return a.getParametr() > b.getParametr();
            }
            public static bool operator <(CComputer a, CComputer b)
            {
                return a.getParametr() < b.getParametr();
            }
            public int CompareTo(CComputer vec)
            {
                if (parametr > vec.parametr)
                    return 1;
                if (parametr < vec.parametr)
                    return -1;
                return 0;
            }

        }

        
        

        // Класс для минимальной кучи
        class MaxHeap <T> where T : IComparable<T> , new()
        {
            public T[] heapArray { get; set; } //массив
            public int capacity { get; set; }//макс размер
            public int current_heap_size { get; set; } // текущее кол-во

            public MaxHeap(int n)
            {
                capacity = n;
                heapArray = new T[capacity];
                current_heap_size = 0;
            }

            public static void Swap<T>(ref T lhs, ref T rhs)
            {
                T temp = lhs;
                lhs = rhs;
                rhs = temp;
            }

            public int Parent(int key)
            {
                return (key - 1) / 2;
            }
            public int Left(int key)
            {
                return 2 * key + 1;
            }
            public int Right(int key)
            {
                return 2 * key + 2;
            }

            public bool insertKey(T key)
            {
                if (current_heap_size == capacity)
                {
                    return false;
                }

                int i = current_heap_size;
                heapArray[i] = key;
                current_heap_size++;

                // это сортировка если порядок нарушен 
                while (i != 0 && heapArray[i].CompareTo(heapArray[Parent(i)])>0)
                {
                    Swap(ref heapArray[i], ref heapArray[Parent(i)]);
                    i = Parent(i);
                }
                Heap();
                return true;
            }

            // Уменьшение значеня////////
            public void decreaseKey(int key, T new_val)
            {
                heapArray[key] = new_val;

                while (key != 0 && heapArray[key].CompareTo(heapArray[Parent(key)])>0)
                {
                    Swap(ref heapArray[key], ref heapArray[Parent(key)]);
                    key = Parent(key);
                }
            }

            public T getMax()
            {
                return heapArray[0];
            }

            public T extractMax()
            {
                if (current_heap_size <= 0)
                {
                   T noob = new T();
                    //пуст от того ничего и не делает
                    return noob;
                }

                if (current_heap_size == 1)
                {
                    current_heap_size--;
                    return heapArray[0];
                }


                T root = heapArray[0];

                heapArray[0] = heapArray[current_heap_size - 1];
                current_heap_size--;
                MaxHeapify(0);

                return root;
            }


            public void deleteKey(int key)
            {
                //T max = new T();
                //decreaseKey(key, max);
                //extractMax();

                Swap<T>(ref heapArray[0],ref heapArray[key]);
                heapArray[0] = heapArray[current_heap_size-1];
                current_heap_size --;
                Heap();

            }

            /// //////////////////


            public void MaxHeapify(int key) //что бы привезти в порядок всё должно быть уже в куче
            {
                int l = Left(key);
                int r = Right(key);

                int smallest = key;
                if (l < current_heap_size && heapArray[l].CompareTo(heapArray[smallest])>0)
                {
                    smallest = l;
                }
                if (r < current_heap_size && heapArray[r].CompareTo(heapArray[smallest])>0)
                {
                    smallest = r;
                }

                if (smallest != key)
                {
                    Swap(ref heapArray[key],
                         ref heapArray[smallest]);
                    MaxHeapify(smallest);
                }
            }

            public T[] showHeap()
            {
                return heapArray;
            }

            public void Heap()
            {
                for (int key = 0; key < current_heap_size; key++)
                {
                    while (key != 0 && heapArray[key].CompareTo(heapArray[Parent(key)]) > 0)
                    {
                        Swap(ref heapArray[key], ref heapArray[Parent(key)]);
                        key = Parent(key);
                    }
                }
            }
            
        }

        MaxHeap<CComputer> Hep = new MaxHeap<CComputer>(40);
        public void CreetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Индекс",typeof(int));
            table.Columns.Add("Частота ЦПУ", typeof(float));
            table.Columns.Add("Кол-во ядер", typeof(int));
            table.Columns.Add("Кол-во RAM памяти", typeof(float));
            table.Columns.Add("Всего места на HD", typeof(float));
            table.Columns.Add("Занятого места на HD", typeof(float));
            table.Columns.Add("Кол-во свободного места", typeof(float));

            for (int i = 0; i < Hep.current_heap_size; i++)//+
            {
                table.Rows.Add(Hep.heapArray[i].getID(), Hep.heapArray[i].getAmountCpu(), Hep.heapArray[i].getCountCores(), Hep.heapArray[i].getAmountRAM(), Hep.heapArray[i].getAmountHD(), Hep.heapArray[i].getAmountOccupiedHD(), Hep.heapArray[i].getFreeAmountHD());
            }

            dataGridView1.DataSource = table;
            Hep.current_heap_size = 0;

        }

        public void CreetList()
        {
            string path = @"C:\Users\vkozy\Desktop\1.txt";
            using (StreamReader reader = new StreamReader(path))
            {

                while (reader.ReadLine() != null)
                {
                    int ID = Convert.ToInt32(reader.ReadLine());
                    float V = Convert.ToSingle(reader.ReadLine());
                    int cores = Convert.ToInt32(reader.ReadLine());
                    float RAM = Convert.ToSingle(reader.ReadLine());
                    float HD = Convert.ToSingle(reader.ReadLine());
                    float occuped = Convert.ToSingle(reader.ReadLine());

  
                    Hep.insertKey(new CComputer(ID, V, cores, RAM, HD, occuped, 0.0f));//+
                }
            }
        }

        public void InFile()
        {
            string path = @"C:\Users\vkozy\Desktop\1.txt";
            StreamWriter sw = new StreamWriter(path); sw.Close();
            using (StreamWriter stream = new StreamWriter(path, true))
            {
                for (int i = 0; i < Hep.current_heap_size; i++)
                {
                    stream.WriteLine(i);
                    stream.WriteLine(Hep.heapArray[i].getID());
                    stream.WriteLine(Hep.heapArray[i].getAmountCpu());
                    stream.WriteLine(Hep.heapArray[i].getCountCores());
                    stream.WriteLine(Hep.heapArray[i].getAmountRAM());
                    stream.WriteLine(Hep.heapArray[i].getAmountHD());
                    stream.WriteLine(Hep.heapArray[i].getAmountOccupiedHD());
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int test;
            CreetList();
            test = comboBox1.SelectedIndex;

            for (int i = 0; i < Hep.current_heap_size; i++)
            {
                Hep.heapArray[i].SetParametrs(test);
            }

            Hep.Heap();

            InFile();
            CreetTable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            CreetList();
            CreetTable();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            CreetList();
            string line = textBox1.Text;
            textBox1.Text = "";
            if (line == "") { return; }
            for (int i = 0;i < Hep.current_heap_size; i++)
            {
                if (Hep.heapArray[i].getID() == Convert.ToInt32(line))
                {
                    Hep.deleteKey(i);
                }
            }

            int test;
            test = comboBox1.SelectedIndex;
            for (int i = 0; i < Hep.current_heap_size; i++)
            {
                Hep.heapArray[i].SetParametrs(test);
            }

            Hep.Heap();
            InFile();
            CreetTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CreetList();
            if (textBox2.Text == "") { return; }
            int ID = Convert.ToInt32(textBox2.Text);
            if (textBox3.Text == "") { return; }
            float V = Convert.ToSingle(textBox3.Text);
            if (textBox4.Text == "") { return; }
            int core = Convert.ToInt32(textBox4.Text);
            if (textBox5.Text == "") { return; }
            float RAM = Convert.ToSingle(textBox5.Text);
            if (textBox6.Text == "") { return; }
            float HD = Convert.ToSingle(textBox6.Text);
            if (textBox7.Text == "") { return; }
            float Occuped = Convert.ToSingle(textBox7.Text);
            Hep.insertKey(new CComputer(ID,V,core,RAM,HD,Occuped,0.0f));
            textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = "";

            int test;

            test = comboBox1.SelectedIndex;
            for (int i = 0; i < Hep.current_heap_size; i++)
            {
                Hep.heapArray[i].SetParametrs(test);
            }

            InFile();
            CreetTable();
        }
    }
}
