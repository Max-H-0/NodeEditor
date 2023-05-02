using Avalonia;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NodeEditor.ViewModels;

internal class NodeViewModel : ViewModelBase
{
    public List<string> Inputs { get; set; } = new() { "String", "Int", "Float" };
    public List<string> Outputs { get; set; } = new() { "Out" };

    public Point Position { get; set; } = new(0, 0);


    public NodeViewModel()
    {
    }

    public NodeViewModel(List<string> inputs, List<string> outputs, Point position)
    {
        Inputs = inputs;
        Outputs = outputs;

        Position = position;
    }
}
