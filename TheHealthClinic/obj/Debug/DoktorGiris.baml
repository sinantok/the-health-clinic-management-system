   M S B A M L   `   `   `  ����D  @Siramatik, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Siramatik	Siramatik  5   	       Siramatik.DoktorGiris   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Siramatik     5   	   -   5          � Title$  Doktor Girişi��5   	   $	��300��6   $	��500��6,    � 
ResizeMode$ NoResize=�68    � Icon$ Images\dd.ico��5	   
    � WindowStartupLocation$ CenterScreen=�66   ��5
      �� 5   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 0.765Q�6)   � 5      $ 	#FF0FDEB9��6   $ 1Q�6-   � 5      $ 	#FF09FFD3��6   5      .��5      � $��0,0,2,0q�6   ��5   
   �� 5      $ 0.5,1&�6"   $ 0.5,0&�63   .��5      � $ Black��6   $ 1Q�6-   � 5      $ 	#FF0FDEB9��6   $ 1Q�61   � 5      $ 	#FF0CB99A��6   $ 0.828Q�61   5       � ColumnDefinitions 5   
   �� 5      .+�5   
   �� $��TC NO:��6   $
��Left=�6!   $��	18,21,0,0q�6<   $	��Top=�6O   $��31��6g   $��60��6s   	 J� RenderTransformOrigin$	 0.119,-0.232&�6~   $��15(�6�   $��Arial)�6�   �� 5   
   -   5        $��txtTC��5      $
��Left=�6   $��31��6:   $��
126,21,0,0q�6F   
 �� TextWrapping$

 Wrap=�6Z   $	��Top=�6o   $	��169��6�   ���	#FFFBFCFF �� 	MaxLength$ 11��6�   �� 5   
   $��ŞİFRE:��6   $
��Left=�6!   $��	18,79,0,0q�6<   $	��Top=�6O   $��38��6g   $��60��6s   $	 0.119,-0.232&�6~   $��15(�6�   $��Arial)�6�   G� 5   
   -   5        $��txtSifre��5      $
��Left=�6&   $��28��6A   $��
126,79,0,0q�6M   $	��Top=�6c   $	��169��6{    � ToolTip$# Şifre En Az 4 Haneli Olmalı��6�   �� 5    
   -   5        $��YENİ KAYIT��5       $
��Left=�6&   $��
18,130,0,0q�6A   $	��Top=�6U   $	��129��6m   $��48��6y   $��18(�6�   $��Arial)�6�   6�   �� 5!   
   -   5        $��GİRİŞ��5!      $
��Left=�6!   $��195,130,0,0q�6<   $	��Top=�6Q   $	��139��6i   $��48��6u   $��Arial)�6�   $��18(�6�   6�   �� 5"   
   -   5        $��ŞİFREMİ UNUTTUM��5"         textblockhover#���   6+   $
��Left=�6T   $��
18,208,0,0q�6o   $	��Top=�6�   $��31��6�   $	��148��6�   $��15(�6�   $��Arial)�6�   6�   �� 5#   
   $��344,21,20,154q�6   $��Images/DoktorGiris.png��6'   $
��Fill=�6G    J� Opacity$
 0.97Q�6V    J� OpacityMask 5$      �� 5%      $ 0.5,1&�6&   $ 0.5,0&�67   .��5&      � $ Black��6#   $ 0.491Q�61   � 5'      $ 	#FF0FDEB9��6#   $ 1Q�65   � 5(      $ 	#FF0DC7A6��6#   $ 0.026Q�65   � 5)      $ 	#FF0BAC90��6#   $ 1Q�65   5*      5,      �� 5-   
   -   5        $ Bir Önceki Sayfaya Git��5-   K   $��0,0,458,235q�6l   ��6   Z� ��65   Z� ��5.      �� 5/       �� ImageSource$ Images/back-image.png��6   51      53      54      