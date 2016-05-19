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
            grp.Width = 171;
            grp.Height = 90;
            grp.Location = point;
            grp.Name = "player_" + order.ToString();
            grp.ForeColor = Color.FromName("HotTrack");
            grp.Text = text;
            form.Controls.Add(grp);

            int spacex = 23,spacey = 29, c = 1;

            for (int i = 1; i < 7; i++)
			{
			    Label label = new Label();
                label.Text = "99";
                label.Name = grp.Name + "_" + i.ToString();
                label.Size = new Size(30, 17);
                label.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                label.ForeColor = Color.Gray;
                //MessageBox.Show(label.Name);
                label.Location = new Point(spacex, spacey);
                spacex += 53;
                if (++c == 4) { spacex = 23; spacey = 58; }
                grp.Controls.Add(label);
               
			}

           

        }

       


    }
}
