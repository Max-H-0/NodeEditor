using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Data;
using Avalonia.LogicalTree;
using Avalonia.Markup.Xaml;
using System;
using System.Diagnostics;
using System.Linq;

namespace NodeEditor.Views.Nodes;

public partial class NodeGraphView : UserControl
{
    public NodeGraphView()
    {
        InitializeComponent();
        
        Loaded += BindContentPresenters;
    }

    void BindContentPresenters(object sender, EventArgs eventArgs)
    {
        ItemsControl itemsControl = this.GetControl<ItemsControl>("NodeContainer");
        var contentPresenters = itemsControl.GetLogicalChildren();

        Debug.WriteLine(itemsControl != null);
        Debug.WriteLine(contentPresenters.Count() != 0);

        foreach (ContentPresenter contentPresenter in contentPresenters) 
        {
            StyledElement child = (StyledElement)contentPresenter.GetLogicalChildren().First();

            contentPresenter.Bind(Canvas.LeftProperty, child.GetBindingObservable(Canvas.LeftProperty));
            contentPresenter.Bind(Canvas.TopProperty, child.GetBindingObservable(Canvas.TopProperty));
        }
    }
}