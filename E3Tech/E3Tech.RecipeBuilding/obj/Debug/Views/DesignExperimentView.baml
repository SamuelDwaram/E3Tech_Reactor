   M S B A M L   `   `   `  ����W  SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces*E3.ReactorManager.Interfaces.UI.Converters  5	      �`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces,E3.ReactorManager.Interfaces.UI.UserControls  5
       E3Tech.RecipeBuilding6   0E3Tech.RecipeBuilding.Views.DesignExperimentView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      o
converters^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces  5	      |AnimationUserControls`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces  5
      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .  (System.Windows.Interactivity.Interaction   Triggers 5   <   /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *Loaded+5       � 	Resources 5      N   HE3.ReactorManager.Interfaces.UI.Converters.ValueComparerCheckerConverter   ValueComparerConverter  CommandButtonStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName  HeaderLabelStyle  SelectionComboBoxStyle  DatePickerStyle(  "System.Windows.Controls.DatePicker��%�  &	        &	      &	 �    &	 Q    &	 �      5   
   63   �� 5   
   " ��6+   .�5      ��  �@�Cursor$
 Hand��6'   �� 5       � �� �#32d5d46+   �� 5       � ��
 �����6+   �� 5       � ��$ 0q�60   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ 5q�6(   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5      "	 ��6&   .R�5       �� $
 border��6!   #��z��61   #��z��6]   #��z��6�    . ._�5!      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5"       5#      P� 5$       �@J�IsMouseOver . . �5%   "   �� $ contentPresenter��6K    � ��
 �  ��6i   5&      P� 5'       � {� .  . �5(   "   �� $ contentPresenter��6K    � ��
 �����6i   �� 5)   "    � ��
 �����6?   5*      5,      5.      5/      �� 50   
   " ��6)   .�51      ��  � ��$ 50��6*   �� 52       � ��$ 18(�6)   �� 53       � �� �	#FF8E94A16+   �� 54       � ��$ Center=�69   55      �� 56   
   " ��6/   .�57      ��  � ��$ 5q�6'   �� 58       �@��SelectedValuePath$
 Name��62   �� 59       � ��$	 200��6)   �� 5:       � ��$ 40��6*   �� 5;       �@�Cursor$
 Hand��6'   �� 5<       � ��$ 0q�60   �� 5=       � ��$ Center=�69   �� 5>       � ��$
 Left=�64   �� 5?       � ��$	 Top=�62   5@      �� 5A   
   "  6(   .�5B      ��  � ��$ 50��6*   �� 5C       � ��$	 200��6)   �� 5D       � ��$ Center=�69   5E      5F      .��5G      � ���#272e3e � ColumnDefinitions 5H   
   �� 5I      $
��0.2* �6   �� 5J      $��9* �6   �� 5K      $	��0.2 �6   .+�5N   
   � $��0��6   ���#272e3e$	��Top=�64   .+�5O      (� $
��Left=�6   $	��Top=�61   .��5P      G   AE3.ReactorManager.Interfaces.UI.UserControls.NavigateButtonType_1  $��2q�6=      ButtonCommandParameter$ 	Dashboard��6k      ButtonCommand 6H   �� *
Navigate+6k   5Q      5R      � 5S   
   $��1��6   ���#1d2532.+�5T      �� !	  Prism.Regions.RegionManager 	  
RegionName$ RecipeBuilder��6   5U      � 5V   
   $��2��6   ���#1d2532.+�5W      �� $��Recipes View��6   $��Recipes��6,   5Y      5[      5\      