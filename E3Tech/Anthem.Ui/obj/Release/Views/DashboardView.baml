   M S B A M L   `   `   `  ����   
Anathem.Ui$    Anathem.Ui.Views.DashboardView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources    FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonStyle �� 
TargetType �� 
TargetType	 ��Name
 ��Name �� Triggers P� Property P� Value �� 
TargetName  fieldDeviceTemplate��%�  &	        &	 N     &	 c     &	 x     &	 �     &	 �     &	 �     &	 �     &	 �     &		 �     &	
     7   7  7  7  7  7 	 &	 $    �� .���  � �� �� .R��� $��2q� . #��� ��$��1��$	 1 2R��� $ 	#FFDDDDDD���� $ 	#FF707070���� $ 	#FFBEE6FD���� $ 	#FF3C7FB1���� $ 	#FFC4E5F6���� $ 	#FF2C628B���� $ 	#FFF4F4F4���� $ 	#FFADB2B5���� $ 	#FF838383���� " ��.���  �@�FocusVisualStyle8   ��  � ��8  ��  � ��8  ��  � ��# � ����  � ��$ 1q���  � ��$ Center=���  � ��$ Center=���  � ��$ 1q���  � �� �� *NavigateCommand+��  � �� �� " ��.R��� $	 border��#��z��#��z��#��z�� . ._��� $
 contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@��IsDefaulted . . ��� $ border��# � �� � ��P�  �@J�IsMouseOver . . ��� $ border�� � ��
 �   ��� $ border�� � ��
 �   �P�  � �� . . ��� $ border�� � ��
 �   ��� $ border�� � ��
 �   �P�  � {� .  . ��� $ border��8   � ���� $ border��8   � ���� $ contentPresenter��8   � ���� .��� ���#1a202c � ColumnDefinitions �� $
��0.7* ��� $��* ��� $��* ��� $��* ��� $��* ��� $
��0.7* � � RowDefinitions �� $
��0.5* ��� $��* ��� $��* ��� $
��0.5* �.+��� $��1��$��1�� � Cursor$
 Hand��.+��� $��1��$��1��$��Recipes��#��� 
 $	��300��$	��300��
������ $��0q�.���� $5��//Anathem.Ui;component/Images/Recipe Control.png���� $��1��$��1��$��Recipes��#��� 
 
������ $��0q�$	��300��$��55��.���� $	��300��$��55��$��1��$��1��$��25(� �� TextAlignment$ Center=�$
��Bold"��� 
��� ���.�	Recipes�� $��1��$��2��$
 Hand��.+��� $��1��$��2��$��ReactorControl��#��� 
 $	��300��$	��300��
������ $��0q�.���� $5��//Anathem.Ui;component/Images/Parameter_Host.png���� $��1��$��2��$��ReactorControl��#��� 
 
������ $��0q�$	��300��$��55��.���� $	��300��$��55��$��1��$��2��$��25(�$ Center=�$
��Bold"��� 
��� ���.�Parameters Host�� $��1��$��3��$
 Hand��.+��� $��1��$��3��$��ProcessFlow��#��� 
 $	��300��$	��300��
������ $��0q�.���� $;��5/Anathem.Ui;component/Images/Process flow diagram.png���� $��1��$��3��$��ProcessFlow��#��� 
 
������ $��0q�$	��300��$��55��.���� $	��300��$��55��$��1��$��3��$��25(�$ Center=�$
��Bold"��� 
��� ���.�Process Diagram�� $��1��$��4��$
 Hand��.+��� $��1��$��4��$��UserManagement��#��� 
 $	��300��$	��300��
������ $��0q�.���� $+��%/Anathem.Ui;component/Images/user.png���� $��1��$��4��$��UserManagement��#��� 
 
������ $��0q�$	��300��$��55��.���� $	��300��$��55��$��1��$��4��$��25(�$ Center=�$
��Bold"��� 
��� ���.�UserManagement�� $��2��$��1��$
 Hand��.+��� $��2��$��1��$��Reports��#��� 
 $	��300��$	��300��
������ $��0q�.���� $3��-/Anathem.Ui;component/Images/reports icon.png���� $��2��$��1��$��Reports��#��� 
 
������ $��0q�$	��300��$��55��.���� $	��300��$��55��$��2��$��1��$��25(�$ Center=�$
��Bold"��� 
��� ���.�	Reports�� $��2��$��2��$
 Hand��.+��� $��2��$��2��$��Trends��#��� 
 $	��300��$	��300��
������ $��0q�.���� $2��,/Anathem.Ui;component/Images/trends-icon.png���� $��2��$��2��$��Trends��#��� 
 
������ $��0q�$	��300��$��55��.���� $	��300��$��55��$��2��$��2��$��25(�$ Center=�$
��Bold"��� 
��� ���.�Trends�� $��2��$��3��$
 Hand��.+��� $��2��$��3��$��Alarms��#��� 
 $	��300��$	��300��
������ $��0q�.���� $0��*/Anathem.Ui;component/Images/Alarmicon.png���� $��2��$��3��$��Alarms��#��� 
 
������ $��0q�$	��300��$��55��.���� $	��300��$��55��$��2��$��3��$��25(�$ Center=�$
��Bold"��� 
��� ���.�Alarms�� $��2��$��4��$
 Hand��.+��� $��2��$��4��$��
AuditTrail��#��� 
 $	��300��$	��300��
������ $��0q�.���� $,��&/Anathem.Ui;component/Images/audit.png���� $��2��$��4��$��
AuditTrail��#��� 
 
������ $��0q�$	��300��$��55��.���� $	��300��$��55��$��2��$��4��$��25(�$ Center=�$
��Bold"��� 
��� ���.�
AuditTrail]�  ��%g  '��       �� " ��.���  �@�Cursor$
 Hand����  � ��$	 Top=���  � ��$ 30,0,0,0q���  � ��$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��
 �������  � ��$ 80����  � ��$ 25(���  � �� �� *NavigateCommand+��  � ��$ 0q���  � �� �� " ��.R��� $	 border��#��z��#��z��#��z�� . ._��� $
 contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@��IsDefaulted . . ��� $ border��# � �� � ��P�  �@J�IsMouseOver . . ��� $ contentPresenter�� � ��
 �  ��