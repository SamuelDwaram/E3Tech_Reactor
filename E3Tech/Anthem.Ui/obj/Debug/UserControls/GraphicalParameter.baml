   M S B A M L   `   `   `  ����W  SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces,E3.ReactorManager.Interfaces.UI.UserControls  5       
Anathem.Ui0   *Anathem.Ui.UserControls.GraphicalParameter   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      yuserControls`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces     5      ���#1d2532$��3q�5   M     � 	Resources  5	         HeaderLabelStyle �� 
TargetType �� Property �� Value  UnitsLabelStyle  TextBoxValueStyle��%Y  &	        &	 �     &	 �    �� 5
   
   " ��6)   .�5      ��  � ��$ 18(�6)   �� 5       � �� �	#FF8E94A16+   �� 5       � ��$ 10,0,0,0q�6(   �� 5       � ��$
 Left=�6;   �� 5       � ��$ Center=�69   5      �� 5   
   " ��6(   .�5      ��  � ��
 �����6+   �� 5       � ��$ 18(�6)   �� 5       � ��$
 Left=�6;   �� 5       � ��$ Center=�69   5      �� 5   
   " ��6*   .�5      ��  � ��$ 5q�6'   �� 5       �@�
InputScope$ Number��6+   �� 5       � ��
 �����6+   �� 5       � �� �#1d25326+   �� 5       � ��$ 0q�60   �� 5       �@��TextWrapping$
 Wrap=�6-   �� 5       � ��$ 12(�6)   �� 5       � ��$ Right=�6;   �� 5        � ��$ Center=�69   5!      5"      .��5#      �  � RowDefinitions 5$   
   �� 5%      $��1* �6   �� 5&      $��1* �6   �� 5'      $��1* �6   .+�5)   
   �� -   5         ��Name$ 
paramLabel��5)      $��0��6   #��[  61   � 5*   
   $��1��6    � ColumnDefinitions 5+      �� 5,      $��1* �6#   �� 5-      $��1* �6#   .+�5/      �� -   5         ��Name$ 
paramInput��5/   &   $��0��6   #��[ 6:   $��22(�6e   �� 50      -   5        $ 
paramUnits��50      $��1��6(   #��[ 68   $��22(�6a   51      ]� 52   
   $��2��6   $��10,0,0,0q�6!   .+�53      G   AE3.ReactorManager.Interfaces.UI.UserControls.ProgressBarAnimation  -   5          Name$ progressAnimation��53   0   $	��Top=�6K   �� 54      -   5        $ 	leftLimit��54      $
��Left=�6'   #��[ 6B   �� 55      -   5        $ 
rightLimit��55      $��Right=�6(   #��[ 6D   56      57      58      