using System;

namespace NodeEditor.Models;

public class NodeAnchor
{
    public string Name { get; init; }

    public Type ValueType { get; init; }

    public NodeAnchorType ConnectionType { get; init; }
    public NodeAnchorConnection Connection { get; private set; }


    public event EventHandler<NodeAnchorConnectionEventArgs> ConnectionChanged;


    public NodeAnchor(string name, Type valueType, NodeAnchorType connectionType)
    {
        Name = name;
        ValueType = valueType;
        ConnectionType = connectionType;
        Connection = new(null, null);
    }


    public void ConnectTo(NodeAnchor other)
    {
        NodeAnchorConnection newConnection = Connection;

        if(ConnectionType == NodeAnchorType.Output) 
        {
            newConnection = new(this, other);
        }        
        else if(ConnectionType == NodeAnchorType.Input) 
        {
            newConnection = new(other, this);
        }

        this.UpdateConnection(newConnection);
        other.UpdateConnection(newConnection);
    }

    protected void UpdateConnection(NodeAnchorConnection connection)
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