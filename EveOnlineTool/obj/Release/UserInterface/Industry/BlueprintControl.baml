   M S B A M L   `   `   `  ���� K  GEveOnlineTool, Version=0.1.19.525, Culture=neutral, PublicKeyToken=null[2clr-namespace:EveOnlineTool.UserInterface.Industry$EveOnlineTool.UserInterface.Industry  ;    5EveOnlineTool.UserInterface.Industry.BlueprintControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Dlocal2clr-namespace:EveOnlineTool.UserInterface.Industry     .��� $��5q�  � DataContext  ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" >�.+��� $��1��$��Blueprint Details��.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $
��Name���� $��1��$��Center=�����  �� Path$ 	Blueprint� �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��1��$��0��$��Besitz���� $��1��$��1��$��Center=�{��� *EnableManualChanges+	 d� 	IsChecked	 �� $ Blueprint.Owned�$ TwoWay=�$ PropertyChanged=��� $��1��$��2��$��Privat (Corp.)���� $��1��$��3��$��Center=�	 �� $ Blueprint.Private�$ TwoWay=�$ PropertyChanged=��� $��2��$��Material Effizienz���� $��2��$��1��$��Center=�{��� *EnableManualChanges+���� $! Blueprint.MaterialEfficency�$ TwoWay=�$ PropertyChanged=�   IntToStringConverter
 �� 	Converter#
 [  �� $��2��$��2��$��Zeit Effizienz���� $��2��$��3��$��Center=�{��� *EnableManualChanges+���� $ Blueprint.TimeEfficency�$ TwoWay=�$ PropertyChanged=�#
 [  9�  9� ExpandDirection$
 Down=�$��3��$��4�� 9� 
IsExpanded .  $ ��Zusätzliche Informationen��$��0,5,0,0q�.��� $��10q� �� $
��Auto ��� $
��Auto �.+��� $)��#Kosten Aufschlüsselung / Durchlauf��$��0��.���  �� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��0��$��Produktionskosten (Basis)���� $��1��$��Center=����� $' !Blueprint.ProductionBaseTaxPerRun�$ OneWay=�$ PropertyChanged=� �� StringFormat$ N2���� $��2��$,��&Produktionskosten (Corporation Steuer)���� $��3��$��Center=����� $. (Blueprint.ProductionCorporationTaxPerRun�$ OneWay=�$ PropertyChanged=�$ N2���� $��1��$��Material Kosten:���� $��1��$��1��$��Center=����� $&  Blueprint.MaterialExpensesPerRun�$ OneWay=�$ PropertyChanged=�$ N2��&   System.Windows.Controls.DataGrid  $��2��$��4��   CanUserAddRows .     
IsReadOnly .    AutoGenerateColumns .     GridLinesVisibility$ 
Horizontal=����� $ Blueprint.Materials�$ OneWay=�$ PropertyChanged=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Bezeichnung��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� $
 Name�$ OneWay=�$ PropertyChanged=�  $ Anzahl�� �� $ Quantity�$ OneWay=�$ PropertyChanged=�  $ Preis / Einheit�� �� $ PricePerUnit�$ OneWay=�$ PropertyChanged=�$ N2��  $ Preis�� �� $ Price�$ OneWay=�$ PropertyChanged=�$ N2��4  .System.Windows.Controls.DataGridCheckBoxColumn  $ 
Produziert�� �� $ Produced�$ OneWay=�$ PropertyChanged=��� $,��&Einnahmen Aufschlüsselung / Durchlauf��$��1��.���  �� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��0��$��Verkaufs Preis���� $��1��$��Center=����� $  Blueprint.SalesPricePerRun�$ OneWay=�$ PropertyChanged=�$ N2���� $��2��$��Verkaufs Steuer���� $��3��$��Center=����� $  Blueprint.SalesTaxesPerRun�$ OneWay=�$ PropertyChanged=�$ N2���� $��1��$��0��$��Broker Gebühren���� $��1��$��1��$��Center=����� $  Blueprint.BrokerFeesPerRun�$ OneWay=�$ PropertyChanged=�$ N2��