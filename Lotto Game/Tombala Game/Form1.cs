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

        const int START_X = 32, START_Y = 88, SPACE_X = 214, SPACE_Y = 122;
        int counter = 1;
        
        ArrayList numberArr = new ArrayList();
        ArrayList winArr = new ArrayList();
        bool firstWinner = true;
        Thread th;

        private void Form1_Load(object sender, EventArgs e)
        {
            Form1.CheckForIllegalCrossThreadCalls = false;
            for (int i = 1; i < 100; i++) listBox1.Items.Add(i);
            for (int i = 1; i < 100; i++) listBox2.Items.Add(i);
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
                    spacey += 122;
                    spacex = START_X;
                }

                ArrayList arr = new ArrayList();

                
                for (int k = 1; k < 7; k++)
			    {
                    int a = Convert.ToInt32(listBox1.Items[rnd.Next(1, listBox1.Items.Count)]);
                    getLabel(i, k).Text = a.ToString();
                    arr.Add(a);
                    listBox1.Items.Remove(a);
			    }

                numberArr.Add(arr);


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
                for (int k = 1; k < 7; k++)
                {
                    getLabel(i, k).Dispose();
                }

                getGroupBox(i).Dispose();
            }


            numberArr = new ArrayList();
            winArr = new ArrayList();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
           
            for (int i = 1; i < 100; i++) listBox1.Items.Add(i);
            for (int i = 1; i < 100; i++) listBox2.Items.Add(i);

            numericUpDown1.Enabled = true;
            button3.Enabled = true;
            counter = 1;
            button1.Enabled = false;
            button2.Enabled = false;
            toolStripStatusLabel1.Text = "Game Finished.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.Items.Count == 0)
            {
                button2.Enabled = false;
                return;
            }
                


            Random rnd = new Random();
            int number = Convert.ToInt32(listBox2.Items[rnd.Next(0, listBox2.Items.Count)].ToString());
            listBox2.Items.Remove(number);
            toolStripStatusLabel1.Text = number.ToString() + " picked.";
            for (int i = 1; i < counter; i++)
            {
                for (int k = 1; k < 7; k++)
                {
                    if (getLabel(i, k).Text != "√" && Convert.ToInt32(getLabel(i, k).Text) == number)
                    {
                        getLabel(i, k).Text = "√";
                        getLabel(i, k).ForeColor = Color.Green;
                        buzzWinnerLabel(getLabel(i, k));
                    }
                }
            }

            winControl(); // wincontrol() function works when any number picked up
        }


        private void winControl(){
            string s = "√√√√√√",f = "";
            
            
            for (int i = 1; i < counter; i++)
            {
                f = "";
                for (int k = 1; k < 7; k++)
                {
                    f += getLabel(i, k).Text;
                }

                if (f == s) {
                    if(!isItInTheArray(winArr,i))
                    winArr.Add(i);
                }
            }

            if(winArr.Count > 0)
            {
                if (firstWinner)
                {
                    MessageBox.Show("Player " + winArr[0].ToString() + " wins.");
                    firstWinner = false; 
                }
      
                toolStripStatusLabel1.Text += " || Player ";

                foreach (var item in winArr)
                    toolStripStatusLabel1.Text += item.ToString() + ",";
                toolStripStatusLabel1.Text = toolStripStatusLabel1.Text.Substring(0, toolStripStatusLabel1.Text.Length - 1);
                toolStripStatusLabel1.Text += " wins.";
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

    }


    

}
