   M S B A M L   `   `   `  ���� K  GEveOnlineTool, Version=0.1.19.525, Culture=neutral, PublicKeyToken=null[2clr-namespace:EveOnlineTool.UserInterface.Industry$EveOnlineTool.UserInterface.Industry  [2clr-namespace:EveOnlineTool.UserInterface.Controls$EveOnlineTool.UserInterface.Controls  d `DotNetProjects.Input.Toolkit, Version=5.0.43.0, Culture=neutral, PublicKeyToken=79778c5f2eed289bgKclr-namespace:System.Windows.Controls;assembly=DotNetProjects.Input.ToolkitSystem.Windows.Controls E    ?EveOnlineTool.UserInterface.Industry.NewIndustryContractControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Dlocal2clr-namespace:EveOnlineTool.UserInterface.Industry     Gcontrols2clr-namespace:EveOnlineTool.UserInterface.Controls     a	controls1Kclr-namespace:System.Windows.Controls;assembly=DotNetProjects.Input.Toolkit     .��� $��5q�  � DataContext  ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" >� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��	Blueprint��=   7EveOnlineTool.UserInterface.Controls.BlueprintsComboBox  -     Name$ BlueprintComboBox��$��1��$��Center=� �� SelectedItem ��  �� Path$ 	Blueprint�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 PropertyChanged=��� $��Auftrags Typ��$��2��?   9EveOnlineTool.UserInterface.Controls.ContractTypeComboBox  -     Name$ ContractTypeComboBox��?   9EveOnlineTool.UserInterface.Controls.ContractTypeComboBox   SelectionChanged$ ContractTypeChanged��$��3��$��Center=� �� SelectedIndex$ 1���� $��1��$��2��$��Material wird geliefert���� -   $ ��MaterialsDeliveredCheckBox��$��1��$��3��$��Center=�{�.  �� $��1��$��0��$��Blueprint wird geliefert���� -   $ ��BlueprintDeliveredCheckBox��$��1��$��1��$��Center=�{�.  �� $��2��$��4��$!��Blueprint Preis Information��$y�Visible=�.���  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��0��$��0��$,��&Buy Order Preis / Einheit (Handelshub)��$��5q��� $��0��$��1��$��Center=����� $ Blueprint.BuyPrice�$	 OneWay=�$
 PropertyChanged=� �� StringFormat$ N2���� $��0��$��2��$-��'Sell Order Preis / Einheit (Handelshub)��$��5q��� $��0��$��3��$��Center=����� $ Blueprint.SellPrice�$	 OneWay=�$
 PropertyChanged=�$ N2���� $��1��$��0��$!��Produktionskosten / Einheit��$��5q��� $��1��$��1��$��Center=����� $ Blueprint.ExpensesPerUnit�$	 OneWay=�$
 PropertyChanged=�$ N2���� $��1��$��2��$��Empf. Preis / Einheit��$��5q��� $��1��$��3��$��Center=����� $ Blueprint.OptimalPrice�$	 OneWay=�$
 PropertyChanged=�$ N2���� $��	Einheiten��$��3��$��0���� -   $��OrderVolumeTextBox��$��3��$��1��$��Center=��� $��Preis pro Einheit��$��3��$��2���� -   $��PriceTextBox��$��3��$��3��$��Center=��� $��4��$��0��$��Erstelle SubAufträge���� $��4��$��1��$��Center=� d� 	IsChecked$ FalseY�{�.  �� $��4��$��2��$��Erlaube Teilaufträge���� -   $��EnablePartitionCheckBox��$��4��$��3��$��Center=�$
 TrueY��� $��5��$��0��$��	Lieferung���� $��5��$��1��$��Center=�{�.  �� $��5��$��2��$��Station (Lieferung)��:   4EveOnlineTool.UserInterface.Controls.StationComboBox  $��5��$��3��$��Center=�{�.  �� $��6��$��0��$��	Anmerkung���� -   $��DescriptionTextBox��$��6��$��1��$��3��$��Center=�