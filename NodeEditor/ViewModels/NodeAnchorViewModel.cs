using NodeEditor.Models;
using NodeEditor.Views.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeEditor.ViewModels;

public class NodeAnchorViewModel : ViewModelBase
{
    private NodeAnchor _nodeAnchor;


    public string Name { get => _nodeAnchor.Name; }

    public Type ValueType { get => _nodeAnchor.ValueType; }

    public NodeAnchorType ConnectionType { get => _nodeAnchor.ConnectionType; }
    public NodeAnchorConnection Connection { get => _nodeAnchor.Connection; }


    public NodeAnchorViewModel(NodeAnchor nodeAnchor)
    {
        _nodeAnchor = nodeAnchor;
    }
}
