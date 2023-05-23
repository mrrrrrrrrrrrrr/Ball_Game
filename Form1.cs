using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace gameTRY20
{
    public partial class Form1 : Form
    {
        WindowsMediaPlayer interact;
        public Bitmap HandlerTexure = Resource1.target,
                      TargetTexure = Resource1.hunter;
        private Point _targetPosition = new Point(300,300);
        private Point _direction = Point.Empty;
        private int _score = 0;
        
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private  void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Random r = new Random();
            timer2.Interval = r.Next(25, 1000);
            _direction.X = r.Next(-2, 3);
            _direction.Y = r.Next(-2, 3);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            interact = new WindowsMediaPlayer();
            interact.URL = "sounds/success.mp3";
            interact.settings.volume = 100;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            var localPosition = this.PointToClient(Cursor.Position);
            _targetPosition.X += _direction.X *1;
            _targetPosition.Y += _direction.Y*1;
            var handlerRect = new Rectangle(localPosition.X - 50, localPosition.Y - 50, 100, 100);
            var targetRect = new Rectangle(_targetPosition.X - 50, _targetPosition.Y - 50, 100, 100);
            g.DrawImage(TargetTexure, handlerRect);
            g.DrawImage(HandlerTexure, targetRect);
            if (_targetPosition.X - 50 < 0 || _targetPosition.X + 50 > 720)
            {
                _direction.X *= -1;
            }

            if (_targetPosition.Y + 50 < 0 || _targetPosition.Y - 50 > 720)
            {
                _direction.Y *= -1;
            }

            Point between = new Point((localPosition.X - 50) - (_targetPosition.X-50), (localPosition.Y - 50) - (_targetPosition.Y-50));
            float distance = (float)Math.Sqrt((between.X * between.X) + (between.Y * between.Y));

            if((((localPosition.X - 50) - (_targetPosition.X-50)) < 10) && (((localPosition.X - 50) - (_targetPosition.X - 50)) > 0) && (((localPosition.Y - 50) - (_targetPosition.Y - 50)) < 10) && (((localPosition.Y - 50) - (_targetPosition.Y - 50)) > 0))
            {
                AddScore(1);
            }

            if((((localPosition.X - 50) - (_targetPosition.X - 50)) < 10) && (((localPosition.X - 50) - (_targetPosition.X - 50)) > 0) && (((localPosition.Y - 50) - (_targetPosition.Y - 50)) < 10) && (((localPosition.Y - 50) - (_targetPosition.Y - 50)) > 0))
            {
                Random r = new Random();
                _targetPosition.X = r.Next(50, 1200);
                 _targetPosition.Y= r.Next(50, 700);
                
                interact.controls.play();
            }

            int a = 10;
            if (_score / a==1)
            {
                int i = 2;
                _targetPosition.X += _direction.X * i;
                _targetPosition.Y += _direction.Y * i;
                i += 1;
                a += 10;
            }

         

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            pause pause = new pause();
            pause.timer1 = this.timer1;
            pause.ShowDialog();
            
            
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Escape)
            {
                button1_Click(new object(), new EventArgs());
            }
                
        }
                
        
    

        private void AddScore(int score)
        {
            _score += score;
            scoreLabel.Text = _score.ToString();
        }

    }
}
