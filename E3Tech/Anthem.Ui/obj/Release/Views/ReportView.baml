   M S B A M L   `   `   `  ���� E  AAnathem.Ui, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Anathem.Ui.ViewsAnathem.Ui.Views  W SE3.ReactorManager.Interfaces, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null�`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces,E3.ReactorManager.Interfaces.UI.UserControls �^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces*E3.ReactorManager.Interfaces.UI.Converters !    Anathem.Ui.Views.ReportView   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0localclr-namespace:Anathem.Ui.Views     yuserControls`clr-namespace:E3.ReactorManager.Interfaces.UI.UserControls;assembly=E3.ReactorManager.Interfaces     u
converters^clr-namespace:E3.ReactorManager.Interfaces.UI.Converters;assembly=E3.ReactorManager.Interfaces     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  X TPrism.Unity.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59R NPrism.Wpf, Version=7.1.0.431, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/   !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  . .  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *GetCompletedBatchesCommand+ � 	Resources B  <E3.ReactorManager.Interfaces.UI.Converters.FontSizeConverter   FontSizeCon  CommonCheckBoxStyle �� 
TargetType �� Property �� Value  CommonTextBoxStyle  CommonButtonStyle	 �� RelativeSource
 �� Path �� 	Converter �� ConverterParameter �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers P� Property P� Value  HeaderLabelStyle��%�  &	        &	      &	 �     &	 q    7   &	       �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��
 �������  � ��$ 20(���  � ��$ 10q��� " ��.���  � ��
 �������  � �� �#1a202c��  � ��$ 20(���  � ��$	 200����  � ��$ 50����  � ��$ 0q���  � ��$
 Left=���  � ��$ Center=���  � ��$ 10,0,0,0q��� " ��.���  � ��
 �������  � �� �� 	 �� *Self+$
 ActualHeight�8   $	 0.3����  � �� �#32ccdf��  � ��$ 5q���  �@�Cursor$
 Hand����  � ��$ 0q���  � �� �� " ��.R��� $ border��#��z��#��z��#��z�� . ._��� $ contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ���  � ��
 �  ���� " ��.���  � ��$ 10,0,0,0q���  � �� �#8E94A1��  � ��$ Center=�.��� ���#1a202c � ColumnDefinitions �� $
��0.2* ��� $��3* ��� $��3* ��� $
��3.5* �.+��� $��0��$��0q����#555C6E._�� ���#1d2532$	��Top=�.+�(� .��G  AE3.ReactorManager.Interfaces.UI.UserControls.NavigateButtonType_1  -   $��2q�   ButtonCommandParameter$ 	Dashboard��   ButtonCommand �� *NavigateCommand+� $��1�����#272e3e$��5q� � RowDefinitions �� $��1* ��� $
��0.5* ��� $��1* ��� $
��0.5* ��� $
��5.5* ��� $
��1.5* �.+�� $��0��.+��� ���� " ��.���  � �� �#1a202c��  � ��$ 0,0,0,5q���  �@J�Effect -  'System.Windows.Media.Effects.BlurEffect     Radius$	 7.5Q�(� $
��Left=�.���� $��Batch Report��$��5q�
�������$��SemiBold"�(� $��1��$
��Left=�.���� $��2q�$��
Batch Name��#��[ (� $��2��$
��Left=�.���� -    ��Name$ SelectedBatchTextBox�� �� 
IsReadOnly . #��[ ���� *SelectedBatch.Name+.��   $ TextChanged��.    �� *!GetFieldDeviceParametersCommand+.��(� $��3��$
��Left=�.���� $��2q�$��Select Parameters��#��[ � $��4�� �� $��1* ��� $
��0.5* �.+��� ���� * AvailableFieldDeviceParameters+���� .R�(� $��25��$
��Left=�$��5,0,5,0q�.���� $��15,0,0,0q�
�����������    $ Checked��.    �� 	 ��  �� Mode$ FindAncestor=� �� AncestorType$ ItemsControlL�$0
 *DataContext.AddToSelectedParametersCommand�   CommandParameter ��   $ 	Unchecked��.    �� 	 �� $ FindAncestor=�$ ItemsControlL�$5
 /DataContext.RemoveFromSelectedParametersCommand� �� ���� .R��� � $��5�� �� $��1* ��� $��2* ��� $��1* � �� $��1* ��� $��1* �.+��� $��1��$��0��$	��pdf��$��5q�#��[ ���� *PrintBatchReportCommand+.��]� $��Center=�$��	10,0,10,0q�.+�F� $
��Left=�$��15��$��15��
������� �� Stretch$
 Fill=�����p ;��������p@Q ;��gffff�p@Q ;��gffff�p@Q�/��gffff�p@Q W��gffff�p@Q����gffff�p@Q����gffff�p@Q����������p@Q`&��������p@�`&����|Ѡ�����|Q����gffff�q@Q�-��gffff�q@��-����|�`S����|Q ���������p@Q ;��������p@�"��`�������`������� �Ѡ"�� �Ѡ"��`��	�� $��Export Batch PDF Report��
��������� $��1��$��1��$	��csv��$��5q�#��[ ���� *PrintBatchReportCommand+.��]� $��Center=�$��	10,0,10,0q�.+�F� $
��Left=�$��15��$��15��
�������$
 Fill=�����p ;��������p@Q ;��gffff�p@Q ;��gffff�p@Q�/��gffff�p@Q W��gffff�p@Q����gffff�p@Q����gffff�p@Q����������p@Q`&��������p@�`&����|Ѡ�����|Q����gffff�q@Q�-��gffff�q@��-����|�`S����|Q ���������p@Q ;��������p@�"��`�������`������� �Ѡ"�� �Ѡ"��`��	�� $��Export Batch CSV Report��
�������� $��2�����#272e3e$��5q� �� $��1* ��� $��1* ��� $��8* �.+�� $��0��.+��� ���� " ��.���  � �� �#1a202c��  � ��$ 0,0,0,5q���  �@J�Effect   $	 7.5Q�(� $
��Left=�.���� $��Existing Batches��$��5q����#8E94A1$��SemiBold"�]� $��1��.+��� $��Search Text :��$��5q����#8e94a1$��SemiBold"�$��20(�$��Center=�$
��Left=��� #��[ $��10q����� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=��� $��2��$
��Auto=�.���� ���� *FilteredBatches+���� .R�(� $
��Left=�$��50��$��5q�.���� ���� *Name+���� 	 �� $ FindAncestor=�$ ItemsControlL�$$
 DataContext.SelectBatchCommand����� ���� " ��.���  �@�Cursor$
 Hand����  � �� �#272e3e��  � ��
 �������  � ��$ 0q���  � ��$
 Left=���  � ��$ Center=���  � ��$ 5q���  � �� �� " ��.R��� $ border��#��z��#��z��#��z�� . ._��� $ contentPresenter��|�.  #��z��#��z����. # z #��z�� P�  �@J�IsMouseOver . . ���   �� 
TargetName$  contentPresenter�� � ��
 �  ������ .R��� � $��3��$��5q����#272e3e