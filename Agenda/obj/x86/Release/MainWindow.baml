   M S B A M L   `   `   `  ���� 
  Agenda    Agenda.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $��
Principale��  � Title$  
MainWindow�� � WindowState$ 	Maximized=� � WindowStyle$
 None=�
���   � � WindowStartupLocation$ CenterScreen=����#0064ef$��13(����#AACEFF$��0q�.��]� ���#0064ef.+�]� -   $��myWindowHeadBar��$	��Top=����#002860$��0q�.+��� -   $��WindowTitle��$��4q�$
��Left=�$
��Left=�
�������$��15(�$
��Bold"�� $��Right=�$��Right=�$��0,0,10,0q�.+��� -   $��Btn_MinimizePrincipale��
�������$��1,0,1,1q� �� CornerRadius$ 0,0,0,5��._��� $��	10,0,10,0q�
�������$��Right=�$��15(�$��	ExtraBold"�_�� -   $��Btn_ClosePrincipale��
�������$��1,0,1,1q�$ 0,0,5,0��$��-1,0,0,0q����#FF5050._��� $��	12,0,12,0q�
�������$��Right=�$��15(�$��	ExtraBold"�X�� -   $
��Tabs��$��Bottom=� �� TabStripPlacement$	 Top=����#0048ac$��0q�$��0q�
���   �$��0q�$��Stretch=�.@��� $��Agenda��$��Center=�$��Stretch=� � UseLayoutRounding . $��0q�.��]� $��Stretch=�$��Stretch=�$
��Auto�����#E0ECFD.+��� $��Bottom=�$��Stretch=�$��Stretch=�$
��Auto��$
��15 5q�.+�]� .+�� $
��Left=�$��Stretch=�.+��� -   $��AjoutRDV��   Btn_Options#��[  $��Ajouter un RDV���� -   $��
LesClients��#��[  $��Les clients���� -	   $
��Save��#��[  $��
Sauvegarde���� -
   $��Works��#��[  $��Travaux��� $��Right=�$��Right=�.+��� -   $��Refresh��#��[  .���� .+��� $��images/refresh.png��$��50���� $��5q�$
��Left=�$��Stretch=�$��Stretch=�$
��Auto��.+�(� $��Stretch=�$	��280��$
��Auto��.��&   System.Windows.Controls.Calendar  -   $��CalendarToChoice���� $��Rendez-vous��  GroupBoxBasic#��[ $	��280��$��5q�$��15(�
���   �$��DemiBold"�.��]� $
��0 15q�.+��� -   $��noRDVselected��$	��Top=�$%��Aucun rendez-vous sélectionné��$��5q��� -   $��RDVselected��$	��Top=�$
��Auto=�$	��200��$y�	Collapsed=�.���� $	��260��$	��Top=�.+�� $
��Auto��$��3q�.+��� $��	Client : �� �� TextAlignment$ Right=�$��60��$��11(�$
��Bold"��� -   $��Details_Client��$��11(�� $��3q�.+��� $��Jour : ��$ Right=�$��60��$��11(�$
��Bold"��� -   $��Details_Jour��$��11(�� $��3q�.+��� $��Heure : ��$ Right=�$��60��$��11(�$
��Bold"��� -   $��Details_Heure��$��11(�� $��3q�.+��� $��Véhicule : ��$ Right=�$��60��$��11(�$
��Bold"��� -   $��Details_Vehicule��$��11(�� $��3q�$��60��.+��� $��
Travaux : ��$ Right=�$��60��$��11(�$
��Bold"��� -   $��Details_Travaux��$
��Wrap=�$��11(�� $��Bottom=�$��Right=�$��0q�.+��� -   $��Btn_FactureRDV��$��images/facture.png��$��40�� � Cursor$
 Hand��$��	15 0 10 0q��� -   $��Btn_DeleteRDV��$��images/DeleteRed.png��$��35��$
 Hand��$��	15 0 10 0q�]� $��Right=�$��Stretch=�$��Stretch=�$
��Auto��$
��Auto��.+��� $	��Top=�
���   �$��1q�$��Stretch=�$��Stretch=�$
��Auto��$
��Auto��
�������$��5q�._�� -   $��Agenda��$��2q�	 � ShowGridLines	 . 
 � ColumnDefinitions
 �� $��1* ��� $��4* ��� $��4* ��� $��4* ��� $��4* ��� $��4* � � RowDefinitions �� $��2* ��� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��6* ��� $��Facturation��$��Center=�$��Stretch=� . $��0q�.��]� -   $��Facture��$
��Auto��$
��Auto��$y�	Collapsed=����#E0ECFD.+�]� $��Right=�$
��Auto��$
��Auto��$��5 5 5 5q�.+��� .+��� $��Travaux à effectuer��#��[ $	��400��$	��400��.���� -   $��Facture_sb_Works��$
��Auto��$
��Auto��$��Stretch=�$��Stretch=�$��Visible=�
���   �.���� -   $��Facture_AllWorks��$��Right=�$��Center=�$��5q�.+��� -   $��Facture_Btn_AddWork��$
 Hand��$��Center=�$��2q�$��30��  Btn_Add#��[ �� $��Forfaits main d'oeuvre��#��[ $��0 5 0 0q�
���   �$��15(�.����  �� Orientation$ Vertical=�$��5q�.+��� $ 
Horizontal=�.+��� $��Forfait main d'oeuvre 1 :���� -   $��Facture_MO1��$	��100��$��10 0 0 0q� �� TextAlignment$ Right=��� $
�� €���� $ 
Horizontal=�.+��� $��Forfait main d'oeuvre 2 :���� -   $��Facture_MO2��$	��100��$��10 0 0 0q�$ Right=��� $
�� €���� $ 
Horizontal=�.+��� $��Forfait main d'oeuvre 3 :���� -   $��Facture_MO3��$	��100��$��10 0 0 0q�$ Right=��� $
�� €���� $ 
Horizontal=�.+��� $��Forfait main d'oeuvre 4 :���� -   $��Facture_MO4��$	��100��$��10 0 0 0q�$ Right=��� $
�� €���� $ 
Horizontal=�.+��� $��Forfait main d'oeuvre 5 :���� -   $��Facture_MO5��$	��100��$��10 0 0 0q�$ Right=��� $
�� €��]� $
��Left=�$
��Auto��$
��Auto��$��Stretch=�$��Stretch=�$��5 5 0 5q�.+��� $
��Auto��$
��Auto��$��Stretch=�$��Stretch=�.+�]� -    $��Facture_Le_Client��$
��Auto��$
��Auto��$��Stretch=�$��Stretch=��� $ 
Horizontal=�$��10q�.+��� $��Type de réglement :��
���   ��� -!   $��Facture_TypeReglement��  ComboBoxFlatStyle#��[ $	��200��$��35��$	��Top=�$��10 0 0 0q� �� 
IsEditable .  ]� .+�� $
��Left=�$��Stretch=�$��	40 70 0 0q�.+��� -"   $��Facture_Enregistrer��#��[  $��Enregistrer��$��20 0 0 0q��� -#   $��Facture_ToExcel��#��[  $��Fichier Excel��$��20 0 0 0q��� -$   $��Facture_Imprimer��#��[  $��Imprimer��$��20 0 0 0q��� $��Devis��$��Center=�$��Stretch=� . $��0q�.��]� -%   $��Devis��$
��Auto��$
��Auto��$y�	Collapsed=����#E0ECFD