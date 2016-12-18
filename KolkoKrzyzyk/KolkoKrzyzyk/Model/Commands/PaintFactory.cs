using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Model.DrawModel;

namespace TicTacToe.Model.Commands
{
    class PaintFactory
    {
        private Pen _pen;
        private CrossDesignator _crossDesignator;

        public PaintFactory(Pen pen, CrossDesignator crossDesignator)
        {
            _pen = pen;
            _crossDesignator = crossDesignator;
        }

        public IPainterCommand GetPainter(GameMark gameMark)
        {
            switch (gameMark)
            {
                case GameMark.Cross:
                    return new DrawCrossCommand(_pen, _crossDesignator);
                case GameMark.Circle:
                    return new DrawCircleCommand(_pen);
                case GameMark.Blank:
                default:
                    return new NullObjectCommand();
            }
        }
    }
}
