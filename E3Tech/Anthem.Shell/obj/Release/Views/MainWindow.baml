   M S B A M L   `   `   `  ���� W  SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces*E3.ReactorManager.Interfaces.UI.Converters   Anathem.Shell$   Anathem.Shell.Views.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  o
converters^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � WindowStartupLocation$ CenterOwner=� � WindowState$ 	Maximized=� � WindowStyle$
 None=�$
��1080��$
��1920��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *NavigateCommand+ � 	Resources N   HE3.ReactorManager.Interfaces.UI.Converters.ValueComparerCheckerConverter   ValueCheckCon  CommandButtonStyle	 �� 
TargetType
 �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName��%5  &	        &	        �� "	 ��.��� 
 �@�Cursor$
 Hand���� 
 � �� �#32d5d4�� 
 � ��
 ������� 
 � ��$ 0q��� 
 � ��$ Center=��� 
 � ��$ Center=��� 
 � ��$ 3q��� 
 � ��$ 5q��� 
 � �� �� " ��.R��� $ border��#��z��#��z��#��z�� . ._��� $ contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ��� $ contentPresenter��
 � ��
 �  ��P�  � {� .  . ��� $ contentPresenter��
 � ��
 ������� 
 � ��
 �����.��� ���#1a202c � RowDefinitions �� $��1* ��� $	��20* �.+��� !  Prism.Regions.RegionManager   
RegionName$ DialogService��]� $��0��$
��Left=�.+��� $
 Logo��]� $��0��$��Right=�.+��� $ LoggedInUserDetails������ "	 ��.��� 
 � y�$ Hidden=� �� Triggers ��  �� Binding �� *
ActiveView+ �� Value  .a��� 
 � y�$ Hidden=���  �� *
ActiveView+ �� 	Converter# [   �� ConverterParameter$  ��	 False.a��� 
 � y�$ Visible=��� �� $ LiveDateTime������ "	 ��.��� 
 � y�$ Hidden=� ��  �� *
ActiveView+  .a��� 
 � y�$ Hidden=���  �� *
ActiveView+# [  $  ��	 False.a��� 
 � y�$ Visible=���  �� Orientation$ 
Horizontal=�$��Right=�.+�(� .���� $��5q�$��0q� � Cursor$
 Hand��$��Right=�$��16(����� *LogOutCommand+���� "	 ��.��� 
 � y�$ Hidden=� ��  �� *
ActiveView+# [  $  ��	 False.a��� 
 � y�$ Visible=���  �� *
ActiveView+	 Login.a��� 
 � y�$ Hidden=���  �� *
ActiveView+  .a��� 
 � y�$ Hidden=����� " ��.R��� $ 
Horizontal=�.+��� $��Log Out�����	#FF8E94A1$��16(�$��Center=�F� ���	#FF8E94A1 �� Stretch$
 Fill=�$��15��$��15��$��Center=��������� �q� Q% �q� Q% �q`�# �q`�#43333�y@Q`�#    �z@S`�#    �z@`�#������z@ Q%������z@Q@y�������z@S@y�������z@gffff&H@������z@gffff&H@    �z@gffff&H@�����Yz@Q@������Yz@Q@�53333cz@S@�53333cz@@������dz@��������dz@Q�A?�����dz@S�A?�����dz@ �=�����dz@ �=53333cz@Q �=������y@S �=������y@ �=    �y@�A?    �y@Q���    �y@S���    �y@@�    �y@@�������y@Q@�53333�y@gffff&H@53333�y@���� �q���� �q��� �q��� �q�`F�����`F����`F����`F����Q���������y@S���������y@���������y@���������y@������F@�����z@�����D@�����z@efffffD@�����z@�����YD@@�������YD@���������YD@`��������YD@��� �n@������D@@��������F@@�������F@��������F@�������F@�������F@������ R���� R��� R��� R���� R���� R��� R��� R� ��@��� ��@�� ��@�� ��@��� ��@��� ��@�� ��@�� ���c��`F�����`F����`F����`F�����`F����	(� .���� $��	Shut Down��#��[ ���� *ClosedCommand+� $��1��.+��� $ SelectedViewPane��