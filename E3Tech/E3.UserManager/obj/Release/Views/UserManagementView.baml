   M S B A M L   `   `   `  ���� I  EE3.UserManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:E3.UserManager.ViewsE3.UserManager.Views  E'clr-namespace:E3.UserManager.ConvertersE3.UserManager.Converters  ?$clr-namespace:E3.UserManager.HelpersE3.UserManager.Helpers  W SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces,E3.ReactorManager.Interfaces.UI.UserControls -    'E3.UserManager.Views.UserManagementView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:E3.UserManager.Views     X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   :
converters'clr-namespace:E3.UserManager.Converters   6	uiHelpers$clr-namespace:E3.UserManager.Helpers   ~AnimationUserControls`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *GetAllUsersCommand+   �� *GetAllRolesCommand+   �� *LoadUserDetailsCommand+ � 	Resources 1   +E3.UserManager.Converters.FontSizeConverter   FontSizeCon  CommandButtonStyle �� 
TargetType �� Property �� Value	 �� RelativeSource
 �� Path �� 	Converter �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value �� 
TargetName  HeaderLabelStyle  UserInputStyle��%�  &	        &	      7   &	 a    &	 �    7     �� " ��.���  � �� �� 	 �� *Self+$
 ActualHeight�8   ��  �@�Cursor$
 Hand����  � �� �#32d5d4��  � ��
 �������  � ��$ 0q���  � ��$ Center=���  � ��$ Center=���  � ��$ 5q���  � ��$ 5q���  � �� �� " ��.R��� $ border��#��z��#��z��#��z�� . ._��� $ contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ��� $ contentPresenter�� � ��
 �  ��P�  � {� .  . ��� $ contentPresenter�� � ��
 �������  � ��
 ������� " ��.���  � �� �	#FF8E94A1��  � ��$ Center=��� " ��.���  � �� �� 	 �� *Self+$
 ActualHeight�8   ��  � �� �#1a202c��  � ��
 �������  � ��$ 3q���  � ��$ 0q���  � ��$ Center=���  � ��$
 Left=�.��� ���#272e3e � ColumnDefinitions �� $
��0.2* ��� $��9* �.+�� $��0�����#272e3e$	��Top=�.+�(� $
��Left=�$	��Top=�.��G  AE3.ReactorManager.Interfaces.UI.UserControls.NavigateButtonType_1  $��2q�   ButtonCommandParameter$ 	Dashboard��   ButtonCommand �� *NavigateCommand+� $��1�����#1d2532 � RowDefinitions �� $
��0.5* ��� $��3* ��� $��6* �.+�(� $��0��$
��Left=�.���� $��User Management��
�������� $��1�� �� $��* ��� $
��0.2* ��� $��* ��� $
��0.2* ��� $��* �.+�� $��0�� �� $��1* ��� $��1* ��� $��1* �.+�� $��0�� �� $��1* ��� $
��1.3* �.+�(� $��0��$
��Left=�.���� $
��Name��#��[ � $��1��.+���  �� 	MaxLength$ 15��#��[ ���� *NewUser.Name+ �� UpdateSourceTrigger$ PropertyChanged=�� $��1�� �� $��1* ��� $
��1.3* �.+�(� $��0��$
��Left=�.���� $��Password��#��[ � $��1��.+�G� 
 � Tag$ PasswordHash��#��[ &    E3.UserManager.Helpers.UiHelpers   Password �� *NewCredentials+$ PropertyChanged=�� $��2�� �� $��1* ��� $��1* ��� $��1* �.+�� $��0�� �� $��1* ��� $
��1.3* �.+�(� $��0��$
��Left=�.���� $��Username��#��[ � $��1��.+��� $ 15��#��[ ���� *NewCredentials.Username+$ PropertyChanged=�� $��1�� �� $��1* ��� $
��1.3* �.+�(� $��0��$
��Left=�.���� $��Confirm Password��#��[ � $��1��.+�G� $ ConfirmPasswordText��#��[  �� *.+$ PropertyChanged=�� $��2�� �� $
��0.5* ��� $��1* ��� $
��0.5* � �� $��1* ��� $��2* ��� $��1* �.+��� $��1��$��1��$��Create New User��#��[ ���� *CreateNewUserCommand+� $��4�� �� $��1* ��� $��1* ��� $��1* �.+�� $��0�� �� $��1* ��� $
��1.3* �.+�(� $��0��$
��Left=�.���� $��Access Level��#��[ � $��1��.+��� $��0,5,25,5q� � Cursor$
 Hand�� �� DisplayMemberPath$
 Name������ *AvailableRoles+���� 	 �� *Self+$
 ActualHeight�# [     SelectionChanged �� *UpdateSelectedRoleCommand+� $��2�� �� $
��0.5* ��� $��1* ��� $
��0.5* � �� $��1* ��� $��5* ��� $��1* �.+��� $��1��$��1��$!��Configure Roles And Modules��#��[ ���� */-OpenRolesAndModulesConfigurationDialogCommand+� $��2�����#1a202c �� $��1* ��� $
��0.6* ��� $
��9.4* �.+�(� $��0��$
��Left=�.����  �� Orientation$ 
Horizontal=�$��3q� ��%K   '��       �� " ��.���  � ��
 �������  � ��$ SemiBold"�.+��� ���� *ExistingUsers.Count+�� $��Users Found��� $��1�� ��%N   '��       �� " ��.���  � �� �#8e94a1��  � ��$ 3,0,0,0q� �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* �.+�(� $��0��$
��Left=�.���� $
��Name��(� $��1��$
��Left=�.���� $��Access Level��(� $��2��$
��Left=�.���� $��CreatedDate��(� $��3��$
��Left=�.���� $��CurrentStatus��(� $��4��.���� $��Enable/Disable��(� $��5��.���� $��Modify���� $��2��$
��Auto=�.���� ���� *ExistingUsers+���� .R�� $��50�����#272e3e$��3q� �� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* ��� $��1* � ��%�   '��       7   �� " ��.���  � ��
 �������  � �� �� 	 �� *Self+$
 ActualHeight�8   ��  � ��$ 5q���  � ��$
 Left=�.+��� $��0������ *Name+�� $��1������ *Roles[0].Name+�� $��2������ *CreatedDate+�� $��3������ *CurrentStatus+�� $��4������ 	 ��   �� Mode$  FindAncestor=�! �� AncestorType$! ItemsControlL�$)
 #DataContext.ChangeUserStatusCommand����� ���� " ��" �� BasedOn#" [ # �� Triggers# �� $ �� Binding$ �� *CurrentStatus+% �� Value
% Active.a���  � ��$ Disable���� $ �� *CurrentStatus+% InActive.a���  � ��$ Enable���� $��5��$��Modify��#��[ ���� 	 �� $  FindAncestor=�$! ItemsControlL�$#
 DataContext.ModifyUserCommand����� ���� .R��� 