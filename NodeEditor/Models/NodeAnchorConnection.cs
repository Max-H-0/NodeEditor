using Avalonia.Interactivity;
using System;

namespace NodeEditor.Models;

public class NodeAnchorConnection
{
    public NodeAnchor Output { get; set; }
    public NodeAnchor Input { get; set; }


    public NodeAnchorConnection(NodeAnchor output, NodeAnchor input)
    {
        Output = output;
        Input = input;
    }
}
