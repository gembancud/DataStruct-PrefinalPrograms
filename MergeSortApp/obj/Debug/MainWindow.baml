   M S B A M L   `   `   `  ����G  CMergeSortApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:MergeSortAppMergeSortApp  5   	       MergeSortApp.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   H DMahApps.Metro, Version=1.6.5.1, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   &localclr-namespace:MergeSortApp  5   	   $��0q�5   	   (  "MahApps.Metro.Controls.MetroWindow    	GlowBrush
  �   � � 
ResizeMode$ CanResizeWithGrip=�5
   	      WindowTransitionsEnabled .   � WindowStartupLocation$ CenterScreen=�5   	    � Title$ 
MERGE SORT��5   	   $	��450��6   $	��800��6)      Flyouts 5      +  %MahApps.Metro.Controls.FlyoutsControl  5   
   .@�5      #  MahApps.Metro.Controls.Flyout  -   5          Name$ yourMahAppFlyout��5      $��Array��68   $	��175��6G   .��5      � .+�5      �� -   5        $��Array��5   +   $��10q�6   5      �� 5       �� Orientation$ Vertical=�6!   $��Bottom=�68   .+�5      �� -   5        $��
NewItemBox��5   "   *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Add new item here��5   "   	   ClearTextButton	 . 5   "   �� 5      -   5        .��5   6   
Add Item6@   �� 5      -   5        .��5   9   Delete Item6F   �� 5       -   5        .��5    8   
Sort Array6D   5"      5#      5$      5%      
   RightWindowCommands
 5(      +  %MahApps.Metro.Controls.WindowCommands  5)   
   .@�5*      d� $��Array��6    � Cursor$
 Hand��5+   [    d� 	IsChecked 6   ��  �� ElementName$ yourMahAppFlyout�� �� Path$ IsOpen�6[   5,      .��5/      %  MahApps.Metro.Controls.FlipView  -   5          Name$ FlipView��5/         
WhiteBrush#���   50      54      56      