using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Tombala_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const int START_X = 44, START_Y = 99, SPACE_X = 276, SPACE_Y = 201;
        int counter = 1;
        
        
        ArrayList winArr = new ArrayList();
        bool[,] zinc = new bool[6,3];
        bool firstWinner = true;
        Thread th;

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1.CheckForIllegalCrossThreadCalls = false;

            for (int i = 1; i <= 99; i++) {
                listBox1.Items.Add(i);
                listBox2.Items.Add(i);
            }

            listBox3.Items.Add("Waiting for starting game...");
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            toolStripStatusLabel1.Text = "Game started.";
            Random rnd = new Random();
            numericUpDown1.Enabled = false;
            int spacex = START_X,spacey = START_Y;

            for (int i = 1; i <= numericUpDown1.Value; i++)
            {
                
                Player p = new Player(this, new Point(spacex, spacey), "Player " + counter.ToString(), counter);
                spacex += SPACE_X;

                if (i % 3 == 0) { 
                    spacey += SPACE_Y;
                    spacex = START_X;
                }

                
     
                for (int k = 1; k <= 15; k++)
			    {
                    int a = Convert.ToInt32(listBox1.Items[rnd.Next(1, listBox1.Items.Count)].ToString());
                    getLabel(i, k).Text = a.ToString();
                    listBox1.Items.Remove(a);
			    }

                sortGroupBoxLabels(i);


                counter++;
            }

            button3.Enabled = false;
            button2.Enabled = true;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < counter; i++)
            {
                for (int k = 1; k <= 15; k++)
                {
                    getLabel(i, k).Dispose();
                }

                getGroupBox(i).Dispose();
            }


            zinc = new bool[6,3];
            winArr = new ArrayList();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            firstWinner = true;

            for (int i = 1; i <= 99; i++)
            {
                listBox1.Items.Add(i);
                listBox2.Items.Add(i);
            }

            numericUpDown1.Enabled = true;
            button3.Enabled = true;
            counter = 1;
            button1.Enabled = false;
            button2.Enabled = false;
            toolStripStatusLabel1.Text = "Game Finished.";
            toolStripStatusLabel1.Text = "Waiting for starting game...";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
            {
                button2.Enabled = false;
                toolStripStatusLabel1.Text = "Game Finished.";
                return;
            }
                


            Random rnd = new Random();
            int number = Convert.ToInt32(listBox2.Items[rnd.Next(0, listBox2.Items.Count)].ToString());
            listBox2.Items.Remove(number);
            toolStripStatusLabel1.Text = number.ToString() + " picked.";
            for (int i = 1; i < counter; i++)
            {
                for (int k = 1; k <= 15; k++)
                {
                    if (getLabel(i, k).Text != "√" && Convert.ToInt32(getLabel(i, k).Text) == number)
                    {
                        getLabel(i, k).Text = "√";
                        getLabel(i, k).Font = new Font("Segoe UI", 12, FontStyle.Bold);
                        getLabel(i, k).ForeColor = Color.Green;
                        buzzWinnerLabel(getLabel(i, k));
                    }
                }
            }

            winControl(); // wincontrol() function works when any number picked up

            zincControl(); // zinc (çinko) control
        }


        private void winControl(){
            string s = "√√√√√√√√√√√√√√√", f = "";
            
            
            for (int i = 1; i < counter; i++)
            {
                f = "";
                for (int k = 1; k <= 15; k++)
                {
                    f += getLabel(i, k).Text;
                }

                if (f == s) {
                    if (!isItInTheArray(winArr, i))
                    {
                        winArr.Add(i);
                        
                        string g = "";
                        foreach (var item in winArr)
                            g += item.ToString() + ",";
                        g = g.Substring(0, g.Length - 1);
                        toolStripStatusLabel1.Text = "Player " + g + " wins.";
                    }
                    
                }
            }

            if(winArr.Count > 0 && firstWinner)
            {
                MessageBox.Show("Player " + winArr[0].ToString() + " wins.");
                firstWinner = false; 
            }
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
            
        }

        public bool isItInTheArray(ArrayList arr, int el) {
            foreach (var item in arr)
                if (Convert.ToInt32(item) == el)
                    return true;

            return false;
            
        }

        private void buzzWinnerLabel(Label lbl){
            th = new Thread(() => buzzThread(lbl));
            th.Start();
        }

        private void buzzThread(Label lbl) {
            Point lo = lbl.Location;
            Point firstPoint = lbl.Location;
            lo.X -= 1;
            lbl.Location = lo;
            for (int i = 0; i < 20; i++)
            {
                lo.X += 2;
                lbl.Location = lo;
                Thread.Sleep(20);
                lo.X -= 2;
                lbl.Location = lo;
                Thread.Sleep(20);
            }

            lbl.Location = firstPoint;
            th.Abort();
        }


        private Label getLabel(int group,int order) {
            return (Label)this.Controls["player_" + group.ToString()].Controls["player_" + group.ToString() + "_" + order.ToString()];
        }

        private GroupBox getGroupBox(int group)
        {
            return (GroupBox)this.Controls["player_" + group.ToString()];
        }

        private void sortGroupBoxLabels(int order) {
            List<int> numberArr = new List<int>();
            for (int i = 1; i <= 15; i++)
                numberArr.Add(Convert.ToInt32(getLabel(order, i).Text));
            numberArr.Sort();
            int one = 1, six = 6, eleven = 11;
            for (int i = 0; i < 13; i+=3) {

                getLabel(order, one++).Text = numberArr[i].ToString();
                getLabel(order, six++).Text = numberArr[i+1].ToString();
                getLabel(order, eleven++).Text = numberArr[i+2].ToString();
            }
                

            
        }


        private void zincControl() {
            string s = "√√√√√", f = "";

            for (int i = 1; i < counter; i++)
            {
                if (!zinc[i-1, 0]) { 

                    f = "";

                    for (int k = 1; k <= 5; k++)
                        f += getLabel(i, k).Text;

                    if (f == s) {
                        zinc[i-1, 0] = true;
                        if(cinkoCount(i) < 3)
                            toolStripStatusLabel1.Text = i.ToString() + ". player, " + cinkoCount(i).ToString() + ". çinko.";
                        for (int k = 1; k <= 5; k++)
                            getLabel(i, k).BackColor = Color.Green;
                    }
                    
                }


                if (!zinc[i-1, 1])
                {
                    f = "";

                    for (int k = 6; k <= 10; k++)
                        f += getLabel(i, k).Text;

                    if (f == s)
                    {
                        zinc[i-1, 1] = true;
                        if (cinkoCount(i) < 3)
                            toolStripStatusLabel1.Text = i.ToString() + ". player, " + cinkoCount(i).ToString() + ". çinko.";
                        for (int k = 6; k <= 10; k++)
                            getLabel(i, k).BackColor = Color.Green;
                    }
                }



                if (!zinc[i-1, 2])
                {
                    f = "";

                    for (int k = 11; k <= 15; k++)
                        f += getLabel(i, k).Text;


                    if (f == s)
                    {
                        zinc[i-1, 2] = true;
                        if (cinkoCount(i) < 3)
                            toolStripStatusLabel1.Text = i.ToString() + ". player, " + cinkoCount(i).ToString() + ". çinko.";
                        for (int k = 11; k <= 15; k++)
                            getLabel(i, k).BackColor = Color.Green;
                    }

                }
            }

        }

        private int cinkoCount(int order){
            int count = 0;
            for (int i = 0; i < 3; i++)
			    if(zinc[order - 1,i])
                    count++;
            return count;
        }

        private void toolStripStatusLabel1_TextChanged(object sender, EventArgs e)
        {
            listBox3.Items.Insert(0, toolStripStatusLabel1.Text);
        }

    }

   

    

}
