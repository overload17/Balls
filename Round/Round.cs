using System;
using System.Drawing;
using System.Windows.Forms;

namespace Round
{
    public class Round : PictureBox
    {
        public int dx;
        public int dy;
        public Color col;

        public Round(Point p)
        {
            Location = p;
            Width = 30;
            Height = 30;
            Random rnd = new Random();
            col = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
            dx = rnd.Next(30) - 15;
            dy = rnd.Next(-15, 15);
        }

        public Round(Point p, int dx, int dy)
        {
            Location = p;
            Width = 30;
            Height = 30;

            Random rnd = new Random();
            col = Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
            this.dx = dx;
            this.dy = dy;
        }

        public void MovePos()
        {

            Point p = Location;
            if (p.X < 0 || p.X + 15 > Parent.Width)
                dx = -dx;
            if (p.Y < 0 || p.Y + 15 > Parent.Height)
                dy = -dy;
            p.Offset(dx, dy);
            Rectangle rec = DisplayRectangle;
            rec.Location = Location;
            for (int i = 0; i < Parent.Controls.Count; i++)
            {
                Rectangle r1 = Parent.Controls[i].DisplayRectangle;
                r1.Location = Parent.Controls[i].Location;
                if (r1.IntersectsWith(rec) && r1 != rec)
                {
                    dx = -dx;
                    dy = -dy;
                    p.Offset(dx, dy);
                    Point p1 = (Parent.Controls[i] as Round).Location;
                    (Parent.Controls[i] as Round).dx = -(Parent.Controls[i] as Round).dx;
                    (Parent.Controls[i] as Round).dy = -(Parent.Controls[i] as Round).dy;
                    p1.Offset((Parent.Controls[i] as Round).dx, (Parent.Controls[i] as Round).dy);
                    (Parent.Controls[i] as Round).Location = p1;
                    break;
                }
            }
            Location = p;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            SolidBrush brush = new SolidBrush(col);
            pe.Graphics.FillEllipse(brush, 0, 0, 30, 30);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Right)
                Parent.Controls.Remove(this);

            if (((MouseEventArgs) e).Button == MouseButtons.Left)
            {
                Explose();
            }
        }

        public void Explose()
        {
            Round b1 = new Round(Location, -2, -2);
            Round b2 = new Round(Location, 2, 2);
            Round b3 = new Round(Location, -2, 2);
            Round b4 = new Round(Location, 2, -2);
            Round b5 = new Round(Location, 0, -3);
            Round b6 = new Round(Location, 0, 3);
            Round b7 = new Round(Location, -3, 0);
            Round b8 = new Round(Location, 3, 0);

            Parent.Controls.Add(b1);
            Parent.Controls.Add(b2);
            Parent.Controls.Add(b3);
            Parent.Controls.Add(b4);
            Parent.Controls.Add(b5);
            Parent.Controls.Add(b6);
            Parent.Controls.Add(b7);
            Parent.Controls.Add(b8);
            Parent.Controls.Remove(this);

        }
    }

}
