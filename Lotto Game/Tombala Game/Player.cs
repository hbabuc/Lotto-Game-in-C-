using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tombala_Game
{
    class Player
    {
        private Form form;

        public Player(Form _form,Point point,string text,int order) {
            form = _form;

            GroupBox grp = new GroupBox();
            grp.Width = 240;
            grp.Height = 159;
            grp.Location = point;
            grp.Name = "player_" + order.ToString();
            grp.ForeColor = Color.FromName("HotTrack");
            grp.Text = text;
            form.Controls.Add(grp);

            int spacex = 28,spacey = 37, c = 1;

            for (int i = 1; i < 16; i++)
			{
			    Label label = new Label();
                label.Text = "90";
                label.Name = grp.Name + "_" + i.ToString();
                label.Size = new Size(30, 25);
                label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                label.ForeColor = Color.FromName("HotTrack");
                label.BorderStyle = BorderStyle.FixedSingle;
                label.TextAlign = ContentAlignment.MiddleCenter;
                label.Location = new Point(spacex, spacey);
                spacex += 39;
                if (c++ % 5 == 0) { spacex = 28; spacey += 38; }
                grp.Controls.Add(label);
               
			}

           

        }

       


    }
}
