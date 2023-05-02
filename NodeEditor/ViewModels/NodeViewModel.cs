using Avalonia;
using NodeEditor.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NodeEditor.ViewModels;

internal class NodeViewModel : ViewModelBase
{
    private Node _node;


    public string Name { get => _node.Name; }

    public List<NodeAnchorViewModel> Inputs { get; set; } = new();
    public List<NodeAnchorViewModel> Outputs { get; set; } = new();

    public Point Position { get; set; }


    public NodeViewModel(Node node, Point position)
    {
        _node = node;

        for(int i = 0; i < node.Inputs?.Count; i++)
        {
            Inputs.Add(new(node.Inputs[i]));
        }

        for (int i = 0; i < node.Outputs?.Count; i++)
        {
            Outputs.Add(new(node.Outputs[i]));
        }

        Position = position;
    }
}