   M S B A M L   `   `   `  ����H  DChatSimulator, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:ChatSimulatorChatSimulator  5   	       ChatSimulator.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:ChatSimulator     5   	     � Title$  
MainWindow��5   	   $	��478��6   $	��800��6)    � WindowStartupLocation$ CenterScreen=�65   .��5	      � .+�5
   
   �� -   5         ��Name$ 
lstMesaggi��5
      $
��Left=�6&   $	��280��6A   $��
41,143,0,0q�6N   $	��Top=�6b   $	��318��6z   �� 5   
   -   5         ��Name$ txtIP��5      $
��Left=�6!   $��400,171,0,0q�6<    �� TextWrapping$
 Wrap=�6Q   $	��Top=�6e   $	��178��6}   $��40��6�   �� 5   
   -   5        $ txtPorta��5      $
��Left=�6$   $��400,249,0,0q�6?   $
 Wrap=�6T   $	��Top=�6h   $	��178��6�   $��40��6�    J� RenderTransformOrigin$ 0.503,-0.162&�6�   �� 5   
   -   5        $ txtMessaggio��5      $
��Left=�6(   $��601,171,0,0q�6C   $
 Wrap=�6X   $	��Top=�6l   $	��167��6�   $	��118��6�   �� 5   
   -   5         ��Name$	 btn��5      $��Invia��6   $
��Left=�6.   $��601,306,0,0q�6I   $	��Top=�6^   $��52��6v   $	��167��6�   $ 0.511,1&�6�   �� 5   
   $��Inserire un IP��6   $
��Left=�6)   $��28��6D   $��400,143,0,0q�6P   $	��Top=�6e   $	��110��6}   4  .System.Windows.Automation.AutomationProperties   Name$  ��6�   �� 5   
   $��Inserire porta��6   $
��Left=�6)   $��28��6D   $��400,216,0,0q�6P   $	��Top=�6e   $	��110��6}   $  ��6�   $ 0.486,-0.516&�6�   �� 5   
   $��Inserire messaggio��6   $
��Left=�6-   $��28��6H   $��601,143,0,0q�6T   $	��Top=�6i   $	��111��6�   $  ��6�   $ 0.486,0.466&�6�   �� 5   
   $��Chat Simulator��6   $��Center=�6)   $	��Top=�6F   $	��800��6^   $��68��6j   $��Center=�6v   $��Center=�6�   $��48(�6�   $��Gabriola)�6�   �� 5   
   $��Received��6   $
��Left=�6#   $��57��6>   $��0,68,0,0q�6J   $	��Top=�6\   $	��400��6t   $��Center=�6�   $��Center=�6�   $��Ink Free)�6�   $��36(�6�   �� 5   
   $
��Send��6   $
��Left=�6   $��57��6:   $��
400,68,0,0q�6F   $	��Top=�6Z   $	��400��6r   $��Ink Free)�6~   $��36(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   -   5        $ btnRubi��5      $��Rubrica��6"   $
��Left=�64   $��52��6O   $��400,306,0,0q�6[   $	��Top=�6p   $	��178��6�   6�   �� 5   
   -   5        $ 
btnStorico��5      $��Storico messaggi inviati��6%   $
��Left=�6H   $��400,377,0,0q�6c   $	��Top=�6x   $	��178��6�   $��46��6�   6�   �� 5   
   -   5        $ btnTutti��5      $��Invia alla rubrica��6#   $
��Left=�6@   $��601,377,0,0q�6[   $	��Top=�6p   $��46��6�   $	��167��6�   6�   5      5      