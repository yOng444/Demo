   M S B A M L   `   `   `  ����G  CCurshachCTO1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:CurshachCTO1CurshachCTO1  5   	       CurshachCTO1.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:CurshachCTO1     5   	     � Title$  Главное окно��5   	   $	��495��6   $	��800��6+   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��Stretch=�6;   $��
10,10,10,0q�6Y   .+�5      �� $��Фильтр��6   $��Center=�6&   $��0,0,5,0q�6A   �� 5      -   5         ��Name$ ComboStatus��5       �� DisplayMemberPath$ 
StatusName��5       �� SelectedValuePath$ 
StatusName��5      $	��120��5      $��20��5      $��0,0,10,0q�5       �� SelectedValue 5      �� *SelectedStatusName+ �� Mode$ TwoWay=�5      �� 5      -   5         ��Name$ BtnOut��5   )   $��Применить��6   $	��100��6N   $��30��6Z   $��Right=�6f   $��10,0,0,0q�6�   �� 5      -   5        $ BtnResetFilter��5   /   $#��Сбросить фильтр��6   $	��120��6d   $��30��6p   $��Right=�6|   $��10,0,0,0q�6�   �� 5      $��
Поиск��6   $��Center=�6%   $��20,0,5,0q�6@   �� 5       -   5        	 ��Name$	 	SearchBox��5       $	��200��6)   $��22��65   $��Center=�6e   �� 5#      $+��%Кол-во выполненного:��6   $��Center=�64   $��20,0,5,0q�6O   �� 5$      -   5        $		 Box��5$      $	��100��6#   $��Center=�6/   
 �� 
IsReadOnly
 . 6J   5%      &   System.Windows.Controls.DataGrid  5(   
   -   5          Name$	 CTO��5(         AutoGenerateColumns .     SelectionMode$ Single=�6<      
IsReadOnly .    CanUserResizeColumns .  $��10,10,10,10q�5)      $��1��6(   $��Stretch=�65      Columns 5*      0  *System.Windows.Controls.DataGridTextColumn  5+      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Номер��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *ApplicationNumber+6Z     5,      $ Дата��6%   $ 65 6S    63   �� *RequestDate+6S     5-      $ Имя клиента��6%   $
 Auto 6_    6:   �� *Clients.Fullname+6_     5.      $ Телефон��6%   $ 60 6X    66   �� *Clients.Phone+6X     5/      $ Марка машины��6%   $
 Auto 6Z    6;   �� *
Cars.Brand+6Z     50      $ Модель машины��6%   $
 Auto 6[    6<   �� *
Cars.Model+6[     51      $ Год выпуска��6%   $
 Auto 6e    6:   �� *Cars.YearOfManufacture+6e     52      $ Тип услуги��6%   $
 Auto 6[    69   �� *Services.Name+6[     53      $ Работник��6%   $
 Auto 6^    67   �� *Employees.Fullname+6^     54      $ Стоимость��6%   $
 Auto 6[    68   �� *Services.Price+6[     55      $ Статус заявки��6%   $
 Auto 6i    6<   �� *RequestStatus.StatusName+6i   4  .System.Windows.Controls.DataGridTemplateColumn  56      $
 Auto 6)      CellTemplate 57      �� 58      .R�59      �� $ BtnEdit��6=   -   5        $ ��Редактировать��59   %   6N   5:      5<      5>      �� 5A   
   $ 
Horizontal=�6   $��2��6.   $��Center=�6;   $��Center=�6X   $��	0,10,0,10q�6s   .+�5B      �� -   5        $��BtnAdd��5B   @   $��Добавить��6   $	��100��6(   $��30��64   6N   �� 5C      -	   5        $��BtnUp��5C   @   $��Обновить��6   $	��100��6(   $��30��64   $��10,0,0,0q�6a   �� 5D      -
   5        $��BtnDelet��5D   ?   $��Удалить��6   $	��100��6'   $��30��63   $��10,0,0,0q�6f   �� 5E      -   5        $��BtnAuthorization��5E   A   $��Вернуться��6   $	��100��6)   $��30��65   $��10,0,0,0q�6x   5F      5G      5H      