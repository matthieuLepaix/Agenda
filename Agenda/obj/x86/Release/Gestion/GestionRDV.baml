   M S B A M L   `   `   `  ���� 
  Agenda    Agenda.Gestion.GestionRDV   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Gérer un rendez-vous�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency .  � WindowState$ Normal=�$��0q�$��15(����#E0ECFD$
��Auto��$
��Auto�� � SizeToContent$ WidthAndHeight=�.��]� $
��Auto��$
��Auto��.+�]� -   $��myWindowHeadBar��$	��Top=����#002860$��0q�.+��� -   $��WindowTitle��$��4q�$
��Left=�$
��Left=�
�������$��15(�$
��Bold"�� $��Right=�$��Right=�$��0,0,10,0q�.+��� -   $��Btn_MinimizePrincipale��
�������$��1,0,1,1q� �� CornerRadius$ 0,0,0,5��._��� $��	10,0,10,0q�
�������$��Right=�$��15(�$��	ExtraBold"�_�� -   $��Btn_ClosePrincipale��
�������$��1,0,1,1q�$ 0,0,5,0��$��-1,0,0,0q����#FF5050._��� $��	12,0,12,0q�
�������$��Right=�$��15(�$��	ExtraBold"�X�� $��Bottom=�$��Center=� �� Orientation$ 
Horizontal=�$��10q�.+��� -   $��Valider��$��30 10q�   Btn_Options#��[  $��10q�$��Center=��� -   $��Facture��$��30 10q�#��[  $��10q�$��Center=��� -   $��Annuler��$��30 10q�#��[  $��10q�$��Center=�]� $��Right=�$
��Auto��$
��Auto��$��5q�.+��� $	��Top=�$
��Auto��.+��� $��Jour - Horaire - Durée��  GroupBoxBasic#��[ .���� $��Center=�$ 
Horizontal=�$
��Auto��$��5q�.+�(  "System.Windows.Controls.DatePicker  -	   $��DatePickerRDV��$
��Left=�$��Center=�� $��5 0 0 0q�.+��� $��Heure : �� �� TextAlignment$ Right=�$��Center=�$
��Bold"��� -
   $��HourRDV��  ComboBoxFlatStyle#��[ $	��100��$��28��.@��� $��	08 Heures���� $��	09 Heures���� $��	10 Heures���� $��	11 Heures���� $��	12 Heures���� $��	13 Heures���� $��	14 Heures���� $��	15 Heures���� $��	16 Heures���� $��	17 Heures���� $��	18 Heures��� $��5 0 0 0q�.+��� $��	Durée : ��$ Right=�$��Center=�$
��Bold"��� -   $��DurationRDV��#��[ $	��100��$��30���� $��Travaux à effectuer��$��0 15 0 0q�#��[ .���� -   $��sb_Works��$
��Auto��$
��Auto��$��Stretch=�$��Stretch=�$��Visible=�.���� $��Right=�$��Center=�$��5q�.+��� -   $��AllWorks��.+��� -   $��Btn_AddWork��	 � Cursor$
	 Hand��$��Center=�$��2q�$��30��  Btn_Add#��[ ]� $
��Left=�$
��Auto��$
��Auto��$��10 5 0 0q�$��Stretch=�$��Stretch=�.+��� $
��Auto��$
��Auto��$��Stretch=�$��Stretch=�.+�� -   $��BtnNewClientPanel��$��Center=�.+��� -   $��BtnNewClient��  Btn_LittleWindow#��[ $��Nouveau Client��$��30 10q�$��0 0 0 10q�]� -   $��	Le_Client��$
��Auto��$
��Auto��$��Stretch=�$��Stretch=�� -   $��ActionVehicule��$��Center=�$y�	Collapsed=�.+��� -   $��AjouterVehicule��#��[ $��Ajouter un véhicule��$��10q��� -   $��ChangerVehicule��#��[ $��Changer le véhicule��$��10q�