using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class PointOfFigure
    {
        
        private Point coordinates;
        private int number;
        
        internal Point Coordinates
        {
            get
            {
                return coordinates;
            }

            set
            {
                coordinates = value;
            }
        }

        public int Number
        {
            get
            {
                return number;
            }

            set
            {
                number = value;
            }
        }

        public PointOfFigure(Point currentCoorinateCursor, int number)
        {
            coordinates = currentCoorinateCursor;
            this.number = number;
        }
    }
}
