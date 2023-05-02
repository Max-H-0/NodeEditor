using System;

namespace NodeEditor.Models;

public class NodeAnchor
{
    public string Name { get; set; }

    public NodeAnchorType Type { get; private set; }
    public NodeAnchorConnection Connection { get; private set; }


    public event EventHandler<NodeAnchorConnectionEventArgs> ConnectionChanged;


    public NodeAnchor(NodeAnchorType nodeAnchorType)
    {
        Type = nodeAnchorType;
    }


    public void ConnectTo(NodeAnchor other)
    {
        NodeAnchorConnection newConnection = Connection;

        if(Type == NodeAnchorType.Output) 
        {
            newConnection = new(this, other);
        }        
        else if(Type == NodeAnchorType.Input) 
        {
            newConnection = new(other, this);
        }

        this.UpdateConnection(newConnection);
        other.UpdateConnection(newConnection);
    }

    public void UpdateConnection(NodeAnchorConnection connection)
    {
        NodeAnchorConnection previousConnection = Connection;
        Connection = connection;

        ConnectionChanged?.Invoke
        (
            this,
            new NodeAnchorConnectionEventArgs(previousConnection, connection)
        );
    }
}