   M S B A M L   `   `   `  ????M  IRZAccountManagerV8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:RZAccountManagerV8.Dialogs.Accounts#RZAccountManagerV8.Dialogs.Accounts  5   	   e7clr-namespace:REghZyFramework.Themes.AttachedProperties)REghZyFramework.Themes.AttachedProperties  5   	   :    4RZAccountManagerV8.Dialogs.Accounts.NewAccountWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Clocal1clr-namespace:RZAccountManagerV8.Dialogs.Accounts     5   	   Fap7clr-namespace:REghZyFramework.Themes.AttachedProperties     5   	     ? Title$  NewAccountWindow??5
   	   $??234.667??6"   $	??400??63      CustomWindowStyle#??[  5   	    J? InputBindings 5      ?? 5   
   
 ?? Key$ Return??6    ?? Command 6"   ?? *ConfirmCommand+ ?? Mode$ OneTime=??? 5   
   $	 Esc??6    6   ?? *CancelCommand+$ OneTime=?.??5      ? .+?5   
   ??  ?? Padding$ 5q?6   $??10,43,315,0q?6    $??Email??65   $	??Top=?6B   ?? 5   
   $ 5q?6   $??10,74,315,0q?6    $??Username??65   $	??Top=?6E   ?? 5   
   $ 5q?6   $??10,105,315,0q?6    $??Password??66   $	??Top=?6F   ?? 5   
   -   5         ??Name$ AccountNameBox??5   z     HintedTextBox#??[ 6   ;   5REghZyFramework.Themes.AttachedProperties.TextHinting   ShowWhenFocused . 
 ? Tag$ Account name here...??6_   $??5q?6?   $??
10,10,10,0q?6?   	 ?? TextWrapping$	 NoWrap=?6?   $??Center=?6  $	??Top=?6'  ??6?   ?? *Name+
 ?? UpdateSourceTrigger$
 PropertyChanged=?6'  ?? 5   
   #??[ 6    . $ Email here...??6_   $??2q?6?   $??
75,43,10,0q?6?   $	 NoWrap=?6?   $	??Top=?6?   $??26??6?   $??Center=?6	  ??6s   ?? *Email+$
 PropertyChanged=?6	  ?? 5   
   #??[ 6    . $ Username here...??6_   $??2q?6?   $??
75,74,10,0q?6?   $	 NoWrap=?6?   $	??Top=?6?   $??26??6  $??Center=?6  ??6v   ?? *
Username+$
 PropertyChanged=?6  ?? 5   
   #??[ 6    . $ Password here...??6_   $??2q?6?   $??75,105,10,0q?6?   $	 NoWrap=?6?   $	??Top=?6?   $??26??6  $??Center=?6  ??6v   ?? *
Password+$
 PropertyChanged=?6  ?? 5   
   $??Create??6B   $??Right=?6S   $??	0,0,90,10q?6o   $??Bottom=?6?   $??75??6?   ???	#FF3B8511$??24??6?   ??6   ?? *ConfirmCommand+$ OneTime=?6?   ?? 5   
   $??Cancel??6A   $??Right=?6R   $??	0,0,10,10q?6n   $??Bottom=?6?   $??75??6?   $??24??6?   ??6   ?? *CancelCommand+$ OneTime=?6?   5      5      