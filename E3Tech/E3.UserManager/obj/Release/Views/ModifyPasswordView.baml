   M S B A M L   `   `   `  ���� I  EE3.UserManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:E3.UserManager.ViewsE3.UserManager.Views  W SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces*E3.ReactorManager.Interfaces.UI.Converters ?$clr-namespace:E3.UserManager.HelpersE3.UserManager.Helpers  -    'E3.UserManager.Views.ModifyPasswordView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:E3.UserManager.Views     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  o
converters^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces  2helpers$clr-namespace:E3.UserManager.Helpers  $	��150��$	��650��.  (System.Windows.Interactivity.Interaction    Triggers  /  )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *LoadWindowCommand+   CommandParameter ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType$ UserControlL� � 	Resources B  <E3.ReactorManager.Interfaces.UI.Converters.FontSizeConverter   FontSizeConverter	 �� 
TargetType
 �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName �� Path �� 	Converter��%w  &	        '��      '�� 8    7   '�� �    7   '�� �    7     �� "	 ��.��� 
 �@�Cursor$
 Hand���� 
 � ��
 ������� 
 � ��$ 0q��� 
 � ��$ Center=��� 
 � ��$ Center=��� 
 � ��$ 5q��� 
 � ��$ 5q��� 
 � �� �� " ��.R��� $ border��#��z��#��z��#��z�� . ._��� $ contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ��� $ contentPresenter��
 � ��
 �  ��P�  � {� . . ��� 
 � �� �#32d5d4P�  � {� .  . ��� $ contentPresenter��
 � ��
 ������� 
 � ��
 ������� "	 ��.��� 
 � ��$ 5q��� 
 � ��$ Center=��� 
 � �� �#1a202c�� 
 � ��$ 0q��� 
 � ��
 ������� 
 � �� ��  �� *Self+$ ActualHeight�8   �� "	 ��.��� 
 � ��$ 5q��� 
 � ��$ 5q��� 
 � ��$ Center=��� 
 � ��$ Right=��� 
 � ��
 ���� �� 
 � ��$ 0q��� 
 � ��
 ������� 
 � �� ��  �� *Self+$ ActualHeight�8   �� "	 ��.��� 
 � �� ��  �� *Self+$ ActualHeight�8   .��� ���#272e3e � RowDefinitions �� $��1* ��� $��1* ��� $��1* �.+��� $��0�� �� ContentStringFormat$ Modify User : {0}��$
��Bold"����#8e94a1$��Center=�$��Center=����� *TobeModifiedUser.Name+����  �� *Self+$ ActualHeight�# [  � $��1�� � ColumnDefinitions �� $��1* ��� $
��1.1* ��� $��2* ��� $��1* �.+��� $��1��$��Password���� -   $��UpdatedPassword��$��2��$��5q�(� $��2��$��Right=�.��]� .+��� $��Update User��$
��Left=�    $ Click��.    �� *UpdateUserCommand+ ��  �� ElementName$ UpdatedPassword��$
 Text�   �� *CloseWindowCommand+�� $��Cancel��$��Right=����� *CloseWindowCommand+