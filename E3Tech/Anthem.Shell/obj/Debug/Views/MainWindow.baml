   M S B A M L   `   `   `  ����W  SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces*E3.ReactorManager.Interfaces.UI.Converters  5   	    Anathem.Shell$   Anathem.Shell.Views.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   o
converters^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces  5   	   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � WindowStartupLocation$ CenterOwner=�5   	    � WindowState$ 	Maximized=�5	   	    � WindowStyle$
 None=�6!   $
��1080��5
   	   $
��1920��6   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *NavigateCommand+5       � 	Resources 5      N   HE3.ReactorManager.Interfaces.UI.Converters.ValueComparerCheckerConverter   ValueCheckCon  CommandButtonStyle	 �� 
TargetType
 �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  &	        &	        5   
   63   �� 5   
   "	 ��6+   .�5      �� 
 �@�Cursor$
 Hand��6'   �� 5      
 � �� �#32d5d46+   �� 5      
 � ��
 �����6+   �� 5      
 � ��$ 0q�60   �� 5      
 � ��$ Center=�6;   �� 5      
 � ��$ Center=�69   �� 5      
 � ��$ 3q�6(   �� 5      
 � ��$ 5q�6'   �� 5      
 � �� 5      �� 5      " ��6&   .R�5      �� $ border��6!   #��z��61   #��z��6]   #��z��6�    . ._�5      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5        5!      P� 5"       �@J�IsMouseOver . . �5#   "   �� $ contentPresenter��6K   
 � ��
 �  ��6i   5$      P� 5%       � {� .  . �5&   "   �� $ contentPresenter��6K   
 � ��
 �����6i   �� 5'   "   
 � ��
 �����6?   5(      5*      5,      5-      5.      .��5/      � ���#1a202c � RowDefinitions 50   
   �� 51      $��1* �6   �� 52      $	��20* �6   .+�54   
   �� !  Prism.Regions.RegionManager   
RegionName$ DialogService��6   ]� 55   
   $��0��6   $
��Left=�6!   .+�56      �� $
 Logo��6   57      ]� 58   
   $��0��6   $��Right=�6!   .+�59      �� $ LoggedInUserDetails��6   ��5:      �� 5;      "	 ��6   .�5<      �� 
 � y�$ Hidden=�67    �� Triggers 5=      �� 5>       �� Binding 6*   �� *
ActiveView+ �� Value  6I   .a�5?   "   �� 
 � y�$ Hidden=�6?   5@      �� 5A       6*   �� *
ActiveView+ �� 	Converter# [   �� ConverterParameter$  ��	 False6�   .a�5B   "   �� 
 � y�$ Visible=�6?   5C      5E      5G      �� 5H      �� 5I      $ LiveDateTime��6   ��5J      �� 5K      "	 ��6   .�5L      �� 
 � y�$ Hidden=�67    5M      �� 5N       6*   �� *
ActiveView+  6I   .a�5O   "   �� 
 � y�$ Hidden=�6?   5P      �� 5Q       6*   �� *
ActiveView+# [  $  ��	 False6�   .a�5R   "   �� 
 � y�$ Visible=�6?   5S      5U      5W      �� 5X       �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�5Y      (� .��5Z      �� $��5q�6   $��0q�6(    � Cursor$
 Hand��6<   $��Right=�6J   $��16(�6f   ��6t   �� *LogOutCommand+��5[      �� 5\      "	 ��6$   .�5]   "   �� 
 � y�$ Hidden=�6?    5^   "   �� 5_   &    62   �� *
ActiveView+# [  $  ��	 False6�   .a�5`   *   �� 
 � y�$ Visible=�6G   5a   '   �� 5b   &    62   �� *
ActiveView+	 Login6Q   .a�5c   *   �� 
 � y�$ Hidden=�6G   5d   '   �� 5e   &    62   �� *
ActiveView+  6Q   .a�5f   *   �� 
 � y�$ Hidden=�6G   5g   '   5i      ��5k      �� 5l      " ��6.   .R�5m   "   �� $ 
Horizontal=�6-   .+�5n   &   �� $��Log Out��6,   ���	#FF8E94A1$��16(�6U   $��Center=�6c   F� 5o   &   ���	#FF8E94A1 �� Stretch$
 Fill=�6<   $��15��6K   $��15��6V   $��Center=�6b   �������� �q� Q% �q� Q% �q`�# �q`�#43333�y@Q`�#    �z@S`�#    �z@`�#������z@ Q%������z@Q@y�������z@S@y�������z@gffff&H@������z@gffff&H@    �z@gffff&H@�����Yz@Q@������Yz@Q@�53333cz@S@�53333cz@@������dz@��������dz@Q�A?�����dz@S�A?�����dz@ �=�����dz@ �=53333cz@Q �=������y@S �=������y@ �=    �y@�A?    �y@Q���    �y@S���    �y@@�    �y@@�������y@Q@�53333�y@gffff&H@53333�y@���� �q���� �q��� �q��� �q�`F�����`F����`F����`F����Q���������y@S���������y@���������y@���������y@������F@�����z@�����D@�����z@efffffD@�����z@�����YD@@�������YD@���������YD@`��������YD@��� �n@������D@@��������F@@�������F@��������F@�������F@�������F@������ R���� R��� R��� R���� R���� R��� R��� R� ��@��� ��@�� ��@�� ��@��� ��@��� ��@�� ��@�� ���c��`F�����`F����`F����`F�����`F����	5u   '   5v   #   5w      5y      5z      (� 5{      .��5|      �� $��	Shut Down��6   #��[ 6S   ��61   �� *ClosedCommand+6S   5}      5~      5      � 5�   
   $��1��6   .+�5�      �� $ SelectedViewPane��6   5�      5�      5�      