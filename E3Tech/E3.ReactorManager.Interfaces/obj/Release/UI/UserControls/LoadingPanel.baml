   M S B A M L   `   `   `  ���� W  SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullk:clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls,E3.ReactorManager.Interfaces.UI.UserControls  d `PresentationFramework.Classic, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35jMclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.ClassicMicrosoft.Windows.Themes ?    9E3.ReactorManager.Interfaces.UI.UserControls.LoadingPanel   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Llocal:clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls     fclassicThemeMclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Classic       � 	Resources     bool2visibility  panelBackground �� EndPoint �� 
StartPoint � Color � Offset  basicButton �� 
TargetType �� Property �� Value5  /Microsoft.Windows.Themes.ClassicBorderDecorator   ClassicBorderBrush	 �� 
TargetType
  Name   BorderBrush   BorderThickness   BorderStyle   
Background J� SnapsToDevicePixels �� Triggers P� Property P� Value �� 
TargetName  closeButton �� BasedOn �� RelativeSource �� AncestorType �� Path �� Mode �� 	Converter  panelMessageText  panelSubMessageText��%x  &	        &	      &	 S     &	 T    7  7   &	 �    &	 r    �� �� $ 0.5,1&�$ 0.5,0&�.��� $ Black��$ 1Q�� $ 	#FF1B1B1B���� " ��.���  � ��# � ����  � ��# � ����  � ��# Z ��  � ��$ 3q���  � ��$ Center=���  � ��$ Center=���  � ��$ 0,0,1,1q���  � �� �� "	 ��.R�  $
 ContentContainer��# z��$ 0q�$
 None=�# z�� . ._��� #��z��#��z����. # z #��z�� P�  �@J�IsKeyboardFocused . . ��� $ ContentContainer�� �@ BorderStyle$ RaisedFocused=�P�  �@��IsDefaulted . . ��� $ ContentContainer�� �@ BorderStyle$ RaisedFocused=�P�  � �� . . ��� $ ContentContainer�� �@ BorderStyle$ RaisedPressed=�P�  �@d�	IsChecked$
 TrueY�. ��� $ ContentContainer�� �@ BorderStyle$ RaisedPressed=�P�  � {� .  . ��� # � �� � ���� 8   " ��.���  � ��$	 Top=���  � ��$ Verdana)���  � ��$ 9(���  � ��$
 Bold"���  � ��
 �������  �@�Cursor$
 Hand����  � ��$
 15,7q���  � �� Z� ��  � y� ��  �� *FindAncestor+" >�$ ShowCloseButton�$ OneWay=�8  �� " ��.���  � ��$
 Left=���  � ��$ 0,11,0,0q���  � ��$ WrapWithOverflow=���  � ��$
 Bold"���  � ��$ 16(���  � ��$	 Top=���  � ��$ Arial)���  �@J�SnapsToDevicePixels . �� " ��.���  � ��$
 Left=���  � ��$	 0,5q���  � ��$
 Wrap=���  � ��$ 12(���  � ��$	 Top=���  � ��$ Arial)���  �@J�SnapsToDevicePixels . .��]� #��[ y���  �� *FindAncestor+" >�$ 	IsLoading�$ OneWay=�# [  .+�F� ����    `���
ט`��	$��1��$��
0,0.25,0,0q� �� Stretch$
 Fill=�$	��Top=�
���   �F� ����    `���
ט`��	$��1��$��
0,0.25,0,0q�$
 Fill=�$	��Top=����	#FF2F2F2F�� -    ��Name$ btnClose��$��Right=�#��[  � ToolTip$ Close��$��Close X��C   =E3.ReactorManager.Interfaces.UI.UserControls.LoadingAnimation  $
��Left=�$
��Left=�$	��Top=�$��88��$��5q��� $	��Top=�$��10,27q�.+��� -    ��Name$ 
txtMessage��#��[ ���	#FFA1C3D2����  �� *FindAncestor+" >�$ Message��� -   $ txtSubMessage��#��[ ���	#FFA1C3D2����  �� *FindAncestor+" >�$ 
SubMessage�