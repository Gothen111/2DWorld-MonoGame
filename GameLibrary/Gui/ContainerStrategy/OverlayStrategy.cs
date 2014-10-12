using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Gui.ContainerStrategy
{
    public class OverlayStrategy : Strategy
    {
        private Container container;

        internal Container Container
        {
            get { return container; }
            set { container = value; }
        }

        public OverlayStrategy()
        {

        }

        public OverlayStrategy(Container _Container)
            : this()
        {
            this.container = _Container;
        }

        public override bool checkComponent(Component _Component)
        {
            if (container.Bounds.Left <= _Component.Bounds.Left && container.Bounds.Right >= _Component.Bounds.Right && container.Bounds.Top <= _Component.Bounds.Top && container.Bounds.Bottom >= _Component.Bounds.Bottom)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
