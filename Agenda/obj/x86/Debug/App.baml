   M S B A M L   `   `   `  ����A  =Agenda, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:WpfApplication1.ConvertersWpfApplication1.Converters  5          
Agenda.App   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      :local(clr-namespace:WpfApplication1.Converters     5        �� 	Resources  5         Btn_Options �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value �� 
TargetName  Btn_LittleWindow  Btn_Add	 �� Padding
 �� Stretch � Cursor E� 
StartPoint �� Point  
Btn_Delete1   +WpfApplication1.Converters.TabSizeConverter  tabSizeConverter `� 	Converter �� RelativeSource �� Mode �� AncestorType �� Path �� CornerRadius ��Name P� 
SourceName  ComboBoxNormalBorderBrush �� Color ! ComboBoxNormalBackgroundBrush # ComboBoxDisabledForegroundBrush # ComboBoxDisabledBackgroundBrush 	 ComboBoxDisabledBorderBrush  
 ComboBoxToggleButtonTemplate � ColumnDefinitions  ComboBoxFlatStyle �� 	ClickMode d� 	IsChecked �� Mode J� IsHitTestVisible �� SelectionBoxItem �� SelectionBoxItemTemplate �� 
IsReadOnly �� 
IsReadOnly  � AllowsTransparency! �� IsDropDownOpen" J� SnapsToDevicePixels# � ActualWidth$ �� MaxDropDownHeight-  'System.Windows.Input.KeyboardNavigation%   DirectionalNavigation  GroupBoxBasic& � RowDefinitions  GroupBoxForClientSearch�� 5   
   %!  &	        &	 g    &	 �    &	 O    &	 �	    '�� �	    7  &	 �    &	 �    &	     &	 .    &		 L    &	
 j    7  7  7  7  7  7 	 7  &	 �    7 
 7  7  &	 �    &	 �    �� 5      " ��6(   .�5      ��  �@�Cursor$
 Hand��6+   �� 5       � ��
 ���� 6/   �� 5       � �� �#0028606/   �� 5       � ��$ DemiBold"�6/   �� 5       � ��$ 30(�6-   �� 5       � ��$
 10 0q�6+   �� 5       � �� 5      �� 5      " ��6*   .R�5      �� $��border��6%   $��0q�5      
������ #��z��5      ._�5   "   �� $
��Left=�63   $��Center=�6N   5       5      P� 5   "    �@J�IsMouseOver . . �5   &   �� $ border��6-    � ��
 �   �6X   5   #   5!      5#      5$      �� 5&      " ��6-   .�5'      ��  �@�Cursor$
 Hand��6+   �� 5(       � �� �#0028606/   �� 5)       � ��
 �����6/   �� 5*       � ��$ DemiBold"�6/   �� 5+       � ��$ 15(�6-   �� 5,       � ��$ Center=�6?   �� 5-       � �� 5.      �� 5/      " ��6*   .R�50      �� $��border��6%   $��0q�51      
������ #��z��53      ._�54   "   �� $
��Left=�63   $��Center=�6N   $
��10 5q�6i   55       56      P� 57   "    �@J�IsMouseOver . . �58   &   �� $ border��6-    � ��
 �   �6X   59   #   5;      5=      5>      �� 5@      " ��6$   .�5A      ��  �@�Cursor$
 Hand��6+   �� 5B       � �� �#0028606/   �� 5C       � �� 5D      �� 5E      " ��6*   .R�5F      �� $��border��6%   $��1q�5G   %   ���#002860
�������$	 5q�5J   %   ._�5K   "   F� $��AddAWork��6)   $

 Fill=�5L   )   
����������#002860$
 Hand��5O   )   $��3��5P   )   ��5Q   &   B� 5R   *   ��5T   2   E� $ 7.5,0&�6=   .��5U   6   �� $ 7.5,15&�6B   5V   3   E� 5W   2   $ 0,7.5&�6=   .��5X   6   �� $ 15,7.5&�6B   5Y   3   5[   +   5^   #   5`      5a      5c      5d      �� 5f      " ��6'   .�5g      ��  �@�Cursor$
 Hand��6+   �� 5h       � �� �#0028606/   �� 5i       � �� 5j      �� 5k      " ��6*   .R�5l      �� $��1q�6%   
���  ��
�������$	 5q�5o   %   ._�5p   "   F� $

 Fill=�6)   
�������
���  ��$
 Hand��5s   )   $��3��5t   )   ��5u   &   B� 5v   *   ��5x   2   E� $	 0,0&�6=   .��5y   6   �� $ 10,10&�6B   5z   3   E� 5{   2   $
 0,10&�6=   .��5|   6   �� $
 10,0&�6B   5}   3   5   +   5�   #   5�      5�      5�      5�        5�      6%   �� 5�      " ��6   .�5�      ��  � �� 5�      `� 5�      8   .1�5�      ��  6&   �� $ FindAncestor=�" ���� 5�      $ ActualWidth�5�   0    5�   &   �� $ FindAncestor=�" ��5�   0   5�      5�      �� 5�       � ��
 �����6/   �� 5�       � ��$ 20(�6-   �� 5�       � �� 5�      �� 5�      " ��6*   .R�5�      � $��Stretch=�6#   $��Stretch=�6A   $
��Auto��5�      $��	-2,-2,0,0q�6*   .+�5�   "   �� $��Border��6)   $	 0q�67   ���#0048ac
���   �$��0q�5�   +   $ 1��6?   $
 Hand��6P   ._�5�   &   �� $ ContentSite��67   $��Center=�6L   $��Center=�5�   #   $��Header��5�   #   $��	22,5,22,5q�6:   ��. 5�   '   5�   #   5�       5�      P� 5�   "    �@��
IsSelected . . �5�   &   ��  �@K�ZIndex$	 100��6E   �� 5�   &    � ��
 �����6C   �� 5�   &   $ Border��6-    � �� �#0048ac6W   �� 5�   &   $ Border��6-    � �� �#5b9efa6X   �� 5�   &   $ Border��6-    � ��$ 0,0,0,4q�6\   5�   #   P� 5�   "   $ Border��6*    �@J�IsMouseOver . . �5�   &   ��  �@K�ZIndex$	 100��6E   �� 5�   &   $ Border��6-    � �� �#5b9efa6W   �� 5�   &   $ Border��6-    � �� �#2e85fd6X   5�   #   5�      5�      5�      �� 5�      $ #e3e9ef��6@   �� 5�      $
 #fff��6D   �� 5�      $
 #888��6F   �� 5�      $
 #eee��6F   �� 5�      $
 #888��6B   �� 5�      " d�6   .R�5�      �  5�      �� 5�      �� 5�      $��20 �6+   .+�5�      �� $��Border��61   $��2��6   8��  $ 0��5�      $��
1, 1, 1, 1q�6    8�� 5�      �� 5�      $��ButtonBorder��6T   $��1��6   $��
1, 1, 1, 1q�6-   ���#444$ 
0, 0, 0, 0��5�      $��
0, 0, 0, 0q�6)   8�� 5�      F� 5�      $��Arrow��6   $��1��6(   =����        �    �� � 	= ��[ � z �� � z     � 	=  	= 	$��Center=�5�      ���#444$��Center=�5�      5�       5�      P� 5�       �@J�IsMouseOver . . �5�      �� $ ButtonBorder��6=    � ��
 �����6W   5�      P� 5�       �@d�	IsChecked$
 TrueY�6@   . �5�      �� $ ButtonBorder��6=    � ��
 �����6W   �� 5�      $ Arrow��67    � �� �	#FF8D979E6J   5�      P� 5�       � {� .  . �5�      �� $ Border��6=   8   � ��6Q   �� 5�      $ ButtonBorder��6=   8   � ��6W   �� 5�      $ ButtonBorder��6?   8   � ��6Y   �� 5�      8   � ��6C   �� 5�      $ Arrow��67    � �� �#9996J   5�      5�      �� 5�      " ��6/   .�5�      ��  �@J�SnapsToDevicePixels . 6B   �� 5�       �@�OverridesDefaultStyle . 6K   �� 5�       � ��$
 Auto=�6O   �� 5�       � ��$
 Auto=�6M   �� 5�       � �� . 6B   �� 5�       � ��
 �   �6;   �� 5�       �@�FocusVisualStyle 6F   Z� �� 5�       � �� 5�      �� 5�      " ��6*   .R�5�      � .+�5�   "   d� $��ToggleButton��6/   $��2��6C   $ Press=�5�      |�.  8��   5�      �� $ IsDropDownOpen� �� *TemplatedParent+$ TwoWay=�5�      �� 5�   "   $��ContentSite��63   $��5, 3, 23, 3q�6F    .  $
��Left=�5�      $��Center=�6:   #��z 5�      #��z 5�      #��z��5�      �� 5�   "   $��PART_EditableTextBox��6*   $��3, 3, 23, 3q�6F   # z 5�      $y�Hidden=�5      
������ $
��Left=�5     $��Center=�61   |�. .��5  (   ��5  &   �� 5  *   " ��6:   .R�5  .   �� $��PART_ContentHost��65   |�.  6M   5  +   .��5  8   5  #   � 5
  "   $��Popup��6(   $��Bottom=�65   |�.    . #��z! 5     $��Slide=�5     .��5  &   � $��DropDown��6+   " . #��z# 5     #��z$ 5     .+�5  *   �� $��DropDownBorder��61   
�������$��
0, 1, 0, 0q�6Z   $ 0��5     $��1,1,1,1q�6*   8�� 5     �� 5  *   $��4q�67   " . .��5  .   �� $% 	Contained=�6=   5  +   5  '   5  #   5      5     P� 5  "    �@��HasItems .  . �5  &   �� $ DropDownBorder��6S    � ��$ 95��6o   5  #   P� 5  "    � {� .  . �5  &   �� 8   � ��6O   5   #   P� 5!  "    �@��
IsGrouping . . �5"  &   ��  � �� .  6V   5#  #   P� 5$  "    �@��
IsEditable . . �5%  &   ��  �@ 	IsTabStop .  6U   �� 5&  &   $ PART_EditableTextBox��6M    � y�$ Visible=�6o   �� 5'  &   $ ContentSite��6M    � y�$ Hidden=�6f   5(  #   5*     5,     5-     �� 55     " ��6*   .�56     ��  � �� 57     �� 58     " ��6*   .R�59     � & 5:  "   �� 5;  &   $
��Auto �64   �� 5<  &   $��* �64   .+�5?  "   �� $��0��6)   $��4q�5@  +   ���#002860���#002860._�5C  &   �� 
�������$��Italic$�6D   $
��Bold"�6W   .��5D  *   �� $��0q�6;   $��Header��5E  ;   ��. 5F  ;   5G  '   5H  #   �� 5J  "   $��1��6)   $��4,0,4,4q�5K  +   ���#002860._�5M  &   �� $��0q�67   5N  #   5P     5Q     5S     5T     �� 5V     " ��64   .�5W     ��  � �� 5X     �� 5Y     " ��6*   .R�5Z     � & 5[  "   �� 5\  &   $
��Auto �64   �� 5]  &   $��* �64   .+�5`  "   �� $��0��6)   $��4q�5a  +   ���#002860���#002860._�5d  &   ]� $
��Auto��60   $��Stretch=�6=   .+�5e  *   �� 
�������$��Italic$�6H   $
��Bold"�6[   $��Clients��5f  0   $
��Left=�6B   5g  +   �� 5h  *   $��0q�6;   $��Header��5i  ;   ��. $��Right=�5j  V   5l  '   5m  #   �� 5o  "   $��1��6)   $��4,0,4,4q�5p  +   ���#002860._�5r  &   �� $��0q�67   5s  #   5u     5v     5x     5y     5|     5~     