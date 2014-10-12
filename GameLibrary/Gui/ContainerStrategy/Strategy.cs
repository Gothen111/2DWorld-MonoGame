using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Gui.ContainerStrategy
{
    public abstract class Strategy
    {
        ///<summary>
        ///Platziert das Objekt so, dass es entsprechend der Strategy in dem Container liegt.
        ///Liefert true, falls das Objekt erfolgreich in dem Container platziert werden konnte, sonst false.s
        ///</summary>
        public abstract bool checkComponent(Component _Component);
    }
}
