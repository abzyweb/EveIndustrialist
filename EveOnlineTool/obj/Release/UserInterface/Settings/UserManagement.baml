   M S B A M L   `   `   `  ���� K  GEveOnlineTool, Version=0.1.19.525, Culture=neutral, PublicKeyToken=null[2clr-namespace:EveOnlineTool.UserInterface.Settings$EveOnlineTool.UserInterface.Settings  9    3EveOnlineTool.UserInterface.Settings.UserManagement   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Dlocal2clr-namespace:EveOnlineTool.UserInterface.Settings     .��� $��5q�  � DataContext  ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" >�.+��  � RowDefinitions �� �� $
��Auto �.+�&   System.Windows.Controls.DataGrid     CanUserAddRows .     AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=�   SelectionUnit$ FullRow=����� 	 �� Path$	 Users�
 �� Mode$
 OneWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedItem �� $	 SelectedUser�$
 TwoWay=�$ PropertyChanged=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $
	 Name�$
 OneWay=�$ PropertyChanged=�  $ Corporation�� �� $	 Corporation�$
 OneWay=�$ PropertyChanged=�  $ Allianz�� �� $	 Alliance�$
 OneWay=�$ PropertyChanged=�4  .System.Windows.Controls.DataGridCheckBoxColumn  $ Authentifiziert�� �� $	 Authenticated�$
 OneWay=�$ PropertyChanged=�  $ Corp. Benutzer�� �� $	 
IsCorpUser�$
 OneWay=�$ PropertyChanged=�9   3EveOnlineTool.UserInterface.Settings.EoiUserControl  $��1��   User �� $	 SelectedUser�$
 OneWay=�$ PropertyChanged=�