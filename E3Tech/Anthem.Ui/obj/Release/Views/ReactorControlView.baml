   M S B A M L   `   `   `  ���� W  SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces,E3.ReactorManager.Interfaces.UI.UserControls  E AAnathem.Ui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Anathem.Ui.UserControlsAnathem.Ui.UserControls �^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces*E3.ReactorManager.Interfaces.UI.Converters  7 clr-namespace:Anathem.Ui.HelpersAnathem.Ui.Helpers �`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces,E3.ReactorManager.Interfaces.UI.UserControls  )   #Anathem.Ui.Views.ReactorControlView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     quc_1`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces     6uc_2%clr-namespace:Anathem.Ui.UserControls     u
converters^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces     6	uiHelpers clr-namespace:Anathem.Ui.Helpers     �AnimationUserControls`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces     X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   ���#1a202c!  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  .  � 	Resources B   <E3.ReactorManager.Interfaces.UI.Converters.FontSizeConverter   FontSizeCon4  .Anathem.Ui.Helpers.ParameterExtractorConverter  ParameterExtractorCon �� 
TargetType �� Property �� Value �� Path �� RelativeSource �� 	ConverterG   AE3.ReactorManager.Interfaces.UI.UserControls.ButtonOnOffAnimation  ActionButtonStyle �� 
TargetType	 ��Name
 J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName  UnitsLabelStyle  TextBoxValueStyle  HeaderLabelStyle��%�  &	        &	      '�� 
     7   '  s     &	 �     &	 �    &	 �    &	 �        �� " ��.���  � �� �#8e94a1��  � �� �� $ ActualHeight� �� *Self+8   �� "  .���  � ��$ 60����  � ��$ 60����  �@�Cursor$
 Hand���� " ��.���  �@�Cursor$
 Hand����  � �� �#32d5d4��  � ��
 �������  � ��$ 0q���  � ��$ Center=���  � ��$ Center=���  � ��$ 3q���  � ��$ 3q���  � �� �� " ��.R��� $	 border��#��z��#��z��#��z��
 . ._��� $ contentPresenter��|�.  #��z��#��z����. #
 z
 #��z�� P�  �@J�IsMouseOver . . ��� $ contentPresenter�� � ��
 �  ���� " ��.���  � ��
 �������  � ��$ 16(���  � ��$
 Left=���  � ��$ Center=��� " ��.���  � ��$ 50����  �@�
InputScope$ Number����  � ��$ 25(���  � ��
 �������  � �� �	#FF272E3E��  � ��$ 0q���  � ��$
 Left=���  � ��$ Center=��� " ��.���  � ��$ 60����  � ��$ 18(���  � �� �	#FF8E94A1��  � ��$ Center=�.��� ���#1d2532 � ColumnDefinitions �� $
��0.2* ��� $��7* ��� $
��2.5* �.+�� $��0�����#555c6e$
��1080��$��Bottom=�G   AE3.ReactorManager.Interfaces.UI.UserControls.NavigateButtonType_1  $	��Top=�$��0��   ButtonCommandParameter$ 	Dashboard��   ButtonCommand �� *NavigateCommand+�� !  Prism.Regions.RegionManager   
RegionName$ ParametersHost��$��1��� $��2�����#272e3e � RowDefinitions �� $	��45* ��� $
��100* ��� $	��19* ��� $	��45* ��� $
��180* ��� $
��180* ��� $
��180* ��� $
��180* ��� $
��180* ��� $
��180* � �� $��1* ��� $��1* ��� $��1* �.+��� $��0��$��0��$	��MVA�����#1d2532$��Center=��� $��0��$��1��$��RV-50L�����#1d2532$��Center=��� $��0��$��2��$��RV-25L�����#1d2532$��Center=��� $��1��$��0��$��2��._�(� $
��Left=�.����  �� Orientation$ 
Horizontal=�$��20,0,0,0q�.+���  � Cursor$
 Hand������ " ��.R�  
 � Tag$ StirrerCommand_1��"  Anathem.Ui.Helpers.UiHelpers   CommandToDevice �� *SendCommandToDevice+�� $��	Stirrer 1��#��[ �� $��1��$��1��$��2��._�(� $
��Left=�.���� $ 
Horizontal=�$��20,0,0,0q�.+��� $
 Hand������ " ��.R�  $ StirrerCommand_2�� �� *SendCommandToDevice+�� $��	Stirrer 2��#��[ �� $��1��$��2��$��2��._�(� $
��Left=�.���� $ 
Horizontal=�$��20,0,0,0q�.+��� $
 Hand������ " ��.R�  $ StirrerCommand_3�� �� *SendCommandToDevice+�� $��	Stirrer 3��#��[ �� $��Center=�$��9��$��0��._�(� $
��Left=�.���� $ Vertical=�$��	20,20,0,0q�.+��� $
 Hand������ " ��.R�  $ StirrerCommand_3�� �� *SendCommandToDevice+�� $��Chiller��#��[ �� $��3��$��3��$��Reactor Parameters��$��Center=����#1d25320  *Anathem.Ui.UserControls.GraphicalParameter  $��4��$��0��$ Temperature_1��   
ParamLabel$ MVA Mass
Temperature��   Units$	 °C��   Limits$ -90|200��  $��4��$��1��$ Temperature_2��$ Reactor Mass
Temperature��$	 °C��$ -90|200��  $��4��$��2��$ Temperature_3��$ Spare RV Mass
Temperature��$	 °C��$ -90|200��  $��5��$��0��$ 
Pressure_1��$ MVA
Pressure��$
 mmHg��$ 0|760��  $��5��$��1��$ 
Pressure_2��$ Reactor Mass
Pressure��$
 mmHg��$ 0|760��  $��5��$��2��$ 
Pressure_3��$ Spare RV Mass
Pressure��$
 mmHg��$ 0|760��  $��6��$��0��$ ReactorLevel_1��$ 	MVA
Level��$	 Lts��$
 0|25��  $��6��$��1��$ ReactorLevel_2��$ Reactor Vessel
Level��$	 Lts��$
 0|50��  $��6��$��2��$ ReactorLevel_3��$ Spare RV Vessel
Level��$	 Lts��$
 0|50��  $��7��$��0��$ StirrerSetpoint_1��   
IsEditable .    ParamDataType$ double��$ MVA Stirrer
Setpoint��$	 RPM��$ 0|200��   Command �� *SendCommandToDevice+  $��7��$��1��$ StirrerSetpoint_2�� . $ double��$ RV Stirrer
Setpoint��$	 RPM��$ 0|200�� �� *SendCommandToDevice+  $��7��$��2��$ StirrerSetpoint_3�� . $ double��$ Spare RV Stirrer
Setpoint��$	 RPM��$ 0|200�� �� *SendCommandToDevice+  $��9��$��1��$ ChillerLevel_1��$ Chiller
Temperature SP��$	 °C��$
 0|50��  $��9��$��2��$ ReactorLevel_1��$ Chiller
Temperature��$	 °C��$
 0|50��(� $��8��$��0�� (� Stretch$
 Fill=�.���� ���	#FF1E2432$��2q�._�]� .+��� $��MVA Stirrer
Feedback��$	��Top=�#��[ I	   CE3.ReactorManager.Interfaces.UI.UserControls.CircularGuageAnimation	  $��	20,0,20,0q�$��Bottom=�  	  
UnitsValue$	  rpm��! 	  MaximumValue$	! 200��" 	  CurrentValue" �� *StirrerCurrentSpeed_1+(� $��8��$��1��$
 Fill=�.���� ���	#FF1E2432$��2q�._�]� .+��� $��RV Stirrer
Feedback��$	��Top=�#��[ 	  $��	20,0,20,0q�$��Bottom=�$	  rpm��$	! 200��" �� *StirrerCurrentSpeed+(� $��8��$��2��$
 Fill=�.���� ���	#FF1E2432$��2q�._�]� .+��� $��Spare RV Stirrer
Feedback��$	��Top=�#��[ 	  $��	20,0,20,0q�$��Bottom=�$	  rpm��$	! 200��" �� *StirrerCurrentSpeed_2+