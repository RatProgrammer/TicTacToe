using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KolkoKrzyzyk.Model.DrawModel
{
    class MyPen
    {
        private Pen _pen;

        public MyPen()
        {
            _pen = new Pen(Color.Black, 3);
        }
    }
}
