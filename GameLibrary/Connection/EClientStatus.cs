using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Connection
{
    public enum EClientStatus
    {
        Connected,
        RequestPlayerPosition,
        RequestedPlayerPosition,
        RequestWorld,
        RequestedWorld,
        RequestRegion,
        RequestedRegion,
        RequestChunk,
        RequestedChunk,
        RequestBlock,
        RequestedBlock,
        JoinWorld,
        JoinedWorld,
        InWorld,
        Disconnected
    }
}
