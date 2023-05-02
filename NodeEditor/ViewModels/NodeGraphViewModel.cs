using Avalonia;
using NodeEditor.Models;
using NodeEditor.Nodes;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

namespace NodeEditor.ViewModels;

internal class NodeGraphViewModel : ViewModelBase
{
    private NodeGraph _nodeGraph;


    public List<NodeViewModel> Nodes { get; set; } = new();


    public NodeGraphViewModel()
    {
        _nodeGraph = new NodeGraph();

        _nodeGraph.Nodes.CollectionChanged += UpdateViewModel;

        _nodeGraph.Nodes.Add
        (
            new TestNode()
        );
    }

    private void UpdateViewModel(object? sender, NotifyCollectionChangedEventArgs eventArgs)
    {
        if(eventArgs.Action == NotifyCollectionChangedAction.Add)
        {
            for(int i = 0; i < eventArgs.NewItems?.Count; i++)
            {
                Node node = (Node)eventArgs.NewItems[i];
                Nodes.Add(new NodeViewModel(node, new Point(0, 0)));
            }
        }

        Debug.WriteLine(Nodes.Count);
    }
}
