   M S B A M L   `   `   `  ����>  :FA2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:FA2FA2  5   	       FA2.ProductList   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:FA2     5   	     � Title$  ProductList��5   	   $	��450��6   $	��800��6*   .��5	      � $��	0,0,0,-16q�6   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ ProductsDataGrid��5
      $��	0,0,0,278q�6X   6k   �� 5   
   -   5         ��Name$ btnEdit��5      $
��Edit��6"   $
��Left=�61   $��367,185,0,0q�6L   $	��Top=�6a   $��94��6y   $��31��6�   6�   �� 5   
   -   5        $ 	btnDelete��5      $��Delete��6$   $
��Left=�65   $��367,225,0,0q�6P   $	��Top=�6e   $��94��6}   $��31��6�   6�   �� 5   
   -   5        $ 
btnProduct��5      $��Add Product��6%   $
��Left=�6;   $��504,185,0,0q�6V   $	��Top=�6k   $��94��6�   $��31��6�   6�   �� 5   
   -   5        $ 
btnBarCode��5      $��Bar Code��6%   $
��Left=�68   $��504,225,0,0q�6S   $	��Top=�6h   $��94��6�   $��31��6�   6�   �� 5   
   $��Product Category��6   $
��Left=�6+   $��
10,185,0,0q�6F   $	��Top=�6Z   �� 5   
   $��Product Name��6   $
��Left=�6'   $��
10,216,0,0q�6B   $	��Top=�6V   �� 5   
   $��
Cost Price��6   $
��Left=�6%   $	��Top=�6@    J� RenderTransformOrigin$ 0.784,-0.153&�6X   $��
10,247,0,0q�6}   �� 5   
   $��Selling Price��6   $
��Left=�6(   $��
10,278,0,0q�6C   $	��Top=�6W   $ 0.322,0.618&�6o   �� 5   
   $��Quantity��6   $
��Left=�6#   $��
10,309,0,0q�6>   $	��Top=�6R   �� 5   
   $��Bar code��6   $
��Left=�6#   $��
10,338,0,0q�6>   $	��Top=�6R   �� 5   
   -   5         ��Name$ cmbCategory��5      $
��Left=�6(   $��161,187,0,0q�6C   $	��Top=�6X   $	��167��6p   .@�5      �� $��
Vegetables��6   �� 5      $��Fruit��6   �� 5      $��Drinks��6   �� 5      $
��Meat��6   �� 5      $��Grains��6   5      �� 5   
   -   5         ��Name$ txtName��5      $
��Left=�6#   $��161,220,0,0q�6>    �� TextWrapping$
 Wrap=�6S   $	��Top=�6g   $	��167��6   �� 5   
   -   5        $ txtCost��5      $
��Left=�6#   $��161,251,0,0q�6>   $
 Wrap=�6S   $	��Top=�6g   $	��167��6   �� 5   
   -	   5        $ 
txtSelling��5      $
��Left=�6&   $��161,282,0,0q�6A   $
 Wrap=�6V   $	��Top=�6j   $	��167��6�   �� 5   
   -
   5        $ txtQuantity��5      $
��Left=�6'   $��161,313,0,0q�6B   $
 Wrap=�6W   $	��Top=�6k   $	��167��6�   �� 5    
   -   5        $ 
txtBarcode��5       $
��Left=�6&   $��161,342,0,0q�6A   $
 Wrap=�6V   $	��Top=�6j   $	��167��6�   5"      5#      