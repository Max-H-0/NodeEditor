using System;
using System.Collections.Generic;

namespace NodeEditor.Models;

public class Node
{
    public string Name { get; set; }

    public List<NodeAnchor> Inputs { get; set; }
    public List<NodeAnchor> Outputs { get; set; }


    public event EventHandler<NodeAnchorConnectionEventArgs> AnchorConnectionChanged;


    public Node() 
    {
        Initialize();
    }

    void Initialize()
    {
        for (int i = 0; i < Inputs?.Count; i++)
        {
            Inputs[i].ConnectionChanged += OnAnchorConnectionChanged;
        }

        for (int i = 0; i < Outputs?.Count; i++)
        {
            Outputs[i].ConnectionChanged += OnAnchorConnectionChanged;
        }
    }

    void OnAnchorConnectionChanged(object sender, NodeAnchorConnectionEventArgs eventArgs)
    {
        AnchorConnectionChanged?.Invoke(this, eventArgs);
    }
}
