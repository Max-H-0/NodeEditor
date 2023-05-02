using Avalonia.Interactivity;
using System;

namespace NodeEditor.Models;

public class NodeAnchorConnectionEventArgs : EventArgs
{
    public NodeAnchorConnection PreviousConnection { get; set; }
    public NodeAnchorConnection NewConnection { get; set; }


    public NodeAnchorConnectionEventArgs(NodeAnchorConnection previousConnection, NodeAnchorConnection newConnection)
    {
        PreviousConnection = previousConnection;
        NewConnection = newConnection;
    }
}
