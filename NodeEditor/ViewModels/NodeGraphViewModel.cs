using NodeEditor.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace NodeEditor.ViewModels;

internal class NodeGraphViewModel : ViewModelBase
{
    private NodeGraph NodeGraph { get; set; }

    public List<NodeViewModel> Nodes { get; set; }


    public NodeGraphViewModel()
    {
        NodeGraph = new NodeGraph
        (
            new ObservableCollection<Node>()
            {
                new Node()
                {
                    Name = "Test Node",
           
                    Inputs = new List<NodeAnchor>() { new NodeAnchor(NodeAnchorType.Input) { Name = "Input" } },
                    Outputs = new List<NodeAnchor>() { new NodeAnchor(NodeAnchorType.Output) { Name = "Output" } }
                }
            }
        );

        NodeGraph.Nodes.CollectionChanged += UpdateViewModel;
    }

    private void UpdateViewModel(object? sender, NotifyCollectionChangedEventArgs e)
    {
        
    }
}
