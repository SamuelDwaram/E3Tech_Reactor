   M S B A M L   `   `   `  ����W  SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces*E3.ReactorManager.Interfaces.UI.Converters  5	      �`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces,E3.ReactorManager.Interfaces.UI.UserControls  5
       E3Tech.RecipeBuilding-   'E3Tech.RecipeBuilding.Views.RecipesView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      o
converters^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces  5	      |AnimationUserControls`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces  5
      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      6  0System.Windows.Interactivity.InvokeCommandAction     Command 6$   �� *Loaded+5       � 	Resources 5      N   HE3.ReactorManager.Interfaces.UI.Converters.ValueComparerCheckerConverter   ValueComparerConverter  CommandButtonStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName  HeaderLabelStyle  SelectionComboBoxStyle  DatePickerStyle(  "System.Windows.Controls.DatePicker��%�  &	        &	      &	 �    &	 Q    &	 �      5   
   63   �� 5   
   " ��6+   .�5      ��  �@�Cursor$
 Hand��6'   �� 5       � �� �#32d5d46+   �� 5       � ��
 �����6+   �� 5       � ��$ 0q�60   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5       � ��$ 5q�6(   �� 5       � ��$ 5q�6'   �� 5       � �� 5      �� 5       "	 ��6&   .R�5!      �� $
 border��6!   #��z��61   #��z��6]   #��z��6�    . ._�5"      �� $ contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5#       5$      P� 5%       �@J�IsMouseOver . . �5&   "   �� $ contentPresenter��6K    � ��
 �  ��6i   5'      P� 5(       � {� .  . �5)   "   �� $ contentPresenter��6K    � ��
 �����6i   �� 5*   "    � ��
 �����6?   5+      5-      5/      50      �� 51   
   " ��6)   .�52      ��  � ��$ 50��6*   �� 53       � ��$ 18(�6)   �� 54       � �� �	#FF8E94A16+   �� 55       � ��$ Center=�69   56      �� 57   
   " ��6/   .�58      ��  � ��$ 5q�6'   �� 59       �@��SelectedValuePath$
 Name��62   �� 5:       � ��$	 200��6)   �� 5;       � ��$ 40��6*   �� 5<       �@�Cursor$
 Hand��6'   �� 5=       � ��$ 0q�60   �� 5>       � ��$ Center=�69   �� 5?       � ��$
 Left=�64   �� 5@       � ��$	 Top=�62   5A      �� 5B   
   "  6(   .�5C      ��  � ��$ 50��6*   �� 5D       � ��$	 200��6)   �� 5E       � ��$ Center=�69   5F      5G      .��5H      � ���#272e3e � ColumnDefinitions 5I   
   �� 5J      $
��0.2* �6   �� 5K      $��9* �6   .+�5M   
   � $��0��6   ���#272e3e$	��Top=�64   .+�5N      (� $
��Left=�6   $	��Top=�61   .��5O      G   AE3.ReactorManager.Interfaces.UI.UserControls.NavigateButtonType_1  $��2q�6=      ButtonCommandParameter$ 	Dashboard��6k      ButtonCommand 6H   �� *
Navigate+6k   5P      5Q      � 5R   
   $��1��6   ���#1d2532 5S      �� 5T      $��1* �6#   �� 5U      $��1* �6#   �� 5V      $��1* �6#   .+�5X      � ���#1a202c$��1��6(   $��Center=�68    � RowDefinitions 5Y      �� 5Z      $
��1.5* �6$   �� 5[      $��3* �6$   .+�5]      (� $��0��6   $��15q�6'   .��5^      �� $��15(�6   
�������$��SemiBold"�6=   $��No Recipes Found...��6S   5_      � 5`      $��1��6   ���#272e3e$��5q�69    5a      �� 5b      $��1* �6(   �� 5c      $��1* �6(   .+�5e      � $��0��6   $��15q�6(    5f      �� 5g      $��1* �6/   �� 5h      $��5* �6/   .+�5j      (� $��0��6"   .��5k      F� $��20��6#   $��20��6.   ���#f17e3d �� Stretch$
 Fill=�6I   �������5� 
������1��`�u������7�� �^�����I�� �^�����O��`�u�effffʂ� 
������Ђ�`� �����Ȃ��o<23333����o<�effffƁ��o<�@D4ޠo<��C��q��5� 
��<)� R�Ӏ�6��D� �D�@_�@UL�gffff^}@S�bO�43333c}@�bO�@��bO�gffffn}@S��M�gffff~}@��M� [.@UL�@�=�gffff2����T433333�� dk`�G�`G��`�G݀�`�G� ��:F� ����:F� ٣gffff:�������~@`W/������~@S`"#������~@��������~@ t� ���`����t ����O�Y�`�,S�Y�������}@���@������ӀY��@��@_��<)� R�� �0� ���  � �� ��`$� ���[�S �������4~@��!� ���d2� ��Ӏ-C� ���bO������4~@�bO��[����M�`$�@ @� �� �0� ��	5l   !   5m      (� 5n      $��1��6"   .��5o      �� $��Please Start a New Batch��6$   
�������$��15(�6Z   5p      5q      � 5r      $��2��6    5s      �� 5t      $��4* �6/   �� 5u      $��2* �6/   .+�5w      (� $��1��6"   .��5x      �� $��Start Batch��6%   $��5q�6;   $��5q�6F   
�������$��0q�6e   $��DesignExperiment��6�   ��6y   �� *
Navigate+6�   5y      5z      5{      5|      5}      � 5~   
   $��1��6   ���#1d2532��5      �� 5�      " �6    �� Triggers 5�      �� 5�       �� Binding 6&   �� *RecipesCount+ �� 	Converter# [   �� ConverterParameter$ 0�� �� Value true6�   .a�5�      ��  � y�$ Hidden=�6;   5�      5�      .+�5�      �� !	  Prism.Regions.RegionManager 	  
RegionName$ RecipeBuilder��6   5�      5�      5�      