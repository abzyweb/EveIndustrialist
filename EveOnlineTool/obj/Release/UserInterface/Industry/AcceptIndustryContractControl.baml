   M S B A M L   `   `   `  ���� K  GEveOnlineTool, Version=0.1.19.525, Culture=neutral, PublicKeyToken=null[2clr-namespace:EveOnlineTool.UserInterface.Industry$EveOnlineTool.UserInterface.Industry  [2clr-namespace:EveOnlineTool.UserInterface.Controls$EveOnlineTool.UserInterface.Controls  H    BEveOnlineTool.UserInterface.Industry.AcceptIndustryContractControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Dlocal2clr-namespace:EveOnlineTool.UserInterface.Industry     Gcontrols2clr-namespace:EveOnlineTool.UserInterface.Controls     .��� $��5q�  � DataContext  ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" >� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��4��$��Auftrag��.���  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � �� $
��Auto ��� $��* ��� $
��Auto ��� $��* �.+��� $��	Blueprint���� $��1��$��Center=�����  �� Path$ Contract.Blueprint� �� Mode$ OneWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $��2��$��Auftrags Typ��?   9EveOnlineTool.UserInterface.Controls.ContractTypeComboBox  -   	  Name$	 ContractTypeComboBox��$��3��$��Center=�{�.  
 �� SelectedItem
 �� $ Contract.OrderType�$ OneWay=�$ PropertyChanged=��� $��1��$��0��$��Preis pro Einheit���� $��1��$��1��$��Center=����� $ Contract.Price�$ OneWay=�$ PropertyChanged=� �� StringFormat$ N2���� $��1��$��2��$��Erlaube Teilaufträge���� $��1��$��3��{�.  $��Center=� d� 	IsChecked �� $ Contract.EnablePartition�$ OneWay=�$ PropertyChanged=��� $��2��$��0��$��	Anmerkung���� $��2��$��1��$��Center=����� $ Contract.Description�$ OneWay=�$ PropertyChanged=��� $��1��$��0��$��	Einheiten���� -   $��VolumeSlider��$��1��$��1��$��2��$��Center=� � Minimum$ 1Q� � Maximum �� $ Contract.Volume� � Value �� $ SelectedVolume�$ TwoWay=�$ PropertyChanged=��� -   $��VolumeTextBox��$��1��$��3��$��Center=�$��10,0,0,0q����� $ SelectedVolume�$ TwoWay=�$ PropertyChanged=�   IntToStringConverter �� 	Converter# [  $ N0��