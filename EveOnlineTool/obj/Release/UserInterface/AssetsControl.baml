   M S B A M L   `   `   `  ���� K  GEveOnlineTool, Version=0.1.19.525, Culture=neutral, PublicKeyToken=nullI)clr-namespace:EveOnlineTool.UserInterfaceEveOnlineTool.UserInterface  /    )EveOnlineTool.UserInterface.AssetsControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     ;local)clr-namespace:EveOnlineTool.UserInterface     .��� $��5q�  � DataContext  ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" >�.+�&   System.Windows.Controls.DataGrid  $��0��   CanUserAddRows .     AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�   SelectionUnit$ FullRow=�$
��0,10q�����  �� Path$ AssetsCollectionView�	 �� Mode$	 OneWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=� �� SelectedItem �� $ SelectedAsset�$	 TwoWay=�$
 PropertyChanged=� �� 
GroupStyle (  "System.Windows.Controls.GroupStyle     ContainerStyle ��  �� 
TargetType" ��.���  �� Property � �� �� Value ��  �� 
TargetType" ��.R�� $��5,5,5,5q� � RowDefinitions �� $
��Auto ��� $
��Auto �.+��� ���� *Name+�� $��1��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    Header$
 Name��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $
 Name�$	 OneWay=�$
 PropertyChanged=�  $
 Auto $ Anzahl�� �� $ Quantity�$	 OneWay=�$
 PropertyChanged=� �� StringFormat$ N0��  $
 Auto $ Preis�� �� $ Price�$	 OneWay=�$
 PropertyChanged=�$ N2��  $
 Auto $ 	Änderung�� �� $ PriceChange�$	 OneWay=�$
 PropertyChanged=�$ N2��  $
 Auto $ Aktueller Preis�� �� $ TradeHubPrice�$	 OneWay=�$
 PropertyChanged=�$ N2��