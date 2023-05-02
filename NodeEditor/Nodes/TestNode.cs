using NodeEditor.Models;
using System.Collections.Generic;

namespace NodeEditor.Nodes;

public class TestNode : Node
{
    public override string Name { get; set; } = "Test Node 01";

    public override List<NodeAnchor> Inputs { get => new()
    {
        new NodeAnchor
        (
            "In A",
            typeof(int),
            NodeAnchorType.Input
        ),
        
        new NodeAnchor
        (
            "In B",
            typeof(float),
            NodeAnchorType.Input
        ),
        
        new NodeAnchor
        (
            "In C",
            typeof(double),
            NodeAnchorType.Input
        ),
    };}  
    
    public override List<NodeAnchor> Outputs { get => new()
    {
        new NodeAnchor
        (
            "Out A",
            typeof(int),
            NodeAnchorType.Output
        ),
        
        new NodeAnchor
        (
            "Out B",
            typeof(float),
            NodeAnchorType.Output
        ),
    };}
}
