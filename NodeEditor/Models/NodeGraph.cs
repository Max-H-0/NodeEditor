using NodeEditor.Reusable;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace NodeEditor.Models;

public class NodeGraph
{
    public ObservableCollection<Node> Nodes { get; set; } = new();

    public MultiKeyDictionary<NodeAnchor, NodeAnchorConnection> Connections { get; set; }


    public NodeGraph() 
    { 
        Initialize();
    }


    void Initialize()
    {
        Connections = new();

        for (int i = 0; i < Nodes.Count; i++)
        {
            SubscribeToEvents(Nodes[i]);
        }
    }
    

    void UpdateConnectionList(object sender, NodeAnchorConnectionEventArgs eventArgs)
    {
        if(eventArgs.PreviousConnection != null)
        {
            Connections.Remove(eventArgs.PreviousConnection);
        }

        List<NodeAnchor> keys = new();
        if (eventArgs.NewConnection.Output != null) keys.Add(eventArgs.NewConnection.Output);
        if (eventArgs.NewConnection.Input != null) keys.Add(eventArgs.NewConnection.Input);

        if(keys.Count != 0)
        {
            Connections.Add(keys, eventArgs.NewConnection);
        }
    }
    

    void SubscribeToEvents(Node node)
    {
        node.AnchorConnectionChanged += UpdateConnectionList;
    }
}
