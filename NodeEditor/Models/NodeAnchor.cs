using System;

namespace NodeEditor.Models;

public class NodeAnchor
{
    public string Name { get; set; }

    public Type ValueType { get; private set; }

    public NodeAnchorType ConnectionType { get; private set; }
    public NodeAnchorConnection Connection { get; private set; } = new(null, null);


    public event EventHandler<NodeAnchorConnectionEventArgs> ConnectionChanged;


    public NodeAnchor(NodeAnchorType nodeAnchorType)
    {
        ConnectionType = nodeAnchorType;
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