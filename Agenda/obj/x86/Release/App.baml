   M S B A M L   `   `   `  ���� A  =Agenda, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:WpfApplication1.ConvertersWpfApplication1.Converters      
Agenda.App   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     :local(clr-namespace:WpfApplication1.Converters       �� 	Resources     Btn_Options �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value �� 
TargetName  Btn_LittleWindow  Btn_Add	 �� Padding
 �� Stretch � Cursor E� 
StartPoint �� Point  
Btn_Delete1   +WpfApplication1.Converters.TabSizeConverter  tabSizeConverter `� 	Converter �� RelativeSource �� Mode �� AncestorType �� Path �� CornerRadius ��Name P� 
SourceName  ComboBoxNormalBorderBrush �� Color ! ComboBoxNormalBackgroundBrush # ComboBoxDisabledForegroundBrush # ComboBoxDisabledBackgroundBrush 	 ComboBoxDisabledBorderBrush  
 ComboBoxToggleButtonTemplate � ColumnDefinitions  ComboBoxFlatStyle �� 	ClickMode d� 	IsChecked �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate �� 
IsReadOnly �� 
IsReadOnly  � AllowsTransparency! �� IsDropDownOpen" J� SnapsToDevicePixels# � ActualWidth$ �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation%   DirectionalNavigation  GroupBoxBasic& � RowDefinitions  GroupBoxForClientSearch�� %|  &	        &	 `    &	 �    &	     &	 7    '�� <    7  &	 m    &	 �    &	 �    &	 �    &		 �    &	
 �    7  7  7  7  7  7 	 7  &	 �    7 
 7  7  &	 W    &	 g    �� " ��.���  �@�Cursor$
 Hand����  � ��
 ���� ��  � �� �#002860��  � ��$ DemiBold"���  � ��$ 30(���  � ��$
 10 0q���  � �� �� " ��.R��� $��border��$��0q�
������ #��z��._��� $
��Left=�$��Center=� P�  �@J�IsMouseOver . . ��� $ border�� � ��
 �   ��� " ��.���  �@�Cursor$
 Hand����  � �� �#002860��  � ��
 �������  � ��$ DemiBold"���  � ��$ 15(���  � ��$ Center=���  � �� �� " ��.R��� $��border��$��0q�
������ #��z��._��� $
��Left=�$��Center=�$
��10 5q� P�  �@J�IsMouseOver . . ��� $ border�� � ��
 �   ��� " ��.���  �@�Cursor$
 Hand����  � �� �#002860��  � �� �� " ��.R��� $��border��$��1q����#002860
�������$	 5q�._�F� $��AddAWork��$

 Fill=�
����������#002860$
 Hand��$��3����B� ��E� $ 7.5,0&�.���� $ 7.5,15&�E� $ 0,7.5&�.���� $ 15,7.5&��� " ��.���  �@�Cursor$
 Hand����  � �� �#002860��  � �� �� " ��.R��� $��1q�
���  ��
�������$	 5q�._�F� $

 Fill=�
�������
���  ��$
 Hand��$��3����B� ��E� $	 0,0&�.���� $ 10,10&�E� $
 0,10&�.���� $
 10,0&�  �� " ��.���  � �� `� 8   .1���  �� $ FindAncestor=�" ���� $ ActualWidth� �� $ FindAncestor=�" ����  � ��
 �������  � ��$ 20(���  � �� �� " ��.R�� $��Stretch=�$��Stretch=�$
��Auto��$��	-2,-2,0,0q�.+��� $��Border��$	 0q����#0048ac
���   �$��0q�$ 1��$
 Hand��._��� $ ContentSite��$��Center=�$��Center=�$��Header��$��	32,5,32,5q���.  P�  �@��
IsSelected . . ���  �@K�ZIndex$	 100����  � ��
 ������� $ Border�� � �� �#0048ac�� $ Border�� � �� �#5b9efa�� $ Border�� � ��$ 0,0,0,4q�P� $ Border�� �@J�IsMouseOver . . ���  �@K�ZIndex$	 100���� $ Border�� � �� �#5b9efa�� $ Border�� � �� �#2e85fd�� $ #e3e9ef���� $
 #fff���� $
 #888���� $
 #eee���� $
 #888���� " d�.R��  �� �� $��20 �.+��� $��Border��$��2��8��  $ 0��$��
1, 1, 1, 1q�8�� �� $��ButtonBorder��$��1��$��
1, 1, 1, 1q����#444$ 
0, 0, 0, 0��$��
0, 0, 0, 0q�8�� F� $��Arrow��$��1��=����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=����#444$��Center=� P�  �@J�IsMouseOver . . ��� $ ButtonBorder�� � ��
 �����P�  �@d�	IsChecked$
 TrueY�. ��� $ ButtonBorder�� � ��
 ������� $ Arrow�� � �� �	#FF8D979EP�  � {� .  . ��� $ Border��8   � ���� $ ButtonBorder��8   � ���� $ ButtonBorder��8   � ���� 8   � ���� $ Arrow�� � �� �#999�� " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� . ��  � ��
 �   ���  �@�FocusVisualStyle Z� ��  � �� �� " ��.R�� .+�d� $��ToggleButton��$��2��$ Press=�|�.  8��   �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=��� $��ContentSite��$��5, 3, 23, 3q� .  $
��Left=�$��Center=�#��z #��z #��z���� $��PART_EditableTextBox��$��3, 3, 23, 3q�# z $y�Hidden=�
������ $
��Left=�$��Center=�|�. .������ " ��.R��� $��PART_ContentHost��|�.  .��� $��Popup��$��Bottom=�|�.    . #��z! $��Slide=�.��� $��DropDown��" . #��z# #��z$ .+��� $��DropDownBorder��
�������$��
0, 1, 0, 0q�$ 0��$��1,1,1,1q�8�� �� $��4q�" . .���� $% 	Contained=� P�  �@��HasItems .  . ��� $ DropDownBorder�� � ��$ 95��P�  � {� .  . ��� 8   � ��P�  �@��
IsGrouping . . ���  � �� .  P�  �@��
IsEditable . . ���  �@ 	IsTabStop .  �� $ PART_EditableTextBox�� � y�$ Visible=��� $ ContentSite�� � y�$ Hidden=��� " ��.���  � �� �� " ��.R�� & �� $
��Auto ��� $��* �.+��� $��0��$��4q����#002860���#002860._��� 
�������$��Italic$�$
��Bold"�.���� $��0q�$��Header����. �� $��1��$��4,0,4,4q����#002860._��� $��0q��� " ��.���  � �� �� " ��.R�� & �� $
��Auto ��� $��* �.+��� $��0��$��4q����#002860���#002860._�]� $
��Auto��$��Stretch=�.+��� 
�������$��Italic$�$
��Bold"�$��Clients��$
��Left=��� $��0q�$��Header����. $��Right=��� $��1��$��4,0,4,4q����#002860._��� $��0q�